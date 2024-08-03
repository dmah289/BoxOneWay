using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 targetPos;

    PlayerMovement playerMovement;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector3 moveDirection;

    [SerializeField] GameObject path;
    [SerializeField] Transform[] points;
    int currentPointIdx;
    public float waitDuration;

    private void Awake()
    {
        points = new Transform[path.transform.childCount];
        for (int i = 0; i < path.transform.childCount; i++)
        {
            points[i] = path.transform.GetChild(i).transform;
        }
    }

    private void OnEnable()
    {
        currentPointIdx = 0;
        targetPos = points[0].transform.position;
        transform.position = points[0].transform.position;
        moveDirection = (targetPos - transform.position).normalized;
    }

    private void Start()
    {
        playerMovement = GameManager.instance.player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
            NextTargetPoint();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    private void NextTargetPoint()
    {
        transform.position = targetPos;
        moveDirection = Vector3.zero;

        currentPointIdx = (currentPointIdx+1) % (2 * points.Length - 1);
        int idx = currentPointIdx;
        idx = idx < points.Length ? idx : 2 * points.Length - 2 - idx;

        targetPos = points[idx].transform.position;
        StartCoroutine(WaitNextPoint());
    }

    private IEnumerator WaitNextPoint()
    {
        yield return new WaitForSeconds(waitDuration);
        moveDirection = (targetPos - transform.position).normalized;
    }
}
