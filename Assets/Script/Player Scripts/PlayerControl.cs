using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public bool fire = false;
    [HideInInspector]
    public bool power = false;
    [HideInInspector]
    public bool watch = false;


    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float vie = 100;
    public AudioClip[] jumpClips;
    public float jumpForce = 1000f;
    public BallShoot ballPrefab;
    private Transform groundCheck;
    private Transform gunCheck;
    private bool grounded = false;
    private Animator anim;

    private float canShoot = 0;
    private float invulnerable = 0;

    void Awake()
    {
        groundCheck = transform.Find("groundCheck");
        gunCheck = transform.Find("gunCheck");
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;

        if (Input.GetButtonDown("Fire1"))
            fire = true;
        else if (Input.GetButtonUp("Fire1"))
            fire = false;
        if (Input.GetButtonDown("Fire2"))
            power = true;
        else if (Input.GetButtonUp("Fire2"))
            power = false;
        if (Input.GetButtonDown("Fire3"))
            watch = true;
        else if (Input.GetButtonUp("Fire3"))
            watch = false;
    }


    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (h != 0)
            anim.SetBool("Run", true);
        else
            anim.SetBool("Run", false);
        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();
        if (jump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
        if (!grounded)
        {
            anim.SetBool("Jump", true);
        }
        else
            anim.SetBool("Jump", false);

        if (watch)
        {
            anim.SetBool("Watch", true);
            Time.timeScale = 0.0f;
            vie = 100;
        }
        else
            anim.SetBool("Watch", false);
        if (power)
        {
            anim.SetBool("Power", true);
        }
        else
            anim.SetBool("Power", false);

        if (fire)
        {
            anim.SetBool("Shoot", true);
            if (canShoot <= 0)
            {
                Instantiate(ballPrefab, gunCheck.position, Quaternion.identity);
                GameObject[] ball = GameObject.FindGameObjectsWithTag("ball");
                BallShoot ballR = ball[ball.Length - 1].GetComponent<BallShoot>();
                ballR.theParent = this.gameObject;
                canShoot = 0.3f;
            }
        }
        else
            anim.SetBool("Shoot", false);

        canShoot -= Time.deltaTime;
        invulnerable -= Time.deltaTime;
    }
    public void getHurt(float lessLife)
    {
        if (invulnerable <= 0)
        {
            vie -= lessLife;
            invulnerable = 0.5f;
            if (vie <= 0)
                Time.timeScale = 0.0f;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "mechant")
        {
            print("mechant");
            if (invulnerable <= 0)
            {
                jump = true;
                getHurt(25);
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}