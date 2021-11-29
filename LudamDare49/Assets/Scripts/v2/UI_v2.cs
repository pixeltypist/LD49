using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_v2 : MonoBehaviour
{
    public Image activeAttackSprite;
    public SpriteHolder activeAttackSpriteHolder;

    public void UpdateActiveAttackSprite()
    {
        activeAttackSprite.sprite = activeAttackSpriteHolder.sprite;
    }
}
