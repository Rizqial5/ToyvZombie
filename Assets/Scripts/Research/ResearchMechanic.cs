using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TvZ.Character;

namespace TvZ.Research
{
    public class ResearchMechanic : MonoBehaviour
    {
        [SerializeField] ListResearchSO listResearch;

        [SerializeField] GameObject cardResearchPrefab;
        [SerializeField] Transform cardSpawnParent;

        private StatSO[] listCharSpawned;

        public void ShowCharList()
        {
            if (listCharSpawned != null) return; 

            listCharSpawned = listResearch.GetListCharResearch();

            for (int i = 0; i < listCharSpawned.Length; i++)
            {
                Instantiate(cardResearchPrefab, cardSpawnParent);
            }
        }
    }
}
