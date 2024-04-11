using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WaypointMover : MonoBehaviour
{
    public Waypoints waypoints;
    public CarSpawner carSpawner;
    public WaypointClass waypointClass;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float safeDistance = 2f;

    private Transform currentWaypoint;

    [SerializeField] private bool canMove = true;

    public int routeIndex;

    public string entityType;

    public bool hasCheckedIn = false;

    private enum MovementState{
        Moving,
        Waiting
    }

    [SerializeField] private MovementState currentMovementState;

    private IEnumerator MovementSM(){
        while(true){
            switch (currentMovementState){
                case MovementState.Moving:
                    MoveTowardsWaypoint();
                    break;

                case MovementState.Waiting:
                    yield return new WaitUntil(() => hasCheckedIn == true);
                    Debug.Log("Bus has checked in");
                    yield return new WaitForSeconds(waypointClass.waitingTime);
                    currentMovementState = MovementState.Moving;
                    break;
            }

            yield return null;
        }
    }

    void Start(){
        waypoints = GameObject.Find("Waypoints").GetComponent<Waypoints>();
        carSpawner = GameObject.Find("Spawn Manager").GetComponent<CarSpawner>();

        if (carSpawner == null){
            Debug.LogWarning("CarSpawner component not found");
        }
        //routeIndex = carSpawner.routeIndex;
        //Debug.Log("Chosen Route index: " + carSpawner.routeIndex);

        currentMovementState = MovementState.Moving;

        // Set the current waypoint to the first waypoint in the list
        currentWaypoint = waypoints.routes[carSpawner.routeIndex].waypoints[0];
        Debug.Log("Current waypoint: " + currentWaypoint.position);

        // Set the current waypoint to the first waypoint in the list
        //currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        //transform.position = currentWaypoint.position;

        // Move the object towards the current waypoint
        //StartCoroutine(MoveTowardsWaypoint());
        StartCoroutine(MovementSM());
    }


    void Update(){
        RotateTowardsWaypoint();
        //CheckIfCanMove();

        routeIndex = carSpawner.routeIndex;
    }

    private void RotateTowardsWaypoint(){
        if (currentWaypoint == null) return;    // If the current waypoint is null, return

        // Calculate the direction to the current waypoint
        Vector3 direction = currentWaypoint.position - transform.position;
        direction.y = 0;

        // Rotate the object to face the current waypoint
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
    }
    
    private void MoveTowardsWaypoint(){
         /* while(true){
            waypointClass = currentWaypoint.GetComponent<WaypointClass>();

            // If the we are not looping and have reached the last waypoint, destroy the object
            if (waypoints.doLoop == false && currentWaypoint == null){
                Destroy(gameObject);
                //yield break;
            }

            if (canMove){
                // Move the object towards the current waypoint
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

                // If the object has reached the current waypoint, set the current waypoint to the next waypoint
                if (transform.position == currentWaypoint.position){
                    /* if (waypointClass.isWaitingPoint){
                        yield return new WaitForSeconds(waypointClass.waitingTime);
                    }

                    if (entityType == "Bus" && waypointClass.isWaitingPoint){  
                        //yield return new WaitUntil(() => hasCheckedIn == true);
                        // yield return new WaitForSeconds(waypointClass.waitingTime);
                    }

                    // set the current waypoint to the next waypoint
                    currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint, carSpawner.routeIndex);
                }
            }

            //yield return null;
        } */

        if (canMove){
            // Destroy vehicle if there is no waypoints left
            if (waypoints.doLoop == false && currentWaypoint == null){
                Destroy(gameObject);
                return;
            }

            Vector3 targetPosition = currentWaypoint.position;
            targetPosition.y = transform.position.y;
            
            // Move towards the waypoint
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            float distanceToPoint = Vector3.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(targetPosition.x, targetPosition.z));

            // If we are at the waypoint, find the next
            if (distanceToPoint <= 0.1f){
                waypointClass = currentWaypoint.GetComponent<WaypointClass>();

                currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint, carSpawner.routeIndex);

                // If we are at a waiting waypoint, wait
                if (waypointClass.isWaitingPoint && entityType == "Bus"){
                    currentMovementState = MovementState.Waiting;
                }
            }
        }

    }

    private void CheckIfCanMove(){
/*         // Draw the ray
        Debug.DrawRay(transform.position, transform.forward * safeDistance, Color.red);

        // Check if object is close to antoher object with "car" as tag
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, safeDistance)){
            if (hit.collider.tag == "Car"){
                canMove = false;
            }
        }
        
        else {
            canMove = true;
        } */

        // Check for overlaps with other colliders
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * safeDistance, safeDistance);
        foreach (var collider in colliders){
            if (collider.CompareTag("Car")){
                Debug.Log("Car detected");
                canMove = false;
                return;
            }
        }

        canMove = true;
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Car")){
            // Check if car is in front of the object
            Vector3 direction = other.transform.position - transform.position;
            float dotProduct = Vector3.Dot(direction, transform.forward);

            if (dotProduct > 0){
                canMove = false;
            }
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Car")){
            canMove = true;
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * safeDistance, safeDistance);
    }
}

