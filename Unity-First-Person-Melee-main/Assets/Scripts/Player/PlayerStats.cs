using UnityEngine;

public class PlayerStats : MonoBehaviour
{
   public float maxHealth = 100f;
   public float currentHealth;
   public float MaxHealth => maxHealth;
   public float CurrentHealth => currentHealth;
   public HealthBar healthBar;
   private void Start()
   {
       currentHealth = maxHealth;
       healthBar.SetSliderMax(maxHealth);
   }
   private void Update()
   {
       if (currentHealth > maxHealth)
           currentHealth = maxHealth;
       if (currentHealth <= 0)
           Die();
   }
   public void TakeDamage(float amount)
   {
       currentHealth -= amount;
       healthBar.SetSlider(currentHealth);
   }
   public void HealPlayer(float amount)
   {
       currentHealth += amount;
       healthBar.SetSlider(currentHealth);
   }
   private void Die()
   {
       Debug.Log("You died!");
   }
}
