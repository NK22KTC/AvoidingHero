using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    PlayerMoves playerMoves;

    PanelManager panelManager;

    GameFlow gameFlow;

    GameObject gameManager;

    GameObject player;
    Transform playerPos;  // 追跡するプレイヤーのTransformコンポーネント

    [SerializeField]
    float moveSpeed = 10f,  // 移動速度
          maxSpeed = 15f,  // 速度の最大値
          acceleration = 10f;  // 加速度

    private Rigidbody2D rb;
    private Vector2 velocity;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager");

        panelManager = gameManager.GetComponent<PanelManager>();
        gameFlow = gameManager.GetComponent<GameFlow>();

        playerMoves = player.GetComponent<PlayerMoves>();
        playerPos = player.transform;

        rb = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
    }

    private void Update()
    {
        if (playerMoves.isGameOver) return;

        if (panelManager.isPaused) return;

        if (gameFlow.avoidTime < 0) return;

        // プレイヤーの方向を向く
        Vector2 direction = playerPos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        // 慣性を考慮して追跡する
        Vector2 targetVelocity = direction.normalized * moveSpeed;
        velocity = Vector2.Lerp(velocity, targetVelocity, acceleration * Time.deltaTime);
        rb.velocity = velocity;

        // 速度の最大値を制限する
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}