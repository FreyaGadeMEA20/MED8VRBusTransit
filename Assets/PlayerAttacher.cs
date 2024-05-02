using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;   
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PlayerAttacher : MonoBehaviour
{
    [SerializeField] GameObject bus;
    [SerializeField] GameObject player;

    [SerializeField] ParentConstraint _constrainer;
    [SerializeField] DynamicMoveProvider _dmp;
    // Start is called before the first frame update
    ConstraintSource constraintSource;
    void Start()
    {
        player = GameObject.Find("XR Player");//.GetComponent<ParentConstraint>();
        _dmp = player.GetComponent<DynamicMoveProvider>();
        constraintSource.sourceTransform = bus.transform; 
        constraintSource.weight = 1;
        _constrainer.AddSource(constraintSource);
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            other.transform.parent = bus.transform;
            //other.transform.position += new Vector3(0, 0.1f, 0);
            //_dmp.useGravity = false;
            Debug.Log("Attached");
            //_constrainer.constraintActive = true;
        }
    }

    void OnTriggerExit(Collider other){
        if (other.tag == "Player"){
            other.transform.parent = null;
            Debug.Log("Dettached");
            //other.transform.position -= new Vector3(0, 0.1f, 0);
            //_dmp.useGravity = true;
            //_constrainer.constraintActive = false;
        }
    }
}
