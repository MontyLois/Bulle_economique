using System.Collections.Generic;
using UnityEngine;

public class Market_Manager : MonoBehaviour
{
    //all data of markets
    private Market_Data[] markets;
    
    
    private List<float> stockValues;
    [Min(2)] public int precision = 10;
    
    
    //for singleton behavior
    public static Market_Manager Instance { get; private set; }

    private void Awake()
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
        markets = GameController.GameDatabase.Markets;
        
        
        stockValues = new List<float>();
        for (int i = 0; i < precision; i++)
        {
            stockValues.Add(0);
        }
        foreach (var market in markets)
        {
            market.SetStockValues(stockValues);
        }
        
    }

    public void UpdateMarkets(float deltaTime)
    {
        foreach (var market in markets)
        {
            market.UpdatePrice(deltaTime);
        }
    }
    
    // for singleton Ensures it's created automatically if accessed before existing
    public static Market_Manager GetInstance()
    {
        if (Instance == null)
        {
            GameObject managerObject = new GameObject("Market_Manager");
            Instance = managerObject.AddComponent<Market_Manager>();
            DontDestroyOnLoad(managerObject);
        }
        return Instance;
    }
}
