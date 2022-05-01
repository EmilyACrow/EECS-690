using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pickup : MonoBehaviour
{
    public int totalNodes = 0;

    public TMP_Text nodeCounter;

    public GameObject node;

    public bool isPicked = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       changeCounter();
    }

    public void changeCounter(){
        if(totalNodes == 0){
            nodeCounter.text = "Data Nodes: 0";
        }
        if(totalNodes == 1){
            nodeCounter.text = "Data Nodes: 1";
        }
        if(totalNodes == 2){
            nodeCounter.text = "Data Nodes: 2";
        }
        if(totalNodes == 3){
            nodeCounter.text = "Data Nodes: 3";
        }
        if(totalNodes == 4){
            nodeCounter.text = "Data Nodes: 4";
        }
        if(totalNodes == 5){
            nodeCounter.text = "Data Nodes: 5";
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "Player"){
            totalNodes = totalNodes + 1;
            Debug.Log("Picked up Node");
            node.transform.position = new Vector3(-10, -10, -10);
        }
    }

}
