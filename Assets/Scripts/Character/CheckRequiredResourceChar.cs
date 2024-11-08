using System.Collections;
using System.Collections.Generic;
using TvZ.Management;
using UnityEngine;

namespace TvZ.Character
{
    public class CheckRequiredResourceChar : MonoBehaviour
    {

        public bool isResourceAvail;

        private StatSO statChar;
        private ResourcesStatSO resourcesStatSO;
        List<bool> resourceEnoughTotal;
        


        public void SetCheck(StatSO statSO, ResourcesStatSO resourcesStatSO)
        {
            statChar = statSO;
            this.resourcesStatSO = resourcesStatSO;
        }
        public void CheckResource()
        {
            List<ResourcesEnum> listResourcesRequired = statChar.GetResoucesListRequired();

            resourceEnoughTotal = new List<bool>();

            foreach (ResourcesEnum item in listResourcesRequired)
            {
                if (resourcesStatSO.GetResources(item) >= statChar.GetResourceRequiredAmount(item))
                {
                    resourceEnoughTotal.Add(true);
                    print(true);
                }else
                {
                    resourceEnoughTotal.Add(false);
                }
            }

            if(resourceEnoughTotal.Contains(false))
            {
                isResourceAvail = false;

            }
            else
            {
                isResourceAvail = true;
            }
        }

        public void BuildToy()
        {
            List<ResourcesEnum> listResourcesRequired = statChar.GetResoucesListRequired();
            

            foreach (ResourcesEnum item in listResourcesRequired)
            {
                resourcesStatSO.AddResources(item, -statChar.GetResourceRequiredAmount(item));
            }
        }
    }
}
