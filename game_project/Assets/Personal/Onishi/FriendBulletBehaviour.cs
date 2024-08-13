using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendBulletBehaviour : MonoBehaviour
{
    public enum BulletMode
    {
        CHARM = 1,
        ATTACK = 2
    };
    public GameObject enemy;
    public GameObject friend;
    [SerializeField] private float speed;
    public BulletMode mode = BulletMode.CHARM;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float hitBoxRange = 1f;
    public GameObject friendParent;

    [SerializeField] GameObject charmHitFX;
    [SerializeField] GameObject attackHitFX;

    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject friendFX;

    // Start is called before the first frame update
    void Start()
    {
        friendParent = GameObject.Find("Friends");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemy != null)
        {
            Vector3 direction = enemy.transform.position - transform.position;
            direction /= direction.magnitude;
            gameObject.transform.position += direction * speed;
        

            MyCollisionEnter2D();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void MyCollisionEnter2D()
    {
        float distance = (enemy.gameObject.transform.position - transform.position).magnitude;
        if (distance < hitBoxRange)
        {    
            var enem = enemy.GetComponent<Enemy>();
            var effectCenter = (enem.gameObject.transform.position + transform.position) / 2f;
            if (mode == BulletMode.CHARM)
            {
                enem.charm += damage;
                if (enem.charm >= enem.Max_charm)
                {
                    var obj = Instantiate(enem.friendPrefab, enemy.transform.position, Quaternion.identity);
                    obj.transform.parent = friendParent.transform;
                    Destroy(Instantiate(friendFX, transform.position, Quaternion.identity), 0.3f);
                    Destroy(enemy);
                }
                Destroy(Instantiate(charmHitFX, effectCenter, Quaternion.identity), 0.3f);
            }
            else if (mode == BulletMode.ATTACK)
            {
                enem.health -= damage;
                if (enem.health <= 0)
                {
                    Destroy(Instantiate(deathFX, enem.transform.position, Quaternion.identity), 0.3f);
                    enem.Death();
                }
                Destroy(Instantiate(attackHitFX, effectCenter, Quaternion.identity), 0.3f);
            }
            Destroy(gameObject);
        }

    }
}
