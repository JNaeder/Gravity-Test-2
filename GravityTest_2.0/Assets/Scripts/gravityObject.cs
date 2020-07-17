using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityObject : MonoBehaviour
{

    public Rigidbody2D orbitAround;

    Rigidbody2D rB;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (orbitAround != null)
        {
            rB.velocity += (MyUniverse.CalculateGravity(orbitAround.transform.position, orbitAround.mass, rB.transform.position, rB.mass, orbitAround.GetComponent<Planet>().gravityMult)) * Time.deltaTime;
        }

        
    }
}
