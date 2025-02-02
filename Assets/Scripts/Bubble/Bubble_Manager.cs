using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering.UI;


public class Bubble_Manager : MonoBehaviour
{
    //all data of bubbles and wands
    private Bubble_Data[] bubbles;
    private Bubble_Wand_Data[] bubble_wands;
    
    //Bubble Wands
    private Bubble_Wand_Data used_bubble_wand; //used bubble wand by the player
    private List<Bubble_Wand_Data> sorted_BubbleWand; //list of bubble wands for the shop
    
    
    //Bubbles
    private Bubble_Data[] player_bubbles; //bubbles owned by the player
    private GameObject current_bubble; //avatar of the bubble selected for showcast
    private GameObject[] bubble_bodies;  //bubbles avatars preinstantiated in the scene 
    
    
    //Bubbles slots
    private bool[] slots; //if slots are unlocked
    private int max_slots=3; //number max of slots
    private int slot_selected; //wich slot is selected
    [SerializeField]private int slot_base_price; //price of a slot
    
    //bubble spawn location
    [SerializeField]
    private Transform bubble_spawner;
    
    //markets
    [SerializeField] private GraphRenderer[] graphs;
    
    //for singleton behavior
    public static Bubble_Manager Instance { get; private set; }
    
    
    protected void Awake()
    {
        //for singleton behavior
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //for singleton behavior_end
        
        //retrieve all data of bubbles and wands
        bubbles = GameController.GameDatabase.Bubbles;
        bubble_wands = GameController.GameDatabase.Bubble_Wands;

        //Wands initialization
        //sort wand by number order
        sorted_BubbleWand = SortList(bubble_wands);
        // Upgrade the wand to the first one available
        UpgradeWand();
       

        //slots initialisation
        player_bubbles = new Bubble_Data[max_slots];
        slots = new bool[max_slots];
        slots[0] = true;
        slot_selected = 0;

        SpawnBubbles();
        GetAllWand(sorted_BubbleWand);

        for (int i = 0; i < graphs.Length; i++)
        {
            graphs[i].SetSlot(i);
        }
        
    }
    
    

    // Create a bubble with random chance of rarity based on the wand bubble
    // Changed the UI of its slot and add graph in graph slot
    public void CreateBubble()
    {
        if (!player_bubbles[slot_selected])
        {
            Bubble_Data bubble_added = null;
            int index = 0;
            
            //get random rarity for the bubble
            int bubble_rarity = used_bubble_wand.GetRandomIndex();
            //get the bubble with corresponding rarity
            for (int i = 0; i < bubbles.Length; i++)
            {
                if (bubbles[i].rarity_order == bubble_rarity)
                {
                    bubble_added = bubbles[i];
                    index = i;
                }
            }
            if (bubble_added)
            {
                //add the bubble to its slot
                player_bubbles[slot_selected] = bubble_added;
                //make the bubble visible
                current_bubble = bubble_bodies[index];
                current_bubble.SetActive(true);
                
                //change UI for selected bubble and new added bubble
                if(UIManager.Instance)
                {
                    UIManager.Instance.UpdateSelectedBubble(bubble_added);
                    UIManager.Instance.UpdateBubbleInventoryUI(bubble_added, slot_selected);
                    UIManager.Instance.UpdateGraphName(bubble_added, slot_selected);
                }
                
                graphs[slot_selected].SetNewMarket(bubble_added.market);
            }
        }
        Debug.Log("Slot already used");
    }

    //le bouton permets soit de vendre la bulle dans le slot, soit d'acheter le slot s'il n'est pas encore débloqué
    public void Slot_Button(int slot_index)
    {
        if (slots[slot_index])
        {
            SellBubble(slot_index);
        }
        else
        {
            if (GameManager.Instance.SpendMoney(slot_base_price*slot_index))
            {
                AddSlot(slot_index);
            }
        }
    }

