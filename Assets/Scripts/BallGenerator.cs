using System.Collections;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab = default;
    [SerializeField] private Sprite[] ballSprites = default;
    [SerializeField] private Sprite bombSprite = default;

    public IEnumerator Spawns(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-0.2f, 0.2f), 8f);
            GameObject ball = Instantiate(ballPrefab, pos, Quaternion.identity);

            int ballID = default;
            if (Random.Range(0.0f, 1.0f) < ParamsSO.Entity.bombRatio)
            {
                ballID = Ball.BOMB_ID;
                ball.GetComponent<SpriteRenderer>().sprite = bombSprite;
            }
            else
            {
                ballID = Random.Range(0, ballSprites.Length);
                ball.GetComponent<SpriteRenderer>().sprite = ballSprites[ballID];
            }
            ball.GetComponent<Ball>().id = ballID;

            yield return new WaitForSeconds(0.04f);
        }
    }
}