using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Bubble_Manager : MonoBehaviour
{
    //all data of bubbles and wands
    private Bubble_Data[] bubbles;
    private Bubble_Wand_Data[] bubble_wands;
    
    //the bubble wand the player use
    private Bubble_Wand_Data used_bubble_wand;
    //list of my super bubble wands for the shop
    private List<Bubble_Wand_Data> sorted_BubbleWand;
    //list of the bubble owned by the player
    private List<Bubble_Data> playerBubbles;
    
    //bubble spawn location
    [SerializeField]
    private Vector3 bubble_spawner;
    
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
        
        //get all data of bubbles and wands
        Bubble_Data[] bubbles = GameController.GameDatabase.Bubbles;
        Bubble_Wand_Data[] bubble_wands = GameController.GameDatabase.Bubble_Wands;

        //sort wand by number order
        //hmmm i could do a queue and just allow to see the next wand in store....
        sorted_BubbleWand = SortList(bubble_wands);
        
        // The first wand is the one the player start with.
        // We remove it from the list as it won't be available in the shop.
        if (sorted_BubbleWand.Count > 0)
        {
            used_bubble_wand = sorted_BubbleWand.FirstOrDefault();
            sorted_BubbleWand.Remove(used_bubble_wand);
        }
        GetAllWand(sorted_BubbleWand);
    }
    
    private void Update()
    {
        
    }

    // Create a bubble with random chance of rarity based on the wand bubble
    // Changed the UI of its slot and add graph in graph slot
    private void CreateBubble(int slot)
    {
        int bubble_rarity = used_bubble_wand.GetRandomIndex();
        Bubble_Data bubble_added = null;
        foreach (var bubble in bubbles)
        {
            if (bubble.rarity_order == bubble_rarity)
            {
                bubble_added = bubble;
            }
        }
        if (bubble_added)
        {
            playerBubbles.Add(bubble_added);
        }
    }

    private void UpgradeWand()
    {
        if (sorted_BubbleWand.Count > 0)
        {
            used_bubble_wand = sorted_BubbleWand.FirstOrDefault();
            sorted_BubbleWand.Remove(used_bubble_wand);
        }
    }
    
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
                    }
                }
                wand_List.Add(newdata);
            }
        }
        return wand_List;
    }

    private void GetAllWand(List<Bubble_Wand_Data> sorted_BubbleWand)
    {
        foreach (var wand in sorted_BubbleWand)
        {
            Debug.Log("wand 1 : " + wand.wand_name);
        }
    }
    
}
