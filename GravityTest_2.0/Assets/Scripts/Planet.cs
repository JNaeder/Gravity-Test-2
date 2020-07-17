using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Planet : MonoBehaviour
{
    public string planetName;
    public float gravityMult;
    public float sphereOfInfulence;
    public float planetRadius;
    public bool isSun;

    public LayerMask gravityMask;

    Rigidbody2D planetRb;

    private void Start()
    {
        planetRb = GetComponent<Rigidbody2D>();
        planetRadius = transform.localScale.x / 2;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereOfInfulence);
    }

    private void FixedUpdate()
    {
        foreach (gravityObject g in ObjectsInInfluence()) {
            g.orbitAround = planetRb;

        }
    }


    public gravityObject[] ObjectsInInfluence() {

        Collider2D[] gObjects = Physics2D.OverlapCircleAll(transform.position, sphereOfInfulence, gravityMask);
        List<gravityObject> listOfRBs = new List<gravityObject>();
        foreach (Collider2D c in gObjects) {
            if (c.GetComponent<gravityObject>() != null) {
                listOfRBs.Add(c.GetComponent<gravityObject>());
            }
        }
        gravityObject[] newRbs = listOfRBs.ToArray();
        return newRbs;
        
    }

    
}
