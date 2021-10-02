using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSetter : MonoBehaviour
{
    public TransformHold transformHold;

    private void Start() {
        transformHold.objTransform = transform;
    }
}
