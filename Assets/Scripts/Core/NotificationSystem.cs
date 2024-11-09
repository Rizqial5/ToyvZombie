using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TvZ.Core
{
    public class NotificationSystem : MonoBehaviour
    {

        [SerializeField] GameObject notifPanel;
        [SerializeField] Transform notifTransform;
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

        public void SpawnNotif(string notifText)
        {
            if (notifPanel == null) return;
            
            GameObject spawnedNotif = Instantiate(notifPanel,notifTransform);

            spawnedNotif.GetComponentInChildren<TextMeshProUGUI>().text = notifText;    

            Destroy(spawnedNotif,timerNotif);


        }
    }
}
