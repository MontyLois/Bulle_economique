using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Bubble_Wand_Data", menuName = "Scriptable Objects/Bubble_Wand_Data")]
public class Bubble_Wand_Data : ScriptableObject
{
    [field: SerializeField]
    public String wand_name { get; private set; }
    
    [field: SerializeField]
    public String description { get; private set; }
    
    [field: SerializeField]
    public String rank { get; private set; }
    
    [field: SerializeField]
    public int price { get; private set; }
    
    [field: SerializeField]
    public int[] rarity_chance { get; private set; }
}
