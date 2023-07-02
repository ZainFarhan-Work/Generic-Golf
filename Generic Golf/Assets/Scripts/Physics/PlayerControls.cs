using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float frictionCoeff;
    [SerializeField] private float stopVelocity;
    private LineRenderer line { get; set; }
    private Rigidbody2D playerBody { get; set; }
    private Vector3? mousePos { get; set; }
    private bool isIdle { get; set; }
    private bool isAiming { get; set; }

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();

        line = GetComponent<LineRenderer>();
        line.enabled = false;

        isAiming = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerBody.velocity.magnitude < stopVelocity)
        {
            Stop();
        }

        ProcessAiming();

        if (Input.GetMouseButtonUp(0))
        {
            if (isAiming)
            {
                Shoot();
            }

        }


        /*
        if (!WorldPoint.HasValue)
        {
            return;
        }
        */


        
        
    }

    private void FixedUpdate()
    {
        if (playerBody.velocity.magnitude > stopVelocity)
        {
            Friction(playerBody.velocity);
        }    
    }


    private void OnMouseDown()
    {
        if (isIdle)
        {
            isAiming = true;
        }
    }

    // Custom Made Functions Below

    private void DrawLine(Vector3 worldPoint)
    {
        worldPoint.z = 0;


        Vector3[] positions = { gameObject.transform.position, worldPoint };

        line.enabled = true;
        line.SetPositions(positions);
        
        
    }

    private void ProcessAiming()
    {
        if (!isAiming || !isIdle)
        {
            return;

        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        DrawLine(mousePos.Value);

    }

    private void Friction(Vector2 velocity)
    {
        velocity -= velocity * frictionCoeff;
        
        playerBody.velocity = velocity;

    }

    private void Stop()
    {
        playerBody.velocity = Vector2.zero;
        playerBody.angularVelocity = 0;

        isIdle = true;
    }

    private void Shoot()
    {
        isAiming = false;
        line.enabled = false;

        // Task: Give the player body a Velocity/Force that is propotional to the linne drawn and oppsite in direction
        

    }


    /*
    private bool CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit2D hit;

        if (Physics2D.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, float.PositiveInfinity))
        {
            hit = Physics2D.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, float.PositiveInfinity);

            return true;
        }

        else
        {
            return false;
        }

        
    }
    */
    
}
