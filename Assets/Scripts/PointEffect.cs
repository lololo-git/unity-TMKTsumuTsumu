using UnityEngine;
using UnityEngine.UI;

public class PointEffect : MonoBehaviour
{
    [SerializeField] private Text text = default;

    public void Show(int score)
    {
        text.text = score.ToString();
    }
}