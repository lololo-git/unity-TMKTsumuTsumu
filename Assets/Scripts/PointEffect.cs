using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointEffect : MonoBehaviour
{
    [SerializeField] private Text text = default;

    public void Show(int score)
    {
        text.text = score.ToString();
        StartCoroutine(MoveUp());
    }

    private IEnumerator MoveUp()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Translate(0, 0.1f, 0);
        }
        Destroy(gameObject, 0.2f);
    }
}