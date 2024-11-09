using System.Collections;
using System.Collections.Generic;
using TvZ.Character;
using UnityEngine;

namespace TvZ.Enemy
{
    [CreateAssetMenu(fileName = "StageProgressionSO", menuName = "Create Asset/ Enemy/ Create Stage Progression")]
    public class StageProgressionSO : ScriptableObject
    {
        [SerializeField] StageLevel[] stageLevels;

        private Dictionary<int, List<StatSO>> enemtTableDict;

        public void BUildDictionary()
        {
            if (enemtTableDict != null) return;

            enemtTableDict = new Dictionary<int, List<StatSO>>();

            foreach (StageLevel item in stageLevels)
            {
                enemtTableDict[item.dayLevel] = item.enemySpawnedList;
            }
        }

        public List<StatSO> GetEnemyLevelList(int dayLevel)
        {
            BUildDictionary();

            

           
            return enemtTableDict[dayLevel];
        }

        public int GetEnemyLevelMax()
        {
            BUildDictionary();
            return enemtTableDict.Count;
        }


    }

    [System.Serializable]
    public class StageLevel
    {
        public int dayLevel;
        public List<StatSO> enemySpawnedList;
    }
}
