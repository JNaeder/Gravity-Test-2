using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public string planetName;
    public float planetGravity;
    public float sphereOfInfulence;
    public float planetRadius;

    public CircleCollider2D gravityTrigger;

    private void Start()
    {
        planetRadius = transform.localScale.x / 2;

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, gravityTrigger.radius * (transform.localScale.x / 2));
    }

}
