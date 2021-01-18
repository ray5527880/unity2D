using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class AI_2 : MonoBehaviour
{
    bool IsBack = false;
    bool IsRigth = false;
    [SerializeField]
    private GameObject RViewBrack;
    [SerializeField]
    private GameObject LViewBrack;
    [SerializeField]
    private OnFloop OnFloop;
    private Vector3 startPoint;
    private float Times;
    private float FTime = 3;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        Times = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        RViewBrack.GetComponent<BoxCollider2D>().enabled = IsRigth;
        LViewBrack.GetComponent<BoxCollider2D>().enabled = !IsRigth;

        if (Time.time - Times > FTime)
        {
            IsRigth = !IsRigth;
            Times = Time.time;
        }

        if (RViewBrack.GetComponent<IsTrigger>().IsTriggers || LViewBrack.GetComponent<IsTrigger>().IsTriggers)
        {
            if ((this.transform.position - startPoint).sqrMagnitude > 3)
            {
                IsBack = true;
                IsRigth = !IsRigth;
            }
            else
            {
                var APosition = new Vector3(GameObject.Find("HeavyBandit").transform.position.x, this.transform.position.y, 0);
                this.transform.position = Vector3.Lerp(this.transform.position, APosition, Time.deltaTime * 0.5f);
            }
        }
        else if (IsBack)
        {
            //var APosition = new Vector3(GameObject.Find("HeavyBandit").transform.position.x, this.transform.position.y, 0);
            this.transform.position = Vector3.Lerp(this.transform.position, startPoint, Time.deltaTime * 0.5f);
            if ((this.transform.position - startPoint).sqrMagnitude < 1)
            {
                IsBack = false;
                IsRigth = !IsRigth;
            }
        }
        else
        {
            this.transform.position += IsRigth ? Vector3.right * Time.deltaTime * 0.5f : -Vector3.right * Time.deltaTime * 0.5f;

        }
        if (OnFloop.IsOnFloop)
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
    }
}
