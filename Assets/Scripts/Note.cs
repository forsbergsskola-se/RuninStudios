using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Vector3 direction = Vector3.forward; // Direction of movement
    public float speed = 5f;                    // Speed of movement
    public bool isOnTrigger;
    public string ID;
    private int destroyAfter = 5; //Trigger this when the button har pressed correctly 
    private ParticleSystem particle;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, destroyAfter);
    }

    public void Hit()
    {
        ParticleSystem explosion = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(explosion.gameObject, destroyAfter);
        Destroy(gameObject);
    }
    

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
