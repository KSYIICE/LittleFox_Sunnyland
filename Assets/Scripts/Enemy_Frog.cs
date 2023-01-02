using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    
    //private Animator anim;
    private Collider2D coll;

    public LayerMask Map;
    private bool faceleft = true;
    public float speed, jumpforce;
    public float leftx, rightx;


    public Transform rightpoint, leftpoint;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        transform.DetachChildren();//子集不继承本角色代码
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }
        // Update is called once per frame
    void Update()
    {
            SwitchAnim();
    }

    void Movement()
    {
        if (faceleft)//面向左边
        {
            if (coll.IsTouchingLayers(Map))
            {
                    anim.SetBool("jumping", true);
                    rb.velocity = new Vector2(-speed, jumpforce);
            }
        }
            else//面向右边
            {
                if (coll.IsTouchingLayers(Map))
                {
                    anim.SetBool("jumping", true);
                    rb.velocity = new Vector2(speed, jumpforce);
                }

            }
    }
    void SwitchAnim()
    {
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1)
            {
                    anim.SetBool("jumping", false);
                    anim.SetBool("downing", true);
            }
        }
        if (coll.IsTouchingLayers(Map) && anim.GetBool("downing"))
        {
                anim.SetBool("downing", false);
        }
        if (coll.IsTouchingLayers(Map))
        {
            if (faceleft)//面向左边
            {
                if (transform.position.x < leftx)
                {
                        transform.localScale = new Vector3(-1, 1, 1);
                        faceleft = false;
                }
            }
            else//面向右边
            {

                if (transform.position.x > rightx)
                {
                        transform.localScale = new Vector3(1, 1, 1);
                        faceleft = true;
                }
            }
        }
    }
}

    

