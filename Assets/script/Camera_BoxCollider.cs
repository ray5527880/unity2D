using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_BoxCollider : MonoBehaviour
{
    // Start is called before the first frame update

    public bool IsTriggerStay;

    public bool IsTriggerWorldEnd = false;

    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IsTriggerStay = true;
        }
        else if (collision.tag == "WorldEnd")
        {
            IsTriggerWorldEnd = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IsTriggerStay = false;
        }
        else if (collision.tag == "WorldEnd")
        {
            IsTriggerWorldEnd = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
