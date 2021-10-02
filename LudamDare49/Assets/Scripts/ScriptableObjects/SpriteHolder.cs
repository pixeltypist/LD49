using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteHolder", menuName = "ScriptableObjects/Sprite Holder")]
public class SpriteHolder : ScriptableObject
{
    public Sprite sprite;

    public void SetSprite(Sprite _sprite)
    {
        sprite = _sprite;
    }
}
