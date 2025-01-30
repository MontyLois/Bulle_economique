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
   
    // for singleton Ensures it's created automatically if accessed before existing
    public static UIManager GetInstance()
    {
        if (Instance == null)
        {
            GameObject managerObject = new GameObject("Market_Manager");
            Instance = managerObject.AddComponent<UIManager>();
            DontDestroyOnLoad(managerObject);
        }
        return Instance;
    }
    
    //update all UI with money amount
    public void UpdateMoneyUI(int money)
    {
        
    }

    public void UpdateWandUI(Bubble_Wand_Data bubble_wand)
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
