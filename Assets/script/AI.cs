using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class AI : MonoBehaviour
{
    private bool IsRigth = true;
    [SerializeField]
    private OnFloop RonFloop;
    [SerializeField]
    private OnFloop LonFloop;
    [SerializeField]
    private OnFloop OnFloop;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += IsRigth ? Vector3.right * Time.deltaTime*0.5f : -Vector3.right * Time.deltaTime * 0.5f;
        if (RonFloop.IsOnFloop ^ LonFloop.IsOnFloop)
            IsRigth = RonFloop.IsOnFloop;
        if (OnFloop.IsOnFloop)
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
    }
}
