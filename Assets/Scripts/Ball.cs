using UnityEngine;

public class Ball : MonoBehaviour
{
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
        this.transform.localScale = Vector3.one * ParamsSO.Entity.activeBallScale;
    }

    public void Unactivate()
    {
        this.transform.localScale = Vector3.one * ParamsSO.Entity.defaultBallScale;
    }
}