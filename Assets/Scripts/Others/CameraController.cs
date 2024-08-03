using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 velocity;
    [SerializeField] Vector3 posOffset;

    [Range(0, 1)]
    public float smoothTime;
    [SerializeField] Vector2 xLimit;
    [SerializeField] Vector2 yLimit;
    public Animator animator;

    private void LateUpdate()
    {
        Vector3 targetPos = GameManager.instance.player.transform.position + posOffset;
        targetPos = new Vector3(Mathf.Clamp(targetPos.x, xLimit.x, xLimit.y), Mathf.Clamp(targetPos.y, yLimit.x, yLimit.y), -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }


}
