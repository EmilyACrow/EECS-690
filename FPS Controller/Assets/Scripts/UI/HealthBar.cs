using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Image HealthBackgroundImage;
    public Image HealthFillImage;
	public Transform Player;
    public float defaultHealth;
    public float currenthealth;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(true)
        {
            HealthFillImage.fillAmount = defaultHealth/currenthealth;
        }
    }
}
