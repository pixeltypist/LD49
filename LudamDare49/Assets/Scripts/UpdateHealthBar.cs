using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealthBar : MonoBehaviour
{
    public FloatVariable playerCurrentHealth;
    public FloatVariable playerMaxHealth;
    public Image slider;

    public void UpdateUI()
    {
        slider.fillAmount = playerCurrentHealth.Value/playerMaxHealth.Value;
    }
}
