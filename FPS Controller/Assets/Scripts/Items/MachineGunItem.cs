using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MachineGunItem : MonoBehaviour, IWeapon
{
    [SerializeField] GameObject _model;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _bulletSpawnPoint;
    [SerializeField] GameObject _muzzleFlash;
    [SerializeField] TextMeshProUGUI _ammoHeader;

    [SerializeField] float _rateOfFire = 0.2f; //Fire rate seconds between shot
    [SerializeField] float _bulletVelocity = 1500;
    [SerializeField] float gunshotVolume = 0.7f;

    private float _reloadTime = 1; //Reload time in seconds

    private bool _isFiring = false;
    private float _bulletLifetime = 2.0f;
    private float _muzzleFlareTime = 0.1f;
    public int totalAmmo;
    public int currentAmmo;
    private int magSize = 20;

    [SerializeField] private AudioSource testAudio = default;
    [SerializeField] private AudioClip[] testAudio2 = default;


    // Start is called before the first frame update
    void Start() {
        totalAmmo = 100;
        currentAmmo = magSize;
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

    private void ReloadHelper(){
        if (totalAmmo+currentAmmo <= magSize){ //If remaining ammo is less than magazine size, just take leftovers into mag.
            currentAmmo += totalAmmo;
            _ammoHeader.color = new Color32(255,0,0,255);
            totalAmmo = 0;
            return;
        } 
        else if(totalAmmo == 0){ //If no remaining ammo, keep value of current ammo count
        _ammoHeader.text = "No Ammo";
            return;
        } 
        else {
        //TODO:
        //Add reload
        //animation here 
        totalAmmo = totalAmmo - (magSize-currentAmmo);
        currentAmmo = magSize;
        _ammoHeader.text = "Ammo";
        _ammoHeader.color = new Color32(255,255,255,255);
        }
    }

    public void Reload() {
        if (currentAmmo == magSize){
            _ammoHeader.text = "Clip full";
            _ammoHeader.color = new Color32(255,0,0,255);
            Invoke("ReloadHelper", _reloadTime);
        } else {
        _ammoHeader.text = "Reloading...";
        _ammoHeader.color = new Color32(100,255,100,255);
        Invoke("ReloadHelper", _reloadTime);
        }
    }

    public void Activate() {

    }

    public void Deactivate() {

    }

    IEnumerator Firing () {
        while(_isFiring) {

            if (currentAmmo < 1){
                StopFiring();
                Invoke("Reload", 0.5f);
                break;
            }
            currentAmmo -= 1;
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
            //set bullet damage
            bullet.GetComponent<Bullet>().BulletDamage = 15f;
            //Fire bullet in the direction
            bullet.GetComponent<Rigidbody>().velocity = _bulletSpawnPoint.transform.forward * _bulletVelocity * Time.deltaTime;
            yield return new WaitForSeconds(_rateOfFire);
        }
        
    }
}
