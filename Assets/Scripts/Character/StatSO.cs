using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TvZ.Character
{
    [CreateAssetMenu(fileName = "StatSO", menuName = "Create Asset/ Character/ Create StatSO")]
    public class StatSO : ScriptableObject
    {

        [SerializeField] GameObject charPrefab;

        [SerializeField] StatCategory[] statCategories;

        private Dictionary<StatEnum, float> statTableLookup;

        private void BuildDictionary()
        {
            if (statTableLookup != null) return;

            statTableLookup = new Dictionary<StatEnum, float>();

            foreach ( StatCategory item  in statCategories)
            {
                statTableLookup[item.statEnum] = item.value;
            }
        }

        public float GetCharStat(StatEnum statEnum)
        {
            BuildDictionary();

            return statTableLookup[statEnum];
        }


        public GameObject GetCharPrefab() { return charPrefab; }


        
    }


    [System.Serializable]
    public class StatCategory
    {
        public StatEnum statEnum;
        public float value;
    }
}
