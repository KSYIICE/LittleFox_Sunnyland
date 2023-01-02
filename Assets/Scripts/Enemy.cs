using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource deathAudio;
    protected Rigidbody2D rb;
    protected CircleCollider2D circleCollider2D;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        transform.GetComponent<CircleCollider2D>().enabled = true;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        deathAudio.Play();
        transform.GetComponent <CircleCollider2D> ().enabled = false;
        anim.SetTrigger("death");
    }
}

