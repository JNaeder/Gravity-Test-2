using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    public Transform world, player, mainGuySpawn;
    public float rotateSpeed = 1f;
    public float enginePower = 1f;
    public LayerMask planetLayer;
    public Planet currentPlanet;
    public bool shipControlStatus;
    public GameObject engineFire;

    public enum shipState {freeMode, antiDirMode};
    public shipState currentShipState;

//testing git!


    Vector2 diffVec;
    Rigidbody2D rb;
    float h;
    float distanceFromPlanet;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPlanet = world.GetComponent<Planet>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shipControlStatus)
        {
            rb.gravityScale = 1;
            ShipMovement();
            ShipMode();
        }
        else {
            rb.gravityScale = 0;

        }

    }

    void ShipMovement()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * enginePower);
            engineFire.SetActive(true);
        }
        else {
            engineFire.SetActive(false);

        }

        h = Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0, 0, -h * rotateSpeed));


        if (Input.GetKeyDown(KeyCode.R)) {
            if (currentShipState == shipState.freeMode)
            {
                currentShipState = shipState.antiDirMode;
            }
            else if (currentShipState == shipState.antiDirMode) {
                currentShipState = shipState.freeMode;
            }

        }
    }

    void ShipMode() {
        if (currentShipState == shipState.antiDirMode) {
            transform.rotation = Quaternion.Euler(0f, 0f, FindShipAngle() + 90);
        }


    }


    public bool isOnPlanetSurface() {
        bool onSurface = Physics2D.OverlapCircle(transform.position, 1f, planetLayer);
        return onSurface;
    }

    public float DistanceFromPlanet() {
        float distanceFromPlanet = Vector2.Distance(player.position, world.position) - currentPlanet.planetRadius - (player.localScale.x /2);
        distanceFromPlanet = Mathf.RoundToInt(distanceFromPlanet);
        return distanceFromPlanet;
    }

    public float ShipSpeed() {
        float shipSpeed = rb.velocity.magnitude;
        return shipSpeed;

    }


    public float FindShipAngle() {
        float rot_z = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        return rot_z;

    }
}
