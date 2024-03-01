using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playFootSteps : MonoBehaviour
{
    AudioManager audioManager;
    public void play(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.playSteps();
    }
}
