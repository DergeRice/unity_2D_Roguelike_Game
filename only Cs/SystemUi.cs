using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemUi : MonoBehaviour
{
    public GameObject Black;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Black.GetComponent<Image>().color.a);
        if (Black.GetComponent<Image>().color.a < 0)
        {
            
            Black.SetActive(false);
        }
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutScreen());

    }
    IEnumerator FadeOutScreen()
    {
        float FadeAmount = 0;
        Black.SetActive(true);
        Black.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        
        while (FadeAmount<1.1)
        {
            
            yield return new WaitForSeconds(0.01f);
            Black.GetComponent<Image>().color = new Color(0, 0, 0, 1 - FadeAmount);

            FadeAmount += 0.01f;
            if (Black.GetComponent<Image>().color.a < 0)
            {
                
                Black.SetActive(false);
                break;
            }

        }
    }
}
