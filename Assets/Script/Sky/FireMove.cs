using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMove : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    float disappearTime = 3f;

    public enum moveDirection { Left, Right, Up, Down }
    public moveDirection moving = moveDirection.Left;

    Vector3 MoveDir()
    {
        float x = moving == moveDirection.Left ? -1 : moving == moveDirection.Right ? 1 : 0,
              y = moving == moveDirection.Down ? -1 : moving == moveDirection.Up ? 1 : 0;

        return new Vector3(x, y, 0);
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        transform.position += (MoveDir() * Time.fixedDeltaTime * moveSpeed);

        disappearTime -= Time.fixedDeltaTime;

        if (disappearTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
