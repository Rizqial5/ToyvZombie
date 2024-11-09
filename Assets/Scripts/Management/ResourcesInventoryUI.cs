using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        [Header("Image UI")]
        [SerializeField] Image toyFragmentImage;
        [SerializeField] Image toyEnergyImage;
        [SerializeField] Image goldImage;
        [SerializeField] Image bluePrintImage;
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

            toyFragmentImage.sprite = resourcesStatSO.GetImage(ResourcesEnum.ToyFragment);
            toyEnergyImage.sprite = resourcesStatSO.GetImage(ResourcesEnum.ToyEnergy);
            goldImage.sprite = resourcesStatSO.GetImage(ResourcesEnum.Gold);
            bluePrintImage.sprite = resourcesStatSO.GetImage(ResourcesEnum.BluePrint);
        }


    }
}
