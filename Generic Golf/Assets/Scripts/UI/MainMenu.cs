using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Play()
    {
        SceneManager.LoadScene(1);
    }

    private void Options()
    {

    }

    private void Credit()
    {

    }

    private void Quit()
    {
        Application.Quit();
    }
}
