using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRManager : MonoBehaviour
{
    [SerializeField] GameObject hand;
    [SerializeField] GameObject item;
    Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        handAnimator = hand.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowGameObject(){
        // Animate hand to hold object
        handAnimator.SetTrigger("HoldObject");

        // Activate gameobject
        item.SetActive(true);

    }

    void HideGameObject(){
        // Return hand to other animation
        handAnimator.SetTrigger("FreeHand");

        // Deactivate gameobject
        item.SetActive(false);
    }
}
