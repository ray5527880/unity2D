using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class monster : MonoBehaviour
{
    [SerializeField]
    public GameObject HP;
    [SerializeField]
    public bool HaveAI;

    [SerializeField]
    public float speed;

    private bool IsMoveCK;

    public Animator animator;

    private bool IsHit = false;
    private bool IsMove = false;

    private bool IsAttack = false;

    public UIManager _UIManager;
    private bool isInvincible;

    private float shake;
    private SpriteRenderer BoxColliderClick;

    [SerializeField]
    public GameObject[] gameObjects;

    [SerializeField]
    public int DelayTime;

    private GameObject ParentGO;

    private Tregger RTregger;
    private Tregger LTregger;

    private float time;

    public int MaxHp = 3;
    private int hp;

    private float times;
    public int NowHp
    {
        get
        {
            return hp;
        }
        set
        {
            if (value > MaxHp)
                hp = MaxHp;
            else if (value < 0)
                hp = 0;
            else
                hp = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        NowHp = MaxHp;
        HP.GetComponent<UI>().MaxValue = MaxHp;

        BoxColliderClick = gameObject.GetComponent<SpriteRenderer>();
        _UIManager = GameObject.Find("GameMaster").GetComponent<UIManager>();

        animator = GetComponent<Animator>();
        times = Time.time;

        ParentGO = gameObject.transform.parent.gameObject;
        RTregger = ParentGO.gameObject.transform.GetChild(1).gameObject.GetComponent<Tregger>();
        LTregger = ParentGO.gameObject.transform.GetChild(2).gameObject.GetComponent<Tregger>();
    }
    public IEnumerator Attack()
    {
        IsAttack = true;
        times = Time.time;
        IsMoveCK = true;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);

        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.4f);
        
        var position = this.transform.position;
        if (GetComponent<SpriteRenderer>().flipX)
            position += new Vector3(0.15f, 0.15f, 0);
        else
            position += new Vector3(-0.15f, 0.15f, 0);
        var obj = Instantiate(gameObjects[0], position, Quaternion.identity);
        Destroy(obj, 0.2f);
        yield return new WaitForSeconds(0.4f);
        //animator.SetBool("attcan", false);

        yield return new WaitForSeconds(2f);
        //castspell();
        IsAttack = false;

    }
    // Update is called once per frame
    void Update()
    {
        RTregger.transform.position = this.transform.position + new Vector3(-0.255f, 0.185f, 0);
        LTregger.transform.position = this.transform.position + new Vector3(0.255f, 0.185f, 0);
        //ParentGO.transform.position = this.transform.worldToLocalMatrix;
        //ParentGO.transform.position += this.transform.position;
        if ((Time.time - time) > DelayTime)
        {
            if (RTregger.IsTregger && !IsAttack)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                time = Time.time;
                StartCoroutine(Attack());
            }
            else if (LTregger.IsTregger && !IsAttack)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                time = Time.time;
                StartCoroutine(Attack());
            }
        }

        if (NowHp == 0)
            Destroy(this.gameObject, 1);

        
        if (HaveAI)
        {
            if ((Time.time - times) > 6)
            {               
                times = Time.time;
            }
            else if ((Time.time - times) > 3)
                Move();
             else
                animator.SetInteger("AnimState", 0);
            //if (IsMoveCK)
            //    Move();
            //if (Time.time - times > 3)
            //{
            //    //animator.SetInteger("AnimState", 0);
            //    //IsMoveCK = !IsMoveCK;
            //    //times = Time.time;
            //}            
        }

    }
    private void Move()
    {
        //var Hor = Input.GetAxis("Horizontal");
       
        //this.animator.SetBool("move", true);

        //f(Mathf.Abs(Hor) > Mathf.Epsilon)
                animator.SetInteger("AnimState", 2);
        //else
        //animator.SetInteger("AnimState", 0);

        if (this.transform.position.x > GameObject.Find("HeavyBandit").transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            this.transform.position += Vector3.left * Time.deltaTime;
            //this.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, this.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            this.transform.position += Vector3.right * Time.deltaTime;
            //this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, this.GetComponent<Rigidbody2D>().velocity.y);
        }
        var trans = GameObject.Find("HeavyBandit").transform.position;        
    }


    private void monsterFlish()
    {
        float _time = Time.time;
        for(int i = 0; i < 30; i++)
        {
            StartCoroutine(Flish());
        }
        IsHit = !IsHit;
    }
    public IEnumerator Flish()
    {

        animator.SetTrigger("Hurt");
        yield return new WaitForSeconds(0.2f);                
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "sword")
        {
            if (!IsHit)
            {
                IsHit = !IsHit;
                NowHp -= 10;
                _UIManager.SetMonsterNow(10);
                monsterFlish();
            }
        }
        //else if (collision.tag == "Player")
        //{
        //    collision.isTrigger = false;
        //}      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (/*HaveAI &&*/ collision.collider.tag == "Player")
        {
            collision.collider.isTrigger = false;
            StartCoroutine(Attack());
        }
    }
}
