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
        currentAmmo.text = gun.currentAmmo.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        totalAmmo.text = gun.totalAmmo.ToString();
        currentAmmo.text = gun.currentAmmo.ToString();

        if(currentAmmo.text == "0"){
            currentAmmo.color = new Color32(255,0,0,255);
        } else {
            currentAmmo.color = new Color32(255,255,255,255);
        }


        if(totalAmmo.text == "0"){
            totalAmmo.color = new Color32(150,0,0,255);
        } else {
            totalAmmo.color = new Color32(255,255,255,255);
        }
    }
}
