using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;   

public class PlayerAttacher : MonoBehaviour
{
    [SerializeField] GameObject bus;

    [SerializeField] ParentConstraint _constrainer;
    // Start is called before the first frame update
    ConstraintSource constraintSource;
    void Start()
    {
        _constrainer = GameObject.Find("XR Player").GetComponent<ParentConstraint>();
        constraintSource.sourceTransform = bus.transform; 
        constraintSource.weight = 1;
        _constrainer.AddSource(constraintSource);
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            //other.transform.parent = bus.transform;
            Debug.Log("Attached");
            _constrainer.constraintActive = true;
        }
    }

    void OnTriggerExit(Collider other){
        if (other.tag == "Player"){
            //other.transform.parent = null;
            Debug.Log("Dettached");
            _constrainer.constraintActive = false;
        }
    }
}
