using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI txtScore;
	[SerializeField] private GameObject scoreAnimator;
	[SerializeField] private Text txtAnimatedText;
	[SerializeField] private Text txtBestScore;

    MultiplayerManager multiPlayer;
	PlayerController player;
	[HideInInspector] public int Score = 0;
	int bestScore = 0;

	void Start()
	{
		multiPlayer = GetComponentInParent<MultiplayerManager>();
		player = GetComponentInParent<PlayerController>();
		txtScore.text = Score.ToString ();	
		int bestScore = PlayerPrefs.GetInt ("BestScore_" + GameController.gameMode.ToString (), Score);
		txtBestScore.text = bestScore.ToString();

	}

	public void AddScore(int scoreToAdd, bool doAnimate = true)
	{
		int oldScore = Score;
		Score += scoreToAdd;

		StartCoroutine (SetScore(oldScore, Score));

		if (doAnimate)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePos.z = 0;
			scoreAnimator.transform.position = Vector3.one * 1000;
			txtAnimatedText.text = "+" + scoreToAdd.ToString ();
			scoreAnimator.SetActive (true);
		}
	}

	public int GetScore()
	{
		return Score;
	}

	IEnumerator SetScore(int lastScore, int currentScore)
	{
		int IterationSize = (currentScore - lastScore) / 10;

		for (int index = 1; index < 10; index++) {
			lastScore += IterationSize;
			txtScore.text =  string.Format("{0:#,#.}", lastScore);
			yield return new WaitForEndOfFrame ();
		}
		txtScore.text =  string.Format("{0:#,#.}", currentScore);
		multiPlayer.ChangeWinner();
		yield return new WaitForSeconds (1F);
		scoreAnimator.SetActive (false);
	}
}
