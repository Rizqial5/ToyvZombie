using System.Collections;
using System.Collections.Generic;
using TvZ.Character;
using TvZ.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject pauseUI;
    

    public bool isPaused {  get; private set; }

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
    public void RetryButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void PauseButton()
    {
        pauseUI.SetActive(true);
        isPaused = true;
    }

    public void ResumeButton()
    {
        pauseUI.SetActive(false);
        isPaused = false;
    }

    public bool CheckToyInField()
    {
        CharStat[] activePlayer;
        List<CharStat> activePlayerList = new List<CharStat>();

        activePlayer = FindObjectsByType<CharStat>(FindObjectsSortMode.InstanceID);

        for (int i = 0; i < activePlayer.Length; i++)
        {
            if (activePlayer[i].gameObject.tag == "Player")
            {
                activePlayerList.Add(activePlayer[i]);
            }
        }

        if (activePlayerList.Count > 0)
        {
            return true;
        }
        else
        {
            NotificationSystem.Instance.SpawnNotifRight("You have'nt placed toy yet");
            return false;
        }


    }
}
