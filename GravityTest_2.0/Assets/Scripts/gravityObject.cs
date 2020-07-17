using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityObject : MonoBehaviour
{

    public Rigidbody2D orbitAround;
    public Vector2 initVel;

    Rigidbody2D rB;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        rB.velocity = initVel;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rB.velocity += MyUniverse.CalculateGravity(orbitAround.transform.position,orbitAround.mass, rB.transform.position, rB.mass) * Time.deltaTime;

        
    }
}
