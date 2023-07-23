using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfPost : MonoBehaviour
{
    private GameObject finishScreen { get; set; }


    void Awake()
    {
        finishScreen = GameObject.Find("Finish Screen");
        finishScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Finish();
        }
    }

    void Finish()
    {
        Time.timeScale = 0;
        finishScreen.SetActive(true);
    }
}
