using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public ShipControl spaceship;
    public GameObject mainGuy;
    public Text speedUI, distanceUI, planetNameUI;
    public Image compassImage;
    public gameState currentGameState;

    public enum gameState { inShip, onFoot };


    MainGuy mainGuyScript;


    // Start is called before the first frame update
    void Start()
    {
        mainGuyScript = mainGuy.GetComponent<MainGuy>();


        ChangeGameState(currentGameState);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        CheckForControls();
    }


    void UpdateUI() {
        if (currentGameState == gameState.inShip)
        {
            speedUI.text = spaceship.ShipSpeed().ToString("F2") + " kmph";
            distanceUI.text = spaceship.DistanceFromPlanet().ToString() + " km";
            compassImage.gameObject.SetActive(true);
            RotateCompass();
        }
        else if (currentGameState == gameState.onFoot) {
            speedUI.text = "n/a kmph";
            distanceUI.text = "n/a km";
            compassImage.gameObject.SetActive(false);
        }
        planetNameUI.text = spaceship.currentPlanet.planetName.ToString();
    }

    void CheckForControls() {
        if (currentGameState == gameState.inShip)
        {
            if (spaceship.isOnPlanetSurface() && spaceship.ShipSpeed() == 0)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ChangeGameState(gameState.onFoot);
                }
            }
        }
        else if (currentGameState == gameState.onFoot) {
            if (mainGuyScript.IsNextToShip()) {
                if (Input.GetKeyDown(KeyCode.E)) {
                    ChangeGameState(gameState.inShip);

                }

            }

        }

    }



    void ChangeGameState(gameState newGameState) {
        currentGameState = newGameState;
        if (currentGameState == gameState.onFoot)
        {
            spaceship.shipControlStatus = false;
            mainGuy.transform.position = spaceship.mainGuySpawn.position;
            mainGuy.SetActive(true);
            mainGuyScript.isOnFoot = true;

        }
        else if (currentGameState == gameState.inShip) {
            spaceship.shipControlStatus = true;
            mainGuy.SetActive(false);
            mainGuyScript.isOnFoot = false;

        }

    }

    void RotateCompass() {
        compassImage.transform.localRotation = Quaternion.Euler(0f, 0f, spaceship.FindShipAngle() - 90);

    }
}
