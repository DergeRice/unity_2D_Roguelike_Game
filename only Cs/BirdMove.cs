using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMove : MonoBehaviour
{
    public float speed;
    public GameObject MainChar, CharDust;
    Transform PlayerTrans;
    public float distance,X;
    Animator anim;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    private float birdYpos;
    [SerializeField]
    private float time, Returntime, ReturntimeMax, f;
    public float BirdYSpeed;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTrans = MainChar.transform;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ReturntimeMax = 5f;
        Returntime = ReturntimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        birdYpos = Mathf.Cos(time) / 3 + 1.1f;
        time += Time.deltaTime * BirdYSpeed;

        Returntime -= Time.deltaTime;
        //transform.Translate(new Vector2(player.position.x - transform.position.x, +f) * Time.deltaTime * speed);
        if (Returntime < 0) { Returntime = 0; }
        if (Returntime <= 0)
        {
            // ReturnPos();
            // Returntime = ReturntimeMax;
        }
        if (MainChar.GetComponent<PlayerMove>().moveInput != null) {
            // StartCoroutine(BirdMoving()); 
        }

        if (Mathf.Abs(transform.position.x - PlayerTrans.position.x) > distance || Mathf.Abs(transform.position.y - PlayerTrans.position.y) > distance)
        {
            speed = 100;
        }
        else speed = 5;

    }

    void DirectionBird()
    {
        if (transform.position.x - PlayerTrans.position.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
    private void LateUpdate()
    {
        //transform.position = Vector3.Lerp(CharDust.transform.position, transform.position, Time.deltaTime * speed);



        //transform.Translate(new Vector2(0, PlayerTrans.position.y + birdYpos - transform.position.y) * Time.deltaTime * speed);
       
        DirectionBird();
        if (MainChar.GetComponent<PlayerMove>().PlayerLookLeft == false)
        {
            BirdPostion(CharDust.transform.position.x - transform.position.x - 0.5f, CharDust.transform.position.y + birdYpos - transform.position.y+1);
        }

        if (MainChar.GetComponent<PlayerMove>().PlayerLookLeft)
        {
            BirdPostion(CharDust.transform.position.x - transform.position.x + 0.5f, CharDust.transform.position.y + birdYpos - transform.position.y +1);
        }
    }

    private void ReturnPos()
    {
        //transform.Translate(new Vector2(PlayerTrans.position.x - transform.position.x,0) * Time.deltaTime * speed);
    }

    IEnumerator BirdMoving()
    {
        yield return new WaitForSeconds(3f);
        transform.Translate(new Vector2(CharDust.transform.position.x - transform.position.x, CharDust.transform.position.y + birdYpos - transform.position.y) * Time.deltaTime * speed);
        //transform.Translate(new Vector2(0, ) * Time.deltaTime * speed);

    }
    public void BirdPostion(float a,float b)
    {
        transform.Translate(new Vector2(a,b) * Time.deltaTime * speed * 0.4f);

    }
}
