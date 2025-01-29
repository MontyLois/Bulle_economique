using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GraphRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    
    [Min(0)]
    public int precision;
    public int maxStockValue;
    
    [Range(0, 10)]
    public float height;
    [Range(0, 10)]
    public float width;
    
    
    private List<float> stockValues;
    
    private void Start()
    {
        stockValues = new List<float>();
        for (int i = 0; i < precision; i++)
            stockValues.Add(0);

        DrawGraph();
    }
    
    public void SetNewStockValue(int newValue)
    {
        stockValues.Add(newValue);
        stockValues.RemoveAt(0);
        
        DrawGraph();
    }
    
    private void DrawGraph()
    {
        lineRenderer.positionCount = precision;
        float currentPoint = 0;

        foreach (float point in stockValues)
        {
            float minX = -width / 2;
            float maxX = width / 2;
            float minY = -height / 2;
            float maxY = height / 2;

            float xPercent = currentPoint / precision;
            float x = Mathf.Lerp(minX, maxX, xPercent);
            
            float yPercent = point / maxStockValue;
            float y = Mathf.Lerp(minY, maxY, yPercent);
            
            lineRenderer.SetPosition((int)currentPoint, new Vector3(x, y , 0));
            currentPoint += 1;
        }
    }
}
