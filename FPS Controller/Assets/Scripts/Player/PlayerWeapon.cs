using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] MachineGunItem weapon;

    // Update is called once per frame
    void Update()
    {
        if(WinCondition.isWin == false){
            if(PauseMenu.isPaused == false){
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
            }
        }

    }
}
