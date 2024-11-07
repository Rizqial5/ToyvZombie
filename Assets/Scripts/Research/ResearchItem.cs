using System.Collections;
using System.Collections.Generic;
using TMPro;
using TvZ.Character;
using TvZ.Management;
using TvZ.TimeMechanic;
using UnityEngine;
using UnityEngine.UI;

namespace TvZ.Research
{
    public class ResearchItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI itemName;


        private List<ResourcesEnum> resourceCategories = new List<ResourcesEnum>();
        private StatSO statChar;
        private ResourcesStatSO resourcesStatSO;
        private ListResearchSO listResearch;

        private ResearchMechanic researchMechanic;

        private bool isResourceAvailable = false;
        



        //Set function untuk mengeset atribut research (resources requirements, teks)
        public void SetResearchItem(string nameItem, List<ResourcesEnum> resourcesCategoriesRequired, ResourcesStatSO resourcesStat, ListResearchSO listResearchSO, StatSO statSO)
        {
            itemName.text = nameItem;
            resourceCategories = resourcesCategoriesRequired;
            resourcesStatSO = resourcesStat;
            statChar = statSO;
            listResearch = listResearchSO;

            GetComponent<Button>().onClick.AddListener(ShowReseachDesc);

        }

        public void ShowReseachDesc()
        {
            

            researchMechanic = FindAnyObjectByType<ResearchMechanic>();

            researchMechanic.researchButton.onClick.RemoveAllListeners();
            researchMechanic.researchButton.onClick.AddListener(StartResearch);

            
        }

        public void StartResearch()
        {
            for (int i = 0; i < resourceCategories.Count; i++)
            {
                if (resourcesStatSO.GetResources(resourceCategories[i]) >= listResearch.GetResearchRequiredAmount(statChar, resourceCategories[i]))
                {

                    resourcesStatSO.AddResources(resourceCategories[i], - listResearch.GetResearchRequiredAmount(statChar, resourceCategories[i]));

                    print( resourceCategories[i] + " : " + resourcesStatSO.GetResources(resourceCategories[i]));

                    FindAnyObjectByType<ResourcesInventoryUI>().ShowResources();
                    isResourceAvailable = true;

                }
                else 
                {
                    isResourceAvailable = false;
                    break;
                }
            }

            if (!isResourceAvailable) return;

            print("Research Berjalan");
            TimeSystem timeSystem = FindAnyObjectByType<TimeSystem>();
            timeSystem.onDayChanged.AddListener(() => { CheckResearch(1); });
            GetComponent<Button>().enabled = false;
            GetComponent<Image>().color = Color.black;


        }

        public void CheckResearch(int requiredDay)
        {
            requiredDay--;

            if(requiredDay <= 0)
            {
                print("Research untuk " + statChar.name + " Selesai");
                ToyInventory.Instance.AddToy(statChar);
                
            }
        }

        //Research button function untuk melakukan pembayaran requirement dan melakukan research
    }
}
