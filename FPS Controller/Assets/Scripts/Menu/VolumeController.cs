using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volume_slider;

    [Range(0.0f, 1.0f)]
    [SerializeField] float masterVolume = 1.0f;

    public float slider_value;

    //void Update(){
    //    AudioListener.volume = masterVolume;
    //}

    void Start(){
        volume_slider = GetComponent<Slider>();
    }

    public void AdjustVolume(float newVolume){
        AudioListener.volume = newVolume;
    }
}