    //change the bubble that is showcast based on the slot selected
    public void ChangeSelectedSlot(int slot_index)
    {
        //check if the slot is available
        if (slots[slot_index])
        {
            UIManager.Instance.UpdateSelectedSlot(slot_selected,slot_index);
            slot_selected = slot_index;
            if (current_bubble != null)
            {
                current_bubble.SetActive(false);
            }
            current_bubble = null;

            //If there is a bubble in the new slot we get the current bubble and set things active
            Bubble_Data currentData = player_bubbles[slot_selected];
           
            if (currentData)
            {
                int index = GetindexOfBubble(currentData);
                current_bubble = bubble_bodies[index];
                if (current_bubble)
                {
                    current_bubble.SetActive(true);
                }
                UIManager.Instance.UpdateSelectedBubble(currentData);
            }
        }
    }

    private int GetindexOfBubble(Bubble_Data bubble)
    {
        for (int i = 0; i < bubbles.Length; i++)
        {
            if (bubble == bubbles[i])
            {
                return i;
            }
        }
        return -1;
    }
    
    
    //pre spawn all bubbles
    private void SpawnBubbles()
    {
        // Initialize the bubble pool with the bubble prefabs from the BubbleData
        bubble_bodies = new GameObject[bubbles.Length];
        
        // Instantiate each bubble model in the pool
        for (int i = 0; i < bubbles.Length; i++)
        {
            bubble_bodies[i] = Instantiate(bubbles[i].body,bubble_spawner );
            bubble_bodies[i].SetActive(false);  // Start with all models deactivated
        }
    }
    
    
    //remove a bubble from the player bubbles at the given slot
    private void RemoveBubble(int index)
    {
        player_bubbles[index] = null;
        if (index == slot_selected)
        {
            if (current_bubble != null)
            {
                current_bubble.SetActive(false);
                current_bubble = null;
            }
        }
        UIManager.Instance.UpdateBubbleInventoryUI(null, index);
        UIManager.Instance.UpdateGraphName(null, index);
        graphs[index].SetNewMarket(null);
    }
    
    //sell bubble in a slot
    public void SellBubble(int index)
    {
        if (player_bubbles[index])
        {
            player_bubbles[index].SellBubble();
            RemoveBubble(index);
        }
    }
    

    // upgrade actual used wand to next rank
    public void UpgradeWand()
    {
        if (sorted_BubbleWand.Count > 0)
        {
            Bubble_Wand_Data next_wand = sorted_BubbleWand.FirstOrDefault();
            if (GameManager.Instance.SpendMoney(next_wand.price))
            {
                used_bubble_wand = next_wand;
                sorted_BubbleWand.Remove(used_bubble_wand);
                UIManager.Instance.UpdateWandUI(used_bubble_wand);
                UIManager.Instance.UpdateNextWandUI(sorted_BubbleWand.FirstOrDefault());
            }
        }
    }
    
    //sort list of wand by rarity
    private List<Bubble_Wand_Data> SortList(Bubble_Wand_Data[] wand_table)
    {
        List<Bubble_Wand_Data> wand_List = new List<Bubble_Wand_Data>();
        if (wand_table.Length > 0)
        {
            foreach (var newdata  in wand_table)
            {
                foreach (var existingdata in wand_List)
                {
                    if (existingdata.number_order > newdata.number_order)
                    {
                        wand_List.Insert(wand_List.IndexOf(existingdata), newdata);
                        break;
                    }
                }

                if (!wand_List.Contains(newdata))
                {
                    wand_List.Add(newdata);
                }
            }
        }
        return wand_List;
    }

    //just to display in console all wand left to get
    private void GetAllWand(List<Bubble_Wand_Data> sorted_BubbleWand)
    {
        int i = 0;
        foreach (var wand in sorted_BubbleWand)
        {
            Debug.Log("wand "+i+" : " + wand.wand_name);
            i++;
        }
    }

    //get new slot for the bubble let's goo
    private void AddSlot(int index)
    {
        slots[index] = true;
        UIManager.Instance.AddSlot(index);
    }
    
}
