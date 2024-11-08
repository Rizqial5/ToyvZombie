using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TvZ.Management;
using UnityEngine.UI;

namespace TvZ.Research
{
    public class ResearchCategoryUnlock : MonoBehaviour
    {
        [SerializeField] GameObject toyResearchButton;

        public void CheckToyResearch()
        {
            bool isToyResearchUnlock = UnlockableResources.Instance.CheckResourcesCategory(ResourcesEnum.BluePrint);

            

            if(!isToyResearchUnlock)
            {
                toyResearchButton.GetComponent<Button>().enabled = false;
                toyResearchButton.GetComponent<Image>().color = Color.gray;
            }
            else if(isToyResearchUnlock)
            {
                toyResearchButton.GetComponent<Button>().enabled = true;
                toyResearchButton.GetComponent<Image>().color = Color.white;
            }
            
        }
    }
}
