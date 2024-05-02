using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopScript : MonoBehaviour
{
    public BusScreenController busScreenController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StopButton(){
        busScreenController.ApplyStopTexture();
    }

    public void Reset(){

    }
}
