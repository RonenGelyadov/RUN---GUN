using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void MenuButtons(string action)
    {
        if (action == "Play")
        {
            SceneManager.LoadScene(1);
        }

        if (action == "Exit")
        {
            Application.Quit();
        }

        if (action == "Menu")
        {
            SceneManager.LoadScene(0);
        }

        if (action == "1")
        {
            SceneManager.LoadScene(2);
        }

        if (action == "2")
        {
            SceneManager.LoadScene(3);
        }
    }
}
