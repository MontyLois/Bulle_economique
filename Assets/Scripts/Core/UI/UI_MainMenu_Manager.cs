using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu_Manager : MonoBehaviour
{
    
    //for singleton behavior
    public static UI_MainMenu_Manager Instance { get; private set; }
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
    public static UI_MainMenu_Manager GetInstance()
    {
        if (Instance == null)
        {
            GameObject managerObject = new GameObject("UI_MainMenu_Manager");
            Instance = managerObject.AddComponent<UI_MainMenu_Manager>();
            DontDestroyOnLoad(managerObject);
        }
        return Instance;
    }
    
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Pop_Market_MainScene");
    }
}
