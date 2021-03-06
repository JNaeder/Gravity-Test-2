﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    public Transform player, mainGuySpawn;
    public float rotateSpeed = 1f;
    public float enginePower;
    public float minEnginePower = 1f;
    public float maxEnginePower = 10f;
    public float maxFuel, fuelAmount;
    public LayerMask planetLayer;
    public Planet currentPlanet;
    public bool shipControlStatus;
    public GameObject engineFire;

    public enum shipState {freeMode, antiDirMode, dirMode};
    public shipState currentShipState;

    gravityObject gravObj;

//testing git!


    Vector2 diffVec;
    Rigidbody2D rb;
    float h;
    float distanceFromPlanet;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravObj = GetComponent<gravityObject>();
        


        //enginePower = maxEnginePower;
        fuelAmount = maxFuel;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gravObj.orbitAround != null)
        {
            currentPlanet = gravObj.orbitAround.GetComponent<Planet>();
        }

        if (shipControlStatus)
        {
            ShipMovement();
            ShipMode();
            SetEnginePower();
        }

    }

    void ShipMovement()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            if (fuelAmount > 0)
            {
                rb.AddForce(transform.up * enginePower);
                engineFire.SetActive(true);
                UseFuel();
            }
        }
        else {
            engineFire.SetActive(false);

        }

        h = Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0, 0, -h * rotateSpeed * Time.deltaTime));


        if (Input.GetKeyDown(KeyCode.R)) {
            if (currentShipState != shipState.antiDirMode)
            {
                currentShipState = shipState.antiDirMode;
            }
            else {
                currentShipState = shipState.freeMode;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (currentShipState != shipState.dirMode)
            {
                currentShipState = shipState.dirMode;
            }
            else{
                currentShipState = shipState.freeMode;
            }
        }
    }

    void ShipMode() {
        if (!isOnPlanetSurface())
        {
            if (currentShipState == shipState.antiDirMode)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, FindShipAngle() + 90);
            }
            else if (currentShipState == shipState.dirMode)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, FindShipAngle() - 90);

            }
        }
    }

    void SetEnginePower() {
        float v = Input.GetAxis("Vertical");
        enginePower += v * Time.deltaTime * 4f;
        enginePower = Mathf.Clamp(enginePower, minEnginePower, maxEnginePower);

    }


    public bool isOnPlanetSurface() {
        bool onSurface = Physics2D.OverlapCircle(transform.position, 1f, planetLayer);
        return onSurface;
    }

    public float DistanceFromPlanet() {
        float distanceFromPlanet = Vector2.Distance(player.position, currentPlanet.transform.position) - currentPlanet.planetRadius - (player.localScale.x /2);
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

    void UseFuel() {
        if (fuelAmount > 0)
        {
            fuelAmount -= Time.deltaTime * enginePower;
        }
        else {
            fuelAmount = 0;
        }

    }
}
