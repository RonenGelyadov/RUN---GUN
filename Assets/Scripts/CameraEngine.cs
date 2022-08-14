using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEngine : MonoBehaviour
{
    public Transform playerPos;

    void Start()
    {
        
    }


    void Update()
    {
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y + 1, -10);
    }
}
