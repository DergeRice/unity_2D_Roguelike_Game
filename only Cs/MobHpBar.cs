using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobHpBar : MonoBehaviour
{
    public Slider MobHealthBar;
    public GameObject Monster;
    public float MobNowHp, MobMaxHp;
    // Start is called before the first frame update
    void Start()
    {
        MobHealthBar.value = MobNowHp / MobMaxHp;

        
    }

    // Update is called once per frame
    void Update()
    {
        MobHealthBar.value = MobNowHp / MobMaxHp;
       
        
    }
    public void DestroyThis()
    {
        MobHealthBar = null;
        Destroy(gameObject);
    }
}
