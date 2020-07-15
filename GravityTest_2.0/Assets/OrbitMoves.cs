using UnityEngine;

[ExecuteInEditMode]
public class OrbitMoves : MonoBehaviour
{

    public int numSteps = 1000;
    public float timeStep = 0.1f;
    public bool usePhysicsTimeStep;

    public bool relativeToBody;
    public TestObject centralBody;
    public float width = 100;

    void Start()
    {
        if (Application.isPlaying)
        {
            HideOrbits();
        }
    }

    void Update()
    {

        if (!Application.isPlaying)
        {
            DrawOrbits();
        }
    }

    void DrawOrbits()
    {
        TestObject[] bodies = FindObjectsOfType<TestObject>();
        VirtualBody[] virtualBodies = new VirtualBody[bodies.Length];
        Vector3[][] drawPoints = new Vector3[bodies.Length][];
        int referenceFrameIndex = 0;
        Vector2 referenceBodyInitialPosition = Vector2.zero;

        // Initialize virtual bodies (don't want to move the actual bodies)
        for (int i = 0; i < virtualBodies.Length; i++)
        {
            virtualBodies[i] = new VirtualBody(bodies[i]);
            drawPoints[i] = new Vector3[numSteps];

            if (bodies[i] == centralBody && relativeToBody)
            {
                referenceFrameIndex = i;
                referenceBodyInitialPosition = virtualBodies[i].position;
            }
        }

        // Simulate
        for (int step = 0; step < numSteps; step++)
        {
            Vector2 referenceBodyPosition = (relativeToBody) ? virtualBodies[referenceFrameIndex].position : Vector2.zero;
            // Update velocities
            for (int i = 0; i < virtualBodies.Length; i++)
            {
                virtualBodies[i].velocity += CalculateAcceleration(i, virtualBodies) * timeStep;
            }
            // Update positions
            for (int i = 0; i < virtualBodies.Length; i++)
            {
                Vector2 newPos = virtualBodies[i].position + virtualBodies[i].velocity * timeStep;
                virtualBodies[i].position = newPos;
                if (relativeToBody)
                {
                    var referenceFrameOffset = referenceBodyPosition - referenceBodyInitialPosition;
                    newPos -= referenceFrameOffset;
                }
                if (relativeToBody && i == referenceFrameIndex)
                {
                    newPos = referenceBodyInitialPosition;
                }

                drawPoints[i][step] = newPos;
            }
        }

        // Draw paths
        for (int bodyIndex = 0; bodyIndex < virtualBodies.Length; bodyIndex++)
        {
            Color pathColour = bodies[bodyIndex].gameObject.GetComponentInChildren<SpriteRenderer>().color; //

                for (int i = 0; i < drawPoints[bodyIndex].Length - 1; i++)
                {
                    Debug.DrawLine(drawPoints[bodyIndex][i], drawPoints[bodyIndex][i + 1], pathColour);
                }

                // Hide renderer
                LineRenderer lineRenderer = bodies[bodyIndex].gameObject.GetComponentInChildren<LineRenderer>();
                if (lineRenderer)
                {
                    lineRenderer.enabled = false;
                }

        }
    }

    Vector2 CalculateAcceleration(int i, VirtualBody[] virtualBodies)
    {
        Vector2 acceleration = Vector2.zero;
        for (int j = 0; j < virtualBodies.Length; j++)
        {
            if (i == j)
            {
                continue;
            }
            Vector2 forceDir = (virtualBodies[j].position - virtualBodies[i].position).normalized;
            float sqrDst = (virtualBodies[j].position - virtualBodies[i].position).sqrMagnitude;
            acceleration += forceDir * (10 * (virtualBodies[j].mass * virtualBodies[i].mass / sqrDst));
        }
        return acceleration;
    }

    void HideOrbits()
    {
        TestObject[] bodies = FindObjectsOfType<TestObject>();

        // Draw paths
        for (int bodyIndex = 0; bodyIndex < bodies.Length; bodyIndex++)
        {
            LineRenderer lineRenderer = bodies[bodyIndex].gameObject.GetComponentInChildren<LineRenderer>();
            lineRenderer.positionCount = 0;
        }
    }

    void OnValidate()
    {
        if (usePhysicsTimeStep)
        {
            timeStep = Universe.physicsTimeStep;
        }
    }

    class VirtualBody
    {
        public Vector2 position;
        public Vector2 velocity;
        public float mass;

        public VirtualBody(TestObject body)
        {
            position = body.transform.position;
            velocity = body.intVelocity;
            mass = body.mass;
        }
    }
}
