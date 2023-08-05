using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    Resolution[]  resolutions;
    public TMP_Dropdown resolutionDropdown;


    void Start()
    {
       resolutions = Screen.resolutions;
       resolutionDropdown.ClearOptions(); 
       
       List<string>  options = new List<string>();
       int currentResolutionindex = 0;
       for (int i = 0; i < resolutions.Length; i++)
       {
        string option = resolutions[i].width + " x " + resolutions[i].height;
        options.Add(option);

        if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height )
        {
         currentResolutionindex = i;   
        }
       }

       resolutionDropdown.AddOptions(options);
       resolutionDropdown.value = currentResolutionindex;
       resolutionDropdown.RefreshShownValue();

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }


    public void Play()
    {
        //SceneManager.LoadScene("Prototype");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options()
    {
        


    }

/*
    public void SetGraphics(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

    }
*/

public void SetGraphics(int qualityIndex)
{
    Debug.Log("Setting graphics quality to index: " + qualityIndex);
    QualitySettings.SetQualityLevel(qualityIndex);
}




    public AudioMixer audioMixer;

    
    public void SetVolume(float volume){
            
             Debug.Log(volume);
            audioMixer.SetFloat("volume", volume);

        }

    public void SetFullscreen(bool isFullscreen)
    {

        Screen.fullScreen = isFullscreen;
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
