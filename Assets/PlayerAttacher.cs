using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacher : MonoBehaviour
{
    [SerializeField] GameObject bus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            other.transform.parent = bus.transform;
            Debug.Log("Attached");
        }
    }

    void OnTriggerExit(Collider other){
        if (other.tag == "Player"){
            other.transform.parent = null;
            Debug.Log("Dettached");
        }
    }
}
