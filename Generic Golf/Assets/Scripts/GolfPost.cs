using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfPost : MonoBehaviour
{
    


    void Awake()
    {
        
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
        Debug.Log("Finish!");
    }
}
