using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Define the different states of the game
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
        LevelUp
    }

    // Store the current state of the game
    public GameState CurrentState;
    // Store the previous state of the game
    public GameState PreviousState;

    [Header("Screens")]
    public GameObject PauseScreen;
    public GameObject ResultScreen;
    public GameObject LevelUpScreen;

    [Header("Current Stat Displays")]
    public TMP_Text CurrentHealthDisplay;
    public TMP_Text CurrentRecoveryDisplay;
    public TMP_Text CurrentMoveSpeedDisplay;
    public TMP_Text CurrentMightDisplay;
    public TMP_Text CurrentProjectileSpeedDisplay;
    public TMP_Text CurrentMagnetDisplay;

    [Header("Results Screen Displays")]
    public Image ChosenCharacterImage;
    public TMP_Text ChosenCharacterName;
    public TMP_Text LevelReachedDisplay;
    public TMP_Text TimeSurvivedDisplay;
    public List<Image> ChosenWeaponsUI = new List<Image>(6);
    public List<Image> chosenPassiveItemsUI = new List<Image>(6);

    [Header("StopWatch")]
    public float TimeLimit;
    public TMP_Text StopwatchDisplay;
    private float _stopwatchTime;

    // Flag to check if the game is over
    public bool IsGameOver = false;

    // Flag to check if the player is choosing their upgrades
    public bool ChoosingUpgrade;

    // Player Object
    public GameObject PlayerObject;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            Debug.LogWarning("EXTRA " + this + " DELETED.");
        }
        else
        {
            Instance = this;
        }

        DisaleScreens();
    }

    private void Update()
    {
        switch(CurrentState)
        {
            case GameState.Gameplay:
                // Gameplay State Codes
                CheckForPauseAndResume();
                UpdateStopwatch();

                break;

            case GameState.Paused:
                // Pause State Codes
                CheckForPauseAndResume();

                break;

            case GameState.GameOver:
                // GameOver State Codes
                if(!IsGameOver)
                {
                    IsGameOver = true;
                    // Stop the game
                    Time.timeScale = 0f;
                    Debug.Log("Game is Over");
                    DisplayResults();
                }
                break;

            case GameState.LevelUp:
                if(!ChoosingUpgrade)
                {
                    ChoosingUpgrade = true;
                    // Stop the game
                    Time.timeScale = 0f;
                    Debug.Log("Upgrades shown");
                    LevelUpScreen.SetActive(true);
                }

                break;
            default:
                Debug.LogWarning("STATE DOES NOT EXIST.");
                break;
        }
    }

    // Define the method to change the state of the game
    public void ChangeState(GameState state)
    {
        CurrentState = state;
    }

    public void PauseGame()
    {
        if(CurrentState != GameState.Paused)
        {
            PreviousState = CurrentState;
            ChangeState(GameState.Paused);
            // Stop the game
            Time.timeScale = 0f;
            PauseScreen.SetActive(true);
            Debug.Log("Game is paused");
        }
    }

    public void ResumeGame()
    {
        if(CurrentState == GameState.Paused)
        {
            ChangeState(PreviousState);
            Time.timeScale = 1f;
            PauseScreen.SetActive(false);
            Debug.Log("Game is resumed");   
        }
    }

    void CheckForPauseAndResume()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(CurrentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void DisaleScreens()
    {
        PauseScreen.SetActive(false);
        ResultScreen.SetActive(false);
        LevelUpScreen.SetActive(false);
    }

    public void GameOver()
    {
        TimeSurvivedDisplay.text = StopwatchDisplay.text;
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        ResultScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData)
    {
        ChosenCharacterImage.sprite = chosenCharacterData.Icon;
        ChosenCharacterName.text = chosenCharacterData.Name;
    }

    public void AssignLevelReachedUI(int levelReachedData)
    {
        LevelReachedDisplay.text = levelReachedData.ToString();
    }

    public void AssignChosenWeaponsAndPassiveItems(List<Image> chosenWeaponsData, List<Image> chosenPassiveItemsData)
    {
        if(chosenWeaponsData.Count != ChosenWeaponsUI.Count || chosenPassiveItemsData.Count != chosenPassiveItemsUI.Count)
        {
            Debug.Log("Chosen weapons and passive items data lists have different lengths");
            return;
        }

        for (int i = 0; i < ChosenWeaponsUI.Count; i++)
        {
            if (chosenWeaponsData[i].sprite)
            {
                ChosenWeaponsUI[i].enabled = true;
                ChosenWeaponsUI[i].sprite = chosenWeaponsData[i].sprite;
            }
            else
            {
                ChosenWeaponsUI[i].enabled = false;
            }
        }

        for (int i = 0; i < chosenPassiveItemsUI.Count; i++)
        {
            if (chosenPassiveItemsData[i].sprite)
            {
                chosenPassiveItemsUI[i].enabled = true;
                chosenPassiveItemsUI[i].sprite = chosenPassiveItemsData[i].sprite;
            }
            else
            {
                chosenPassiveItemsUI[i].enabled = false;
            }
        }
    }

    void UpdateStopwatch()
    {
        _stopwatchTime += Time.deltaTime;

        UpdateStopwatchDisplay();

        if(_stopwatchTime >= TimeLimit)
        {
            GameOver();
        }
    }

    void UpdateStopwatchDisplay()
    {
        // Calculate the number of minutes and seconds that have elapsed
        int minutes = Mathf.FloorToInt(_stopwatchTime / 60);
        int seconds = Mathf.FloorToInt(_stopwatchTime % 60);

        StopwatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);
        PlayerObject.SendMessage("RemoveAndApplyUpgrades");
    }

    public void EndLevelUp()
    {
        ChoosingUpgrade = false;
        Time.timeScale = 1f;
        LevelUpScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
    }
}
