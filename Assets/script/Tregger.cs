using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tregger : MonoBehaviour
{
    [SerializeField]
    public int DelayTime;
    public bool IsTregger;

    private float Times;
    // Start is called before the first frame update
    void Start()
    {
        IsTregger = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (IsTregger && (Time.time - Times) > DelayTime)
        //    IsTregger = false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !IsTregger)
        {
            IsTregger = true;
            Times = Time.time;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        IsTregger = false;
    }
}
