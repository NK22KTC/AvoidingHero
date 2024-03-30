using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoMove : MonoBehaviour
{
    float moveSpeed = 15f;

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * moveSpeed;
    }
}
