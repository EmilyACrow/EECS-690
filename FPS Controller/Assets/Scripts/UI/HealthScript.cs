using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [Header("PlayerHealth Parameters")]
    [SerializeField] public float TotalHealth = 100f;
    [SerializeField] public float CurrentHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if(CurrentHealth <= 0) {
            // Destroy(gameObject);
            Debug.Log(name + "is Dead(destroy method is not implement)" );

        }
    }

    public void ApplyDamage(float damage)
    {
        CurrentHealth -= damage;
    }
}
