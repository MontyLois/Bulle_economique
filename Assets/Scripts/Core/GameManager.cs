using System;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    //for singleton behavior
    public static GameManager Instance { get; private set; }

    //amount of monay owned by the player
    public int money { get; private set; }
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
    public static GameManager GetInstance()
    {
        if (Instance == null)
        {
            GameObject managerObject = new GameObject("Market_Manager");
            Instance = managerObject.AddComponent<GameManager>();
            DontDestroyOnLoad(managerObject);
        }
        return Instance;
    }
    
    //when we gain money and get more stonk
    public void AddMoney(int amount)
    {
        money += amount;
        UIManager.Instance.UpdateMoneyUI(money);
    }

    //when we spend money to slay even more
    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UIManager.Instance.UpdateMoneyUI(money);
            return true;
        }
        return false;
    }
    
    
}
