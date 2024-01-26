using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KniferController : MonoBehaviour
{
    Transform playerTransform;

    BoxCollider2D detector;

    Rigidbody2D rb;

    List<KnifeController> knifes = new List<KnifeController>();
    [SerializeField] GameObject knife;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        detector = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        detector.isTrigger = true;
        detector.size = new Vector2(20, 5);


        for (int i = 0; i < 5; i++)
        {
            knifes.Add(Instantiate(knife).GetComponent<KnifeController>());
            knifes[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {




    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerTransform == null)
        {
            playerTransform = collision.transform;
        }
    }
}
