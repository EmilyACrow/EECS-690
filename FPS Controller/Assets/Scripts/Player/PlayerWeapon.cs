using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] MachineGunItem weapon;
    [SerializeField] public NodeTypeA nodeA;
    [SerializeField] private Transform wPlayer;

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
            }
        } else {
            nodeA._nodeName.SetActive(false);
        }


        if (Input.GetKeyDown("o")){
            nodeA.ReleaseDeactivate();

        }
    }
}
