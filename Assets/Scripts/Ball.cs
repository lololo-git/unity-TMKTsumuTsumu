using UnityEngine;

public class Ball : MonoBehaviour
{
    public int id;

    // Start is called before the first frame update
    private void Start()
    {
        this.Unactivate();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Activate()
    {
        this.transform.localScale = Vector3.one * ParamsSO.Entity.activeBallScale;
        this.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public void Unactivate()
    {
        this.transform.localScale = Vector3.one * ParamsSO.Entity.defaultBallScale;
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}