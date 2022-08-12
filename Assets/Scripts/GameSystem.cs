using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private BallGenerator ballGenerator = default;
    [SerializeField] private List<Ball> removeBalls = new List<Ball>();
    [SerializeField] private Text scoreText = default;

    private Ball currentDraggingBall;
    private bool isDragging;
    private int score;

    private void Start()
    {
        score = 0;
        StartCoroutine(ballGenerator.Spawns(50));
    }

    private void Update()
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

    private void OnDragBegin()
    {
        Ball ball = GetPointededBall();
        if (ball)
        {
            AddRemoveBall(ball);
            isDragging = true;
        }
    }

    private void OnDragging()
    {
        Ball ball = GetPointededBall();
        if (ball && ball.id == currentDraggingBall.id)
        {
            float distance = Vector2.Distance(
                ball.transform.position,
                currentDraggingBall.transform.position);
            if (distance < 1.5)
            {
                AddRemoveBall(ball);
            }
        }
    }
    private void OnDragEnd()
    {
        int removeCount = removeBalls.Count;
        if (removeCount >= 3)
        {
            for (int i = 0; i < removeCount; i++)
            {
                Destroy(removeBalls[i].gameObject);
            }
            StartCoroutine(ballGenerator.Spawns(removeCount));
            AddScore(removeCount * 100);
        }
        for (int i = 0; i < removeCount; i++)
        {
            removeBalls[i].Unactivate();
        }
        removeBalls.Clear();

        isDragging = false;
    }

    private void AddScore(int point)
    {
        score += point;
        scoreText.text = score.ToString();
    }

    private Ball GetPointededBall()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (!hit)
            return null;
        Ball ball = hit.collider.GetComponent<Ball>();
        return ball ? ball : null;
    }

    private void AddRemoveBall(Ball ball)
    {
        currentDraggingBall = ball;
        if (removeBalls.Contains(ball) == false)
        {
            removeBalls.Add(ball);
            ball.Activate();
        }
    }
}