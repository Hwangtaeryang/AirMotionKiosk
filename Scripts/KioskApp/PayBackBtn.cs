using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayBackBtn : MonoBehaviour
{
    public static PayBackBtn instance { get; private set; }

    Animator animator;

    public bool payBack;    //2층 회전판에 보내는 신호값



    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }



    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("L_Hand") || other.CompareTag("R_Hand"))
        {
            Debug.Log("백");
            payBack = true;
            SoundManager.instance.BackSound();
            animator.SetBool("PayBackBtn", true);
        }
    }
}
