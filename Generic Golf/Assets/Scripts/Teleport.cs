using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    /*
    private Transform destination;

    public bool isbox1;
    public float distance = 0.2f;
*/

    // Start is called before the first frame update
    void Start()
    {
        /*
     if (isbox1 == false)
     {
        destination = GameObject.FindGameObjectWithTag("brown box 1").GetComponent<Transform>;
     }  
     else
     {
        destination = GameObject.FindGameObjectWithTag("brown box 2").GetComponent<Transform>;
     }
        */
    }


    public Transform destinationPortal;
    private Rigidbody2D playerBody;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.transform);
        }
    }

    private void TeleportPlayer(Transform player)
    {
        // Disable physics interactions during teleportation
        playerBody = player.GetComponent<Rigidbody2D>();
        playerBody.simulated = false;


        Vector2 offset = (Vector2)player.position - (Vector2)transform.position;
        player.position = (Vector2)destinationPortal.position + offset;

        playerBody.simulated = true;
    }

/*
    void OnTriggerEnter2D(Collider2D other){

        if (Vector2.Distance(transform.position, other.transform.position) > distance)
        {
            other.transform.position = new Vector2 (destination.position.x, destination.position.y);
        }
    }
*/

}
