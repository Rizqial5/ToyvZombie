using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TvZ.Management;
using TvZ.Character;
using TvZ.Core;
using TMPro;

namespace TvZ.UI
{
    public class ToyCardPlaceUI : MonoBehaviour
    {
        [SerializeField] DragDropHandler toyCardUI;
        [SerializeField] Transform cardListUILayout;

        private List<StatSO> listToyAvailable = new List<StatSO>();
        private List<StatSO> listSpawned = new List<StatSO>();

        public void ShowToyList()
        {
            //if (listToyAvailable.Count > 0) return;

            

            listToyAvailable = ToyInventory.Instance.GetToyInventory();


            if (listToyAvailable.Count == listSpawned.Count) return;



            for (int i = 0; i < listToyAvailable.Count; i++)
            {
                if (listSpawned.Contains(listToyAvailable[i])) continue;

                DragDropHandler toyCardSpawned = Instantiate(toyCardUI, cardListUILayout);
                
                toyCardSpawned.objectToSpawn = listToyAvailable[i].GetCharPrefab();

                toyCardSpawned.GetComponentInChildren<TextMeshProUGUI>().text = listToyAvailable[i].name;

                listSpawned.Add(listToyAvailable[i]);
            }
        }
        
    }
}
