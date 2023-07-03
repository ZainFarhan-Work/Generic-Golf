using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private int rotationSpeed = 100;
    private float rotationAngle { get; set; } = 0;

    // Update is called once per frame
    void Update()
    {
        rotationAngle += rotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationAngle));
    }
}
