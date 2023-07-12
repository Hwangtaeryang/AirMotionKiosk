using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class2Rot : MonoBehaviour
{
    public static Class2Rot instance { get; private set; }


    Quaternion startRotation;   //2층 시작 위치
    Animator animator;

    [Header("결제텍스트조형물")]
    public GameObject payText;
    public GameObject cardText;

    [Header("백버튼")]
    public GameObject payBack;
    public GameObject cardBack;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        //2층 시작 위치 세팅
        startRotation = Quaternion.Euler(-90.00001f, 0f, 9.95f);
        transform.localRotation = startRotation;

        payBack.SetActive(false);
        cardBack.SetActive(false);
    }

    
    void Update()
    {
        //페이결제 선택 시
        if(PayTextColumn.instance.payMentChoice)
        {
            StartCoroutine(PayMentChoice());
        }

        else if(CardTextColumn.instance.cardMentChoice)
        {
            StartCoroutine(CardMentChoice());
        }



        //페이결제 백 버튼 선택 시
        if(PayBackBtn.instance.payBack)
        {
            StartCoroutine(PayBackButton());
        }
        //카드결제 백 버튼 선택 시
        else if(CardBackBtn.instance.cardBack)
        {
            StartCoroutine(CardBackButton());
        }
    }


    //결제 수단 제로페이 선택 시 함수
    IEnumerator PayMentChoice()
    {
        PayBackBtn.instance.payBack = false;    //페이 백 버튼 선택 해지
        yield return new WaitForSeconds(1f);
        animator.SetBool("RotPay", true);   //2층 페이결제 화면으로 돌림.

        yield return new WaitForSeconds(2f);

        //SoundManager.instance.EnterPhoneSound(); //핸드폰 원통 도는 소리

        animator.SetBool("PayPillar", true);    //핸드폰 원통 돌리기
        payText.GetComponent<Animator>().SetBool("PayRot", false);  //페이 글자 회전 멈추기

        yield return new WaitForSeconds(0.5f);
        payBack.SetActive(true);
        
        yield return new WaitForSeconds(0.2f);
        PayTextColumn.instance.payMentChoice = false;   //결제선택 화면에 있는 선택 해지
    }


    //결제 수단 카드 선택 시 함수
    IEnumerator CardMentChoice()
    {
        CardBackBtn.instance.cardBack = false;  //카드 백 버튼 선택 해지
        yield return new WaitForSeconds(1f);
        animator.SetBool("RotCard", true);  //2층 카드결제 화면으로 돌림

        yield return new WaitForSeconds(2f);

        //SoundManager.instance.EnterPhoneSound(); //카드 들어가는 소리

        animator.SetBool("CardPut", true);  //카드 넣기 애니
        cardText.GetComponent<Animator>().SetBool("CardRot", false);    //카드글자 회전 멈춤
        
        yield return new WaitForSeconds(0.5f);
        cardBack.SetActive(true);
        

        yield return new WaitForSeconds(0.2f);
        CardTextColumn.instance.cardMentChoice = false; //결제선택 화면에 있는 선택 해지
    }



    //제로페이 페이지에서 백버튼 시 함수
    IEnumerator PayBackButton()
    {
        //PayTextColumn.instance.payMentChoice = false;   //결제선택 화면에 있는 선택 해지

        animator.SetBool("RotPay", false);      //결제기본 페이지로 백
        PayTextColumn.instance.WhileColor();

        yield return new WaitForSeconds(1.2f);
        animator.SetBool("PayPillar", false);   //폰 원래 방향(대각선으로)
        transform.localRotation = Quaternion.Euler(-90.00001f, 0f, 9.95f);
        payBack.GetComponent<Animator>().SetBool("PayBackBtn", false);
        payBack.transform.localPosition = new Vector3(0.2506f, 0.1754f, 0.2507f);
        payBack.SetActive(false);
        
        PayBackBtn.instance.payBack = false;
    }

    //카드 페이지에서 백버튼 시 함수
    IEnumerator CardBackButton()
    {
        //CardTextColumn.instance.cardMentChoice = false; //결제선택 화면에 있는 선택 해지
        //CardTextColumn.instance.cardMentComplete = false;

        animator.SetBool("RotCard", false); //결제기본 페이지로 백
        CardTextColumn.instance.WhileColor();

        yield return new WaitForSeconds(1.2f);
        animator.SetBool("CardPut", false); //카드 원래대로
        transform.localRotation = Quaternion.Euler(-90.00001f, 0f, 9.95f);
        cardBack.GetComponent<Animator>().SetBool("CardBackBtn", false);
        //cardBack.transform.localPosition = new Vector3(-0.2727f, 0.1271f, 0.2508f);
        cardBack.SetActive(false);
        
        CardBackBtn.instance.cardBack = false;
    }

    public void SuccessReSet()
    {
        cardBack.GetComponent<Animator>().SetBool("CardBackBtn", false);
        payBack.GetComponent<Animator>().SetBool("PayBackBtn", false);

        cardBack.SetActive(false);
        payBack.SetActive(false);

        animator.SetBool("RotPay", false);      //결제기본 페이지로 백
        animator.SetBool("RotCard", false); //결제기본 페이지로 백
        animator.SetBool("CardPut", false); //카드 원래대로
        animator.SetBool("PayPillar", false);   //폰 원래 방향(대각선으로)
        transform.localRotation = startRotation;
    }

}
