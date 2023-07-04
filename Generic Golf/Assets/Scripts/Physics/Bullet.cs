using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int speed;
    private Rigidbody2D bulletBody { get; set; }
    private GameObject player { get; set; }
    private float timer { get; set; } = 10;
    private float time { get; set; } = 0;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        bulletBody = GetComponent<Rigidbody2D>();
        
        bulletBody.velocity = transform.right * speed;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time > timer)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            player.transform.position = player.GetComponent<PlayerControls>().origin;

        }

        Destroy(gameObject);
    }
}
