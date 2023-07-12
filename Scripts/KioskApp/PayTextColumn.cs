using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PayTextColumn : MonoBehaviour
{
    public static PayTextColumn instance { get; private set; }

    public bool payMentChoice;  //제로페이 결제 선택(2층 원통 페이결제 화면으로 돌리는 변수)
    public bool payMentComplete;    //페이 결제 완료하는 변수
    public GameObject payBack;  //제로페이 백버튼

    Animator animator;
    Renderer payRend;   //텍스트 렌더러

    [Header("텍스트 색상변경")]
    public float speed;
    public Color startColor;
    public Color endColor;
    float timeLeft = 0;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        payRend = GetComponent<Renderer>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("L_Hand") || other.CompareTag("R_Hand"))
        {
            SoundManager.instance.ChoiceSound();
            payMentChoice = true;   //페이화면으로 돌리는 변수
            payMentComplete = true; //페이 결제 변수
            animator.SetBool("PayRot", true);   //페이 돌리기
            payBack.GetComponent<Animator>().SetBool("PayBackBtn", false);  //제로페이백버튼 초기화 돌려놓기
            StartCoroutine("ChangeColor");
        }
    }

    IEnumerator ChangeColor()
    {
        //Debug.Log("----???");
        while (timeLeft < 3f)
        {
            timeLeft = (timeLeft + Time.deltaTime) * speed;

            if (timeLeft >= 3f)
                timeLeft = 3.1f;

            payRend.material.color = Color.Lerp(startColor, endColor, timeLeft);
            //Debug.Log(payRend.material.color);

            yield return new WaitForEndOfFrame();
        }
    }

    public void WhileColor()
    {
        payMentChoice = false;   //결제선택 화면에 있는 선택 해지
        payMentComplete = false;

       timeLeft = 0f;  //색변경 초 초기화
        payRend.material.color = startColor;
    }
}
