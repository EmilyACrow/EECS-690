using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    //[Range(0.0f, 1.0f)]
    //[SerializeField] float masterVolume = 1.0f;
    float masterVolume = 1.0f;

    void Update(){
        AudioListener.volume = masterVolume;
    }
}
