using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StackManager : Singleton<StackManager> 
{
	List<string> screenStack = new List<string>();

	public GameObject mainMenu;
	public GameObject selectModeScreen;
	public GameObject settingsScreen;
	public GameObject selectLanguageScreen;
	public GameObject pauseSceen;
	public GameObject recueScreen;
	public GameObject gameOverScreen;
	public GameObject shopScreen;
	public GameObject purchaseSuccessScreen;
	public GameObject purchaseFailScreen;
	public GameObject quitConfirmGameScreen;
	private MultiplayerManager multiPlayerManager;
	GameObject gamePlayScreen;
	float scaleValue = 0.343201f;
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		mainMenu.Activate();
		multiPlayerManager = GameObject.FindWithTag("MultiplayerManager").GetComponent<MultiplayerManager>();
	}
	public GameObject SpawnUIScreen(string name)
	{
		GameObject thisScreen = (GameObject)Instantiate (Resources.Load ("Prefabs/UIScreens/" + name));
		thisScreen.name = name;
		thisScreen.transform.SetParent (GameController.Instance.UICanvas.transform);
		thisScreen.transform.localPosition = Vector3.zero;
		thisScreen.transform.localScale = Vector3.one;
		thisScreen.GetComponent<RectTransform> ().sizeDelta = Vector3.zero;
		thisScreen.Activate();
		return thisScreen;
	}

	public void ActivateGamePlay()
	{
		// todo Multiplayer players
		if(gamePlayScreen == null)
		{
            for (int i = 0; i < 4; i++)
            {
				gamePlayScreen = (GameObject)Instantiate(Resources.Load("Prefabs/UIScreens/GamePlay"));

				gamePlayScreen.name = "GamePlay" + i.ToString();

				gamePlayScreen.transform.SetParent(GameObject.FindWithTag("MultiplayerManager").transform);//MultiplayerManager.Transfrom
																										   // that will change with
				multiPlayerManager.AddPlayer(gamePlayScreen.GetComponent<PlayerController>());
				switch (i)
                {
					case 0:
						gamePlayScreen.transform.localPosition = new Vector3(0, -257f, 0);
						gamePlayScreen.GetComponent<PlayerController>().PlayerType = (PlayerType)i;
						gamePlayScreen.transform.localScale = new Vector3(scaleValue, scaleValue, 0f);
						
						
						break;

					case 1:
						gamePlayScreen.transform.localScale = new Vector3(scaleValue, scaleValue, 0f);
						gamePlayScreen.transform.localPosition = new Vector3(700, 0f, 0);
						gamePlayScreen.transform.localRotation = Quaternion.Euler(0, 0, 90f);
						gamePlayScreen.GetComponent<PlayerController>().PlayerType = (PlayerType)i;
						
						break;

					case 2:
						gamePlayScreen.transform.localScale = new Vector3(scaleValue, scaleValue, 0f);
						gamePlayScreen.transform.localPosition = new Vector3(0, 257, 0);
						gamePlayScreen.transform.localRotation = Quaternion.Euler(0, 0, 180f);
						gamePlayScreen.GetComponent<PlayerController>().PlayerType = (PlayerType)i;
						
						break;

					case 3:
						gamePlayScreen.transform.localScale = new Vector3(scaleValue, scaleValue, 0f);
						gamePlayScreen.transform.localPosition = new Vector3(-700, 0, 0);
						gamePlayScreen.transform.localRotation = Quaternion.Euler(0, 0, 270f);
						gamePlayScreen.GetComponent<PlayerController>().PlayerType = (PlayerType)i;
						
						break;
					default:
                        break;
                }
				//   gamePlayScreen.transform.localPosition = Vector3.zero;
				
			
				//gamePlayScreen.GetComponent<RectTransform>().sizeDelta = Vector3.zero;
				gamePlayScreen.Activate();
			}
			
		}
	}

	public void DeactivateGamePlay()
	{
		if(gamePlayScreen != null)
		{
			Destroy(gamePlayScreen);
		}
	}

	public void Push(string screenName)
	{
		if(!screenStack.Contains(screenName))
		{
			screenStack.Add(screenName);
		}
	}

	public string Peek()
	{
		if(screenStack.Count > 0)
		{
			return screenStack[screenStack.Count-1];
		}
		return "";
	}

	public void Pop(string screenName)
	{
		if(screenStack.Contains(screenName))
		{
			screenStack.Remove(screenName);
		}
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(InputManager.Instance.canInput())
			{
				if(screenStack.Count > 0) {
					ProcessBackButton(Peek());
				}
			}
		}
	}

	void ProcessBackButton(string currentScreen)
	{
		Debug.Log(currentScreen);
		switch(currentScreen)
		{
			case "MainScreen":
			quitConfirmGameScreen.Activate();
			break;

			case "SelectMode":
			selectModeScreen.Deactivate();
			break;

			case "Quit-Confirm-Game":
			quitConfirmGameScreen.Deactivate();
			break;

			case "Shop":
			//shopScreen.Deactivate();
			break;

			case "Settings":
			settingsScreen.Deactivate();
			break;

			case "SelectLanguage":
			selectLanguageScreen.Deactivate();
			break;

			case "GamePlay":
			pauseSceen.Activate();
			break;

			case "Paused":
			pauseSceen.Deactivate();
			break;

			case "GameOver":
			gameOverScreen.Deactivate();
			mainMenu.Activate();
			break;

			case "PurchaseFail":
			purchaseFailScreen.Deactivate();
			break;

			case "PurchaseSuccess":
			purchaseSuccessScreen.Deactivate();
			shopScreen.Deactivate();
			break;
		}
	}
}
