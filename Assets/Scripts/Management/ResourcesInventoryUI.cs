using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace TvZ.Management
{
    public class ResourcesInventoryUI : MonoBehaviour
    {
        [SerializeField] ResourcesStatSO resourcesStatSO;

        [Header("UI Object")]
        [SerializeField] TextMeshProUGUI toyFragmentValueText;
        [SerializeField] TextMeshProUGUI toyEnegrgyValueText;
        [SerializeField] TextMeshProUGUI goldVaueText;
        [SerializeField] TextMeshProUGUI bluePrintValueText;

        private void Start()
        {
            
        }

        private void Update()
        {
            ShowResources();
        }

        public void ShowResources()
        {
            toyFragmentValueText.text = resourcesStatSO.GetResources(ResourcesEnum.ToyFragment).ToString();
            toyEnegrgyValueText.text = resourcesStatSO.GetResources(ResourcesEnum.ToyEnergy).ToString();
            goldVaueText.text = resourcesStatSO.GetResources(ResourcesEnum.Gold).ToString();
            bluePrintValueText.text = resourcesStatSO.GetResources(ResourcesEnum.BluePrint).ToString();
        }


    }
}
