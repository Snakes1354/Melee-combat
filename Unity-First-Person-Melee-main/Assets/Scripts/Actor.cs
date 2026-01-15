using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour
{
    int currentHealth;
    public int maxHealth;

    void Awake()
    {
        currentHealth = maxHealth;
        // Sets hp at the start.
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        // Makes it take damage.

        if(currentHealth <= 0)
        {
            Death();
            // Call Death method when it hits 0 hp.
        }
    }

    void Death()
    {
        Destroy(gameObject);
        // Death function
        // Temporary: Destroy Object
    }
}