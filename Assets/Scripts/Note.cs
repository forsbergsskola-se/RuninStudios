using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Vector3 direction = Vector3.forward; // Direction of movement
    public float speed = 5f;                    // Speed of movement
    public bool isOnTrigger;
    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
