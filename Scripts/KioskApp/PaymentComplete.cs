using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaymentComplete : MonoBehaviour
{
    public static PaymentComplete instance { get; private set; }


    public float timeLeft = 0;
    public bool receiptPaper;   //영수증단계로 이동 변수
    public GameObject coinPartical; //코인 파티클

    public bool getNetwork; //네트워크 오픈(정보 모두 보내기)
    public bool networkState;   //네트워크 통신을 위한 변수

    [Header("계산 내용")]
    public TextMeshProUGUI name;
    public TextMeshProUGUI won;
    public TextMeshProUGUI amount;
    public TextMeshProUGUI totle;
    public TextMeshProUGUI paymenttotle;
    private bool payComplete;
    private bool phoneAnim;

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
        //결제 완료 될때까지의 시간
        if(PayTextColumn.instance.payMentComplete)
        {
            timeLeft += Time.deltaTime;

            name.text = PlayerPrefs.GetString("DrinkName");
            won.text = PlayerPrefs.GetInt("Price").ToString();
            amount.text = PlayerPrefs.GetInt("Amount").ToString();
            PlayerPrefs.SetInt("TotalPrice", PlayerPrefs.GetInt("Price") * PlayerPrefs.GetInt("Amount"));
            totle.text = PlayerPrefs.GetInt("TotalPrice").ToString();
            paymenttotle.text = PlayerPrefs.GetInt("TotalPrice").ToString();


            if (timeLeft >= 3.1f && timeLeft <= 3.2f && phoneAnim == false)
            {
                phoneAnim = true;
                SoundManager.instance.EnterPhoneSound();
            }

            if (timeLeft >= 8f && timeLeft <= 8.1f && payComplete == false)
            {
                payComplete = true;
                SoundManager.instance.PayMentEndSound();    //결제완료 사운드
            }

            if (timeLeft > 8.5f && timeLeft <= 9f)
            {
                coinPartical.SetActive(true);
            }


            if (timeLeft >= 10)
            {
                CameraMoving.instacne.GetFloor1 = true;     //카메라 1층으로 이동 변수
                PayTextColumn.instance.payMentComplete = false; //결제 완료 됫으니 초기화
                coinPartical.SetActive(false);
                receiptPaper = true;    //영수증으로 가라는 변수

                //PlayerPrefs.SetInt("OrderNumber", 123);
                //PlayerPrefs.SetString("OrderTime", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                // 서버에 값 저장 : 반환값은 OrderNumber
                Web_PC.Instance.SendOrder();

                getNetwork = true;
                payComplete = false;
                phoneAnim = false;
                networkState = true;
                timeLeft = 10.1f;
                //Debug.Log("끝" + timeLeft);
            }
        }

        //네트워크로 정보를 다른앱쪽으로 보내기위함
        if (getNetwork && Web_PC.Instance.save_completed)
        {
            Web_PC.Instance.save_completed = false;
            getNetwork = false;
            All_AppManager.instance.SetOrderNumber();
        }

        //페이 결제 백버튼(취소) 눌렀을 때
        if (PayBackBtn.instance.payBack)
        {
            Debug.Log("페이백");
            timeLeft = 0;
            PayTextColumn.instance.payMentComplete = false;
        }
    }
}
