using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DustFollowing : MonoBehaviour
{
    public GameObject Aim,Player,Bird;
    public float X,Y;
    Vector2 Dust;
    Rigidbody2D rigid;
    bool changeAble;
    SpriteRenderer Sp;
    Animator animator;
    // Start is called before the first frame update

    void Start()
    {
        //PlayerSize= Aim.GetComponent<BoxCollider2D>().size.x;
        rigid = GetComponent<Rigidbody2D>();
        Sp= GetComponent<SpriteRenderer>();
        animator = Player.GetComponent<Animator>();
        Sp.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        animator = Player.GetComponent<Animator>();
        if (animator.GetBool("Running") && !Player.GetComponent<PlayerMove>().PlayerSkilling)
        {
            Sp.enabled = true;
        }
        else
        if (!animator.GetBool("Running") && !Player.GetComponent<PlayerMove>().PlayerSkilling)
        {
            Sp.enabled = false;
        }
        // rigid.position = new Vector2(Aim.transform.position.x-X, Aim.transform.position.y-Y);
        //   changeAble = false;
        if (Player.GetComponent<PlayerMove>().PlayerLookLeft == false)
        {
            rigid.position = new Vector3(Aim.transform.position.x + X, Player.transform.position.y - Y, 0);
            Sp.flipX = true;
            
        }

        if (Player.GetComponent<PlayerMove>().PlayerLookLeft)
        {
            rigid.position = new Vector3(Aim.transform.position.x - X, Player.transform.position.y - Y, 0);
            Sp.flipX = false;
            
        }

        
    }

           
}

