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
}
