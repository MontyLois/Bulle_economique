using System;
using System.Collections.Generic;
using Core.WeightedDropRate;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Bubble_Wand_Data", menuName = "Scriptable Objects/Bubble_Wand_Data")]
public class Bubble_Wand_Data : ScriptableObject
{
    [field: SerializeField]
    public String wand_name { get; private set; }
    
    [field: SerializeField]
    public int number_order { get; private set; }
    
    [field: SerializeField]
    public String description { get; private set; }
    
    [field: SerializeField]
    public String rank { get; private set; }
    
    [field: SerializeField]
    public int price { get; private set; }
    
    [field: SerializeField]
    public Texture icon { get; private set; }
    
    [field: SerializeField] 
    private WeightedDropRate<int> rarityDrop;


    public int GetRandomIndex() => rarityDrop.GetRandomValue();
}
