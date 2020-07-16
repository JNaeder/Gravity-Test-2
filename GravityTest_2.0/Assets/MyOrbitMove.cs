using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MyOrbitMove : MonoBehaviour
{

    public Planet planet;
    public int numSteps;
    public float timeStep;
    

    Rigidbody2D planetRb;

    // Start is called before the first frame update
    void Start()
    {
        planetRb = planet.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void    Update()
    {
        DrawOrbits();
    }



    void DrawOrbits()
    {
       
        Rigidbody2D[] bodies = planet.ObjectsInInfluence();
        VirtualBody[] virtualBodies = new VirtualBody[bodies.Length];
        Vector2[][] drawPoints = new Vector2[bodies.Length][];


        // initializing
        for (int i = 0; i < virtualBodies.Length; i++)
        {
            TestObject tObject = bodies[i].GetComponent<TestObject>();
            virtualBodies[i] = new VirtualBody(bodies[i], tObject);
            drawPoints[i] = new Vector2[numSteps];
        }

        // Simulate
        for (int step = 0; step < numSteps; step++)
        {
            for (int i = 0; i < virtualBodies.Length; i++)
            {
                if (step == 0)
                {
                   virtualBodies[i].velocity = virtualBodies[i].intVelocity;
                }
                virtualBodies[i].velocity += CalculateAcceleration(virtualBodies[i]);
            }
            // Update positions
            for (int i = 0; i < virtualBodies.Length; i++)
            {
                Vector2 newPos = virtualBodies[i].position + virtualBodies[i].velocity * 0.01f;
                //Debug.Log("Simulated - Position: " + newPos + " Veclocity: " + virtualBodies[i].velocity);
                virtualBodies[i].position = newPos;

                drawPoints[i][step] = newPos;
            }
        }

        // Draw paths
        for (int bodyIndex = 0; bodyIndex < virtualBodies.Length; bodyIndex++)
        {
            Color pathColour = Color.green;

            for (int i = 0; i < drawPoints[bodyIndex].Length - 1; i++)
            {
                Debug.DrawLine(drawPoints[bodyIndex][i], drawPoints[bodyIndex][i + 1], pathColour);
            }

        }


    }


    Vector2 CalculateAcceleration(VirtualBody r)
    {
        Vector2 velocity;

        Vector2 difference = new Vector2(planet.transform.position.x, planet.transform.position.y) - r.position;
        Vector2 gravityDirection = difference.normalized;
        float distance = difference.magnitude;

        float gravityForce = planet.gravityMult * ((planetRb.mass * r.mass) / (distance * distance));

        velocity = (gravityDirection * gravityForce * 0.01f);
        return velocity;
    }



    class VirtualBody
    {
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 intVelocity;
        public float mass;

        public VirtualBody(Rigidbody2D body, TestObject body2)
        {
            position = body.transform.position;
            velocity = body.velocity;
            intVelocity = body2.intVelocity;
            mass = body.mass;
        }
    }

}
