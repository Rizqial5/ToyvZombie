using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TvZ.Character;

namespace TvZ.Research
{
    [CreateAssetMenu(fileName = "ListResearchSO", menuName = "Create Asset/ Research/ Create List Research")]
    public class ListResearchSO : ScriptableObject
    {
        [SerializeField] StatSO[] listCharResearch;

        public StatSO[] GetListCharResearch()
        { return listCharResearch; }    
    }
}
