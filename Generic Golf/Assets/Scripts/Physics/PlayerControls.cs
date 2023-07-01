using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private Vector2 startPoint { get; set; }
    private Vector2 endPoint { get; set; }
    private Rigidbody2D playerBody { get; set; }
    private LineRenderer line { get; set; }

    [SerializeField] private int power;

    private void Awake()
    {

        playerBody = GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();

        line.positionCount = 2;
        line.startWidth = 0.08f;
        line.endWidth = 0.08f;
    }


    void Update()
    {
        line.SetPosition(0, gameObject.transform.position);


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); ;
            
        }

        Throw();

    }

    private void Throw()
    {
        float xDis = endPoint.x - startPoint.x;
        float yDis = endPoint.y - startPoint.y;

        float angle = Mathf.Atan(yDis / xDis);
        float totalVel = (xDis / Mathf.Cos(angle)) * power;

        line.SetPosition(1, new Vector2(-1 * xDis, -1 * yDis));


        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            try
            {
                playerBody.velocity = new Vector2(-1 * totalVel * Mathf.Cos(angle), -1 * totalVel * Mathf.Sin(angle));

            }


            catch
            {
                playerBody.velocity = new Vector2(0, -1 * yDis * power);
            }

        }



    }



    /* public void Throw(InputAction.CallbackContext context)
       {
           Vector3 startPoint = new Vector2();
           Vector3 endPoint = new Vector2();

           float angle;


           if( context.started)
           {
               startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

               line.SetPosition(0, gameObject.transform.position);
           }

           else if (context.canceled)
           {
               endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

               line.SetPosition(1, gameObject.transform.position + (endPoint - startPoint));

               playerBody.AddForce(power * (endPoint - startPoint), ForceMode2D.Impulse);
           }

       } */

}
