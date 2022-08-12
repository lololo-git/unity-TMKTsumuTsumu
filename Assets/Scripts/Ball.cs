using UnityEngine;

public class Ball : MonoBehaviour
{
    public const float BALL_ACTIVE_SCALE = 1.4f;

    public int id;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Activate()
    {
        this.transform.localScale = Vector3.one * BALL_ACTIVE_SCALE;
    }

    public void Unactivate()
    {
        this.transform.localScale = Vector3.one * 1.0f;
    }
}