using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class HealthBarScript : MonoBehaviour
{
    public Slider HealthBarSlider;
    public TextMeshProUGUI healthBarValueText;

    public int maxHealth;
    public int currHealth;

    void Start()
    {
        currHealth = maxHealth;
    }

    void Update()
    {
        healthBarValueText.text = currHealth.ToString() + "/" + maxHealth.ToString();

        HealthBarSlider.value = currHealth;
        HealthBarSlider.maxValue = maxHealth;
    }
}
