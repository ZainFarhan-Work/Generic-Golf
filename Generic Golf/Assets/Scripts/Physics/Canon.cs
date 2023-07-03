using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private float timeInterval;
    private Transform firePoint { get; set; }
    public GameObject bullet;
    private float time { get; set; }


    void Awake()
    {
        firePoint = gameObject.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > timeInterval) 
        { 
            Shoot();
            time = 0;
        }

    }


    private void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
