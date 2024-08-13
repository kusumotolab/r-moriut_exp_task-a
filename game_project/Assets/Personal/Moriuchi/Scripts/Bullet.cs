using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 0.03f; //弾のスピード
    [SerializeField] private float damage = 1f;
    public bool isAttack; //攻撃用の弾か判定する用のフラグ
    public bool isCharm; //魅了用の弾か判定する用のフラグ
    [SerializeField] private GameObject charmFriend;
    [SerializeField] private GameObject attackFriend;
    [SerializeField] private GameObject friendParent;
    private Vector3 direction;
    private Transform thisTransform;

    [SerializeField] GameObject charmHitFX;
    [SerializeField] GameObject attackHitFX;

    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject friendFX;
    

    void Start(){
        friendParent = GameObject.Find("Friends");
        thisTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition;
        Vector3 addVector;
        addVector = new Vector3(direction.x * Time.deltaTime, direction.y * Time.deltaTime, 0);
        addVector.Normalize();
        newPosition = thisTransform.position + addVector * speed;
        transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);

        //ビューポート座標を取得
        float viewX = Camera.main.WorldToViewportPoint(transform.position).x;
        float viewY = Camera.main.WorldToViewportPoint(transform.position).y;
        if (viewX < 0 || viewX > 1 || viewY < 0 || viewY > 1){
            Destroy(this.gameObject); //画面外に出たら削除
        }
    }
 
    public void getVector(Vector3 from,Vector3 to)
    {
        direction = new Vector3(to.x - from.x, to.y - from.y, to.z - from.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy"){
            var enem = other.GetComponent<Enemy>();
            var effectCenter = (other.gameObject.transform.position + transform.position) / 2f;
            if (enem != null)
            {
                if (isAttack)
                { //攻撃用の弾の場合
                    Debug.Log("攻撃用の弾です");
                    enem.health -= damage;
                    if (enem.health <= 0)
                    {
                        Destroy(Instantiate(deathFX, enem.transform.position, Quaternion.identity), 0.3f);
                        enem.Death();
                    }
                    Destroy(Instantiate(attackHitFX, effectCenter, Quaternion.identity), 0.3f);
                }
                else if (isCharm)
                { //魅了用の弾の場合
                    Debug.Log("魅了用の弾です");
                    enem.charm += damage;
                    if (enem.charm >= enem.Max_charm)
                    {
                        var obj = Instantiate(enem.friendPrefab, enem.transform.position, Quaternion.identity);                     
                        obj.transform.parent = friendParent.transform;
                        Destroy(Instantiate(friendFX, enem.transform.position, Quaternion.identity), 0.3f);
                        Destroy(enem.gameObject);

                    }
                    Destroy(Instantiate(charmHitFX, effectCenter, Quaternion.identity), 0.3f);
                }
                SetFriendsState(enem, isCharm);
                Destroy(this.gameObject);
            }
        }
    }

    private void SetFriendsState(Enemy enem, bool isCharm)
    {
        foreach(Transform fri in friendParent.transform)
        {
            var fb = fri.GetComponent<FriendBehaviour>();
            fb.enemy = enem.gameObject;
            if (fb != null)
            {
                if (fb.mode == FriendBehaviour.AttackMode.CHARM && isCharm)
                {
                    fb.shootTimer = fb.shootDefaultTime;
                }
                else if (fb.mode == FriendBehaviour.AttackMode.ATTACK && !isCharm)
                {
                    fb.shootTimer = fb.shootDefaultTime;
                }
            }
        }
    }

}
