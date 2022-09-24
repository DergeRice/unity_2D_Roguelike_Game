using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageMark : MonoBehaviour
{
    Animator animator;
    TextMeshPro damageMark;
    Color alpha;
    public float alphaSpeed;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        damageMark = GetComponent<TextMeshPro>();
       // alpha = damageMark.color;
    }

    // Update is called once per frame
    void Update()
    {
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("BasicDamaged")) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            Destroy(gameObject);
        }

        //alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        //damageMark.color = alpha;
    }
}
