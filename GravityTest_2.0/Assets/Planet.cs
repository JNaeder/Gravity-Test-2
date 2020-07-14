using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Planet : MonoBehaviour
{
    public string planetName;
    public float planetGravity;
    public float sphereOfInfulence;
    public float planetRadius;

    public CircleCollider2D gravityTrigger;
    public PointEffector2D pointEff;

    private void Start()
    {
        planetRadius = transform.localScale.x / 2;

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, gravityTrigger.radius * (transform.localScale.x / 2));
    }

    private void Update()
    {
        pointEff.forceMagnitude = -planetGravity;
    }

}
