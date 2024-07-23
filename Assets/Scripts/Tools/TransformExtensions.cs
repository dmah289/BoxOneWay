using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void ResetTransform(this Transform transform, Component item)
    {
        transform.parent = null;
        transform.position = item.gameObject.transform.position;
        transform.eulerAngles = item.gameObject.transform.eulerAngles;
        transform.localScale = item.gameObject.transform.localScale;
    }
}
