using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TvZ.Core
{
    public class GridLayout : MonoBehaviour
    {
        [SerializeField] GameObject gridPrefab;
        [SerializeField] Transform gridSpawnParent;
        [SerializeField] GameObject roadZone;     
        [SerializeField] int rows = 5;            
        [SerializeField] int columns = 5;         

        private Vector2 roadZoneSize;   


        private void Awake()
        {
            roadZone = gameObject;
        }
        void Start()
        {
            // Get the size of the roadZone based on its SpriteRenderer or Collider
            if (roadZone != null)
            {
                // If roadZone has a SpriteRenderer
                SpriteRenderer spriteRenderer = roadZone.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    roadZoneSize = spriteRenderer.bounds.size;
                }
                // If roadZone has a BoxCollider2D
                else if (roadZone.GetComponent<BoxCollider2D>() != null)
                {
                    roadZoneSize = roadZone.GetComponent<BoxCollider2D>().size;
                }
                else
                {
                    Debug.LogWarning("roadZone must have a SpriteRenderer or BoxCollider2D to get its size.");
                }
            }

            GenerateGrid();
        }

        void GenerateGrid()
        {
            // Calculate the size of each grid cell based on roadZone's size and the number of rows and columns
            Vector2 cellSize = new Vector2(roadZoneSize.x / columns, roadZoneSize.y / rows);

            // Loop through rows and columns to instantiate objects in a grid
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    GameObject spawnedGrid;
                    // Calculate the position for each object using the cell size
                    Vector2 position = new Vector2(
                        roadZone.transform.position.x - (roadZoneSize.x / 2) + (cellSize.x * (column + 0.5f)),
                        roadZone.transform.position.y - (roadZoneSize.y / 2) + (cellSize.y * (row + 0.5f))
                    );

                    // Instantiate the prefab at the calculated position
                    spawnedGrid = Instantiate(gridPrefab, position, Quaternion.identity);

                    spawnedGrid.transform.SetParent(gridSpawnParent);
                }
            }
        }
    }
}
