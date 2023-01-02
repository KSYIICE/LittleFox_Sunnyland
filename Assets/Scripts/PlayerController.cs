using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;  //[SerializeField]可以使private变量显示
    public Collider2D coll;
    public BoxCollider2D disColl;
    public Transform tapPoint;
    public float speed = 400;
    public float jumpSpeed;
    public int Cherry;
    public AudioSource jumpAudio;
    public AudioSource harmAduio;
    public AudioSource CherryAduio;

    public LayerMask Map;

    private Animator anim;

    public Text CherryNumber;

    private int twojump;

    private bool isHurt,isJump;//默认是false

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            isJump = true;
        }
        Crouch();
        CherryNumber.text = Cherry.ToString();
    }

    private void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();
        if(isJump)
        {
            Jump();
        }
    }

    void Movement()
    {
        float move = Input.GetAxis("Horizontal");
        float face = Input.GetAxisRaw("Horizontal");

        if(move != 0)
        {
            rb.velocity = new Vector2(move * speed * Time.fixedDeltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(face));
        }
        if(face != 0)
        {
            transform.localScale = new Vector3(face, 1, 1);
        }
    }

    void Jump()
    {
        if (twojump > 1)
            {

                anim.SetBool("jumping", true);
                twojump--;
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                jumpAudio.Play();
                isJump = false;
            }
        if(coll.IsTouchingLayers(Map))
        {
            twojump = 2;
        }
    }

    void Crouch()
    {
        if(!Physics2D.OverlapCircle(tapPoint.position,0.2f,Map))
        {
            if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("crouching", true);
                transform.GetComponent<BoxCollider2D>().enabled = false;
                speed = 100;
            }
            else
            {
                transform.GetComponent<BoxCollider2D>().enabled = true;
                speed = 400;
                anim.SetBool("crouching", false);
            }
        }
    }

    void SwitchAnim()
    {
        anim.SetBool("idle", false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(Map))
        {
            anim.SetBool("downing", true);
            anim.SetBool("idle", false);
        }
        if (anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("downing", true);
            }
        }
        else if(isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running",0);
            if (Mathf.Abs(rb.velocity.x)<0.1f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if(coll.IsTouchingLayers(Map))
        {
            anim.SetBool("downing", false);
            anim.SetBool("idle", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集樱桃
        if(collision.tag == "Cherry")
        {
            CherryAduio.Play();
            collision.GetComponent<Animator>().Play("isGet");
        }
        if(collision.tag == "Deadline")
        {
            GetComponent<AudioSource>().enabled = false;
            anim.SetBool("hurt", true);
            Invoke(nameof(ReStart), 1f);//延迟1s执行ReStart函数
        }
    }

    public void CherryGet()
    {
        Cherry++;
    }

    //碰到敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemies") 
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("downing"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                anim.SetBool("jumping", true);
            }else if(transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                harmAduio.Play();
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                harmAduio.Play();
                isHurt = true;
            }
        }
        
    }

    void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//重置当前场景（SceneManager.GetActiveScene().name为获得当前场景的名字）
    }
}
