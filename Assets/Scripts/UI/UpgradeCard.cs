using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TvZ.Upgrade;
using UnityEngine.Events;
using TvZ.Management;

namespace TvZ.UI
{
    public class UpgradeCard : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameUpgrade;
        [SerializeField] TextMeshProUGUI upgradeDesc;
        [SerializeField] Image upgradeImage;
        [SerializeField] TextMeshProUGUI upgradeCostText;
        [SerializeField] Button upgradeButton;
        [SerializeField] TextMeshProUGUI completedDesc;
        [SerializeField] GameObject competedFilterImage;

        private bool isUpgrading;
        public ResourcesEnum upgradeResourcesType {  get; private set; }

        public void SetUpgradeCard(ResourcesEnum cardName, string cardDesc, string upgradeCost, Sprite upgradeImage)
        {
            nameUpgrade.text = cardName.ToString();
            upgradeDesc.text = cardDesc;
            upgradeCostText.text = upgradeCost;

            this.upgradeImage.sprite = upgradeImage;

            upgradeResourcesType = cardName;
        }

        public void SetUpgradeButton(UnityAction onClickButton)
        {
            upgradeButton.onClick.AddListener(onClickButton);
            
        }

        public void SetUpgradeComplete()
        {
            completedDesc.text = "Max Upgrade";

            upgradeButton.onClick.RemoveAllListeners();
            competedFilterImage.SetActive(true);
            upgradeButton.enabled = false;
        }

        public void SetUpgradeStatus(bool isUpgrade)
        {
            isUpgrading = isUpgrade;

            if(isUpgrading)
            {
                competedFilterImage.SetActive(true);
                completedDesc.text = "Upgrading...";
                upgradeButton.enabled = false;
            }
            else
            {
                competedFilterImage.SetActive(false);
                completedDesc.text = string.Empty;
                upgradeButton.enabled = true;
            }
        }

    }
}
