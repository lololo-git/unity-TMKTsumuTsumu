﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private BallGenerator ballGenerator = default;
    [SerializeField] private List<Ball> removeBalls = new List<Ball>();
    [SerializeField] private Text scoreText = default;
    [SerializeField] private GameObject pointEffectPrefab = default;

    private Ball currentDraggingBall;
    private bool isDragging;
    private int score;

    private void Start()
    {
        score = 0;
        StartCoroutine(ballGenerator.Spawns(ParamsSO.Entity.initBallCount));
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
            if (ball.isBomb())
            {
                ExplodeBomb(ball);
            }
            else
            {
                AddRemoveBall(ball);
                isDragging = true;
            }
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
            if (distance < ParamsSO.Entity.ballDistance)
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
            ExplodeBalls(removeBalls);
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

    private void ExplodeBalls(IEnumerable<Ball> balls, bool withEffect = true)
    {
        int count = default;
        foreach (Ball ball in balls)
        {
            ball.Explode();
            count++;
        }
        StartCoroutine(ballGenerator.Spawns(count));

        if (withEffect)
        {
            UpdateScoreWithEffect(balls.Last().transform.position, count);
        }
    }

    private void ExplodeBomb(Ball bomb)
    {
        Collider2D[] hitObj = Physics2D.OverlapCircleAll(
            bomb.transform.position, ParamsSO.Entity.BombExplosionRadius);

        List<Ball> explosionList = new List<Ball>();
        for (int i = 0; i < hitObj.Length; i++)
        {
            Ball ball = hitObj[i].GetComponent<Ball>();
            if (ball)
            {
                explosionList.Add(ball);
            }
        }

        // Withoud effect. Effect raise from bomb
        ExplodeBalls(explosionList, false);
        UpdateScoreWithEffect(bomb.transform.position, explosionList.Count());
    }

    private void UpdateScoreWithEffect(Vector2 effectPos, int ballCount)
    {
        Debug.Log(ballCount);
        int adding = ballCount * ParamsSO.Entity.scorePoint;

        GameObject effectObj = Instantiate(
            pointEffectPrefab, effectPos, Quaternion.identity);
        PointEffect pointEffect = effectObj.GetComponent<PointEffect>();
        pointEffect.Show(adding);
        Destroy(effectObj, ParamsSO.Entity.pointEffectRemain);

        score += adding;
        scoreText.text = score.ToString();
    }
}