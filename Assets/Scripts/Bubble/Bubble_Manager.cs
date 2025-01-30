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
    private int slots; //number of current slots availables
    private int max_slots=3; //number max of slots
    private int slot_selected; //wich slot is selected
    
    //bubble spawn location
    [SerializeField]
    private Transform bubble_spawner;
    
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
        slots = 1;
        slot_selected = 0;

        SpawnBubbles();
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
                    UIManager.Instance.UpdateBubbleInventoryUI(player_bubbles);
                }
            }
        }
        Debug.Log("Slot already used");
    }

    //change the bubble that is showcast based on the slot selected
    public void ChangeSelectedSlot(int slot_index)
    {
        slot_selected = slot_index;
        if (current_bubble != null)
        {
            current_bubble.SetActive(false);
        }
        current_bubble = null;

        Bubble_Data currentData = player_bubbles[slot_selected];
        if (currentData)
        {
            current_bubble = bubble_bodies[currentData.rarity_order];
            if (current_bubble)
            {
                current_bubble.SetActive(true);
            }
            UIManager.Instance.UpdateSelectedBubble(currentData);
        }
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
        UIManager.Instance.UpdateBubbleInventoryUI(player_bubbles);
    }
    
    //sell bubble in a slot
    public void SellBubble(int index)
    {
        player_bubbles[index].SellBubble();
        RemoveBubble(index);
    }
    

    // upgrade actual used wand to next rank
    private void UpgradeWand()
    {
        if (sorted_BubbleWand.Count > 0)
        {
            used_bubble_wand = sorted_BubbleWand.FirstOrDefault();
            sorted_BubbleWand.Remove(used_bubble_wand);
            UIManager.Instance.UpdateWandUI(used_bubble_wand);
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
                wand_List.Add(newdata);
            }
        }
        return wand_List;
    }

    //just to display in console all wand left to get
    private void GetAllWand(List<Bubble_Wand_Data> sorted_BubbleWand)
    {
        foreach (var wand in sorted_BubbleWand)
        {
            Debug.Log("wand 1 : " + wand.wand_name);
        }
    }

    //get new slot for the bubble let's goo
    private void AddSlot()
    {
        if (slots < max_slots)
        {
            slots++;
        }
        UIManager.Instance.AddSlot();
    }
    
}
