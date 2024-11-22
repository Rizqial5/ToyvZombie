using System.Collections;
using System.Collections.Generic;
using TMPro;
using TvZ.Interfaces;
using TvZ.UI;
using UnityEngine;
using UnityEngine.Pool;

namespace TvZ.Core
{
    public class NotificationSystem : MonoBehaviour
    {

        [SerializeField] NotifPanelUI notifPanel;
        [SerializeField] Transform notifTransformRight;
        [SerializeField] Transform notifTransformLeft;
        [SerializeField] float timerNotif = 1f;

        public static NotificationSystem Instance;

        private ObjectSpawner notifSpawner;

        private ObjectPool<GameObject> notifPool;

        private void Awake()
        {
            notifSpawner = GetComponent<ObjectSpawner>();

            if (Instance == null)
            {
                Instance = this;
                
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            notifSpawner.SetObjectPrefab(notifPanel.gameObject, notifTransformRight, notifTransformRight);
        }

        public void SpawnNotifRight(string notifText)
        {
            if (notifPanel == null) return;


            

            GameObject spawnedNotif = notifSpawner._pool.Get();
            spawnedNotif.transform.parent = notifTransformRight;

            spawnedNotif.GetComponentInChildren<TextMeshProUGUI>().text = notifText;

            StartCoroutine(DestroyTimer(spawnedNotif, timerNotif));


        }
        public void SpawnNotifLeft(string notifText)
        {
            if (notifPanel == null) return;

           

            GameObject spawnedNotif = notifSpawner._pool.Get();
            spawnedNotif.transform.parent = notifTransformLeft;

            spawnedNotif.GetComponentInChildren<TextMeshProUGUI>().text = notifText;

            StartCoroutine(DestroyTimer(spawnedNotif, timerNotif));


        }

        private IEnumerator DestroyTimer(GameObject objectDestroy,float seconds)
        {
            yield return new WaitForSeconds(seconds);

            objectDestroy.GetComponent<NotifPanelUI>().ReleaseObject();
        }

        
    }
}
