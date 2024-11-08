using System.Collections;
using System.Collections.Generic;
using TMPro;
using TvZ.Character;
using UnityEngine;
using UnityEngine.UI;

public class ToyCardUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] Transform descLayout;
    [SerializeField] Image cardImage;

    

    private StatSO statSO;

    private List<GameObject> descObjectSpawned = new List<GameObject>();

    public void SetToyCard(StatSO statSO)
    {
        //modif
        this.statSO = statSO;
    }

    public void SetToyCardDesc(float resourcesAmount, GameObject descObject)
    {
        if (descObjectSpawned.Count > 0) return;

        GameObject spawnedDesc = Instantiate(descObject, descLayout);

        //spawnedDesc.GetComponentInChildren<Image>().sprite = resourcesImage;
        spawnedDesc.GetComponentInChildren<TextMeshProUGUI>().text = resourcesAmount.ToString();

        descObjectSpawned.Add(spawnedDesc);
    }

    
}
