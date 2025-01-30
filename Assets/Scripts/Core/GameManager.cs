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
    
    //when we gain money
    public void AddMoney(int amount)
    {
        money += amount;
        UIManager.Instance.UpdateMoneyUI(money);
    }

    //when we loose money
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
