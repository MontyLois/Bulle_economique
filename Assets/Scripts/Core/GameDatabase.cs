using UnityEngine;

public class GameDatabase
{
        public Bubble_Wand_Data[] Bubble_Wands { get; private set; }
        public Bubble_Data[] Bubbles { get; private set; }
        
        public Market_Data[] Markets { get; private set; }
        
        // Permet de récuperer les objet dans les ressources
        public GameDatabase()
        {
            Bubble_Wands = Resources.LoadAll<Bubble_Wand_Data>("Bubble_Wands");
            Bubbles = Resources.LoadAll<Bubble_Data>("Bubbles");
            Markets = Resources.LoadAll<Market_Data>("Markets");
        }
}
