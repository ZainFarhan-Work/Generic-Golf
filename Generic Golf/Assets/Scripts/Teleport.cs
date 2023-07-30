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
    //void Start()
    //{
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
    //}

/*
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
*/

    public Transform destinationPortal;
    private Rigidbody2D playerBody;
    private bool isTeleporting = false; // Flag to prevent consecutive teleportation
    private float teleportCooldown = 0.5f; // Cooldown time before the player can teleport again
    private int playerLayer; // The layer of the player
    private int ignoreTeleportLayer; // The layer index for the "IgnoreTeleport" layer

    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.NameToLayer("Player");
        ignoreTeleportLayer = LayerMask.NameToLayer("IgnoreTeleport");
    }

    private void Update()
    {
        // Your frame rate independent movement code here
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTeleporting && other.CompareTag("Player"))
        {
            TeleportPlayer(other.transform);
        }
    }

    private void TeleportPlayer(Transform player)
    {
        // Disable physics interactions during teleportation
        playerBody.simulated = false;
        isTeleporting = true;

        // Change the player's layer to "IgnoreTeleport" to prevent immediate re-entry
        player.gameObject.layer = ignoreTeleportLayer;

        Vector2 offset = (Vector2)player.position - (Vector2)transform.position;
        player.position = (Vector2)destinationPortal.position + offset;

        // Re-enable physics and reset the player's layer after a short delay
        StartCoroutine(EnablePhysicsWithCooldown(player.gameObject));
    }

    private IEnumerator EnablePhysicsWithCooldown(GameObject playerObject)
    {
        // Wait for a short delay before enabling physics again
        yield return new WaitForSeconds(teleportCooldown);

        // Reset the player's layer to "Player"
        playerObject.layer = playerLayer;
        playerBody.simulated = true;
        isTeleporting = false;
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
