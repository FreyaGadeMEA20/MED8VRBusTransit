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

    private bool PHONE_STATE_CHANGED = false;
    private bool SIGN_STATE_CHANGED = false;
    private bool BUS_STATE_CHANGED = false;

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

    void UpdateSchoolState(){
        // Check if the GameObject is visible in the viewport
        if (IsGameObjectVisibleInViewport(gameObject))
        {
            // The GameObject is visible, do something
            Debug.Log("GameObject is visible in the viewport");
        }
        else
        {
            // The GameObject is not visible, do something else
            Debug.Log("GameObject is not visible in the viewport");
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
        // Get the main camera
        Camera mainCamera = Camera.main;

        // Calculate the viewport position of the GameObject
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(gameObject.transform.position);

        // Check if the GameObject is within the viewport bounds
        if (viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1 && viewportPosition.z > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
