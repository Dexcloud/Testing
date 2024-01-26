using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    internal enum TargetType
    {
        player = 0,
        enemy = 1
    };

    TargetType target = TargetType.enemy;

    CircleCollider2D col;

    Vector2 moveDirection = Vector2.zero;

    float lifeTimer = 0;

    float bulletSpeed = 15;

    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        col.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += bulletSpeed * Time.deltaTime * (Vector3) moveDirection;

        lifeTimer += Time.deltaTime;
        if (lifeTimer >= 3) ResetBullet();
    }

    void ResetBullet()
    {
        lifeTimer = 0;
        this.gameObject.SetActive(false);
    }

    public Vector2 MoveDirection
    {
        get { return moveDirection; }
        set { moveDirection = value; }
    }

    internal TargetType Target
    {
        get { return target; }
        set { target = value; }
    }

    internal float BulletSpeed
    {
        get { return bulletSpeed; }
        set { bulletSpeed = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target == TargetType.enemy && collision.CompareTag("Ground"))
        {
            ResetBullet();
        }
        if (target == TargetType.player && collision.CompareTag("Player"))
        {
            ResetBullet();
        }
    }
}
