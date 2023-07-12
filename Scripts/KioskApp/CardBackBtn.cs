using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBackBtn : MonoBehaviour
{
    public static CardBackBtn instance { get; private set; }

    Animator animator;

    public bool cardBack;    //2층 회전판에 보내는 신호값




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
            cardBack = true;
            SoundManager.instance.BackSound();
            animator.SetBool("CardBackBtn", true);
            
        }
    }
}
