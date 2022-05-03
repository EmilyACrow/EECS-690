using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOutput : MonoBehaviour
{
    public AudioSource sound;
    private bool played = false;

    void OnTriggerEnter(Collider other){
        var color = GetComponent<Renderer>();
        if(played == false){
            sound.Play();
            played = true;
            color.material.SetColor("_Color", Color.red);
        }
    }
}
