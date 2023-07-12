using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerApp : MonoBehaviour
{
    public static ManagerApp instance { get; private set; }

    public GameObject popup;

    public bool btnOn;  //숫자버튼 
    public bool nextBtnOn;  //다음버튼
    public bool previousBtnOn;  //이전버튼
    public bool noticeState;    //알람상태
    public bool networkOn;  //네트워크 상태
    public bool completeBtn;    //주문완료버튼 여부(MyMenu스크립에서 사용)
    public Toggle noticeToggle; //알람토글

    public TextMeshProUGUI testText;   //테스트용

    int networkCount = 0;   //정보 한번만 들어오게 하기위한 이중 조건 변수

    [Header("주문내용")]
    public Text order;  //주문 음료
    public Text amount; //수량
    public Text number; //주문번호
    public Text time;   //시간
    public Text price;  //가격


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {


        btnOn = false;  //페이지 버튼 눌렀는지 상태 여부
        noticeState = true; //초기 세팅값(On모드)
    }

    // Update is called once per frame
    void Update()
    {
        if(noticeState == false)
        {
            //Debug.Log("여긴");
            popup.SetActive(false);
        }
        else if(noticeState && OrderBasePage.instance.networkOn && networkCount == 0)
        {
            //Debug.Log("안들와유");
            networkCount = 1;
            Sound_Manager_Moriter.instance.DingdongSound();
            StartCoroutine(NoticePopup());

            StartCoroutine(NetworkCount());
        }


        if (networkOn)
        {
            Debug.Log("안들어왔나요?");
            testText.text = All_AppManager.instance.time.ToString();
            //networkOn = false;
        }
    }

    IEnumerator NetworkCount()
    {
        yield return new WaitForSeconds(6.1f);
        networkCount = 0;
    }

    public void NoticeToggle()
    {
        if(noticeToggle.isOn)
        {
            //Debug.Log("Off");
            noticeState = false;
        }
        else
        {
            //Debug.Log("On");
            noticeState = true;
        }
    }

    IEnumerator NoticePopup()
    {
        popup.SetActive(true);
        number.text = All_AppManager.instance.orderNumber.ToString();
        amount.text = OrderBasePage.instance.amount.ToString();
        order.text = OrderBasePage.instance.drinkname;              
        time.text = OrderBasePage.instance.time;
        price.text = OrderBasePage.instance.totalPrice.ToString();


        yield return new WaitForSeconds(5f);

        popup.SetActive(false);
    }
}
