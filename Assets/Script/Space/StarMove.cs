using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMove : MonoBehaviour
{
    float moveSpeed = 15;

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * moveSpeed;
    }
}
