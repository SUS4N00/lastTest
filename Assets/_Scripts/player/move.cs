using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    bool isGrounded;
    bool isJumping;
    float timming;
    float where;
    float stamina;
    float staminaDash;
    float staminaJump;
    private bool isFacingRight = true;
    public bool checkGround = false;
    public float speed;
    public float move;
    public float jumpHeight;
    public float footSize = 0.1f;
    public float dashSpeed;
    public float timeToNextDash;
    private Rigidbody2D rb;
    private Animator anim;
    public Transform jumpPoint;
    public LayerMask grLayer;
    public AudioManager audioManager;
    public BoxCollider2D boxColliderPlayer;

    void Start()
    {
        speed = 5f;
        dashSpeed = 10;
        jumpHeight = 5.7f;
        staminaDash = 15f;
        staminaJump = 15f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        stamina = GetComponent<player>().currentStamina;
        timming = Time.time;
        move = Input.GetAxis("Horizontal");
        if(GetComponent<player>().hited){
            move = 0f;
        }
        if(anim.GetCurrentAnimatorStateInfo(3).IsName("attack") || anim.GetCurrentAnimatorStateInfo(3).IsName("throw") || anim.GetCurrentAnimatorStateInfo(2).IsName("playerHealing")){
            move = 0f;
        }
        if(jumpPoint == null){
            checkGround = false;
        }
        Collider2D ground = Physics2D.OverlapCircle(jumpPoint.position, footSize, grLayer);
        
        isGrounded = ground != null;
        isJumping = !isGrounded && rb.velocity.y > 0.1f;

        if(timming > timeToNextDash && Input.GetKeyDown(KeyCode.L) && stamina >= staminaDash){
            GetComponent<player>().DecreaseStamina(staminaDash);
            dash();
        }
        jump(isGrounded);
        flip(move);
        //sound

        groundSound();
        //anim
        anim.SetBool("move", Mathf.Abs(move) > 0.1f);
        anim.SetBool("jump", isJumping);
    }

    void FixedUpdate()
    {
        //move = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(move, 0f);
        if(!anim.GetCurrentAnimatorStateInfo(2).IsName("dash")){

            rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);

            //Vector2 newPosition = rb.position + moveDirection * speed * Time.fixedDeltaTime;
            //rb.MovePosition(newPosition);
        }
    }

    void flip(float move)
    {
        if ((move > 0 && !isFacingRight) || (move < 0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            //Vector3 scale = transform.localScale;
            //scale.x *= -1;
            //transform.localScale = scale;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    void jump(bool isGrounded)
    {
        if (Input.GetKeyDown(KeyCode.K) && isGrounded && stamina >= staminaJump)
        {
            GetComponent<player>().DecreaseStamina(staminaJump);
            audioManager.playSfx(audioManager.jump);
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
    }

    void groundSound(){
        if(checkGround == false && jumpPoint != null){
            checkGround = true;
            audioManager.playSfx(audioManager.ground);
        }
    }

    void dash(){
        timeToNextDash = timming + 0.7f;
        if(Input.GetKey(KeyCode.A)){
            where = -1;
        }else if(Input.GetKey(KeyCode.D)){
            where = 1;
        }else{
            where = 0;
        }
        audioManager.playSfx(audioManager.surfers);
        Vector2 moveDirection = new Vector2(where, 0f);
        rb.velocity = new Vector2(moveDirection.x * dashSpeed, rb.velocity.y);
        anim.SetTrigger("dash");
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(jumpPoint.position, footSize);
    }
}