using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOutput : MonoBehaviour
{
    public AudioSource sound;
    private bool played = false;

    public GameObject current_clip;
    public GameObject next_clip;

    public int time_to_next_clip = 5;

    void OnTriggerEnter(Collider other){
        var color = GetComponent<Renderer>();
        if(played == false){
            sound.Play();
            played = true;
            color.material.SetColor("_Color", Color.red);
            StartCoroutine(timer());
            //next_clip.SetActive(true);
            current_clip.transform.position = new Vector3(10, 10, 10);
        }
    }

    IEnumerator timer(){
        yield return new WaitForSeconds(time_to_next_clip);
        next_clip.SetActive(true);
    }
}
