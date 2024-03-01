using UnityEngine;

public class arrow : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    public int damage;
    float rot;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("healPoint");

        direction = player.transform.position - transform.position;
        rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.45f){
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        }
        if(timer > 5){
            Destroy(gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.layer == 7 && !other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(2).IsName("surfers") && timer >=0.45f){
            other.GetComponent<player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
