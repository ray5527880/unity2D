using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knith : MonoBehaviour
{
    private Player player;
    private Animator animator;
    private Rigidbody2D myRigidbody;

    private float times;

    private Quaternion knifeTransform
    {
        get
        {
            var trans = Quaternion.Euler(0f, 0f, 0f);
            float x = animator.GetFloat("X");
            float y = animator.GetFloat("Y");

            if (x > 0)
            {
                trans = Quaternion.Euler(0f, 0f, -90f);
                this.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, 0);
            }
            else if (x < 0)
            {
                trans = Quaternion.Euler(0f, 0f, 90f);
                this.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y , 0);
            }
            else if (y > 0)
            {
                trans = Quaternion.Euler(0f, 0f, 0f);
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, 0);
            }
            else if (y < 0)
            {
                trans = Quaternion.Euler(0f, 0f, 180f);
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 1, 0);
            }
            return trans;
        }
    }

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        //myRigidbody = GetComponent<Rigidbody2D>();
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().animator;
        var dic = GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position;
        var angle = Mathf.Atan2(dic.x, dic.y);

        this.transform.rotation = knifeTransform;
        times = Time.time;
        Destroy(this.gameObject, 1);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "moster")
    //    {
    //        other.GetComponent<monster>().NowHp - 1;
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        
    }
}
