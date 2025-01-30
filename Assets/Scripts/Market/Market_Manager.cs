using UnityEngine;

public class Market_Manager : MonoBehaviour
{
    //for singleton behavior
    public static Market_Manager Instance { get; private set; }

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
    public static Market_Manager GetInstance()
    {
        if (Instance == null)
        {
            GameObject managerObject = new GameObject("Market_Manager");
            Instance = managerObject.AddComponent<Market_Manager>();
            DontDestroyOnLoad(managerObject);
        }
        return Instance;
    }
}
