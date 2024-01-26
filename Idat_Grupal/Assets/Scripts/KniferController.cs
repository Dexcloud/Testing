using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KniferController : MonoBehaviour
{
    public Transform playerTransform;

    List<KnifeController> knifes = new List<KnifeController>();
    [SerializeField] GameObject knife;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            knifes.Add(Instantiate(knife).GetComponent<KnifeController>());
            knifes[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {


        //print(playerTransform.position);
    }

    private void FixedUpdate()
    {
        if (playerTransform == null)
        {
            if (Physics2D.OverlapBox(transform.position, new Vector2(5, 1.5f), 0, LayerMask.GetMask("Player")).transform != null)
            {
                playerTransform = Physics2D.OverlapBox(transform.position, new Vector2(5, 1.5f), 0, LayerMask.GetMask("Player")).transform;
            }
            

            
            //playerTransform = Physics2D.OverlapBox(transform.position, new Vector2(5, 1.5f), 0, LayerMask.GetMask("Player")).transform;
            
        }
        
        //if (playerTransform == null)
        //{
        //    return;
            
        //    //playerTransform = Physics2D.OverlapBox(transform.position, new Vector2(5, 1.5f), 0, LayerMask.GetMask("Player")).transform;
            
        //}
        //else
        //{
        //    playerTransform = Physics2D.OverlapBox(transform.position, new Vector2(5, 1.5f), 0, LayerMask.GetMask("Player")).transform;
        //}
    }
}
