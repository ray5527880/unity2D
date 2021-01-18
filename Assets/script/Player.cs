using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private SpriteRenderer BoxColliderClick;
    public UIManager _UIManager;
    [Range(1,10)]
    public int speed;

    [SerializeField]
    public int NowHp = 3;

    public Animator animator;
    [SerializeField]
    private Vector2 direction;

    [SerializeField]
    public GameObject[] gameObjects;

    [SerializeField]
    public GameObject MPObjects;

    public bool IsHit;

    private bool IsHited = false;
    private int BackType;
    private Vector3 BackTransform;

    // Start is called before the first frame update

    bool isAttacking = false;
    bool IsJump = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        //MPObjects.GetComponent<UI>().MaxValue = 100;
        //MPObjects.GetComponent<UI>().MyCurenValue = 100;
        BoxColliderClick = gameObject.GetComponent<SpriteRenderer>();
        _UIManager = GameObject.Find("GameMaster").GetComponent<UIManager>();
        _UIManager.SetPlayHPMax(3);
        _UIManager.SetPlayMPMax(100);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "monster")
        {
            IsHited = true;
            if (this.transform.position.x < collision.transform.position.x)
            {
                BackTransform = this.transform.position - Vector3.right * 5;
                BackType = 1;
            }
            else if (this.transform.position.x > collision.transform.position.x)
            {
                BackTransform = this.transform.position - Vector3.left * 5;
                BackType = 2;
            }
            else
            {
                BackType = 0;
                return;
            }
            IsHit = !IsHit;
            NowHp -= 1;
            _UIManager.SetPlayHPNow(1);
            monsterFlish();
             
        }
    }
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "monsterHitBox")
        {
            if (!IsHit)
            {
                IsHited = true;
                if (this.transform.position.x < collision.transform.position.x)
                {
                    BackTransform = this.transform.position - Vector3.right * 5;
                    BackType = 1;
                }
                else if (this.transform.position.x > collision.transform.position.x)
                {
                    BackTransform = this.transform.position - Vector3.left * 5;
                    BackType = 2;
                }
                else
                {
                    BackType = 0;
                    return;
                }
                IsHit = !IsHit;
                NowHp -= 1;
                _UIManager.SetPlayHPNow(1);
                monsterFlish();
            }
        }
        if (collision.tag == "monster")
        {
            IsHited = true;
            
            IsHit = !IsHit;
            NowHp -= 1;
            _UIManager.SetPlayHPNow(1);
            monsterFlish();

        }
    }
    private void monsterFlish()
    {
        float _time = Time.time;
        for (int i = 0; i < 30; i++)
        {
            StartCoroutine(Flish());
        }
        IsHit = !IsHit;
    }
    public IEnumerator Flish()
    {
        BoxColliderClick.enabled = false;
        yield return new WaitForSeconds(0.1f);
        MoveBack();
        BoxColliderClick.enabled = true;
        yield return new WaitForSeconds(0.1f);
        MoveBack();
        BoxColliderClick.enabled = false;
        yield return new WaitForSeconds(0.1f);
        MoveBack();
        BoxColliderClick.enabled = true;
        IsHited = false;
        //shake += Time.time;
        //取余运算，结果是0到被除数之间的值
        //如果除数是1 1.1 1.2 1.3 1.4 1.5 1.6 1.7 1.8 1.9 2.0 2.1
        //那么余数是0 0.1 0.2 0.3 0.4 0.5 0.6 0.7 0.8 0.9 0 0.1


    }
    public void Move()
    {
        direction = Vector2.zero;

        var Hor = Input.GetAxis("Horizontal");
        if (Mathf.Abs(Hor) > Mathf.Epsilon)
            animator.SetInteger("AnimState", 2);
        else
            animator.SetInteger("AnimState", 0);

        //Combat Idle
        //else if (m_combatIdle)
        //    m_animator.SetInteger("AnimState", 1);

        //Idle



        if (Hor != 0)
        {
            animator.SetFloat("AirSpeed", this.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (!isAttacking&&!IsHited)
        {           
            //if (Hor > 0)
            //{
            //    direction += Vector2.right;
            //    this.transform.rotation = Quaternion.Euler(0, 0, 0);
            //}
            //else if (Hor < 0)
            //{
            //    direction += Vector2.right;
            //    this.transform.rotation = Quaternion.Euler(0, 180, 0);
            //}
            if (Input.GetKey(KeyCode.X))
            {
                StartCoroutine(Attack());
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                if (!IsJump)
                    StartCoroutine(Jump());
            }
        }
              

        this.transform.Translate(direction * speed * Time.deltaTime);
        //if (direction.x != 0 || direction.y != 0)
        //{
        //    AnimateMovement(direction);
        //}
        //else
        //{
        //    animator.SetLayerWeight(1, 0);
        //}

    }
    private void MoveBack() 
    {
        switch (BackType)
        {
            case 0:
                break;
            case 1:
                this.transform.position = Vector3.Lerp(this.transform.position, BackTransform, Time.deltaTime);
                break;
            case 2:
                this.transform.position = Vector3.Lerp(this.transform.position, BackTransform, Time.deltaTime);
                break;
        }
    }
    public IEnumerator Jump()
    {
        IsJump = true;
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
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
        _UIManager.SetPlayMPNow(10);
        isAttacking = true;
        animator.SetBool("attcan", true);
        var position = this.transform.position;
        position += new Vector3(0.5f, 0,0);
        var obj = Instantiate(gameObjects[0], position, Quaternion.identity);
        Destroy(obj, 0.1f);
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("attcan", false);
        //castspell();
        isAttacking = false;

    }
    private void castspell()
    {

        MPObjects.GetComponent<UI>().MyCurenValue -= 10;
        Debug.Log(MPObjects.GetComponent<UI>().MyCurenValue);
        Instantiate(gameObjects[0], this.transform.position, Quaternion.identity);
    }
    private void PlayAttack(string name,bool type)
    {
        

    }
}
