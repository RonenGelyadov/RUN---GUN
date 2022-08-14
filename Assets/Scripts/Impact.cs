using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    float impactTime;

    private void Awake()
    {
        impactTime = 0.5f;
    }


    void Update()
    {
        impactTime -= Time.deltaTime;

        if(impactTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
