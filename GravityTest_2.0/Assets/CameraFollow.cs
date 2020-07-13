using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform spaceShip, planet, mainGuy;
    public float maximumCameraZoomOut = 50f;

    Vector3 camPos;
    float distanceFromPlanet;

    GameController gM;
    Camera cam;
    ShipControl spaceShipScript;

    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameController>();
        cam = Camera.main;
        spaceShipScript = spaceShip.GetComponent<ShipControl>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (gM.currentGameState == GameController.gameState.inShip)
        {
            FocusOnShip();
        }
        else if (gM.currentGameState == GameController.gameState.onFoot) {
            FocusOnMainGuy();
        }


    }


    void FocusOnShip() {
        camPos = new Vector3(spaceShip.position.x, spaceShip.position.y, -10);

        transform.position = camPos;
        transform.localRotation = Quaternion.Euler(Vector3.zero);


        if (spaceShipScript.isOnPlanetSurface())
        {
            cam.orthographicSize = 3;
        }
        else
        {
            distanceFromPlanet = Vector2.Distance(spaceShip.position, planet.position);
            distanceFromPlanet = Mathf.Clamp(distanceFromPlanet, 20f, maximumCameraZoomOut + 15f);
            cam.orthographicSize = distanceFromPlanet - 15;
        }

    }


    void FocusOnMainGuy() {
        camPos = new Vector3(mainGuy.position.x, mainGuy.position.y, -10);
        transform.position = camPos;
        cam.orthographicSize = 2;

        transform.localRotation = mainGuy.localRotation;


    }
}
