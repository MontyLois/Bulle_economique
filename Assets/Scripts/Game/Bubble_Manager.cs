using System;
using UnityEngine;

public class Bubble_Manager : MonoBehaviour
{
    private Bubble_Data[] bubbles;
    private Bubble_Wand_Data[] bubble_wands;
     
    protected void Awake()
    {
        Bubble_Data[] bubbles = GameController.GameDatabase.Bubbles;
        Bubble_Wand_Data[] bubble_wands = GameController.GameDatabase.Bubble_Wands;
    }

    private void Update()
    {
        
    }
}
