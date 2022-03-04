using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] MachineGunItem _weapon;
    // [SerializeField] GameObject[] _weapons;
    // [SerializeField] GameObject _bullet;
    // [SerializeField] GameObject _bulletSpawnPoint;
    // [SerializeField] GameObject _muzzleFlash;
    // [SerializeField] float _rateOfFire = 0.2f; //Fire rate seconds between shot
    // [SerializeField] float _bulletVelocity = 1500;
    // Start is called before the first frame update

    // private bool _isFiring = false;
    // private float _bulletLifetime = 2.0f;

    // Update is called once per frame
    void Update()
    {
        //Check if player is pressing fire button
        if(Input.GetMouseButtonDown(0)) {
            //If true, try to fire weapon
            IWeapon canShoot = _weapon.GetComponent<IWeapon>();
            if (canShoot != null) { _weapon.Fire(); }
        }

        if (Input.GetMouseButtonUp(0)) {
            _weapon.StopFiring();
        }
        
    }

    // IEnumerator Firing () {
    //     while(_isFiring) {
    //         //Rotate muzzle flash
    //         _muzzleFlash.transform.eulerAngles = new Vector3(
    //             _muzzleFlash.transform.eulerAngles.x,
    //             _muzzleFlash.transform.eulerAngles.y,
    //             Random.Range(0, 360) 
    //         );
    //         //Instantiate bullet
    //         GameObject bullet = Instantiate(_bullet, 
    //             _bulletSpawnPoint.transform.position, 
    //             Quaternion.Euler(transform.eulerAngles.x + 90, transform.eulerAngles.y, transform.eulerAngles.z));
    //         bullet.SendMessage("StartWithParameters", _bulletLifetime);
    //         //FIre bullet in the direction
    //         bullet.GetComponent<Rigidbody>().velocity = _bulletSpawnPoint.transform.forward * _bulletVelocity * Time.deltaTime;
    //         yield return new WaitForSeconds(_rateOfFire);
    //     }
        
    // }
    
}
