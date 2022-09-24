using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagedMark : MonoBehaviour
{
    Animator animator;
    TextMeshPro damageMark;
    Color alpha;
    float alphaSpeed;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        damageMark = GetComponent<TextMeshPro>();
        alpha = damageMark.color;
    }

    // Update is called once per frame
    void Update()
    {
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        damageMark.color = alpha;
    }
}
