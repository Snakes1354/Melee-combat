using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
   public Slider healthSlider;
   public TMP_Text healthText;
   public PlayerStats playerStats;
   
   private void Start()
   {
       UpdateHealthText();
   }
   public void SetSlider(float amount)
   {
       healthSlider.value = amount;
       UpdateHealthText();
   }
   public void SetSliderMax(float amount)
   {
       healthSlider.maxValue = amount;
       healthSlider.value = amount;
       UpdateHealthText();
   }
   private void UpdateHealthText()
   {
       healthText.text =
           playerStats.CurrentHealth + " / " + playerStats.MaxHealth;
   }
}