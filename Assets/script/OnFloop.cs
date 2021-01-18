using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFloop : MonoBehaviour
{
    public bool IsOnFloop;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        IsOnFloop = collision.tag == "floop" ? true : false;        
    }
}
