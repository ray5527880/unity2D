using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;
public class Camera : MonoBehaviour
{
    private int speed;
    [SerializeField]
    public Object player;
    [SerializeField]
    public Object BoxCollider_B;
    [SerializeField]
    public Object BoxCollider_T;
    [SerializeField]
    public Object BoxCollider_R;
    [SerializeField]
    public Object BoxCollider_L;
    [SerializeField]
    public Object BoxCollider_R_END;
    [SerializeField]
    public Object BoxCollider_L_END;

    private bool IsLookaAt=true;

    private GameObject[] WorldEnds;

    private float LPoint, RPoint, TPoint, BPoint;


    private Vector2 playVector;
    // Start is called before the first frame update
    void Start()
    {
        WorldEnds = GameObject.FindGameObjectsWithTag("WorldEnd");

        LPoint = 0;
        RPoint = 0;
        TPoint = 0;
        BPoint = 0;

        this.transform.position = ((GameObject)player).transform.position;
        speed = ((GameObject)player).GetComponent<play>().speed;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (IsLookaAt)
        {
            this.transform.position = ((GameObject)player).transform.position;
        }

        if (((GameObject)BoxCollider_R_END).GetComponent<Camera_BoxCollider>().IsTriggerWorldEnd)
        {
            IsLookaAt = false;
            //this.transform.position = ((GameObject)player).transform.position;
        }
        //if (((GameObject)BoxCollider_B).GetComponent<Camera_BoxCollider>().IsTriggerStay)
        //{
        //    this.transform.position -= this.transform.up * Time.deltaTime * speed;
        //}
        //if (((GameObject)BoxCollider_T).GetComponent<Camera_BoxCollider>().IsTriggerStay)
        //{
        //    this.transform.position += this.transform.up * Time.deltaTime * speed;
        //}
        //if (((GameObject)BoxCollider_R_END).GetComponent<Camera_BoxCollider>().IsTriggerWorldEnd)
        //{
        //    if (((GameObject)BoxCollider_L).GetComponent<Camera_BoxCollider>().IsTriggerStay)
        //    {
        //        this.transform.position -= this.transform.right * Time.deltaTime * speed;
        //    }
        //}
        //else if (((GameObject)BoxCollider_L_END).GetComponent<Camera_BoxCollider>().IsTriggerWorldEnd)
        //{
        //    if (((GameObject)BoxCollider_R).GetComponent<Camera_BoxCollider>().IsTriggerStay)
        //    {
        //        this.transform.position += this.transform.right * Time.deltaTime * speed;
        //    }
        //}
        //else if (((GameObject)BoxCollider_R).GetComponent<Camera_BoxCollider>().IsTriggerStay!= ((GameObject)BoxCollider_L).GetComponent<Camera_BoxCollider>().IsTriggerStay)
        //{
        //    if (((GameObject)BoxCollider_R).GetComponent<Camera_BoxCollider>().IsTriggerStay)
        //    {
        //        this.transform.position += this.transform.right * Time.deltaTime * speed;
        //    }
        //    else 
        //    {
        //        this.transform.position -= this.transform.right * Time.deltaTime * speed;
        //    }

        //}
        //else if (((GameObject)BoxCollider_L).GetComponent<Camera_BoxCollider>().IsTriggerStay)
        //{
        //    this.transform.position -= this.transform.right * Time.deltaTime * speed;
        //}

        //var e = new WaitForSecondsRealtime(1);
    }
    private bool IsTouchWorldEnd()
    {
        bool reValue = false;
        foreach (var item in WorldEnds)
        {
            if (item.transform.position.x < LPoint)
                LPoint = item.transform.position.x;
            if (item.transform.position.x > RPoint)
                RPoint = item.transform.position.x;
            if (item.transform.position.y < BPoint)
                BPoint = item.transform.position.x;
            if (item.transform.position.y > TPoint)
                TPoint = item.transform.position.x;
        }
        return reValue;
    }
}
