using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject textPrefab;
    private TextMeshPro textComponent;

    [Min(2)] public int precision = 10;
    public int maxStockValue = 100;

    [Range(0, 50)] public float height = 10;
    [Range(0, 50)] public float width = 10;

    [Range(0.1f, 5f)] public float pointSpacing = 1f; // ✅ Espacement des points
    [Range(0.5f, 10f)] public float yScale = 2f; // ✅ Échelle verticale
    [Range(0.5f, 5f)] public float xScale = 1f; // ✅ Échelle horizontale

    public Vector3 textPosition = new Vector3(10f, 10f, 0); // ✅ Texte fixe

    private List<float> stockValues;

    private void Start()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
        
        stockValues = new List<float>();

        for (int i = 0; i < precision; i++)
        {
            stockValues.Add(0);
        }

        lineRenderer.widthMultiplier = 0.2f; // ✅ Rendre la ligne plus visible

        // Création du texte sans modifier sa position
        GameObject textObj = Instantiate(textPrefab, transform);
        textComponent = textObj.GetComponent<TextMeshPro>();
        textComponent.fontSize = 400;
        textComponent.color = Color.white;
        textComponent.text = "0";
        textComponent.transform.position = textPosition; // ✅ Texte ne bouge pas

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

        // Mise à jour uniquement du texte
        textComponent.text = stockValues[stockValues.Count - 1].ToString();
    }
}
