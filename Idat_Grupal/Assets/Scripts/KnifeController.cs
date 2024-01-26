using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    float angle = 0;

    float gravity = 30;

    float verticalSpeed = 0;
    float horizontalSpeed = 15;

    BoxCollider2D col;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        col.isTrigger = true;

        MoveVector = new Vector2(10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        angle += 360 * Time.deltaTime;
        if (angle > 360) angle -= 360;
        transform.eulerAngles = new Vector3(0, 0, angle);

        VerticalSpeed -= gravity * Time.deltaTime;

        transform.position += (Vector3) MoveVector * Time.deltaTime;

    }


    internal void SetMoveVector(Vector2 direction, float speed = 15)
    {
        horizontalSpeed = speed * direction.x;
        verticalSpeed = speed * direction.y;
    }

    internal float VerticalSpeed
    {
        get { return verticalSpeed; }
        set { verticalSpeed = value; }
    }

    internal float HorizontalSpeed
    {
        get { return horizontalSpeed; }
        set { horizontalSpeed = value; }
    }

    internal Vector2 MoveVector
    {
        get { return new Vector2(horizontalSpeed, verticalSpeed); }
        set { horizontalSpeed = value.x; verticalSpeed = value.y; }
    }

}
