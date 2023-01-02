using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy
{

    //private Collider2D coll;

    public Transform UpPoint,UnderPoint;
    public float speed;
    public float upy, undery;
    private bool facedown = true;
    // Start is called before the first frame update
    protected  override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //coll = GetComponent<Collider2D>();

        upy = UpPoint.position.y;
        undery = UnderPoint.position.y;
        Destroy(UpPoint.gameObject);
        Destroy(UnderPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (transform.position.y > upy)
        {
            facedown = true;
        }
        if(transform.position.y <undery)
        {
            facedown = false;
        }

        if(facedown)
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        
    }
}
