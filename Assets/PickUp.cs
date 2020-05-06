﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    float throwForce = 1200f;
    Vector3 objectPos;
    float distance;

    public float maxDistance = 3f;
    public bool canHold = true;
    public GameObject item;
    public GameObject tempParent;
    public bool isHolding = false;
    public Text pickupText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(item.transform.position, tempParent.transform.position);
        if(distance >= maxDistance){
            isHolding = false;
        }
        if(isHolding){
            pickupText.enabled = false;
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.transform.SetParent(tempParent.transform);
            // Position the omulus to the right of the player's view
            item.transform.localPosition = new Vector3(1, 0, 2);

            if(Input.GetButtonDown("Fire1")){
                item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
                isHolding = false;
            }
        }else{
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;
        }
    }

    // Display text when looking at item and close enough to it.
    void OnMouseOver(){
        if(distance <= maxDistance && !isHolding){
            pickupText.enabled = true;
            if(Input.GetButtonDown("Use")){
                isHolding = true;
                item.GetComponent<Rigidbody>().useGravity = false;
                item.GetComponent<Rigidbody>().detectCollisions = true;
            }
        }else{
            pickupText.enabled = false;
        }
    }

    void OnMouseExit(){
        pickupText.enabled = false;
    }
}
