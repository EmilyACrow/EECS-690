using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI totalAmmo;
    public MachineGunItem gun;
    // Start is called before the first frame update
    void Start()
    {
        totalAmmo.text = gun.totalAmmo.ToString();
        currentAmmo.text = totalAmmo.text;
        
    }

    // Update is called once per frame
    void Update()
    {
        totalAmmo.text = gun.totalAmmo.ToString();
        currentAmmo.text = gun.currentAmmo.ToString();
    }
}
