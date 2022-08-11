using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{

    [SerializeField] BallGenerator ballGenerator = default;
    bool isDragging;
    [SerializeField] List<Ball> removeBalls = new List<Ball>();

    void Start()
    {
        StartCoroutine(ballGenerator.Spawns(40));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnDragBegin();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnDragEnd();
        }
        else if (isDragging)
        {
            OnDragging();
        }
    }

    void OnDragBegin()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit)
        {
            Ball ball = hit.collider.GetComponent<Ball>();
            if (!ball)
                return;

            AddRemoveBall(ball);
            isDragging = true;
        }
    }

    void OnDragging()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit)
        {
            Ball ball = hit.collider.GetComponent<Ball>();
            if (!ball)
                return;

            AddRemoveBall(ball);
        }
    }
    void OnDragEnd()
    {
        int removeCount = removeBalls.Count;
        for (int i = 0; i < removeCount; i++)
        {
            Destroy(removeBalls[i].gameObject);
        }
        removeBalls.Clear();

        isDragging = false;
    }

    void AddRemoveBall(Ball ball)
    {
        if (removeBalls.Contains(ball) == false) {
            removeBalls.Add(ball);
        }
    }
}
