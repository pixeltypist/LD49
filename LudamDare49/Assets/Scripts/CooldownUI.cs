using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public Image cooldown;
    //float totalCooldown, totalTimeLeft;

    public void AdjustSlider(float totalTime, float timeLeft)
    {
        cooldown.fillAmount = 1 - (timeLeft/totalTime);
    }
}
