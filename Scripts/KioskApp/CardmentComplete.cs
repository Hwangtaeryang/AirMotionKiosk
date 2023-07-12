using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardmentComplete : MonoBehaviour
{
    public static CardmentComplete instance { get; private set; }

    public float timeLeft = 0;
    public bool receiptPaper;   //영수증단계로 이동 변수
    public GameObject coinPartical; //코인 파티클

    public bool getNetwork; //네트워크 오픈(정보 모두 보내기)
    public bool networkState;   //네트워크 통신을 위한 변수

    [Header("계산 내용")]
    public TextMeshProUGUI name;
    public TextMeshProUGUI won;
    public TextMeshProUGUI amount;
    public TextMeshProUGUI total;
    public TextMeshProUGUI paymenttotle;
    private bool cardComplete;
    private bool cardAnim;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        coinPartical.SetActive(false);
    }

    
    void Update()
    {
        if(CardTextColumn.instance.cardMentComplete)
        {
            timeLeft += Time.deltaTime;

            name.text = PlayerPrefs.GetString("DrinkName");

            won.text = PlayerPrefs.GetInt("Price").ToString();

            amount.text = PlayerPrefs.GetInt("Amount").ToString();

            PlayerPrefs.SetInt("TotalPrice", PlayerPrefs.GetInt("Price") * PlayerPrefs.GetInt("Amount"));
            total.text = PlayerPrefs.GetInt("TotalPrice").ToString();

            paymenttotle.text = PlayerPrefs.GetInt("TotalPrice").ToString();

            if (timeLeft >= 3.1f && timeLeft <= 3.2f && cardComplete == false)
            {
                cardComplete = true;
                SoundManager.instance.EnterCardSound();
            }

            if (timeLeft >= 8f && timeLeft <= 8.1f && cardAnim == false)
            {
                cardAnim = true;
                SoundManager.instance.CardMentEndSound();    //결제완료 사운드
            }

            if (timeLeft > 8.5f && timeLeft <= 9f)
                coinPartical.SetActive(true);


            if (timeLeft >= 10)
            {
                
                // 주문을 서버에 저장 ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                // PlayerPrefs.GetString("DrinkName");
                // PlayerPrefs.GetInt("Price").ToString();
                // PlayerPrefs.GetInt("Amount").ToString();
                // PlayerPrefs.GetInt("TotalPrice").ToString();
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                CameraMoving.instacne.GetFloor1 = true;     //카메라 1층으로 이동 변수
                CardTextColumn.instance.cardMentComplete = false;   //결제 완료 됬으니 초기화
                coinPartical.SetActive(false);
                receiptPaper = true;    //영수증으로 가라는 변수

                // 주문번호, 시간은 서버에서 자동으로 업뎃
                //PlayerPrefs.SetInt("OrderNumber", 123);
                //PlayerPrefs.SetString("OrderTime", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                // 서버에 값 저장 : 반환값은 OrderNumber
                Web_PC.Instance.SendOrder();

                getNetwork = true;
                cardComplete = false;
                cardAnim = false;
                networkState = true;
                timeLeft = 10.1f;
            }
        }

        //네트워크로 정보를 다른앱쪽으로 보내기위함 
        if (getNetwork && Web_PC.Instance.save_completed)
        {
            Web_PC.Instance.save_completed = false;
            getNetwork = false;
            All_AppManager.instance.SetOrderNumber();
        }

        //카드 결제 백버튼(취소) 눌렀을 때
        if(CardBackBtn.instance.cardBack)
        {
            timeLeft = 0;
            CardTextColumn.instance.cardMentComplete = false;
        }
    }

}
