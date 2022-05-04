using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] MachineGunItem weapon;
<<<<<<< Updated upstream
    [SerializeField] public NodeTypeA nodeA;
    [SerializeField] private Transform wPlayer;

=======
>>>>>>> Stashed changes
    // Update is called once per frame
    void Update()
    {
        float distA = Vector3.Distance(nodeA.getModel().transform.position, wPlayer.position);
        //Check if player is pressing fire button
        if(Input.GetMouseButtonDown(0)) {
            //If true, try to fire weapon
            IWeapon canShoot = weapon.GetComponent<IWeapon>();
            if (canShoot != null) {
                if (weapon.currentAmmo > 0){
                weapon.Fire();
                }
            }
        }

<<<<<<< Updated upstream
        if (Input.GetMouseButtonUp(0)) {
            weapon.StopFiring();
        }

        if (Input.GetKeyDown("r")){
            weapon.Reload();
        }

        if (distA < 3 && nodeA._nodeModel.activeSelf){
             nodeA._nodeName.SetActive(true);

             if (Input.GetKeyDown("p")){
                nodeA.PickupActivate();
=======
                if (Input.GetMouseButtonUp(0)) {
                    weapon.StopFiring();
                }
                if (Input.GetKeyDown("r")){
                    weapon.StopFiring();
                    MachineGunItem myW = weapon.GetComponent<MachineGunItem>();
                    myW._model.transform.position = myW._model.transform.position - new Vector3(0,(float)0.2,0);
                    weapon.Reload();
                    Invoke("resetPos", (float)myW._reloadTime);
                }
>>>>>>> Stashed changes
            }
        } else {
            nodeA._nodeName.SetActive(false);
        }


        if (Input.GetKeyDown("o")){
            nodeA.ReleaseDeactivate();

        }
    }
    void resetPos(){
        MachineGunItem myW = weapon.GetComponent<MachineGunItem>();
        myW._model.transform.position = myW._model.transform.position + new Vector3(0,(float)0.2,0);
    }
}
