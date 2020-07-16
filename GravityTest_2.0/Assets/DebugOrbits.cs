using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DebugOrbits : MonoBehaviour
{
    public Rigidbody2D newObject;
    public Rigidbody2D planet;
    public int stepNum = 10;

    public LineRenderer lineRend;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawOrbit();
    }



    void DrawOrbit() {
        // init fake bodies
        FakeBody firstObject = new FakeBody();
        firstObject.fakeBody(newObject);
        Vector3[] drawPoints = new Vector3[stepNum];

        //simulate
        for (int step = 0; step < stepNum; step++) {
            firstObject.velocity += MyUniverse.CalculateGravity(planet.transform.position, planet.mass, firstObject.position, firstObject.mass);
            Vector2 newPos = firstObject.position + firstObject.velocity;
            firstObject.position = newPos;
            drawPoints[step] = newPos;
        }

        // draw orbit
        for (int i = 0; i < drawPoints.Length - 1; i++) {
            //Debug.DrawLine(drawPoints[i], drawPoints[i + 1]);
            lineRend.SetPosition(i, drawPoints[i]);
        }


    }


    class FakeBody {
        public Vector2 position;
        public Vector2 velocity;
        public float mass;

        public void fakeBody(Rigidbody2D rB) {
            position = rB.transform.position;
            velocity = rB.gameObject.GetComponent<gravityObject>().initVel;
            mass = rB.mass;
        }


    }

}
