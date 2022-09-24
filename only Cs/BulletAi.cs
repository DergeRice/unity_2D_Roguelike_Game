using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAi : MonoBehaviour
{
    public float bulletSpeed, bulletRemainTime, damage;
    public Vector2 target, GunPos,BulletDesti;
    public Rigidbody2D rigid;
    public GameObject Player, Monster;

    public Vector2 MobPos, BasicMove;
    // Start is called before the first frame update
    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Start()
    {
        MobPos = new Vector2(0, 0);
        Player = GameObject.Find("Main Char");
        BasicMove = new Vector2(-(Player.transform.position.x - rigid.transform.position.x), 0).normalized * bulletSpeed;
        if (Player.GetComponent<MagicGunClass>().NearestMob != null)
        {
            target = Player.GetComponent<MagicGunClass>().NearestMob.transform.position;
        }
        else target = new Vector2(0, 0);
        GunPos = Player.GetComponent<MagicGunClass>().BulletPos;
    }

    // Update is called once per frame
    public void Update()
    {
        




    }
    public void FixedUpdate()
    {
        bulletRemainTime -= Time.deltaTime;
        if (bulletRemainTime <= 0) { Destroy(gameObject); }
        else
        {
            if (Mathf.Abs(target.x) > 0)
            {
                BulletDesti = new Vector2(target.x - GunPos.x, 0) + new Vector2(0, target.y - GunPos.y);
                rigid.velocity = BulletDesti.normalized * bulletSpeed;
            }else rigid.velocity =BasicMove;


        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("sibal");
        if (collision.GetComponent<Collider2D>().gameObject.CompareTag("Monter"))
        {
            Monster = collision.gameObject;
            Monster.GetComponent<MobDamageSystem>().KnockBackDisCal(0);

            damage = Player.GetComponent<PlayerStats>().PlayerAD;
            Monster.GetComponent<MobDamageSystem>().TakeDamaged((int)damage / 3);
            Monster.GetComponent<MobMove>().MobAngry();
            Destroy(gameObject);

        }
    }



}
