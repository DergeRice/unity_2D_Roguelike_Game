using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill1Cool : MonoBehaviour
{
    public float Skill_1_cooltime,coolcounttime;
    public Image Skill1CoolTimeImg;
    public GameObject Player;
    public bool Skill1Able;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Skill1Able)
        {
            Skill_1_cooltime -= Time.deltaTime;
            Skill1CoolTimeImg.fillAmount = (1.0f/ Skill_1_cooltime);
        }
        else
        {
            SkillCoolTimeSetting();
        }
    }
    public void Skill1Pressed()
    {
        Skill1Able = false;

    }
    public void SkillCoolTimeSetting()
    {

    }
}
