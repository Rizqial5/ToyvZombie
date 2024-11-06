using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TvZ.UI
{
    public class DayTimeUI : MonoBehaviour
    {



        [Header("Menu UI")]
        [SerializeField] GameObject placeMenuUI;
        [SerializeField] GameObject upgradeMenuUI;
        [SerializeField] GameObject researchMenuUI;

        [SerializeField] Button backMenuButton;

        private void Start()
        {
            backMenuButton.onClick.AddListener(BackMenuButton);
        }

        public void PlaceMenuButton()
        {
            placeMenuUI.SetActive(true);
            backMenuButton.gameObject.SetActive(true);

            gameObject.SetActive(false);
            backMenuButton.onClick.AddListener(() => { placeMenuUI.SetActive(false); });
        }

        public void UpgradeMenuButton()
        {
            upgradeMenuUI.SetActive(true);
            backMenuButton.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        public void ResearchButton()
        {
            researchMenuUI.SetActive(true);
            backMenuButton.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        public void BackMenuButton()
        {
            gameObject.SetActive(true);

            backMenuButton.gameObject.SetActive(false);
            
        }
    }
}
