using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayMentPageBack : MonoBehaviour
{
    public static PayMentPageBack instance { get; private set; }


    public bool paymentPageBack;    //카메라에 보내는 bool 값
    Animator animator;



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
        if (!paymentPageBack)
        {
            animator.SetTrigger("Idle");
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("L_Hand") || other.CompareTag("R_Hand"))
        {
            //현재 오브젝트 이름이 PayMentPageBackBtn 이고, 결제페이지 터치를 한 상태이면
            if (gameObject.name == "PayMentPageBackBtn")
            {
                paymentPageBack = true; //카메라에 신호 넘긴다.
                SoundManager.instance.BackSound();
                animator.SetTrigger("PayMentPageBack");
            }
        }
    }
}
