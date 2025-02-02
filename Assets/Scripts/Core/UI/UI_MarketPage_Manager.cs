using TMPro;
using UnityEngine;

public class UI_MarketPage_Manager : MonoBehaviour
{
    //all data of bubbles
    private Bubble_Data[] bubbles;
    private int bubble_selected; //wich index of the bubble selected
    
    //bubble
    [SerializeField] private TextMeshProUGUI  bubble_name;
    [SerializeField] private TextMeshProUGUI  bubble_rarity;
    [SerializeField] private TextMeshProUGUI  bubble_price;
    [SerializeField] private TextMeshProUGUI  bubble_max_price;
    [SerializeField] private TextMeshProUGUI  bubble_min_price;

    [SerializeField] private GameObject marketPage;
    
    //market graph
    [SerializeField] private GraphRenderer graph;
    
    //for singleton behavior
    public static UI_MarketPage_Manager Instance { get; private set; }
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
        
        //retrieve all data of bubbles
        bubbles = GameController.GameDatabase.Bubbles;

        //display the first bubble data
        ChangeSelectedSlot(0);
        
        graph.SetSlot(3);
    }
    
    // for singleton Ensures it's created automatically if accessed before existing
    public static UI_MarketPage_Manager GetInstance()
    {
        if (Instance == null)
        {
            GameObject managerObject = new GameObject("UI_MarketPage_Manager");
            Instance = managerObject.AddComponent<UI_MarketPage_Manager>();
            DontDestroyOnLoad(managerObject);
        }
        return Instance;
    }
    
    
    //change the bubble that is showcast based on the slot selected
    public void ChangeSelectedSlot(int slot_index)
    {
            //UIManager.Instance.UpdateSelectedSlot(slot_selected,slot_index);
            bubble_selected = slot_index;
            
            //we retrive the data of the bubble selected
            Bubble_Data currentData = bubbles[bubble_selected];

            bubble_name.text = currentData.Category_name;
            bubble_rarity.text = currentData.rarity;
            bubble_price.text = currentData.market.current_price.ToString();
            bubble_min_price.text = currentData.market.min_price.ToString();
            bubble_max_price.text = currentData.market.max_price.ToString();
            graph.SetNewMarket(currentData.market);
    }

    public void Quit_Page()
    {
        marketPage.SetActive(false);
    }
    
    public void Open_Page()
    {
        marketPage.SetActive(true);
    }
}
