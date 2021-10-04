using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SpriteHolder", menuName = "ScriptableObjects/Sprite Holder")]
public class SpriteHolder : ScriptableObject
{
    public Sprite sprite;

    //public Image imageUI;

    public GameObject mapSegment;

    public void SetSprite(Sprite _sprite)
    {
        sprite = _sprite;
    }

    public void RevealMap()
    {
        mapSegment.GetComponent<Image>().enabled = false;
    }

    public void TogglePlayer()
    {

    }
}
