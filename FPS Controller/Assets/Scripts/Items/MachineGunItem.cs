using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunItem : MonoBehaviour, IWeapon
{
    [SerializeField] GameObject _model;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _bulletSpawnPoint;
    [SerializeField] GameObject _muzzleFlash;
    [SerializeField] float _rateOfFire = 0.2f; //Fire rate seconds between shot
    [SerializeField] float _bulletVelocity = 1500;
    [SerializeField] float gunshotVolume = 0.7f;

    private bool _isFiring = false;
    private float _bulletLifetime = 2.0f;
    private float _muzzleFlareTime = 0.1f;
    public int totalAmmo;
    public int currentAmmo;

    [SerializeField] private AudioSource testAudio = default;
    [SerializeField] private AudioClip[] testAudio2 = default;


    // Start is called before the first frame update
    void Start() {
        totalAmmo = 999;
        currentAmmo = 169;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire() {
        _muzzleFlash.GetComponent<Renderer>().enabled = true;
        _isFiring = true;
        StartCoroutine(Firing());
        
    }

    public void StopFiring() {
        _muzzleFlash.GetComponent<Renderer>().enabled = false;
        _isFiring = false;
    }

    public void Reload() {

    }

    public void Activate() {

    }

    public void Deactivate() {

    }

    IEnumerator Firing () {
        while(_isFiring) {

            testAudio.PlayOneShot(testAudio2[Random.Range(0, testAudio2.Length-1)], gunshotVolume); //Sound generation

            //Rotate muzzle flash
            _muzzleFlash.transform.eulerAngles = new Vector3(
                _muzzleFlash.transform.eulerAngles.x,
                _muzzleFlash.transform.eulerAngles.y,
                Random.Range(0, 360) 
            );
            //Instantiate bullet
            GameObject bullet = Instantiate(_bullet, 
                _bulletSpawnPoint.transform.position, 
                Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
            bullet.SendMessage("StartWithParameters", _bulletLifetime);
            //Fire bullet in the direction
            bullet.GetComponent<Rigidbody>().velocity = _bulletSpawnPoint.transform.forward * _bulletVelocity * Time.deltaTime;
            yield return new WaitForSeconds(_rateOfFire);
        }
        
    }
}
