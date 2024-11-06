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
        [SerializeField] TextMeshProUGUI goldVaueText;
        [SerializeField] TextMeshProUGUI bluePrintValueText;

        private void Start()
        {
            ShowResources();
        }

        public void ShowResources()
        {
            toyFragmentValueText.text = resourcesStatSO.GetResources(ResourcesEnum.ToyFragment).ToString();
            goldVaueText.text = resourcesStatSO.GetResources(ResourcesEnum.Gold).ToString();
            bluePrintValueText.text = resourcesStatSO.GetResources(ResourcesEnum.BluePrint).ToString();
        }


    }
}
