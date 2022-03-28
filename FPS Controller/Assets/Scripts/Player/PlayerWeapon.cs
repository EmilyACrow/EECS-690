using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] MachineGunItem _weapon;

    // Update is called once per frame
    void Update()
    {
        //Check if player is pressing fire button
        if(Input.GetMouseButtonDown(0)) {
            //If true, try to fire weapon
            IWeapon canShoot = _weapon.GetComponent<IWeapon>();
            if (canShoot != null) { 
                _weapon.Fire(); 
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            _weapon.StopFiring();
        }    
    }
}
