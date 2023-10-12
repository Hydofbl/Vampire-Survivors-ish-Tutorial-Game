using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public void GoToCharacterSelection()
    {
        SceneController.Instance.ChangeScene("CharacterSelection");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
