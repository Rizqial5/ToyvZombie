using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TvZ.Core
{
    public class NotificationSystem : MonoBehaviour
    {

        [SerializeField] GameObject notifPanel;
        [SerializeField] Transform notifTransformRight;
        [SerializeField] Transform notifTransformLeft;
        [SerializeField] float timerNotif = 1f;

        public static NotificationSystem Instance;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SpawnNotifRight(string notifText)
        {
            if (notifPanel == null) return;
            
            GameObject spawnedNotif = Instantiate(notifPanel,notifTransformRight);

            spawnedNotif.GetComponentInChildren<TextMeshProUGUI>().text = notifText;    

            Destroy(spawnedNotif,timerNotif);


        }
        public void SpawnNotifLeft(string notifText)
        {
            if (notifPanel == null) return;

            GameObject spawnedNotif = Instantiate(notifPanel, notifTransformLeft);

            spawnedNotif.GetComponentInChildren<TextMeshProUGUI>().text = notifText;

            Destroy(spawnedNotif, timerNotif);


        }
    }
}
