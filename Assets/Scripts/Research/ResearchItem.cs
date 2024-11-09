using System.Collections;
using System.Collections.Generic;
using TMPro;
using TvZ.Character;
using TvZ.Core;
using TvZ.Management;
using TvZ.TimeMechanic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace TvZ.Research
{
    public class ResearchItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI itemName;
        [SerializeField] TextMeshProUGUI statusResearch;
        [SerializeField] GameObject statusImage;
        [SerializeField] Image researchImage;

        

        public ResearchCategory researchCategory {  get; private set; }

        private List<ResourcesEnum> resourceCategories = new List<ResourcesEnum>();

        private StatSO statChar;
        private ResourcesEnum unlockAbleResources;

        private ResourcesStatSO resourcesStatSO;
        private ListResearchSO listResearch;

        private ResearchMechanic researchMechanic;

        private bool isResourceAvailable = false;
        private bool isDoneResearch = false;
        private TimeSystem timeSystem;



        private void Start()
        {
            timeSystem = FindAnyObjectByType<TimeSystem>();
            
        }
        //Set function untuk mengeset atribut research (resources requirements, teks)
        public void SetResearchChar(string nameItem, List<ResourcesEnum> resourcesCategoriesRequired, ResourcesStatSO resourcesStat, ListResearchSO listResearchSO, StatSO statSO, Sprite researchImage)
        {
            itemName.text = nameItem;
            resourceCategories = resourcesCategoriesRequired;
            resourcesStatSO = resourcesStat;
            statChar = statSO;
            listResearch = listResearchSO;
            this.researchImage.GetComponent<RectTransform>().localScale = new Vector3(4, 4, 1);
            this.researchImage.raycastPadding = new Vector4(80, 80, 80, 80);
            this.researchImage.sprite = researchImage;

            researchCategory = ResearchCategory.CharResearch;

            GetComponent<Button>().onClick.AddListener(() => { ShowReseachDesc(ResearchCategory.CharResearch); });

        }

        public void SetResearchResource(ResourcesStatSO resourcesStat, ListResearchSO listResearchSO, ResourcesEnum resourcesEnum, Sprite researchImage)
        {
            itemName.text = resourcesEnum.ToString();

            researchCategory = ResearchCategory.ResourceResearch;
            listResearch = listResearchSO;
            resourcesStatSO = resourcesStat;
            unlockAbleResources = resourcesEnum;

            this.researchImage.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            this.researchImage.raycastPadding = new Vector4(1, 1, 1, 1);
            this.researchImage.sprite = researchImage;
            

            GetComponent<Button>().onClick.AddListener(() => { ShowReseachDesc(ResearchCategory.ResourceResearch); });
        }

        public void ShowReseachDesc(ResearchCategory researchCategory)
        {
            

            researchMechanic = FindAnyObjectByType<ResearchMechanic>();

            researchMechanic.researchButton.onClick.RemoveAllListeners();
            researchMechanic.researchButton.onClick.AddListener(() => { StartResearchItem(researchCategory); });

            
        }

        public void StartResearchItem(ResearchCategory researchCategory)
        {
            if(researchCategory == ResearchCategory.CharResearch)
            {
                CheckResourceForChar();
            }
            else if(researchCategory == ResearchCategory.ResourceResearch)
            {
                CheckResourceForResource();
            }

            

            if (!isResourceAvailable)
            {
                NotificationSystem.Instance.SpawnNotifRight("Resource Tidak Cukup");
                //
                return; 
            }

            ChangeResearchStatus("Researching");
            GetComponent<Button>().enabled = false;
            

            

            timeSystem.onDayChanged.AddListener(() => { CheckResearch(1, researchCategory); });


        }

        private void CheckResourceForChar() // bug 
        {
            for (int i = 0; i < resourceCategories.Count; i++)
            {
                if (resourcesStatSO.GetResources(resourceCategories[i]) >= listResearch.GetCharResearchRequiredAmount(statChar, resourceCategories[i]))
                {

                    resourcesStatSO.AddResources(resourceCategories[i], -listResearch.GetCharResearchRequiredAmount(statChar, resourceCategories[i]));

                    print(resourceCategories[i] + " : " + resourcesStatSO.GetResources(resourceCategories[i]));

                    FindAnyObjectByType<ResourcesInventoryUI>().ShowResources();
                    isResourceAvailable = true;

                }
                else
                {
                    isResourceAvailable = false;
                    break;
                }
            }
        }

        private void CheckResourceForResource()
        {
           
            if (resourcesStatSO.GetResources(ResourcesEnum.Gold) >= listResearch.GetGoldResearchRequirements(unlockAbleResources))
            {
                resourcesStatSO.AddResources(ResourcesEnum.Gold, -listResearch.GetGoldResearchRequirements(unlockAbleResources));

                FindAnyObjectByType<ResourcesInventoryUI>().ShowResources();

                isResourceAvailable = true;
            }
            else
            {
                isResourceAvailable = false;

            }
        }

        public void CheckResearch(int requiredDay, ResearchCategory researchCategory)
        {

            if (isDoneResearch) return;

            requiredDay--;

            if(requiredDay <= 0)
            {
                ChangeResearchStatus("Completed");
                NotificationSystem.Instance.SpawnNotifRight("Research Completed");
                

                switch (researchCategory)
                {
                    case ResearchCategory.CharResearch:

                        ToyInventory.Instance.AddToy(statChar);
                        break;

                    case ResearchCategory.ResourceResearch:
                        UnlockableResources.Instance.AddResourcesCategpry(unlockAbleResources);
                        break;

                }

                isDoneResearch = true;

            }
        }

        public void ChangeResearchStatus(string statusText)
        {
            statusResearch.text = statusText;
            statusImage.SetActive(true);
        }

        
    }

    public enum ResearchCategory
    {
        CharResearch,
        ResourceResearch,
    }
}
