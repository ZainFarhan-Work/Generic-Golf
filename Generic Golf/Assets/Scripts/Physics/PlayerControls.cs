using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    
    [SerializeField] private int shotPower;
    [SerializeField] private float maxSpeed;
    [Space]
    [SerializeField] private float frictionCoeff;
    [SerializeField] private float stopVelocity;
    [NonSerialized] public Vector3 origin;
    
    private GameObject pauseMenu { get; set; }
    private GameObject gameOver { get; set; }
    private LineRenderer line { get; set; }
    private Rigidbody2D playerBody { get; set; }
    private Vector3 mousePos { get; set; }
    private Vector3 direction { get; set; }
    private bool isIdle { get; set; }
    private bool isAiming { get; set; }

    // Score Variables

    [Space]
    [Header("Score")]
    [SerializeField] private int maxShots;
    private GameObject scoreBoard { get; set; }
    private TextMeshProUGUI strokes { get; set; }
    private TextMeshProUGUI hits { get; set; }
    private TextMeshProUGUI scoreText { get; set; }
    private TextMeshProUGUI grade { get; set; }
    private float remainingShots { get; set; }
    private int strokesHit { get; set; }
    private float score { get; set; }
    private float shotPercentage { get; set; }
    

    private void Awake()
    {
        // Starting Score

        scoreBoard = GameObject.FindGameObjectWithTag("ScoreBoard");

        hits = GameObject.Find("Hits").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        grade = GameObject.Find("Grade").GetComponent<TextMeshProUGUI>();

        scoreBoard.SetActive(false);


        remainingShots = maxShots;
        strokes = GameObject.Find("Stroke Count").GetComponent<TextMeshProUGUI>();
        strokes.text = "Strokes:" + remainingShots;

        // End of Score

        gameOver = GameObject.FindGameObjectWithTag("Game Over");
        gameOver.SetActive(false);
        pauseMenu = GameObject.FindGameObjectWithTag("Pause Menu");
        pauseMenu.SetActive(false);

        playerBody = GetComponent<Rigidbody2D>();

        line = GetComponent<LineRenderer>();
        line.startWidth = 0.08f;
        line.endWidth = 0.08f;
        line.enabled = false;

        isAiming = false;

    }

    private void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        ProcessAiming();

        if (playerBody.velocity.magnitude < stopVelocity)
        {
            Stop();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isAiming)
            {
                Shoot();
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            Score();
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreBoard.SetActive(false);
        }


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

    private void ProcessAiming()
    {
        if (!isAiming || !isIdle)
        {
            return;

        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        DrawLine(mousePos);



    }

    private void DrawLine(Vector3 worldPoint)
    {
        worldPoint.z = 0;

        Vector3 differece = new Vector3(worldPoint.x - transform.position.x, worldPoint.y - transform.position.y, 0);

        direction = transform.position - differece;

        Vector3 localDirection = direction - transform.position;

        if (localDirection.magnitude > maxSpeed)
        {
            localDirection = Vector3.ClampMagnitude(localDirection, maxSpeed);
        }

        direction = localDirection + transform.position;

        Vector3[] positions = { transform.position, direction };

        line.enabled = true;
        line.SetPositions(positions);

    }

    private void Shoot()
    {
        isAiming = false;
        isIdle = false;
        line.enabled = false;
        strokesHit++;
        remainingShots--;

        strokes.text = "Strokes:" + remainingShots;

        Vector2 localDirection = direction - transform.position;

        /*
        if (localDifference.magnitude > maxSpeed)
        {
            localDifference = Vector2.ClampMagnitude(localDifference, maxSpeed);
        }
        */

        playerBody.AddForce(localDirection * shotPower, ForceMode2D.Impulse);
    }

    private void Friction(Vector2 velocity)
    {
        velocity -= velocity * frictionCoeff;

        playerBody.velocity = velocity;

        //playerBody.AddForce(-velocity * frictionCoeff, ForceMode2D.Force);


    }

    private void Stop()
    {
        playerBody.velocity = Vector2.zero;
        playerBody.angularVelocity = 0;

        isIdle = true;

        if (maxShots == 0)
        {
            Die();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;

        pauseMenu.SetActive(true);
    }

    private void Die()
    {
           gameOver.SetActive(true);
    }

    private void Score()
    {
        scoreBoard.SetActive(true);

        hits.text = "Hits: " + strokesHit + "/" + maxShots;

        if (strokesHit > 0)
        {
            score = Mathf.RoundToInt((1.0f / strokesHit) * 1000);
        }        

        scoreText.text = "Score: " + score;

        shotPercentage = Mathf.RoundToInt(remainingShots / maxShots * 100.0f);

        if (shotPercentage >= 90)
        {
            grade.text = "A+";
        }
        else if (shotPercentage >= 80)
        {
            grade.text = "A";
        }
        else if (shotPercentage >= 70)
        {
            grade.text = "B";
        }
        else if (shotPercentage >= 60)
        {
            grade.text = "C";
        }
        else if (shotPercentage >= 50)
        {
            grade.text = "D";
        }
        else
        {
            grade.text = "F";
        }
    }



    /*
    private void Shoot()
    {
        isAiming = false;
        line.enabled = false;

        float xDis = mousePos.x - transform.position.x;
        float yDis = mousePos.y - transform.position.y;

        float angle = Mathf.Atan(yDis / xDis);
        float totalVel = (xDis / Mathf.Cos(angle)) * shotPower;

        try
        {
            Vector2 velocity = new Vector2 (-1 * totalVel * Mathf.Cos(angle) , -1 * totalVel * Mathf.Sin(angle));

            playerBody.velocity = velocity;

        }

        catch
        {
            playerBody.velocity = new Vector2(0, Mathf.Clamp(-1 * yDis * shotPower, -maxSpeed, maxSpeed));
        }

    }
    */


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
