using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendBehaviour : MonoBehaviour
{
    public enum FriendState
    {
        INITIALIZE = 1,
        FOLLOW = 2
    };

    public enum AttackMode
    {
        CHARM = 1,
        ATTACK = 2
    };


    [SerializeField] private float InitializeEndThreshold = 0.3f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject centerPoint;
    [SerializeField] private float moveCoefficient = 0.1f;
    [SerializeField] private float gravity = 5f;
    public float shootTimer;
    public float shootDefaultTime = 2f;
    [SerializeField] private float shootPeriod = 1f;
    public AttackMode mode = AttackMode.CHARM;
    public GameObject enemy;

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    
    private float shotTime;
    private FriendState currentState = FriendState.INITIALIZE;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        centerPoint = GameObject.Find("FriendCenter");
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        if (currentState == FriendState.INITIALIZE)
        {
            float distance = (gameObject.transform.position - centerPoint.transform.position).magnitude;
            if (distance <= InitializeEndThreshold)
            {
                circleCollider.isTrigger = false;
                currentState = FriendState.FOLLOW;
            }
        }
        shootTimer -= Time.deltaTime;
        if(enemy != null && shootTimer > 0)
        {
            if (Time.time > shotTime + shootPeriod)
            {
                shotTime = Time.time;
                Shoot();
            }
        }
    }

    private void Move()
    {
        if (currentState == FriendState.INITIALIZE)
        {
            Vector3 direction = centerPoint.transform.position - gameObject.transform.position;
            Vector3 delta = direction / direction.magnitude * moveCoefficient;
            gameObject.transform.Translate(delta);
        }
        else
        {
            Vector3 direction = centerPoint.transform.position - gameObject.transform.position;
            Vector3 power = direction * gravity;
            rb.AddForce(power);
        }
    }

    private void Shoot()
    {
        var bul = Instantiate(bullet, transform);
        Destroy(bul, 20f);
        FriendBulletBehaviour fbb = bul.GetComponent<FriendBulletBehaviour>();
        fbb.enemy = enemy;
    }


}
