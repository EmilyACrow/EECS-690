using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(Cleanup());
    }

    void Update() {
        
    }

    IEnumerator Cleanup() {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
