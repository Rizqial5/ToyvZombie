using System.Collections;
using System.Collections.Generic;
using TvZ.Character;
using UnityEngine;

namespace TvZ.Management
{
    public class ToyInventory : MonoBehaviour
    {
        public static ToyInventory Instance;
        public List<StatSO> toyAvaialableList = new List<StatSO>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void AddToy(StatSO toy)
        {
            if (toyAvaialableList.Contains(toy)) return;

            toyAvaialableList.Add(toy);
        }

        public void RemoveToy(StatSO toy)
        {
            toyAvaialableList.Remove(toy);
        }

        public List<StatSO> GetToyInventory()
        {
            

            return toyAvaialableList;
        }
    }
}
