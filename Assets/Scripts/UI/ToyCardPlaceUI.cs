using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TvZ.Management;
using TvZ.Character;
using TvZ.Core;
using TMPro;
using Unity.VisualScripting;

namespace TvZ.UI
{
    public class ToyCardPlaceUI : MonoBehaviour
    {
        [SerializeField] ToyCardUI toyCardUI;
        [SerializeField] Transform cardListUILayout;
        [SerializeField] GameObject descObject;

        [SerializeField] ResourcesStatSO resourcesStatSO;

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

                ToyCardUI toyCardSpawned = Instantiate(toyCardUI, cardListUILayout);
                
                toyCardSpawned.GetComponent<DragDropHandler>().objectToSpawn = listToyAvailable[i].GetCharPrefab();

                toyCardSpawned.GetComponentInChildren<TextMeshProUGUI>().text = listToyAvailable[i].name;

                toyCardSpawned.GetComponent<CheckRequiredResourceChar>().SetCheck(listToyAvailable[i], resourcesStatSO);

                toyCardSpawned.SetToyCard(listToyAvailable[i]);

                foreach (ResourcesEnum item in listToyAvailable[i].GetResoucesListRequired())
                {
                    toyCardSpawned.SetToyCardDesc(listToyAvailable[i].GetResourceRequiredAmount(item), descObject);
                }

                listSpawned.Add(listToyAvailable[i]);
            }
        }

        

        
        
    }
}
