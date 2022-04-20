using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public Image HealthBackgroundImage;
    public Image HealthFillImage;
	private PlayerController PlayerController;
    private GameObject PLayer;
    private HealthScript HealthScript;

    // Start is called before the first frame update
    void Start()
    {
        PLayer = GameObject.Find("Player");
        PlayerController = PLayer.GetComponent<PlayerController>();
        HealthScript = PlayerController.PlayerHealthScript;
    }

    // Update is called once per frame
    void Update()
    {
        if(HealthScript != null)
        {
            // Debug.Log("Health update " );
            HealthFillImage.fillAmount = HealthScript.CurrentHealth / HealthScript.TotalHealth;
        }
        else {
            Debug.Log("HealthScript == null; gameobject name: " + gameObject.name );
        }
    }
}
