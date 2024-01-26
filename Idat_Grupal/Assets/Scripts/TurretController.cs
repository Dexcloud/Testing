using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    List<BulletController> bullets = new List<BulletController>();
    [SerializeField] GameObject bullet;

    Rigidbody2D rb;

    float shootTimer = 0;
    int shootCounter = 0;

    int bulletCounter = 0;

    Vector2 shootDirection = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        for (int i = 0; i < 15; i++)
        {
            bullets.Add(Instantiate(bullet).GetComponent<BulletController>());
            bullets[i].transform.SetParent(this.transform);
            bullets[i].gameObject.SetActive(false);
        }

        switch (transform.rotation.z)
        {
            case 0:
                shootDirection = Vector2.up;
                break;
            case 45:
                shootDirection = (Vector2.up + Vector2.left).normalized;
                break;
            case 90:
                shootDirection = Vector2.left;
                break;
            case 135:
                shootDirection = (Vector2.left + Vector2.down).normalized;
                break;
            case -45:
                shootDirection = (Vector2.right + Vector2.up).normalized;
                break;
            case -90:
                shootDirection = Vector2.right;
                break;
            case -135:
                shootDirection = (Vector2.right + Vector2.down).normalized;
                break;
            default:
                shootDirection = Vector2.down;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shootTimer >= 0) shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            Shoot(shootDirection);
            if (shootCounter < 3)
            {
                shootTimer = 0.25f;
                shootCounter += 1;
            }
            if (shootCounter == 3)
            {
                shootTimer = 0.75f;
                shootCounter = 0;
            }
        }
    }

    void Shoot(Vector2 direction)
    {
        if (bullets[bulletCounter].isActiveAndEnabled == false)
        {
            bullets[bulletCounter].gameObject.SetActive(true);
            bullets[bulletCounter].Target = BulletController.TargetType.player;
            bullets[bulletCounter].BulletSpeed = 10;
            bullets[bulletCounter].transform.position = this.transform.position;
            bullets[bulletCounter].MoveDirection = direction;

            bulletCounter = bulletCounter == 14 ? 0 : bulletCounter + 1;
        }
    }

    Vector2 AdjustDirection(Vector2 direction)
    {
        if (direction.x > 0 && direction.y > 0) // Primer Cuadrante
        {
            if (direction.x > direction.y)
            {
                if (GetDistance(direction, 0) < GetDistance(direction, 1)) direction = Vector2.right;
                else direction = Vector2.right + Vector2.up;
            }
            else //(direction.x <= direction.y)
            {
                if (GetDistance(direction, Mathf.Infinity) < GetDistance(direction, 1)) direction = Vector2.up;
                else direction = Vector2.right + Vector2.up;
            }
        }
        else if (direction.x < 0 && direction.y > 0) // Segundo Cuadrante
        {
            if (-direction.x > direction.y)
            {
                if (GetDistance(direction, 0) < GetDistance(direction, -1)) direction = Vector2.left;
                else direction = Vector2.left + Vector2.up;
            }
            else //(-direction.x <= direction.y)
            {
                if (GetDistance(direction, Mathf.Infinity) < GetDistance(direction, -1)) direction = Vector2.up;
                else direction = Vector2.left + Vector2.up;
            }
        }
        else if (direction.x < 0 && direction.y < 0) // Tercer Cuadrante
        {
            if (-direction.x > -direction.y)
            {
                if (GetDistance(direction, 0) < GetDistance(direction, 1)) direction = Vector2.left;
                else direction = Vector2.left + Vector2.down;
            }
            else //(-direction.x <= -direction.y)
            {
                if (GetDistance(direction, Mathf.Infinity) < GetDistance(direction, 1)) direction = Vector2.down;
                else direction = Vector2.left + Vector2.down;
            }
        }
        else if (direction.x > 0 && direction.y < 0) // Cuarto Cuadrante
        {
            if (direction.x > -direction.y)
            {
                if (GetDistance(direction, 0) < GetDistance(direction, -1)) direction = Vector2.right;
                else direction = Vector2.right + Vector2.down;
            }
            else //(direction.x <= -direction.y)
            {
                if (GetDistance(direction, Mathf.Infinity) < GetDistance(direction, -1)) direction = Vector2.down;
                else direction = Vector2.right + Vector2.down;
            }
        }
        else if (direction.x > 0 && direction.y == 0) direction = Vector2.right;
        else if (direction.x == 0 && direction.y > 0) direction = Vector2.up;
        else if (direction.x < 0 && direction.y == 0) direction = Vector2.left;
        else if (direction.x == 0 && direction.y < 0) direction = Vector2.down;
        else direction = Vector2.right; // x == 0; y == 0

        return direction.normalized;
    }

    float GetDistance(Vector2 point, float m)
    {
        float A, B, divider;

        if (m == Mathf.Infinity)
        {
            A = 1;
            B = 0;
        }
        else
        {
            A = m;
            B = -1;
        }

        divider = Mathf.Sqrt(A * A + B * B);

        return Mathf.Abs(A * point.x + B * point.y) / divider;
    }
}
