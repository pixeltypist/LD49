using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowActiveWeapon : MonoBehaviour
{
    public Image image;

    public SpriteHolder sprite;

    public void UpdateSprite()
    {
        image.sprite = sprite.sprite;
    }
}
