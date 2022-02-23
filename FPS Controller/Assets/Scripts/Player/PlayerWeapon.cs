using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] GameObject[] _weapons;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _bulletSpawnPoint;
    [SerializeField] GameObject _muzzleFlash;
    [SerializeField] float _rateOfFire = 0.2f; //Fire rate seconds between shot
    [SerializeField] float _bulletVelocity = 1500;
    // Start is called before the first frame update

    private bool _isFiring = false;

    // Update is called once per frame
    void Update()
    {
        //Check if player is pressing fire button
        if(Input.GetMouseButtonDown(0)) {
            _muzzleFlash.GetComponent<Renderer>().enabled = true;
            _isFiring = true;
            StartCoroutine(Firing());
        } 

        if(Input.GetMouseButtonUp(0)) {
            _muzzleFlash.GetComponent<Renderer>().enabled = false;
            _isFiring = false;
        }
        //If true, fire weapon
        
    }

    IEnumerator Firing () {
        while(_isFiring) {
            //Rotate muzzle flash
            _muzzleFlash.transform.eulerAngles = new Vector3(
                _muzzleFlash.transform.eulerAngles.x,
                _muzzleFlash.transform.eulerAngles.y,
                Random.Range(0, 360) 
            );
            //Instantiate bullet
            GameObject bullet = Instantiate(_bullet, _bulletSpawnPoint.transform.position, Quaternion.identity);
            //FIre bullet in the direction
            bullet.GetComponent<Rigidbody>().velocity = _bulletSpawnPoint.transform.forward * _bulletVelocity * Time.deltaTime;
            yield return new WaitForSeconds(_rateOfFire);
        }
        
    }
    
}
