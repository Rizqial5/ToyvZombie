using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

namespace TvZ.UI
{
    public class DayTimeUI : MonoBehaviour
    {


        [Header("Menu UI")]
        [SerializeField] GameObject placeMenuUI;
        [SerializeField] GameObject upgradeMenuUI;
        [SerializeField] GameObject researchMenuUI;

        [SerializeField] Button backMenuButton;

        [Header("CloseAnimation")]
        [SerializeField] float yEndPos;

        private float oldYpos;
        private float oldSelecetedXPos;

        private void Start()
        {
            backMenuButton.onClick.AddListener(BackMenuButton);
        }


        public async void PlaceMenuButton()
        {

            await CloseUIAnimation();
            await SelectedUIOpenAnim(placeMenuUI, -803f);

            backMenuButton.gameObject.SetActive(true);
            gameObject.SetActive(false);

            

            backMenuButton.onClick.AddListener(async () => { await SelectedUICloseAnim(placeMenuUI,-1276); });
        }

        

        public async void UpgradeMenuButton()
        {
   
            await CloseUIAnimation();

            await SelectedUIOpenAnim(upgradeMenuUI,11f);

            backMenuButton.gameObject.SetActive(true);
            gameObject.SetActive(false);
            backMenuButton.onClick.AddListener(async () => { await SelectedUICloseAnim(upgradeMenuUI,-1695); });
        }

        public async void ResearchButton()
        {

            await CloseUIAnimation();

            await SelectedUIOpenAnim(researchMenuUI, -28f);

            backMenuButton.gameObject.SetActive(true);

            gameObject.SetActive(false);
            backMenuButton.onClick.AddListener(async () => { await SelectedUICloseAnim(researchMenuUI,-1796); });
        }

        public void BackMenuButton()
        {
            BackUIAnimation();
            gameObject.SetActive(true);

            backMenuButton.gameObject.SetActive(false);
            
        }

        public async Task CloseUIAnimation()
        {
            oldYpos = transform.position.y;
            await GetComponent<RectTransform>().DOAnchorPosY(yEndPos,.3f).AsyncWaitForCompletion();


        }


        public async Task SelectedUIOpenAnim(GameObject targetedUI, float targetedXPos)
        {
            targetedUI.SetActive(true);
            

            await targetedUI.GetComponent<RectTransform>().DOAnchorPosX(targetedXPos, .3f).AsyncWaitForCompletion();

        }

        public async Task SelectedUICloseAnim(GameObject targetedUI, float targetedXPos)
        {
            await targetedUI.GetComponent<RectTransform>().DOAnchorPosX(targetedXPos, .3f).AsyncWaitForCompletion();
            targetedUI.SetActive(false);

        }

        public void BackUIAnimation()
        {
            transform.DOMoveY(oldYpos, .8f);
            
        }
    }
}
