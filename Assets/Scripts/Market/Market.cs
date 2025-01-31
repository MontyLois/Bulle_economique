using UnityEngine;
using UnityEngine.Serialization;

public class Market : MonoBehaviour
{
    public GraphRenderer graph;

    private float currentWaitTime;
    private float waitTime = 10;
    private int current_price;
    
    private int min_price=0;
    private int max_price=10;
    
    private void Update()
    {
       
    }

    private void ChangeValue(float deltaTime)
    {
        currentWaitTime += deltaTime;
        if (currentWaitTime >= waitTime)
        {
            currentWaitTime = 0;
            current_price = Random.Range(min_price, max_price);
           // graph.SetNewStockValue(current_price);
        }
    }

    private void setWaitTime(float newtime)
    {
        waitTime = newtime;
    }
}
