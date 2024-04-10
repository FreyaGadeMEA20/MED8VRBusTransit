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
    private GameState currentState;

    // -- Objects for checking states -- //
    public GameObject phone;
    //public GameObject signArea;
    //public CheckIn checkIn;
    //public TransportScript bus;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial state
        currentState = GameState.SCHOOL;
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
                    GameState = GameState.PHONE;
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

    }

    void UpdateSignState(){

    }
    
    void UpdateBusState(){
        
    }

    
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
