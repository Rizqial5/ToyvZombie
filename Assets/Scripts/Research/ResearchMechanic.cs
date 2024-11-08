using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TvZ.Character;
using TvZ.Management;
using UnityEngine.UI;
using TMPro;
using System.CodeDom.Compiler;

namespace TvZ.Research
{
    public class ResearchMechanic : MonoBehaviour
    {
        [Header("UI Element")]
        [SerializeField] GameObject researchDescUI;
        [SerializeField] ResearchItem cardResearchPrefab;
        [SerializeField] Transform cardSpawnParent;

        [Header("UI Research Description Elemment")]
        [SerializeField] Transform researchRequiredLayout;
        [SerializeField] TextMeshProUGUI researchRequiredDesc;
        [SerializeField] TextMeshProUGUI researchFunctionDesc;

        public Button researchButton;

        [Header("Data")]
        [SerializeField] ResourcesStatSO resourcesStatSO;
        [SerializeField] ListResearchSO listResearch;
        

        private List<StatSO> listCharSpawned = new List<StatSO>(); 
        private List<ResourcesEnum> listResourceSpawned = new List<ResourcesEnum>();
        private List<TextMeshProUGUI> listDescSpawned = new List<TextMeshProUGUI>();

        private List<ResearchItem> listResearchSpawned = new List<ResearchItem>();  

        
        
        public void ShowCharList()
        {
            if (!UnlockableResources.Instance.CheckResourcesCategory(ResourcesEnum.BluePrint)) return;

            StatusResearchList(ResearchCategory.CharResearch, true);
            StatusResearchList(ResearchCategory.ResourceResearch, false);

            if (listCharSpawned.Count > 0) return;

            listCharSpawned = listResearch.GetListCharResearch();

            foreach (StatSO item in listCharSpawned)
            {
                SpawnCharResearch(item);
            }


        }

        private void SpawnCharResearch(StatSO item)
        {
            ResearchItem cardSpawned = Instantiate(cardResearchPrefab, cardSpawnParent);

            List<ResourcesEnum> resourcesEnums = listResearch.GetCharResearchRequirementCategory(item);

            

            cardSpawned.GetComponent<Button>().onClick.AddListener(ShowResearchDescUI);
            cardSpawned.GetComponent<Button>().onClick.AddListener(() => { ShowCharResearchDesc(item, resourcesEnums); });


            cardSpawned.SetResearchChar(item.name, resourcesEnums, resourcesStatSO, listResearch, item);

            listResearchSpawned.Add(cardSpawned);
        }

        public void ShowResourceList()
        {
            StatusResearchList(ResearchCategory.ResourceResearch, true);
            StatusResearchList(ResearchCategory.CharResearch, false);

            if (listResourceSpawned.Count > 0) return;

            listResourceSpawned = listResearch.GetListResourceResearch();

            foreach (ResourcesEnum item in listResourceSpawned)
            {
                SpawnResourceResearch(item);

                
            }
        }

        private void SpawnResourceResearch(ResourcesEnum item)
        {
            ResearchItem cardSpawned = Instantiate(cardResearchPrefab, cardSpawnParent);

            

            cardSpawned.GetComponent<Button>().onClick.AddListener(ShowResearchDescUI);
            cardSpawned.GetComponent<Button>().onClick.AddListener(() => { ShowResourceResearchDesc(item); });

            cardSpawned.SetResearchResource(resourcesStatSO, listResearch, item);



            listResearchSpawned.Add(cardSpawned);
        }


        public void ShowResearchDescUI()
        {
            researchDescUI.SetActive(true);
        }

        public void ShowCharResearchDesc(StatSO statChar, List<ResourcesEnum> resourceEnums)
        {
            foreach (ResourcesEnum item in resourceEnums)
            {
                TextMeshProUGUI spawnedDesc = SpawnDescriptionResearch();

                spawnedDesc.text = item.ToString() + " : " + listResearch.GetCharResearchRequiredAmount(statChar, item);

            }

            researchFunctionDesc.text = listResearch.GetResearchCharDesc(statChar);

        }

        public void ShowResourceResearchDesc(ResourcesEnum resourcesEnum)
        {
            TextMeshProUGUI spawnedDesc = SpawnDescriptionResearch();

            spawnedDesc.text = "Gold : " + listResearch.GetGoldResearchRequirements(resourcesEnum);
            researchFunctionDesc.text = listResearch.GetResourceDesc(resourcesEnum);

        }

        private TextMeshProUGUI SpawnDescriptionResearch()
        {
            TextMeshProUGUI spawnedDesc = Instantiate(researchRequiredDesc, researchRequiredLayout);
            listDescSpawned.Add(spawnedDesc);
            return spawnedDesc;
        }


        public void CloseReasearchDesc()
        {
            researchDescUI.SetActive(false);

            listDescSpawned.ForEach(spawned => { Destroy(spawned.gameObject); });

            listDescSpawned.Clear();
        }

        private void StatusResearchList(ResearchCategory researchCategory,bool isEnabled)
        {
            if (listResearchSpawned.Count > 0)
            {
                foreach (ResearchItem item in listResearchSpawned)
                {
                    if(item.researchCategory == researchCategory)
                    {
                        item.gameObject.SetActive(isEnabled);
                    }

                }
            }
        }

        private void ClearCharList()
        {
            if (listCharSpawned.Count > 0)
            {
                listCharSpawned.Clear();
            }

        }
        private void ClearResourceList()
        {
            if (listResourceSpawned.Count > 0)
            {
                listResourceSpawned.Clear();
            }

        }
    }
}
