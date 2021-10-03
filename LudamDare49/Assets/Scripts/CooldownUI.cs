using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public Image cooldown;
    float totalCooldown, totalTimeLeft;

    void AdjustSlider()
    {
        cooldown.fillAmount = 1 - (totalTimeLeft/totalCooldown);
    }
}
