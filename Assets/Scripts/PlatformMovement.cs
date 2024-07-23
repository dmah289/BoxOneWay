using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 targetPos;

    [SerializeField] MovementController movementController;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector3 moveDirection;

    [SerializeField] GameObject path;
    [SerializeField] Transform[] points;
    int pointIdx;
    int pointCnt;
    int direction;
    public float waitDuration;

    private void Awake()
    {
        points = new Transform[path.transform.childCount];
        for (int i = 0; i < path.transform.childCount; i++)
        {
            points[i] = path.transform.GetChild(i).transform;
        }
    }

    private void Start()
    {
        movementController = GameManager.instance.player.GetComponent<MovementController>();

        pointIdx = 0;
        pointCnt = points.Length;
        direction = 1;

        targetPos = points[0].transform.position;
        transform.position = points[0].transform.position;

        CalculateDirection();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, targetPos) < 0.05f)
            NextPoint();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementController.isOnPlatform = true;
            movementController.platformRb = rb;
            movementController.playerRb.gravityScale *= 50;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementController.isOnPlatform = false;
            movementController.platformRb = null;
            movementController.playerRb.gravityScale /= 50;

        }
    }

    private void CalculateDirection()
    {
        moveDirection = (targetPos - transform.position).normalized;
    }

    private void NextPoint()
    {
        transform.position = targetPos;

        moveDirection = Vector3.zero;

        if (pointIdx == pointCnt-1)
            direction = -1;

        if(pointIdx == 0)
            direction = 1;

        pointIdx += direction;
        targetPos = points[pointIdx].transform.position;
        StartCoroutine(WaitNextPoint());
    }

    private IEnumerator WaitNextPoint()
    {
        yield return new WaitForSeconds(waitDuration);
        CalculateDirection();
    }
}
