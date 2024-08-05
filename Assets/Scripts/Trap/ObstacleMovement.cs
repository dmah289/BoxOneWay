using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] Vector2 targetPos;
    [Range(0,5)]
    [SerializeField] float speed;

    [SerializeField] Transform path;
    Vector2[] points;
    int currentPointIndex;
    int direction;

    [Range(0,2)]
    [SerializeField] float waitDuration;
    [SerializeField] int speedMultiplier = 1;

    private void Awake()
    {
        points = new Vector2[path.transform.childCount];
        for(int i = 0; i < points.Length; i++)
        {
            points[i] = path.GetChild(i).position;
        }
    }

    private void OnEnable()
    {
        currentPointIndex = 1;
        direction = 1;
        targetPos = points[currentPointIndex];
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speedMultiplier * speed * Time.deltaTime);

        if((Vector2)transform.position == targetPos)
            NextPoint();
    }

    private void NextPoint()
    {
        if (currentPointIndex == 0 || currentPointIndex == points.Length - 1)
            direction *= -1;

        currentPointIndex += direction;
        targetPos = points[currentPointIndex];
        StartCoroutine(WaitNextPoint());
    }

    IEnumerator WaitNextPoint()
    {
        speedMultiplier = 0;
        yield return new WaitForSeconds(waitDuration);
        speedMultiplier = 1;
    }
}
