using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Bubble_Manager : MonoBehaviour
{
    private Bubble_Data[] bubbles;
    private Bubble_Wand_Data[] bubble_wands;
    private Bubble_Wand_Data used_bubble_wand;
    
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
        List<Bubble_Wand_Data> sorted_BubbleWand = SortList(bubble_wands);
        
        //The first wand is the one the player start with.
        // could be remove from the list as it won't be available in the shop.
        if (sorted_BubbleWand.Count > 0)
        {
            used_bubble_wand = sorted_BubbleWand.FirstOrDefault();
            Debug.Log("the used wand is : "+used_bubble_wand.wand_name);
            Debug.Log(" Chance random : "+used_bubble_wand.GetRandomIndex());
        }
        GetAllWand(sorted_BubbleWand);
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
    private void Update()
    {
        
    }
}
