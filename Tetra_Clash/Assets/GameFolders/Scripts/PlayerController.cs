using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum PlayerType
{
    Player1,
    Player2,
    Player3,
    Player4
}
public enum GameState
{
    Play,
    Lose,
    Win
}
public class PlayerController : MonoBehaviour
{
    public GameObject playerLosePanel;
    public GamePlay GamePlay { get; protected set; }
    public GamePlayUI GamePlayUI { get;protected  set; }
    public GameBoardGenerator GameBoardGenerator { get; protected set; }
    public GameProgressManager GameProgressManager { get; protected set; }
    public ScoreManager ScoreManager { get; protected set; }
    public BlockShapeSpawner BlockShapeSpawner { get; protected set; }
    public Sprite EmptyBlockSprite;
    public GameObject bgImg;
    public GameObject crownImg;
    public TouchArea TouchArea { get; set; }
    public GameState GameState= GameState.Play;
    public PlayerType PlayerType { get; set; }
    public AudioClip playerLose;
    private void Awake()
    {
        TouchArea = GetComponent<TouchArea>();
        GamePlayUI = GetComponent<GamePlayUI>();
        GamePlay = GetComponent<GamePlay>();
        GameBoardGenerator = GetComponent<GameBoardGenerator>();
        GameProgressManager = GetComponent<GameProgressManager>();
        ScoreManager = GetComponent<ScoreManager>();
        BlockShapeSpawner = GetComponentInChildren<BlockShapeSpawner>(); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetLoser();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetWinner();
        }
    }
    public void SetWinner()
    {
        crownImg.SetActive(true);
        bgImg.GetComponent<Image>().color = Color.green;  
    }
    public void SetLoser()
    {
        crownImg.SetActive(false);
        bgImg.GetComponent<Image>().color = Color.white;
    }
    public void PlayerLose()
    {
        GamePlayUI.GameOver();
        AudioManager.Instance.PlaySound(playerLose);
        GameState = GameState.Lose;
        playerLosePanel.SetActive(true);
        BlockShapeSpawner.gameObject.SetActive(false);
    }
    public void ClosePlayerLosePanel()
    {
        playerLosePanel.SetActive(false);
    }
}
