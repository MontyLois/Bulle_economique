using System.Collections;
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
        
        //retrieve all data of markets
        markets = GameController.GameDatabase.Markets;
        
        
        
        foreach (var market in markets)
        {
            market.SetStockValues(precision);
        }
        
    }
    
    private void Start()
    {
        foreach (var market in markets)
        {
            StartCoroutine(UpdateStockRoutine(market));
        }
        
    }

    // Allow to update each market to a given time
    private IEnumerator UpdateStockRoutine(Market_Data market)
    {
        while (true)
        {
            market.UpdatePrice();
            yield return new WaitForSeconds(market.wait_time); 
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
