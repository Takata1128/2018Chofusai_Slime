using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class Expression : MonoBehaviour
{
    protected Animator animator;
    protected int smileId;
    protected int idleId;
    protected int angryId;
    protected int stayColliderNum=0;
    protected GameObject rightHand;
    protected GameObject leftHand;
    protected GameObject refObj;
    protected int fingerCnt;
    protected float touchspeed;
    protected const float threshspeed = 1.0f;
    public int isTouched=0;


    void Start()
    {
        animator = GetComponent<Animator>();
        smileId = Animator.StringToHash("Smile");
        idleId = Animator.StringToHash("Idle");
        angryId = Animator.StringToHash("Angry");
        rightHand = GameObject.FindGameObjectWithTag("RightHand");
        leftHand = GameObject.FindGameObjectWithTag("LeftHand");
        refObj = GameObject.Find("GameObject");

    }

    void Update()
    {
         if (Input.GetKey(KeyCode.B))
         {
             transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
             transform.position -= new Vector3(0, 0.005f, 0);
         }
         if (Input.GetKey(KeyCode.S))
         {
             transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
             transform.position += new Vector3(0, 0.005f, 0);
         }

        /*Debug.Log(PythonInterface.radius_ori);
        Debug.Log(transform.localScale.x);
        while (transform.localScale.x<=PythonInterface.radius_ori/250.0f)
        {
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            transform.position -= new Vector3(0, 0.005f, 0);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "finger")
        {
            fingerCnt++;
            if (fingerCnt == 1||fingerCnt==2)
            {
                animator.SetBool(angryId, true);
            }
            else
            {
                animator.SetBool(angryId, false);
                animator.SetBool(smileId, true);
            }
        }
        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "finger")
        {
            fingerCnt--;
        }

        if (fingerCnt == 0)
        {

            animator.SetBool(smileId, false);
            animator.SetBool(angryId, false);
        }

    }
}