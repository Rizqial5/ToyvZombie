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
    [SerializeField] Image heroImage;
    

    

    private StatSO statSO;

    private List<GameObject> descObjectSpawned = new List<GameObject>();

    public void SetToyCard(StatSO statSO)
    {
        //modif
        this.statSO = statSO;
    }

    public void SetToyCardDesc(float resourcesAmount, GameObject descObject, Color bgColor, Sprite toyImage, Sprite resourcesImage)
    {
        if (descObjectSpawned.Count > 0) return;

        GameObject spawnedDesc = Instantiate(descObject, descLayout);

        cardImage.color = bgColor;
        heroImage.sprite = toyImage;

        spawnedDesc.GetComponentInChildren<Image>().sprite = resourcesImage;
        spawnedDesc.GetComponentInChildren<TextMeshProUGUI>().text = resourcesAmount.ToString();

        descObjectSpawned.Add(spawnedDesc);
    }

    
}
