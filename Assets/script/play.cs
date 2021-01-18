using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play : MonoBehaviour
{
    private enum EnumAnimation
    {
        stand = 0,
        walk = 1,
        Run = 2,
        Jump = 3,
        Hited = 4,
        Attack=5
    }
    //private bool IsAttack, IsMove, IsJump, IsHited;
    private EnumAnimation playStatu;



    private SpriteRenderer BoxColliderClick;
    public UIManager _UIManager;
    [Range(1, 10)]
    public int speed;

    [SerializeField]
    public int NowHp = 3;
    [SerializeField]
    public float JumpHigth;

    public Animator animator;
    [SerializeField]
    private Vector2 direction;

    [SerializeField]
    public GameObject[] gameObjects;

    [SerializeField]
    public GameObject MPObjects;

    private bool IsGrounded = false;

    public bool IsHit;

    private Sensor_Bandit mSensor_Bandit;

    [SerializeField]
    public bool IsHited = false;
    [SerializeField]
    public bool iiii;

    private int BackType;
    private Vector3 BackTransform;

    public Transform SowedHitBox;
    [SerializeField]
    public float attackRang;
    [SerializeField]
    public LayerMask monsterLayer;


    // Start is called before the first frame update

    bool isAttacking = false;
    bool IsJump = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        BoxColliderClick = gameObject.GetComponent<SpriteRenderer>();
        _UIManager = GameObject.Find("GameMaster").GetComponent<UIManager>();
        _UIManager.SetPlayHPMax(3);
        _UIManager.SetPlayMPMax(100);

        mSensor_Bandit = gameObject.transform.GetChild(0).gameObject.GetComponent<Sensor_Bandit>();
        playStatu = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsHit && !IsHited)
        {
            var Hor = Input.GetAxis("Horizontal");

            if (Hor > 0)
                GetComponent<SpriteRenderer>().flipX = true;
            else if (Hor < 0)
                GetComponent<SpriteRenderer>().flipX = false;

            if (Input.GetKeyDown(KeyCode.Z) && mSensor_Bandit.State() && !isAttacking)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, JumpHigth);
            }

            if (Input.GetKey(KeyCode.X) && mSensor_Bandit.State() && !isAttacking && !IsHited)
            {
                if (!isAttacking)
                    StartCoroutine(Attack());
            }
            else if (!mSensor_Bandit.State() && !isAttacking && !IsHited)
            {
                IsGrounded = true;
                animator.SetTrigger("Jump");
                animator.SetBool("Grounded", !mSensor_Bandit.State());
            }
            else if (Mathf.Abs(Hor) > Mathf.Epsilon)
                animator.SetInteger("AnimState", 2);
            else
                animator.SetInteger("AnimState", 0);

            if (!isAttacking)
            {
                if (Hor != 0)
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(Hor * speed, this.GetComponent<Rigidbody2D>().velocity.y);
                else
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        
    }

    void MasterAnimation()
    {
        switch (playStatu)
        {
            case EnumAnimation.stand:
                animator.SetInteger("AnimState", 0);
                break;
            case EnumAnimation.Run:
                animator.SetInteger("AnimState", 2);
                break;
            case EnumAnimation.Jump:
                animator.SetTrigger("Jump");
                break;
            case EnumAnimation.Hited:
                animator.SetTrigger("Hurt");
                break;
            case EnumAnimation.Attack:
                animator.SetTrigger("Attack");
                break;
        }
    }
    void MasterTransform()
    {
        switch (playStatu)
        {
            case EnumAnimation.stand:
                break;
            case EnumAnimation.Run:
                break;
            case EnumAnimation.Jump:
                break;
            case EnumAnimation.Hited:
                break;
            case EnumAnimation.Attack:
                break;
        }
    }
    void MasterSwitch()
    {

    }




    public void OnCollisionEnter2D(Collision2D collision)
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "monsterHitBox")
        {
            if (!IsHit)
            {
                if (collision.transform.position.x > this.transform.position.x)
                {
                    BackType = 1;
                }
                else if (collision.transform.position.x < this.transform.position.x)
                {
                    BackType = 2;
                }
                else
                    BackType = 0;

                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
                MoveBack();

                IsHit = !IsHit;
                NowHp -= 1;
                _UIManager.SetPlayHPNow(1);
                monsterFlish();
            }
        }
        else if (collision.tag == "monster")
        {
            if (!IsHit)
            {
                if (collision.transform.position.x > this.transform.position.x)
                {
                    BackType = 1;
                }
                else if (collision.transform.position.x < this.transform.position.x)
                {
                    BackType = 2;
                }
                else
                    BackType = 0;

                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
                MoveBack();
                IsHit = !IsHit;
                NowHp -= 1;
                _UIManager.SetPlayHPNow(1);
                monsterFlish();
            }
        }
    }
    private void monsterFlish()
    {
    
        float _time = Time.time;
      
        StartCoroutine(Flish());
       
    }
    public IEnumerator Flish()
    {
        animator.SetTrigger("Hurt");
        yield return new WaitForSeconds(0.6f);
       
        IsHited = false;
        IsHit = false;
      
    }
    private void keyboard()
    {

    }
    public void Move()
    {
        
    }
    private void MoveBack()
    {

        switch (BackType)
        {
            case 0:
                break;
            case 1:
                GetComponent<SpriteRenderer>().flipX = true;
                
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed/2, this.GetComponent<Rigidbody2D>().velocity.y);
                break;
            case 2:
                GetComponent<SpriteRenderer>().flipX = false;                
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed/2, this.GetComponent<Rigidbody2D>().velocity.y);                
                break;
        }
    }

    public IEnumerator Jump()
    {
        IsJump = true;

        this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, JumpHigth);
        yield return new WaitForSeconds(1);
        IsJump = false;
    }
    public void AnimateMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
    }
    public IEnumerator Attack()
    {
        //var hitEn = Physics2D.OverlapCircleAll(SowedHitBox.position, attackRang, monsterLayer);
        //foreach (var item in hitEn)
        //{
        //    Debug.Log("Hit" + item.name);
        //}


        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);

        _UIManager.SetPlayMPNow(10);
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.4f);
        var position = this.transform.position;
        if (GetComponent<SpriteRenderer>().flipX)
            position += new Vector3(0.15f, 0.15f, 0);
        else
            position += new Vector3(-0.15f, 0.15f, 0);
        var obj = Instantiate(gameObjects[0], position, Quaternion.identity);
        Destroy(obj, 0.2f);
        yield return new WaitForSeconds(0.5f);       
        isAttacking = false;

    }
    private void OnDrawGizmosSelected()
    {
        if (SowedHitBox == null)
            return;
        Gizmos.DrawWireSphere(SowedHitBox.position, attackRang);
    }
    private void castspell()
    {

        MPObjects.GetComponent<UI>().MyCurenValue -= 10;
        Debug.Log(MPObjects.GetComponent<UI>().MyCurenValue);
        Instantiate(gameObjects[0], this.transform.position, Quaternion.identity);
    }
    private void PlayAttack(string name, bool type)
    {


    }
}
