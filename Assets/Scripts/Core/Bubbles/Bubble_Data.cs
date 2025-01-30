using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Bubble_Data", menuName = "Scriptable Objects/Bubble_Data")]
public class Bubble_Data : ScriptableObject
{
    [field: SerializeField]
    public String Category_name { get; private set; }
    
    [field: SerializeField]
    public String rarity { get; private set; }
    
    [field: SerializeField]
    public int rarity_order { get; private set; }
    
    [field: SerializeField]
    public int base_price { get; private set; }
    
    [field: SerializeField]
    public GameObject body { get; private set; }
    
    [field: SerializeField]
    public Market_Data market { get; private set; }
    
    public void SellBubble()
    {
        GameManager.Instance.AddMoney((int)market.current_price);
    }
}
