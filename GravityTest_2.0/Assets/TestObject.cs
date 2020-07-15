using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{

    public Vector2 intVelocity;
    public float mass;

    Rigidbody2D rB;
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        rB.velocity = intVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
