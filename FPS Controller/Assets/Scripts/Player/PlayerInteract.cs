using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] public NodeTypeA nodeA;

    // Update is called once per frame
    void Update()
    {
        /*
        if(PauseMenu.isPaused == false){
            if (true){//distA < 3){ //removed by Jake to fix compilation error
                 nodeA._nodeName.SetActive(true);

                 if (Input.GetKeyDown("p")){
                    nodeA.PickupActivate();
                }
                if (nodeA._nodeIcon.activeSelf){
                nodeA._nodeName.SetActive(false);
             }
            }


            if (Input.GetKeyDown("o")){
                nodeA.ReleaseDeactivate();

            }
        } 
        */
    }
}
