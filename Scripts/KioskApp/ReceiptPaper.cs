using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReceiptPaper : MonoBehaviour
{
    public static ReceiptPaper instance { get; private set; }

    Animator animator;

    public Transform cup; // 음료 초기 위치
    public Transform menuPan;   //메뉴판
    public TextMeshProUGUI orderNum;

    bool orderEND;   //주문 완료
    public int counting = 0;

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
        if(PaymentComplete.instance.receiptPaper)
        {
            StartCoroutine(ReceiptPayMove());
        }
        else if(CardmentComplete.instance.receiptPaper)
        {
            StartCoroutine(ReceiptCardMove());
        }

        if(orderEND && counting == 0)
        {
            counting = 1;
            orderEND = false;
            //주문 시간 저장
            PlayerPrefs.SetString("Progress", "N");

            //PlayerPrefs.SetString("OrderTime", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //Debug.Log(PlayerPrefs.GetString("OrderTime"));

        }
    }


    //페이영수증 
    IEnumerator ReceiptPayMove()
    {
        orderEND = true;
        orderNum.text = PlayerPrefs.GetInt("OrderNumber").ToString();   //주문번호 가져오기
        menuPan.GetComponent<Animator>().SetBool("MenuDown", false);
        menuPan.localPosition = new Vector3(0.055f, 0.0325f, 0.325f);   //메뉴판 초기화 위치
        CupCtrl.instance.CupReSet();    //컵 리셋
        MenuPanCtrl.instance.MenuReSet();   //메뉴판 리셋
        //cup.localPosition = new Vector3(-7.5859f, 0.836f, 0.6441f); //컵 초기화 위치
        PaymentComplete.instance.timeLeft = 0f; //결제 시간 초기화
        Class2Rot.instance.SuccessReSet(); //2층 결제 선택 페이지 초기화
        PayTextColumn.instance.WhileColor();    //선택글자 흰색으로
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("PaperAnim", true);

        yield return new WaitForSeconds(3.5f);
        CameraMoving.instacne.GetFloor3 = true;

        yield return new WaitForSeconds(0.1f);
        animator.SetBool("PaperAnim", false);
        orderEND = false;
        counting = 0;
        PaymentComplete.instance.receiptPaper = false;
    }

    //카드 영수증
    IEnumerator ReceiptCardMove()
    {
        orderEND = true;
        orderNum.text = PlayerPrefs.GetInt("OrderNumber").ToString();   //주문번호 가져오기
        menuPan.GetComponent<Animator>().SetBool("MenuDown", false);
        menuPan.localPosition = new Vector3(0.055f, 0.0325f, 0.325f);   //메뉴판 초기화 위치
        CupCtrl.instance.CupReSet();
        MenuPanCtrl.instance.MenuReSet();   //메뉴판 리셋
        //cup.localPosition = new Vector3(-7.5859f, 0.836f, 0.6441f); //컵 초기화 위치
        CardmentComplete.instance.timeLeft = 0f;    //결제시간 초기화
        Class2Rot.instance.SuccessReSet(); //2층 결제 선택 페이지 초기화
        CardTextColumn.instance.WhileColor();   //글자 흰색으로 초기화

        yield return new WaitForSeconds(1.5f);
        animator.SetBool("PaperAnim", true);

        yield return new WaitForSeconds(4.5f);
        CameraMoving.instacne.GetFloor3 = true;

        yield return new WaitForSeconds(0.1f);
        animator.SetBool("PaperAnim", false);
        orderEND = false;
        counting = 0;
        CardmentComplete.instance.receiptPaper = false;

    }
}
