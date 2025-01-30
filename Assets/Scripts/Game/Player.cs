using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public GraphRenderer graph;

    private float currentWaitTime;
    public float waitTime;
    
    private void Update()
    {
        
        currentWaitTime += Time.deltaTime;
        if (currentWaitTime >= waitTime)
        {
            currentWaitTime = 0;
            
            int rand = Random.Range(0, 10);
            graph.SetNewStockValue(rand);
        }
        
    }
}
