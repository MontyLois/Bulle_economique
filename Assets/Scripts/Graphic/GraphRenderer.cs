using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GraphRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [Min(2)] private int precision = 10;
    private int maxStockValue = 100;

    [Range(0, 50)] public float height = 10;
    [Range(0, 50)] public float width = 10;

    [Range(0.1f, 5f)] public float pointSpacing = 1f; // ✅ Espacement des points
    [Range(0.5f, 10f)] public float yScale = 2f; // ✅ Échelle verticale
    [Range(0.5f, 5f)] public float xScale = 1f; // ✅ Échelle horizontale

    public Vector3 textPosition = new Vector3(10f, 10f, 0); // ✅ Texte fixe

    private List<float> stockValues;
    private Market_Data marketData;
    private int slot;

    private void Start()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
        stockValues = new List<float>();

        for (int i = 0; i < precision; i++)
        {
            stockValues.Add(0);
        }
        lineRenderer.widthMultiplier = 0.2f; // ✅ Rendre la ligne plus visible
        DrawGraph();
    }

    private void Update()
    {
        if (marketData)
        {
            maxStockValue = marketData.max_price;
            stockValues.Clear();
            stockValues.AddRange(marketData.stockValues);
            UIManager.Instance.UpdateGraphCurrentPrice(marketData.current_price, slot);
        }
        else
        {
            maxStockValue = 100;
            resetStockValues();
            UIManager.Instance.UpdateGraphCurrentPrice(0, slot);
        }
        DrawGraph();
    }

    private void resetStockValues()
    {
        stockValues.Clear();
        for (int i = 0; i < precision; i++)
        {
            stockValues.Add(0);
        }
    }

    public void SetNewMarket(Market_Data new_marketData)
    {
        marketData = new_marketData;
    }

    public void SetSlot(int index)
    {
        slot = index;
    }

    private void DrawGraph()
    {
        lineRenderer.positionCount = stockValues.Count;

        float startX = -width / 2;
        float startY = -height / 2;
        float maxY = height / 2;

        for (int i = 0; i < stockValues.Count; i++)
        {
            float x = startX + (i * pointSpacing * xScale); // ✅ Espacement horizontal
            float yPercent = stockValues[i] / (float)maxStockValue;
            float y = Mathf.Lerp(startY, maxY, yPercent) * yScale; // ✅ Échelle verticale

            Vector3 position = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, position);
        }
    }
}
