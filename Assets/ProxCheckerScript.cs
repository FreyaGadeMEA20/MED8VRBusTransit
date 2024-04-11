using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxCheckerScript : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] GameObject sign;
    bool playerInProx { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        sign = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            // Perform actions when player enters proximity
            Debug.Log("Player is within proximity");
            playerInProx = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
            playerInProx = false;
    }

    public bool CheckPlayerProximity()
    {
        return playerInProx;
    }
}
