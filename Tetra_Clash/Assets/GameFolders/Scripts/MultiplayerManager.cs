using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiplayerManager : MonoBehaviour
{

    public GameObject GameOverScreen;
    List<PlayerController> Players = new List<PlayerController>();
    public TextMeshProUGUI winnerNameText;
    public Button restartButton;
    float currentTime = 300f;
    public float CurrentTime => currentTime;
    public AudioClip gameOver;
    public GameObject pausePanel;
    private bool isPause = false;
    public GameObject pauseButton;
    public GameObject winnerImage;
    public GameObject drawImage;
    public GameObject winnerNamePanel;

    private void Awake()
    {
        restartButton.onClick.AddListener(RestartButton);
    }

    private void Start()
    {
        currentTime = 300f;
    }

    private void Update()
    {
        if (!isPause)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                TimeOut();
            }
        }
    }

    public void LosePanelButton(bool pause)
    {
        foreach (var item in Players)
        {
            item.BlockShapeSpawner.gameObject.SetActive(!pause);
        }
        isPause = pause;
    }

    public void ExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }

    public void AddPlayer(PlayerController player)
    {
        Players.Add(player);
    }
    
    public void ChangeWinner()
    {
        int maxPoint = 0;
        int winnerIndex = 0;
        for (int i = 0; i < Players.Count; i++)
        {
            if (maxPoint < Players[i].ScoreManager.GetScore())
            {
                maxPoint = Players[i].ScoreManager.GetScore();
                winnerIndex = i;

            }
        }
        for (int i = 0; i < Players.Count; i++)
        {
            if (i == winnerIndex)
            {
                Players[i].SetWinner();
            }
            else
            {
                Players[i].SetLoser();
            }
        }
    }

    public void PauseButton()
    {
        foreach (var item in Players)
        {
            item.BlockShapeSpawner.gameObject.SetActive(false);
        }
        pausePanel.SetActive(true);
        isPause = true;
    }

    public void ContinueButton()
    {  
        foreach (var item in Players)
        {
            if (item.GameState != GameState.Lose)
            {
                item.BlockShapeSpawner.gameObject.SetActive(true);
            }  
        }
        isPause = false;
        pausePanel.SetActive(false);
    }

    public void TekrarOynaButton()
    {
        pausePanel.SetActive(false);
        TimeOut();
    }

    public void TimeOut()
    {
        pauseButton.SetActive(false);
        currentTime = 0;
        foreach (var item in Players)
        {
            if (item.GameState != GameState.Lose)
            {
                OnPlayerLose(item);
            }
        }
    }

    public void OnPlayerLose(PlayerController player)
    {
         player.PlayerLose();

        bool everyPlayerFailed = true;

        foreach (var item in Players)
        {

            if (item.GameState != GameState.Lose)
            {
                everyPlayerFailed = false;
            }
        }

        if (everyPlayerFailed)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        AudioManager.Instance.PlaySound(gameOver);
        int maxScore = 0;
        int equalCounter = 0;
        foreach (var item in Players)
        {
            if (item.ScoreManager.Score > maxScore)
            {
                maxScore = item.ScoreManager.Score;
            } 
        }
        foreach (var item in Players)
        {
            if (item.ScoreManager.Score == maxScore)
            {
                equalCounter++;
                item.GameState = GameState.Win;
                winnerNameText.text = "Oyuncu " + ((int)item.PlayerType + 1).ToString();  
            }
            if (equalCounter > 1)
            {
                winnerImage.SetActive(false);
                winnerNamePanel.SetActive(false);
                drawImage.SetActive(true);
            }
            else
            {
                winnerImage.SetActive(true);
            }
            item.ClosePlayerLosePanel();
        }

        GameOverScreen.SetActive(true);
    }
}
