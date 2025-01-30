using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Bubble_Data bubbleData;
    public GameObject bubbleObject;

    public Bubble(Bubble_Data data, GameObject obj)
    {
        this.bubbleData = data;
        this.bubbleObject = obj;
    }

    public void Sell()
    {
        if (bubbleObject != null)
        {
            GameObject.Destroy(bubbleObject); // Remove from scene
        }
        GameManager.Instance.AddMoney((int)bubbleData.market.current_price);
    }
}
