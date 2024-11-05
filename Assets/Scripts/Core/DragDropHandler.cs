using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TvZ.Core
{
    public class DragDropHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public GameObject objectToSpawn;  // Prefab yang akan di-drag ke world space
        private Camera mainCamera;  // Kamera utama (digunakan untuk konversi screen space ke world space)
        public SnapPoint[] snapPoints;  // Array snap points di world space
        public float snapRange = 1f;  // Jarak maksimum untuk snapping

        private GameObject draggedObject; // Objek yang sedang didrag




        

        public UnityEvent onDropChar;


        private void Awake()
        {
            
            mainCamera = Camera.main;
        }

        private void Start()
        {
            


            snapPoints = FindObjectsOfType<SnapPoint>();

            
        }

        private void Update()
        {
            FindSnapPoints();
        }


        

        public void FindSnapPoints()
        {
            if (snapPoints.Length > 0) return;
            snapPoints = FindObjectsOfType<SnapPoint>();
        }

        // Dipanggil saat pointer di tekan (awal drag)
        public void OnPointerDown(PointerEventData eventData)
        {
       
            // Buat objek yang didrag dari prefab
            draggedObject = Instantiate(objectToSpawn);
            draggedObject.SetActive(false);  // Sembunyikan dulu, akan ditampilkan saat drag
        }

        // Dipanggil saat drag dimulai
        public void OnBeginDrag(PointerEventData eventData)
        {
            // Tidak ada logika khusus di sini, objek akan ditampilkan saat drag
            
        }

        // Dipanggil selama drag
        public void OnDrag(PointerEventData eventData)
        {
            
            // Ubah posisi mouse dari screen space ke world space
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;  // Jarak z dari kamera ke object, sesuaikan dengan jarak kamera ke objek di world space
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            // Tampilkan objek yang sedang di-drag dan posisikan di world space sesuai posisi mouse
            draggedObject.SetActive(true);
            draggedObject.transform.position = worldPosition;

            foreach (SnapPoint item in snapPoints)
            {
                item.ActivateView(true);
            }
        }

        // Dipanggil saat drag selesai
        public void OnEndDrag(PointerEventData eventData)
        {
            
            // Ketika drag selesai, objek diletakkan di world space di posisi terakhir mouse
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;  // Jarak z dari kamera ke object
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            // Cek apakah objek berada di dekat snap point
            Transform nearestSnapPoint = GetNearestSnapPoint(worldPosition);

            if (nearestSnapPoint != null)
            {
                // Jika ada snap point yang dekat, letakkan objek di sana
                draggedObject.transform.SetParent(nearestSnapPoint);
                draggedObject.transform.position = nearestSnapPoint.position;

                nearestSnapPoint.GetComponent<BoxCollider2D>().enabled = true;



                // Behaviour yang ingin ditambahkan apabila karakter sudah di drop
                

                onDropChar.Invoke();
            }
            else
            {
                // Jika tidak ada snap point yang dekat, tempatkan objek di posisi mouse terakhir
                Destroy(draggedObject);
            }

            foreach (SnapPoint item in snapPoints)
            {
                item.ActivateView(false);
            }
        }

        // Mendapatkan snap point terdekat jika dalam jarak snapRange
        private Transform GetNearestSnapPoint(Vector3 currentPosition)
        {
            Transform nearestPoint = null;
            float minDistance = Mathf.Infinity;
            SnapPoint selectedSnapPoint;

            // Cari snap point terdekat
            foreach (SnapPoint snapPoint in snapPoints)
            {

                float distance = Vector3.Distance(currentPosition, snapPoint.transform.position);
                if (distance < snapRange && distance < minDistance)
                {
                    nearestPoint = snapPoint.transform;
                    selectedSnapPoint = snapPoint;
                    minDistance = distance;

                    if (selectedSnapPoint.isOccupied) return null;
                }


            }


            return nearestPoint;
        }
    }
}
