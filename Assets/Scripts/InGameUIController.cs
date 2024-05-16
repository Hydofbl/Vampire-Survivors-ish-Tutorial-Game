using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GetComponent<GameManager>();
    }

    public void ResumeButton()
    {
        _gameManager.ResumeGame();
    }

    public void QuitButton()
    {
        SceneController.Instance.ChangeScene("MainMenu");
    }
}
