using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [Header("スピード")] public float speed = 3.0f;
    [Header("最大移動距離")] public float maxDistance = 10.0f;

    private Rigidbody2D rb;
    private Vector3 defaultPos;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        defaultPos = transform.position;
        player = GameObject.FindWithTag("Player");
        targetPos = player.transform.position;
    }

    void FixedUpdate()
    {
        float d = Vector3.Distance(transform.position, defaultPos);

        //最大移動距離を超えている
        if (d > maxDistance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            rb.MovePosition(transform.position += Vector3.Normalize(targetPos - defaultPos) * Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().GetSetHealth--;
        }
        Destroy(this.gameObject);
    }


}
