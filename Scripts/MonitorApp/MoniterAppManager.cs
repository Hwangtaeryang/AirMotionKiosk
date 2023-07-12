using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoniterAppManager : MonoBehaviour
{
    public static MoniterAppManager instance { get; private set; }

    public TextMeshProUGUI orderNumber;
    public TextMeshProUGUI[] waitingNumber;
    public TextMeshProUGUI[] completeNumber;

    public OrderNumberData ordernumberdata_script;

    public Text btn;

    //public int pinNumber;
    public bool one;
    public int minNumber = -1;   //준문완료되지 않은 처음숫자
    public int maxNumber = -1;   //완료되지 않은 숫자 끝
    public int orderNum; //주문번호
    public bool networkOn;  //네트워크 열림 여부
    public bool completeBtn;    //완료 버튼 신호
    public int i_index;

    int index = 1;
    int networkCount = 0;   //통신 한번만 들어오게 하기 위한 이중조건 변수
    int networkCount2 = 0;  //통신 한번만 들어오게 하기 위한 이중조건 변수
    int oneCount = 0;   //웨이팅 구역에 한번만 적용하기 위한 이중조건 변수


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        //orderNumber.text = "";
        //for (int i = 0; i < 4; i++)
        //{
        //    completeNumber[i].text = "";
        //    waitingNumber[i].text = "";
        //}
        //Setup();
        Invoke("Setup", 0.01f);
    }


    void Setup()
    {
        //텍스트 초기화
        orderNumber.text = "";
        for (int i = 0; i < 4; i++)
        {
            completeNumber[i].text = "";
            waitingNumber[i].text = "";
        }




        if (ordernumberdata_script.orderNumberList.Count == 0)
        {
            //텍스트 초기화
            for (int i = 0; i < 4; i++)
                waitingNumber[i].text = "";
        }
        else
        {
            //텍스트 초기화
            for (int i = 0; i < 4; i++)
                waitingNumber[i].text = "";
            //if (ordernumberdata_script.orderNumberList.Count <= 4)
            //{
            //    pinNumber = ordernumberdata_script.orderNumberList.Count - 1;   //배열에 넣어야하기때문에 기존 수보다 -1

            //    for (int i = 0; i < ordernumberdata_script.orderNumberList.Count; i++)
            //    {
            //        waitingNumber[i].text = ordernumberdata_script.orderNumberList[i].orderNum.ToString();
            //    }
            //}
            //else if(ordernumberdata_script.orderNumberList.Count > 4)
            //{
            //    pinNumber = 3;  //배열에 넣어야하기때문에 기존 수보다 -1

            //    for (int i = 0; i < 4; i++)
            //    {
            //        waitingNumber[i].text = ordernumberdata_script.orderNumberList[i].orderNum.ToString();
            //    }
            //}

            Debug.Log("주문완료 되지 않은 숫자 " + i_index);
            //주문완료가 되지 않은 최초 넘버를 구하기 위한 식
            for (int i = 0; i < ordernumberdata_script.orderNumberList.Count; i++)
            {
                if (ordernumberdata_script.orderNumberList[i].progress == false)
                {
                    if (minNumber == -1)
                    {
                        minNumber = i;
                        maxNumber = i;
                        Debug.Log("주문완료 되지 않은 숫자 " + i + " ::: " + i_index);
                    }
                }
            }



            //저장된 리스트 갯수가 4개 이하일 때
            if (ordernumberdata_script.orderNumberList.Count <= 4)
            {
                //Debug.Log("4개 이하 " + ordernumberdata_script.orderNumberList.Count + " 몇개 "+ minNumber);

                for (int i = 0; i < ordernumberdata_script.orderNumberList.Count; i++)
                {
                    //0부터 채워넣어야하고 / 데이터는 false인 인자부터 들고와야해서 i+minNumber(최초 숫자)를 더해준다.
                    waitingNumber[i].text = ordernumberdata_script.orderNumberList[i + minNumber].orderNum.ToString();
                    maxNumber++;
                }
            }
            else if (ordernumberdata_script.orderNumberList.Count > 4)
            {
                //Debug.Log("4개 이상 " + ordernumberdata_script.orderNumberList.Count + " 몇개 " + minNumber);

                for (int i = 0; i < 4; i++)
                {
                    //0부터 채워넣어야하고 / 데이터는 false인 인자부터 들고와야해서 i+minNumber(최초 숫자)를 더해준다.
                    waitingNumber[i].text = ordernumberdata_script.orderNumberList[i + minNumber].orderNum.ToString();
                    maxNumber++;
                }
            }

            Debug.Log("최대" + maxNumber);

        }
    }


    void Update()
    {
        //주문이 들어왔다는 신호받으면 실행되는 부분
        if (TestApp.instance.orderState)//networkOn && networkCount == 0)//TestApp.instance.orderState)
        {
            networkOn = false;
            networkCount = 1;
            TestApp.instance.orderState = false;
            ////서버에서 주문번호만 넣어주면 됨 TestApp.instance.orderNum

            //ordernumberdata_script.orderNumberList.Add(new OrderNumber(orderNum, false));
            //Setup();

            ordernumberdata_script.orderNumberList.Add(new OrderNumber(TestApp.instance.orderNum, false));
            Setup();
            Debug.Log(TestApp.instance.orderNum);

            for (int i = 0; i < 4; i++)
            {
                if (waitingNumber[i].text == "" && one)// && oneCount == 0)
                {
                    waitingNumber[i].text = orderNum.ToString();// TestApp.instance.orderNum.ToString();
                                                                //Debug.Log("___" + i + waitingNumber[0].text + "::::"+ ordernumberdata_script.orderNumberList.Count);
                    one = false;
                    oneCount = 1;
                }
                //Debug.Log(i +" ___ "+waitingNumber[i].text+ "::::"+ ordernumberdata_script.orderNumberList.Count);
            }

            btn.text = waitingNumber[0].text;
            StartCoroutine(NetworkCount());

            //StartCoroutine(One_Waiting_Order());
        }


        //음료 준비됬다는 신호받으면 실행되는 부분
        if (ButtonCtrl.instance.completeState)//completeBtn && networkCount2 == 0)//ButtonCtrl.instance.completeState)
        {
            ButtonCtrl.instance.completeState = false;
            completeBtn = false;
            networkCount2 = 1;
            Sound_Manager_Moriter.instance.DingdongSound();
            //ordernumberdata_script.orderNumberList[i_index].progress = true;

            //대기열에 아무것도 없을때
            if (orderNumber.text == "")
            {
                orderNumber.text = waitingNumber[0].text;

                for (int i = 0; i < 4; i++)
                {
                    if (i != 3)
                        waitingNumber[i].text = waitingNumber[i + 1].text;
                    else if (i == 3)
                        waitingNumber[i].text = "";
                }


                for (int k = 0; k < 4; k++)
                {
                    if (waitingNumber[k].text == "")
                    {
                        Debug.Log(k);
                        if (k != 3)
                        {
                            if (waitingNumber[k + 1].text != "")
                                waitingNumber[k].text = ordernumberdata_script.orderNumberList[k + minNumber + 1].orderNum.ToString();
                        }
                    }
                }

                StartCoroutine(NetworkCount2());
            }
            //대기열에 번호가 있을때
            else if (orderNumber.text != "")
            {
                Debug.Log("대기번호있음 " + ordernumberdata_script.orderNumberList.Count);
                for (int j = 3; j >= 0; j--)
                {
                    if (j != 0)
                        completeNumber[j].text = completeNumber[j - 1].text;
                    else if (j == 0)
                        completeNumber[j].text = "";
                }
                completeNumber[0].text = orderNumber.text;
                orderNumber.text = waitingNumber[0].text;


                for (int i = 0; i < 4; i++)
                {
                    if (i != 3)
                    {
                        Debug.Log("들어왔어요!!!!" + i + ":::" + waitingNumber[i + 1].text);
                        waitingNumber[i].text = waitingNumber[i + 1].text;
                    }
                    else if (i == 3)
                    {
                        waitingNumber[i].text = "";
                    }

                }

                for (int k = 0; k < 4; k++)
                {
                    if (waitingNumber[k].text == "")
                    {
                        index += 1;
                        Debug.Log("++" + (k + minNumber + index) + ":" + ordernumberdata_script.orderNumberList.Count);
                        if ((k + minNumber + index) <= ordernumberdata_script.orderNumberList.Count - 1)
                        {
                            Debug.Log("++" + k + ":" + index + "::" + minNumber);
                            waitingNumber[k].text = ordernumberdata_script.orderNumberList[k + minNumber + index].orderNum.ToString();
                        }

                    }
                }
            }
        }


    }

    IEnumerator NetworkCount()
    {
        yield return new WaitForSeconds(6.1f);
        networkCount = 0;
        networkOn = false;
    }


    IEnumerator NetworkCount2()
    {
        yield return new WaitForSeconds(1f);
        networkCount2 = 0;
        completeBtn = false;
    }

    IEnumerator One_Waiting_Order()
    {
        yield return new WaitForSeconds(6.1f);
        oneCount = 0;
        one = false;
    }
}
