using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusGameManager : MonoBehaviour
{
    // Define the possible states
    public enum GameState
    {
        SCHOOL,
        PHONE,
        SIGN,
        BUS
    }

    // Current state of the game
    [SerializeField] GameState currentState;

    // -- Objects for checking states -- //
    public GameObject phone;
    //public GameObject signArea;
    //public CheckIn checkIn;
    //public TransportScript bus;
    
    [SerializeField] ProxCheckerScript SignScript;
    [SerializeField] GameObject SignBeam;
    [SerializeField] ProxCheckerScript BusStopScript;
    [SerializeField] GameObject StopBeam;
    [SerializeField] CarSpawner carSpawer;
    [SerializeField] GameObject busDeath;
    private bool hasCheckedIn{
        get{return HasCheckedIn;}
        set{HasCheckedIn = value;}
    }

    public bool HasCheckedIn;
    // Start is called before the first frame update
    void Start()
    {
        // Set the initial state
        currentState = GameState.SCHOOL;
        //StartCoroutine(fade());
    }

    // Update is called once per frame
    void Update()
    {
        // Check for state transitions
        switch (currentState)
        {
            case GameState.SCHOOL:
                // Handle SCHOOL state logic
                UpdateSchoolState();
                break;

            case GameState.PHONE:
                // Handle PHONE state logic
                UpdatePhoneState();
                break;

            case GameState.SIGN:
                // Handle SIGN state logic
                UpdateSignState();
                break;

            case GameState.BUS:
                // Handle BUS state logic
                UpdateBusState();
                break;
        }
    }

    // Countdown timer variables
    private float countdownTimer = 3f;
    private bool isCountingDown = true;
    void UpdateSchoolState() {
        // Check if the GameObject is visible in the viewport
        if (IsGameObjectVisibleInViewport(phone)) {
            // The GameObject is visible, do something

            if (isCountingDown) {
                countdownTimer -= Time.deltaTime;
                Debug.Log($"GameObject has been visible in the viewport for {(countdownTimer-3)*-1} seconds");

                if (countdownTimer <= 0f) {
                    // Countdown finished, do something
                    Debug.Log("Countdown finished");
                    currentState = GameState.PHONE;
                    SignBeam.SetActive(true);
                    // Reset the countdown timer
                    //countdownTimer = 3f;
                    isCountingDown = false;
                }
            }
            else {
                // Reset the countdown timer if the GameObject is not visible
                countdownTimer = 3f;
                isCountingDown = true;
            }
        }
    }

    void UpdatePhoneState(){
        if(SignScript.CheckPlayerProximity()){
            if (isCountingDown) {
                countdownTimer -= Time.deltaTime;
                Debug.Log($"Player has been by the sign for {(countdownTimer-3)*-1} seconds");

                if (countdownTimer <= 0f) {
                    // Countdown finished, do something
                    Debug.Log("Countdown finished");
                    currentState = GameState.SIGN;
                    SignBeam.SetActive(false);
                    StopBeam.SetActive(true);
                    // Reset the countdown timer
                    //countdownTimer = 3f;
                    isCountingDown = false;
                }
            }
            else {
                // reset the countdown if it is not counting down
                countdownTimer = 3f;
                isCountingDown = true;
            }
        } else {
            // Reset the countdown timer if the GameObject is not visible
            countdownTimer = 3f;
            isCountingDown = true;
        }
    }

    void UpdateSignState(){
        if(BusStopScript.CheckPlayerProximity()){
            if (isCountingDown) {
                countdownTimer -= Time.deltaTime;
                Debug.Log($"Player has been by the bus stop for {(countdownTimer-3)*-1} seconds");

                if (countdownTimer <= 0f) {
                    // Countdown finished, do something
                    Debug.Log("Countdown finished");
                    currentState = GameState.BUS;
                    carSpawer.canSpawnBus = true;

                    busDeath.SetActive(false);
                    // Reset the countdown timer
                    //countdownTimer = 3f;
                    isCountingDown = false;
                }
            }
            else {
                // reset the countdown if it is not counting down
                countdownTimer = 3f;
                isCountingDown = true;
            }
        } else {
            // Reset the countdown timer if the GameObject is not visible
            countdownTimer = 3f;
            isCountingDown = true;
        }

        // Set the car waypoint at the crosswalk to 'waiting' so the player can cross safely
    }
    
    void UpdateBusState(){
        if (!carSpawer.canSpawnBus)
            carSpawer.canSpawnBus = true;

        if(hasCheckedIn)
        {
            WaypointMover wp = GameObject.FindWithTag("Bus").transform.parent.gameObject.GetComponent<WaypointMover>();

            //StartCoroutine(screenFader.FadeOutAndReloadScene());
            //wp.hasCheckedIn = true;

            //screenFader.FadeOut();
        }

        // Let the cars move again from the sign as the player has crossed the street
    }

    //public void FadeOutRL(){
    //    StartCoroutine(screenFader.FadeOutAndReloadScene());
    //}

    
    bool IsGameObjectVisibleInViewport(GameObject gameObject)
    {
        if(!gameObject.activeSelf) {
            countdownTimer = 3f;
            return false;
        }

        // Get the main camera
        Camera mainCamera = Camera.main;

        // Calculate the viewport position of the GameObject
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(gameObject.transform.position);

        // Check if the GameObject is within the viewport bounds
        if (viewportPosition.x >= 0.2
            && viewportPosition.x <= .8
            && viewportPosition.y >= 0.2
            && viewportPosition.y <= .8
            && viewportPosition.z > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
