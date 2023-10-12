using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneController.Instance.ChangeScene("Game");
    }

    public void BackToMenu()
    {
        SceneController.Instance.ChangeScene("MainMenu");
    }
}
