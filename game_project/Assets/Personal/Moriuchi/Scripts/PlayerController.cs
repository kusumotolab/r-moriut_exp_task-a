using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 0.03f; //移動スピード
    [SerializeField] private GameObject bulletPrefab; //弾のプレハブ
    public float health = 5.0f; //体力
    public float maxHealth = 5.0f;
    [SerializeField] private GaugeUIControllerBase hpGauge;
    [SerializeField]
    private Sprite attackSprite;
    [SerializeField]
    private Sprite friendSprite;

    // Add.
    [SerializeField]
    private Transform upperLimit;
    [SerializeField]
    private Transform bottomLimit;
    [SerializeField]
    private Transform leftLimit;
    [SerializeField]
    private Transform rightLimit;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        hpGauge = GameObject.Find("PlayerHPGaugeUI").GetComponent<GaugeUIControllerBase>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 playerPosition = transform.position;

        if (Input.GetKey(KeyCode.W)){
            // Add.
            if(transform.position.y < upperLimit.position.y)
            {
                playerPosition.y += speed;
            }
        }else if (Input.GetKey(KeyCode.A)){
            // Add.
            if(transform.position.x > leftLimit.position.x)
            {
                playerPosition.x -= speed;
            }
        }else if (Input.GetKey(KeyCode.S)){
            // Add.
            if(transform.position.y > bottomLimit.position.y)
            {
                playerPosition.y -= speed;
            }
        }else if (Input.GetKey(KeyCode.D)){
            // Add.
            if(transform.position.x < rightLimit.position.x)
            {
                playerPosition.x += speed;
            }
        }

        transform.position = playerPosition;

        var screenPos = Camera.main.WorldToScreenPoint( transform.position );
        var direction = Input.mousePosition - screenPos;
        var angle = GetAim( Vector3.zero, direction );
        Vector3 localAngle = transform.localEulerAngles;
        localAngle.z = (angle - 90);
        transform.localEulerAngles = localAngle; 

        action();
        SetHpGauge();
    }

    private void action(){
        if(Input.GetMouseButtonDown(0)){ //左クリックをした場合
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().isAttack = true;
            bullet.GetComponent<SpriteRenderer>().color = Color.white;
            bullet.GetComponent<SpriteRenderer>().sprite = attackSprite;
            bullet.GetComponent<Bullet>().getVector(transform.position,Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }else if(Input.GetMouseButtonDown(1)){ //右クリックをした場合
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().isCharm = true;
            bullet.GetComponent<SpriteRenderer>().color = Color.red;
            bullet.GetComponent<SpriteRenderer>().sprite = friendSprite;
            bullet.GetComponent<Bullet>().getVector(transform.position,Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    public float GetSetHealth
    {
        get { return health; }
        set { health = value; }
    }

    public float GetAim( Vector2 from, Vector2 to )
    {
        float dx = to.x - from.x;
        float dy = to.y - from.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }

    private void SetHpGauge()
    {
        float rate = Mathf.Min(1f, health / maxHealth);
        hpGauge.setGaugeWidth(rate);
    }
}
