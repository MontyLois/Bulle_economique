using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Market_Data", menuName = "Scriptable Objects/Market_Data")]
public class Market_Data : ScriptableObject
{
    
    [field: SerializeField]
    public int min_price { get; private set; }
    
    [field: SerializeField]
    public int max_price { get; private set; }
    
    [field: SerializeField]
    public int current_price { get; private set; }
    
    [field: SerializeField]
    public float wait_time { get; private set; }
    
    [field: SerializeField]
    public GameObject graph { get; private set; }
    
    [field: SerializeField]
    public List<float> stockValues { get; private set; }
    
    private float currentWaitTime=0;
    
    public void UpdatePrice(float deltaTime)
    {
        currentWaitTime += deltaTime;
        if (currentWaitTime >= wait_time)
        {
            currentWaitTime = 0;
            current_price = Random.Range(min_price, max_price);
            
            //add new price to stock value and delete older values
            stockValues.Add(current_price);
            stockValues.RemoveAt(0);
        }
    }

    public void SetStockValues(List<float> newstock)
    {
        stockValues = newstock;
    }

    private void setWaitTime(float newtime)
    {
        wait_time = newtime;
    }
}
