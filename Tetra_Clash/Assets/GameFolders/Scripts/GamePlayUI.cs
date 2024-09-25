using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class GamePlayUI : MonoBehaviour
{
	[SerializeField] private GameObject alertWindow;
	Text txtAlertText;
	[SerializeField] TextMeshProUGUI scoreText;
	[SerializeField] TextMeshProUGUI nameText;
	public GameOverReason currentGameOverReson;
	PlayerController player;
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		player = GetComponent<PlayerController>();
		nameText.text = "Oyuncu " + ((int)player.PlayerType + 1).ToString();
		txtAlertText = alertWindow.transform.GetChild (0).GetComponentInChildren<Text> ();
	}

	public void OnPauseButtonPressed(){
		if (InputManager.Instance.canInput ()) {
			AudioManager.Instance.PlayButtonClickSound ();
			StackManager.Instance.pauseSceen.SetActive(true);
		}
	}
	public void GameOver()
    {
		scoreText.text = player.ScoreManager.GetScore().ToString();
    }

	public void ShowAlert()
	{
		alertWindow.SetActive (true);
		if (!IsInvoking ("CloseAlert")) {
			Invoke ("CloseAlert", 2F);
		}
	}

	/// <summary>
	/// Closes the alert.
	/// </summary>
	void CloseAlert()
	{
		alertWindow.SetActive (false);
	}

	/// <summary>
	/// Shows the rescue.
	/// </summary>
	/// <param name="reason">Reason.</param>
	public void ShowRescue(GameOverReason reason)
	{
		currentGameOverReson = reason;
		StartCoroutine (ShowRescueScreen(reason));
	}

	/// <summary>
	/// Shows the rescue screen.
	/// </summary>
	/// <returns>The rescue screen.</returns>
	/// <param name="reason">Reason.</param>
	IEnumerator ShowRescueScreen(GameOverReason reason)
	{		
		#region time mode
		if(GameController.gameMode == GameMode.TIMED || GameController.gameMode == GameMode.CHALLENGE){
			player.GamePlay.timeSlider.PauseTimer();
		}
		#endregion

		switch (reason) {
		case GameOverReason.OUT_OF_MOVES:
			txtAlertText.SetLocalizedTextForTag ("txt-out-moves");
			break;
		case GameOverReason.BOMB_COUNTER_ZERO:
			txtAlertText.SetLocalizedTextForTag ("txt-bomb-blast");
			break;
		case GameOverReason.TIME_OVER:
			txtAlertText.SetLocalizedTextForTag ("txt-time-over");
			break;
		}

		yield return new WaitForSeconds (0.5F);
		alertWindow.SetActive (true);
		yield return new WaitForSeconds (1.5F);
		alertWindow.SetActive (false);
		StackManager.Instance.recueScreen.Activate();
	}
}

/// <summary>
/// Game over reason.
/// </summary>
public enum GameOverReason
{
	OUT_OF_MOVES = 0,
	BOMB_COUNTER_ZERO = 1,
	TIME_OVER
}