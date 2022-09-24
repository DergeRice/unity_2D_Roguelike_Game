using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentCal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static class GetPercent
    {
        public static bool GetThisChanceResult(float Chance)
        {
            
            if (Chance < 0.0000001f)
            {
                Chance = 0.0000001f;
            }

            bool Success = false;
            int RandAccuracy = 10000000;
            float RandHitRange = Chance * RandAccuracy;
            int Rand = UnityEngine.Random.Range(1, RandAccuracy + 1);
            if (Rand <= RandHitRange)
            {
                Success = true;
            }
            else { Success = false; }
            
            return Success;
        }

        public static bool GetThisChanceResult_Percentage(float Percentage_Chance)
        {
            if (Percentage_Chance < 0.0000001f)
            {
                Percentage_Chance = 0.0000001f;
            }

            Percentage_Chance = Percentage_Chance / 100;

            bool Success = false;
            int RandAccuracy = 10000000;
            float RandHitRange = Percentage_Chance * RandAccuracy;
            int Rand = UnityEngine.Random.Range(1, RandAccuracy + 1);
            if (Rand <= RandHitRange)
            {
                Success = true;
            }else { Success = false; }
            return Success;
        }
    }
}
