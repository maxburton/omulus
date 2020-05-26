using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraHandler : MonoBehaviour{
    public Camera camPlayer;
    public Camera camOmulus;
    
    void Start(){
        
        camPlayer.enabled = true;
        camOmulus.enabled = false;
        camPlayer.GetComponent<AudioListener>().enabled = true;
        camOmulus.GetComponent<AudioListener>().enabled = false;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.C)) {
            camPlayer.enabled = !camPlayer.enabled;
            camOmulus.enabled = !camOmulus.enabled;
            camPlayer.GetComponent<AudioListener>().enabled = !camPlayer.GetComponent<AudioListener>().enabled;
            camOmulus.GetComponent<AudioListener>().enabled = !camOmulus.GetComponent<AudioListener>().enabled;
        }
    }
}
