using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    public TextMeshPro CurrentAmmo;
    public TextMeshPro TotallAmmo;
    // Start is called before the first frame update
    void Start()
    {

        MachineGunItem handl = gameObject.GetComponent(typeof(MachineGunItem)) as MachineGunItem;
        TotallAmmo.text = handl.TotallAmmo.ToString();
        CurrentAmmo.text = TotallAmmo.text;
        
    }

    // Update is called once per frame
    void Update()
    {
        MachineGunItem handl = gameObject.GetComponent(typeof(MachineGunItem)) as MachineGunItem;
        TotallAmmo.text = handl.TotallAmmo.ToString();
        CurrentAmmo.text = handl.CurrentAmmo.ToString();
    }
}
