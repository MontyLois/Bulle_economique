using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    
    //so i can add spawner in the scene
   private void Awake()
   {
       if (Bubble_Manager.Instance != null) {
           Bubble_Manager.Instance.SetSpawner(transform); 
       }
   }
}
