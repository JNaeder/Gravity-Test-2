using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Planet : MonoBehaviour
{
    public string planetName;
    public float gravityMult;
    public float sphereOfInfulence;
    public float planetRadius;

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
        foreach (Rigidbody2D r in ObjectsInInfluence()) {
            ApplyGravity(r);

        }
    }


    Rigidbody2D[] ObjectsInInfluence() {

        Collider2D[] gObjects = Physics2D.OverlapCircleAll(transform.position, sphereOfInfulence, gravityMask);
        List<Rigidbody2D> listOfRBs = new List<Rigidbody2D>();
        foreach (Collider2D c in gObjects) {
            if (c.GetComponent<Rigidbody2D>() != null) {
                listOfRBs.Add(c.GetComponent<Rigidbody2D>());
            }
        }
        Rigidbody2D[] newRbs = listOfRBs.ToArray();
        return newRbs;
        
    }

    void ApplyGravity(Rigidbody2D rB) {
        Vector2 difference = transform.position - rB.transform.position;
        Vector2 gravityDirection = difference.normalized;
        float distance = difference.magnitude;

        float gravityForce = gravityMult * ((planetRb.mass * rB.mass) / (distance * distance));

        rB.AddForce(gravityDirection * gravityForce);

        Debug.Log(rB.gameObject.name + " " + (gravityDirection * gravityForce));

    }



}
