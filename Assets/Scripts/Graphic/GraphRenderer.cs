using System.Collections.Generic; // Ajout de l'espace de noms pour List<T>
using TMPro;
using UnityEngine;

public class GraphRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject textPrefab; // Prefab contenant un TextMeshPro
    private TextMeshPro textComponent; // Un seul texte pour afficher la dernière valeur

    [Min(0)] public int precision;
    public int maxStockValue;

    [Range(0, 50)] public float height;
    [Range(0, 50)] public float width;

    private List<float> stockValues; // Liste des valeurs du graphe

    private void Start()
    {
        stockValues = new List<float>();

        for (int i = 0; i < precision; i++)
        {
            stockValues.Add(0);
        }

        // Création d'un seul texte pour afficher la dernière valeur
        GameObject textObj = Instantiate(textPrefab, transform);
        textComponent = textObj.GetComponent<TextMeshPro>();
        textComponent.fontSize = 400; // Ajuste la taille du texte
        textComponent.color = Color.white; // Couleur du texte
        textComponent.text = "0"; // Valeur de départ

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
        Vector3 lastPosition = Vector3.zero;

        for (int i = 0; i < stockValues.Count; i++)
        {
            float minX = -width / 2;
            float maxX = width / 2;
            float minY = -height / 2;
            float maxY = height / 2;

            float xPercent = currentPoint / precision;
            float x = Mathf.Lerp(minX, maxX, xPercent);

            float yPercent = stockValues[i] / maxStockValue;
            float y = Mathf.Lerp(minY, maxY, yPercent);

            Vector3 position = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, position);
            lastPosition = position; // Stocke la dernière position
            currentPoint += 1;
        }

        // Mise à jour du texte : 
        textComponent.text = stockValues[stockValues.Count - 1].ToString(); // Affiche la dernière valeur

        // Déplacer le texte avec un décalage (modifie ces valeurs pour voir les changements)
        textComponent.transform.position = new Vector3(lastPosition.x + 430f, lastPosition.y + 210f, 0); // Ajuster la position selon les besoins
    }
}