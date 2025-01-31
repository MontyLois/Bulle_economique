using UnityEngine;

namespace Core.WeightedDropRate
{
    [System.Serializable]
    public struct WeightedDropRate<T>
    {
        [System.Serializable]
        private struct Info
        {
            [SerializeField] public int weight;
            [SerializeField] public T value;
        }

        [SerializeField] 
        private Info[] infos;
        
        public T GetRandomValue()
        {
            int maxWeight = 0;

            var possibilities = infos.Length;
            int[] weights = new int[possibilities];

            for (int i = 0; i < possibilities; i++)
            {
                maxWeight += infos[i].weight;
                weights[i] = maxWeight;
            }

            int rnd = Random.Range(0, maxWeight);

            for (int i = 0; i < possibilities; i++)
            {
                if (rnd <= weights[i])
                    return infos[i].value;
            }

            return default;
        }
    }
}