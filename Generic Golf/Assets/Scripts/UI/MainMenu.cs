using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        //SceneManager.LoadScene("Prototype");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options()
    {

    }

    public void Credit()
    {

    }

    public void Quit()
    {
        Debug.Log ("QUIT");
        Application.Quit();
    }
}
