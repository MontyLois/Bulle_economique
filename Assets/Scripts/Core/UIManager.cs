using UnityEngine;

public class UIManager : MonoBehaviour
{
    //for singleton behavior
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        //for singleton behavior
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //for singleton behavior_end
    }

    //update all UI with money amount
    public void UpdateMoneyUI(int money)
    {
        
    }

    public void UpdateBubbleInventoryUI(Bubble_Data[] bubbleDatas)
    {
        
    }
    
    public void AddSlot()
    {
        
    }

    public void UpdateSelectedBubble(Bubble_Data bubbleData)
    {
        
    }
}
