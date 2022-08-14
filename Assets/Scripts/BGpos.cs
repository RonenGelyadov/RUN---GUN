using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGpos : MonoBehaviour
{
    public Transform playerPos;

    void Start()
    {
        
    }


    void Update()
    {
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y + 2, 15);
    }
}
