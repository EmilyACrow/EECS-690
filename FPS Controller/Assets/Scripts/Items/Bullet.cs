using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update() {
        
    }

    void StartWithParameters(float time) {
        StartCoroutine(Cleanup(time));
    }

    IEnumerator Cleanup(float time) {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
