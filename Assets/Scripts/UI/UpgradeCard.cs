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

        private bool isUpgrading;
        public ResourcesEnum upgradeResourcesType {  get; private set; }

        public void SetUpgradeCard(ResourcesEnum cardName, string cardDesc, string upgradeCost)
        {
            nameUpgrade.text = cardName.ToString();
            upgradeDesc.text = cardDesc;
            upgradeCostText.text = upgradeCost;

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
            GetComponent<Image>().color = Color.gray;
            upgradeButton.enabled = false;
        }

        public void SetUpgradeStatus(bool isUpgrade)
        {
            isUpgrading = isUpgrade;

            if(isUpgrading)
            {
                GetComponent<Image>().color = Color.gray;
                completedDesc.text = "Upgrading...";
                upgradeButton.enabled = false;
            }
            else
            {
                GetComponent<Image>().color = Color.white;
                completedDesc.text = string.Empty;
                upgradeButton.enabled = true;
            }
        }

    }
}
