using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameDatabase GameDatabase { get; private set; }
    
    //allow to load the class when the game start and before the scene load
    private void Start()
    {
        
        
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Load()
    {
        //generate the database
        GameDatabase = new GameDatabase();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C key was pressed let's spawn a bubble");
            if (Bubble_Manager.Instance != null)
            {
                Bubble_Manager.Instance.CreateBubble();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key was pressed, we are upgrading the wand ehe");
            if (Bubble_Manager.Instance != null)
            {
                Bubble_Manager.Instance.UpgradeWand();
            }
        }
        
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S key was pressed, we are selling geemeee moneeey");
            if (Bubble_Manager.Instance != null)
            {
                Bubble_Manager.Instance.SellBubble(0);
            }
        }
    }
}
