using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //for singleton behavior
    public static UIManager Instance { get; private set; }

    //bubble wand
    [SerializeField] private RawImage bubblerIcon;
    [SerializeField] private TextMeshProUGUI bubbler_Name;
    [SerializeField] private TextMeshProUGUI bubbler_Rarity;
    [SerializeField] private TextMeshProUGUI next_bubbler_price;
    
    //cash
    [SerializeField] private TextMeshProUGUI  bubble_cash;
    
    //bubble
    [SerializeField] private TextMeshProUGUI  bubble_name;
    [SerializeField] private TextMeshProUGUI  bubble_rarity;

    //slots
    [SerializeField] private TextMeshProUGUI[] text_solts;
    [SerializeField] private RawImage[] icons_slots;
    [SerializeField] private RawImage[] icon_availability_slot;
    [SerializeField] private Texture dolar_icon;
    
    
    //markets
    [SerializeField] private TextMeshProUGUI[] text_price_market;
    [SerializeField] private TextMeshProUGUI[] text_name_bubble_market;
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
        bubble_cash.text = money.ToString();
    }

    public void UpdateWandUI(Bubble_Wand_Data bubble_wand)
    {
        bubblerIcon.texture = bubble_wand.icon;
        bubbler_Name.text = bubble_wand.wand_name;
        bubbler_Rarity.text = bubble_wand.rank;
    }

    public void UpdateGraphName(Bubble_Data bubble, int index)
    {
        if (bubble)
        {
            text_name_bubble_market[index].text = bubble.Category_name;
        }
        else
        {
            text_name_bubble_market[index].text = "";
        }
        
    }

    public void UpdateGraphCurrentPrice(int current_price, int index)
    {
        text_price_market[index].text = current_price.ToString();
    }

    public void UpdateNextWandUI(Bubble_Wand_Data bubble_wand)
    {
        if (bubble_wand)
        {
            next_bubbler_price.text = bubble_wand.price.ToString();
        }
        else
        {
            next_bubbler_price.text = "";
        }
    }

    public void UpdateBubbleInventoryUI(Bubble_Data bubbleDatas, int index)
    {
        if (bubbleDatas)
        {
            text_solts[index].text = "VENDRE";
            icons_slots[index].texture = bubbleDatas.icon;
            icons_slots[index].gameObject.SetActive(true);
        }
        else
        {
            text_solts[index].text = "";
            icons_slots[index].gameObject.SetActive(false);
        }
    }
    
    public void AddSlot(int index)
    {
        text_solts[index].text = "";
        icon_availability_slot[index].texture = dolar_icon;
        icons_slots[index].gameObject.SetActive(false);
    }

    public void UpdateSelectedBubble(Bubble_Data bubbleData)
    {
        bubble_name.text = bubbleData.Category_name;
        bubble_rarity.text = bubbleData.rarity;
    }
}
