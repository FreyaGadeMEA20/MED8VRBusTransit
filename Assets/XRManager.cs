using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRManager : MonoBehaviour
{
    [SerializeField] GameObject hand;
    [SerializeField] GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowGameObject(GameObject gO){
        // Animate hand to hold object
        // Activate gameobject

        item = gO;
    }

    void HideGameObject(){
        // Return hand to other animation
        // Deactivate gameobject

        // clear item
    }
}
