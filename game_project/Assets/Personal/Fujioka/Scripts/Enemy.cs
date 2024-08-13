using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Attack,
        Charm,
    }
    public EnemyType enemyType;

    public enum EnemyState
    {
        Idle,       // �ҋ@
        Move,       // �ړ�
        Attack,     // �U��
    }
    [SerializeField] private EnemyState currentState;

    [Header("MoveSpeeed")] public float speed;

    [Header("PlayerRange")] public float playerDistance;
    private float playerRange;

    [Header("AttackObject")] public GameObject attackObj;
    [Header("AttackInterval")] public float interval;
    public float timer;
    private float attackTimer;
    private float easingTimer;

    [Header("Health")] public float health = 5.0f;
    private float MaxHealth;
    [Header("MaxCharm")] public float Max_charm = 5.0f;
    [Header("NowCharm")] public float charm = 0.0f;
    [Header("FriendFlg")] [SerializeField] private bool isFriend;

    [Header("Score")] public int score = 1;

    [Header("HealthUI")] [SerializeField] GaugeUIControllerBase gaugeUIHealth;
    [Header("CharmUI")] [SerializeField] GaugeUIControllerBase gaugeUICharm;
    private bool UIHealth;
    private float UIHealthTimer;
    private bool UICharm;
    private float UICharmTimer;

    private float damageTimer;

    public float knockBackPower = 3.0f;   // �m�b�N�o�b�N�������
    public float knockBackTime = 0.5f;   // �m�b�N�o�b�N�����鎞��
    public Vector3 knockBackVec;

    private bool isMove;
    private bool isAttack;
    private GameObject player;
    private ScoreHandler scoreHandler; 

    Vector3 moveTargetPosition;

    public GameObject friendPrefab;

    // Start is called before the first  frame update
    void Start()
    {

        timer = 0.0f;
        easingTimer = 0.0f;
        currentState = EnemyState.Move;
        //enemyType = (EnemyType)Enum.ToObject(typeof(EnemyType), UnityEngine.Random.Range(0, 2));
        isFriend = false;
        isMove = false;
        isAttack = true;
        UIHealth = false;
        UICharm = false;
        UIHealthTimer = 0.0f;
        UICharmTimer = 0.0f;
        damageTimer = 0.0f;

        playerRange = playerDistance + (UnityEngine.Random.Range(0, 20) / 10);

        player = GameObject.FindWithTag("Player");
        scoreHandler = GameObject.FindWithTag("ScoreHandler").GetComponent<ScoreHandler>();

        // �����_���ŖړI�n�ݒ�
        moveTargetPosition = moveRandomPosition();

        MaxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyType == EnemyType.Charm)
        {
            if (moveTargetPosition == transform.position)  //�Aplayer�I�u�W�F�N�g���ړI�n�ɓ��B����ƁA
            {
                moveTargetPosition = moveRandomPosition();  //�A�ړI�n���Đݒ�
            }
            transform.position = Vector3.MoveTowards(transform.position, moveTargetPosition, speed * Time.deltaTime);
        }
        else
        {
            // �X�e�[�g�ɉ������s��
            switch (currentState)
            {
                case EnemyState.Idle:
                    Idle();
                    break;
                case EnemyState.Move:
                    Move();
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
            }
            if (attackTimer > 0) attackTimer -= Time.deltaTime;
        }

        KnockBack();

        // �̗͂�0�ȉ��Ŏ��S
        if (health <= 0)
        {
            Death();
        }
        // Max�����l�𒴂����Ȃ璇�Ԃ�
        if(charm >= Max_charm)
        {
            //
            isFriend = true;
        }

        // �v���C���[�Ƃ̋������v�Z
        PlayertoDistance();

        // �_���[�W���󂯂��Ƃ��ꎞ�I�ɓ���
        Damaged();

        easingTimer += Time.deltaTime;

        UIDisplay();

    }

    private void FixedUpdate()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �����̈ʒu�ƐڐG���Ă����I�u�W�F�N�g�̈ʒu�Ƃ��v�Z���āA�����ƕ������o���Đ��K��(���x�x�N�g�����Z�o)
        Vector3 distination = (transform.position - player.transform.position).normalized;

        knockBackVec = distination * knockBackPower;
        knockBackTime = 0.5f;

        if(other.gameObject.GetComponent<Bullet>())
        {
            if (other.gameObject.GetComponent<Bullet>().isAttack)
            {
                UIHealth = true;
                UIHealthTimer = 2.0f;
            }
            if (other.gameObject.GetComponent<Bullet>().isCharm)
            {
                UICharm = true;
                UICharmTimer = 2.0f;
            }
        }
        damageTimer = 0.2f;
    }

    void Damaged()
    {
        if(damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }
    }

    void UIDisplay()
    {
        if (UIHealth) gaugeUIHealth.setGaugeVisible(true);
        else gaugeUIHealth.setGaugeVisible(false);
        if (UICharm) gaugeUICharm.setGaugeVisible(true);
        else gaugeUICharm.setGaugeVisible(false);

        gaugeUIHealth.setGaugeWidth(health / MaxHealth);

        gaugeUICharm.setGaugeWidth(charm / Max_charm);

        if (UIHealthTimer > 0)
        {
            UIHealthTimer -= Time.deltaTime;
        }
        else UIHealth = false;
        if (UICharmTimer > 0)
        {
            UICharmTimer -= Time.deltaTime;
        }
        else UICharm = false;
    }

    void KnockBack()
    {
        knockBackTime -= Time.deltaTime;
        if (knockBackTime > 0)
            transform.position += (knockBackVec) * Time.deltaTime * speed;

    }


    private void Idle()
    {
        //if (timer > interval)
        //{
        //    // �v���C���[�Ƃ̋���������Ă���Ȃ�
        //    if (isMove)
        //    {
        //        playerRange = playerDistance + (UnityEngine.Random.Range(0, 20) / 6.0f);
        //        currentState = EnemyState.Move;
        //    }
        //    else currentState = EnemyState.Attack;
        //    timer = 0.0f;
        //}
        //else
        //{
        //    timer += Time.deltaTime;
        //}

        // �v���C���[�Ƃ̋���������Ă���Ȃ�
        if (isMove)
        {
            playerRange = playerDistance + (UnityEngine.Random.Range(0, 20) / 6.0f);
            currentState = EnemyState.Move;
            isAttack = true;
            timer = 0.0f;
        }
        else
        {
            if (isAttack && attackTimer <= 0)
            {
                currentState = EnemyState.Attack;
                isAttack = false;
                timer = 0.0f;
            }
            if (timer > interval)
            {
                isAttack = true;
                timer = 0.0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        

    }

    private void Move()
    {
        Vector3 player_pos = player.transform.position;
        Vector3 enemy_pos = transform.position;

        Vector3 vec = Vector3.Normalize(player_pos - enemy_pos);
        float dis = Vector3.Distance(player_pos,enemy_pos);

        //// �����_���ȉ��x�N�g���𐶐�
        //Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f, UnityEngine.Random.Range(-1f, 1f)).normalized;
        //Vector3 originalDirection = Vector3.Normalize(player_pos - enemy_pos);
        //// �����_���ȃx�N�g�������̕����x�N�g���ɉ�����
        //Vector3 finalDirection = originalDirection + randomOffset;

        //// ���K�����đ��x��ۂ�
        //finalDirection = finalDirection.normalized;

        // �߂Â��鋗���܂ňړ�������
        if (!isMove)
        {
            currentState = EnemyState.Idle;
        }
        else
        {
            transform.position += (vec) * Time.deltaTime * speed;
        }
    }

    private void Attack()
    {
        GameObject g = Instantiate(attackObj);
        //g.transform.SetParent(transform);
        g.transform.position = attackObj.transform.position;
        g.transform.rotation = attackObj.transform.rotation;
        g.SetActive(true);
        currentState = EnemyState.Idle;
        attackTimer = interval;
    }

    private void PlayertoDistance()
    {
        Vector3 player_pos = player.transform.position;
        Vector3 enemy_pos = transform.position;

        Vector3 vec = Vector3.Normalize(player_pos - enemy_pos);
        float dis = Vector3.Distance(player_pos, enemy_pos);

        // �߂Â��鋗���܂ňړ�������
        if (playerRange > dis) isMove = false;
        else isMove = true;
    }

    private Vector3 moveRandomPosition()  // �ړI�n�𐶐��Ax��y�̃|�W�V�����������_���ɒl���擾 
    {
        Vector3 player_pos = player.transform.position;
        Vector3 randomPos = new Vector3(UnityEngine.Random.Range(player_pos.x + 9, player_pos.x - 9), UnityEngine.Random.Range(player_pos.y + 5, player_pos.y - 5), 0);
        return randomPos;
    }

    public void Death()
    {
        if (scoreHandler != null)
        {
            scoreHandler.AddScore(score);
        }
        Destroy(this.gameObject);
    }

    public float GetSetHealth
    {
        get { return health; }
        set { health = value; }
    }

    public float GetSetCharm
    {
        get { return charm; }
        set { charm = value; }
    }

    public bool GetSetFriend
    {
        get { return isFriend; }
        set { isFriend = value; }
    }

    Vector2 CubicOut(float t, float totaltime, Vector2 min, Vector2 max)
    {
        max -= min;
        t = t / totaltime - 1;
        return max * (t * t * t + 1) + min;
    }
}
