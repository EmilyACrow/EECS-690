using Unity;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image HealthFillImage;
    public Image HealthBackground;

    Health _PlayerHealth;

    void Start()
    {
        PlayerController playerController = GameObject.FindObjectOfType<PlayerController>();
        // DebugUtility.HandleErrorIfNullFindObject<PlayerController, PlayerHealthBar>(playerController, this);

        _PlayerHealth = playerController.GetComponent<Health>();
        // DebugUtility.HandleErrorIfNullGetComponent<Health, PlayerHealthBar>(m_PlayerHealth, this,
        //     playerController.gameObject);
    }

    void Update()
    {
        // update health bar value
        // HealthFillImage.fillAmount = _PlayerHealth / 10;
        HealthFillImage.fillAmount = 0.3f;
        
    }
}
