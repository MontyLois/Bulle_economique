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
            else
            {
                Debug.Log("wtf where is my manager ?");
            }
            
        }
    }
}
