using UnityEngine;

public class Ball : MonoBehaviour
{
    public const int BOMB_ID = -1;
    public int id;

    [SerializeField] private GameObject explosionPrefab = default;

    // Start is called before the first frame update
    private void Start()
    {
        this.Unactivate();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public bool isBomb()
    {
        return id == BOMB_ID;
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

    public void Explode()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(
            explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, ParamsSO.Entity.explosionRemain);
    }
}