using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHpGaugeError : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }
}
