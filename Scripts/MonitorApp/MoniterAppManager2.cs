using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoniterAppManager2 : MonoBehaviour
{
    public static MoniterAppManager2 instance { get; private set; }

    public TextMeshProUGUI orderNumber;
    public TextMeshProUGUI[] waitingNumber;
    public TextMeshProUGUI[] completeNumber;

    public OrderNumberData ordernumberdata_script;

    //5분 쿨타임 주기위해 스크립트 들고온다.
    public TextDisappearCoolTime textOrderCoolTime_script;
    public TextDisappearCoolTime textComplete1CoolTime_script;
    public TextDisappearCoolTime textComplete2CoolTime_script;
    public TextDisappearCoolTime textComplete3CoolTime_script;
    public TextDisappearCoolTime textComplete4CoolTime_script;

    public Text btn;
    public int noCompleteCount = 0; //완료되지 않은 시작 인덱스 구할때 한번만 적용하기 위한 이중조건 변수
    public int noCompleteMinIndex = -1; //완료되지않은 시작 인덱스
    public int noCompleteMaxIndex = -1; //완료되지않은 마지막 인덱스
    public bool one;
    public int orderNum; //주문번호
    public int index = 1;   //리스트 정보를 한칸씩 넘겨주기 위한 변수

    public bool networkOn;  //통신 보냈는지 여부
    public int networkCount = 0;    //통신 한번만 받기 위한 이중조건 변수
    public bool completeBtn;    //완료 버튼 신호
    public int networkCount2 = 0;    //통신 한번만 받기 위한 이중조건 변수


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {
        StartCoroutine(SetUp());
    }

    IEnumerator SetUp()
    {
        yield return new WaitForSeconds(1f);


        if (ordernumberdata_script.orderNumberList.Count == 0)
        {
            //텍스트 초기화
            orderNumber.text = "";
            for (int i = 0; i < 4; i++)
            {
                completeNumber[i].text = "";
                waitingNumber[i].text = "";
            }
        }
        else
        {
            //텍스트 초기화
            orderNumber.text = "";
            for (int i = 0; i < 4; i++)
            {
                completeNumber[i].text = "";
                waitingNumber[i].text = "";
            }

            //주문완료 되지 않은 시작인덱스, 마지막 인덱스 구하기
            for (int i = 0; i < ordernumberdata_script.orderNumberList.Count; i++)
            {
                if (ordernumberdata_script.orderNumberList[i].progress == false)
                {
                    if (noCompleteMinIndex == -1)
                    {
                        noCompleteMinIndex = i;
                        noCompleteMaxIndex = i;
                    }
                }
            }

            ///처음 모니터앱에 세팅해주기 위한 초기 배치
            ///저장된 리스트 갯수가 4개 이하일 때
            if (ordernumberdata_script.orderNumberList.Count <= 4)
            {
                for (int i = 0; i < ordernumberdata_script.orderNumberList.Count; i++)
                {
                    waitingNumber[i].text = ordernumberdata_script.orderNumberList[i + noCompleteMinIndex].orderNum.ToString();
                    noCompleteMaxIndex++;
                }
            }
            ///저장된 리스트 갯수가 4개 이상일 때
            else if (ordernumberdata_script.orderNumberList.Count > 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    waitingNumber[i].text = ordernumberdata_script.orderNumberList[i + noCompleteMinIndex].orderNum.ToString();
                    noCompleteMaxIndex++;
                }
            }

        }
    }


    void Update()
    {
        //주문이 들어왔다는 신호를 받으면 실행되는 부분
        if (networkOn && networkCount == 0)//TestApp.instance.orderState)
        {
            networkOn = false;
            networkCount = 1;
            //TestApp.instance.orderState = false;

            //ordernumberdata_script.orderNumberList.Add(new OrderNumber(TestApp.instance.orderNum, false));
            Debug.Log(orderNum);
            ordernumberdata_script.orderNumberList.Add(new OrderNumber(orderNum, false));
            ListSetting();
            StartCoroutine(NetworkCount());
        }

        //음료 준비됬다는 신호받으면 실행되는 부분
        if (completeBtn && networkCount2 == 0)//ButtonCtrl.instance.completeState)
        {
            completeBtn = false;
            networkCount2 = 1;
            //ButtonCtrl.instance.completeState = false;
            Sound_Manager_Moriter.instance.DingdongSound();
            
            Debug.Log("작은거" + noCompleteMinIndex);
            //StartCoroutine(CompleteUIUpdate2(noCompleteMinIndex));
            CompleteUIUpdate(noCompleteMinIndex);
            StartCoroutine(NetworkCount2());
        }

        //5분 쿨타임이 끝나면 한칸씩 밀어낸다.
        if(textOrderCoolTime_script.orderNum_CoolEnd)
        {
            textOrderCoolTime_script.orderNum_CoolEnd = false;

            //주문완료열에 있는 번호들 한칸씩 뒤로 뺀다.
            for (int i = 3; i >= 0; i--)
            {
                if (i != 0)
                    completeNumber[i].text = completeNumber[i - 1].text;
                else if (i == 0)
                    completeNumber[i].text = "";
            }
            completeNumber[0].text = orderNumber.text;  //주문완료열 첫번째에 완료주문번호판에 있는 주문번호를 넣어준다.
            orderNumber.text = waitingNumber[0].text;   //완료주문번호판에는 대기열 첫번째에 있는 정보를 넣어준다.
        }
        
    }

    //주문완료 되지 않는 시작인텍스 , 마지막 인덱스를 구한다.
    public void NoCompleteIndexFind()
    {
        Debug.Log("총갯수" + ordernumberdata_script.orderNumberList.Count);
        //주문완료 되지 않은 시작인덱스, 마지막 인덱스 구하기
        for (int i = 0; i < ordernumberdata_script.orderNumberList.Count; i++)
        {
            if (ordernumberdata_script.orderNumberList[i].progress == false)
            {
                Debug.Log("완료안됨" + i);
                if(ordernumberdata_script.orderNumberList.Count == 1)
                {
                    if (noCompleteMinIndex == -1)
                    {
                        noCompleteMinIndex = i;
                        noCompleteMaxIndex = i;
                    }
                }
                else
                {
                    if (noCompleteMinIndex != -1 && noCompleteCount == 0)
                    {
                        noCompleteCount = 1;
                        noCompleteMinIndex = i;
                        noCompleteMaxIndex = i;
                    }
                }
                
            }
        }

        noCompleteCount = 0;
    }

    ///주문 들어오면 리스트 셋팅해주는 함수
    public void ListSetting()
    {

        NoCompleteIndexFind();
        ///처음 모니터앱에 세팅해주기 위한 초기 배치
        ///저장된 리스트 갯수가 4개 이하일 때
        //if (ordernumberdata_script.orderNumberList.Count <= 4)
        //{
        //    for (int i = 0; i < ordernumberdata_script.orderNumberList.Count; i++)
        //    {
        //        Debug.Log("죽것다" + i + " ::: " + (i + noCompleteMinIndex));
        //        waitingNumber[i].text = ordernumberdata_script.orderNumberList[i + noCompleteMinIndex].orderNum.ToString();
        //        noCompleteMaxIndex++;
        //    }
        //}
        /////저장된 리스트 갯수가 4개 이상일 때
        //else if (ordernumberdata_script.orderNumberList.Count > 4)
        //{
        //    for (int i = 0; i < 4; i++)
        //    {
        //        waitingNumber[i].text = ordernumberdata_script.orderNumberList[i + noCompleteMinIndex].orderNum.ToString();
        //        noCompleteMaxIndex++;
        //    }
        //}




        for (int i = 0; i < 4; i++)
        {
            if (waitingNumber[i].text == "" && one)// && oneCount == 0)
            {
                waitingNumber[i].text = orderNum.ToString(); //TestApp.instance.orderNum.ToString();
                Debug.Log("___" + i +"::"+ orderNum.ToString() + "::::"+ ordernumberdata_script.orderNumberList.Count);
                one = false;
            }
            //Debug.Log(i +" ___ "+waitingNumber[i].text+ "::::"+ ordernumberdata_script.orderNumberList.Count);
        }

        btn.text = waitingNumber[0].text;
    }

    IEnumerator CompleteUIUpdate2(int _num)
    {
        yield return new WaitForSeconds(0.5f);

        ordernumberdata_script.orderNumberList[_num].progress = true;
        Debug.Log(_num + "___" + ordernumberdata_script.orderNumberList[_num].progress);


        //완료주문번호판에 아무것도 없을 때
        if (orderNumber.text == "")
        {
            orderNumber.text = waitingNumber[0].text;

            //대기열에 있는 정보 한칸씩 당기기
            for (int i = 0; i < 4; i++)
            {
                if (i != 3)
                    waitingNumber[i].text = waitingNumber[i + 1].text;
                else if (i == 3)
                    waitingNumber[i].text = "";
            }

            //빈칸인 대기열에 리스트가 있으면 정보를 넣어준다.
            for (int i = 0; i < 4; i++)
            {
                if (waitingNumber[i].text == "")
                {
                    //i가 3보다 작아야 대기열인덱스가 오버플로우가 안난다. 
                    //if(i != 3)
                    //{
                    //Debug.Log(i + " :noCompleteMinIndex: " + noCompleteMinIndex + " :i + noCompleteMinIndex + 1: " + (i + noCompleteMinIndex + 1));
                    //대기열에 정보를 넣어주기 위해 리스트 총 갯수보다 넣어줄 리스트인덱스가 작아야한다.
                    if ((i + noCompleteMinIndex + 1) < ordernumberdata_script.orderNumberList.Count)
                        waitingNumber[i].text = ordernumberdata_script.orderNumberList[i + noCompleteMinIndex + 1].orderNum.ToString();
                    //}
                }
            }
        }
        //완료주문번호판에 번호가 있을 경우
        else if (orderNumber.text != "")
        {
            //주문완료열에 있는 번호들 한칸씩 뒤로 뺀다.
            for (int i = 3; i >= 0; i--)
            {
                if (i != 0)
                    completeNumber[i].text = completeNumber[i - 1].text;
                else if (i == 0)
                    completeNumber[i].text = "";
            }
            completeNumber[0].text = orderNumber.text;  //주문완료열 첫번째에 완료주문번호판에 있는 주문번호를 넣어준다.
            orderNumber.text = waitingNumber[0].text;   //완료주문번호판에는 대기열 첫번째에 있는 정보를 넣어준다.

            //대기열에 있는 정보들 한칸씩 옮기기
            for (int i = 0; i < 4; i++)
            {
                if (i != 3)
                    waitingNumber[i].text = waitingNumber[i + 1].text;
                else if (i == 3)
                    waitingNumber[i].text = "";
            }


            //빈칸인 대기열에 리스트가 있으면 정보를 넣어준다.
            for (int i = 0; i < 4; i++)
            {
                if (waitingNumber[i].text == "")
                {
                    index += 1;

                    Debug.Log((i + noCompleteMinIndex + index) + " ::: " + ordernumberdata_script.orderNumberList.Count);
                    Debug.Log(i + " ::: " + noCompleteMinIndex + " :: " + index);
                    if ((i + noCompleteMinIndex + index) < ordernumberdata_script.orderNumberList.Count - 1)
                    {
                        waitingNumber[i].text = ordernumberdata_script.orderNumberList[i + noCompleteMinIndex + index].orderNum.ToString();
                    }


                }
            }
        }
    }


    ///음료준비 완료버튼 누르면 모니터앱 UI업데이트 시키는 함수
    public void CompleteUIUpdate(int _num)
    {
        ordernumberdata_script.orderNumberList[_num].progress = true;
        Debug.Log(_num + "___" + ordernumberdata_script.orderNumberList[_num].progress);
        NoCompleteIndexFind();

        //완료주문번호판에 아무것도 없을 때
        if (orderNumber.text == "")
        {
            orderNumber.text = waitingNumber[0].text;

            //대기열에 있는 정보 한칸씩 당기기
            for (int i = 0; i < 4; i++)
            {
                if (i != 3)
                    waitingNumber[i].text = waitingNumber[i + 1].text;
                else if (i == 3)
                    waitingNumber[i].text = "";
            }

            //빈칸인 대기열에 리스트가 있으면 정보를 넣어준다.
            for (int i = 0; i < 4; i++)
            {
                if (waitingNumber[i].text == "")
                {
                    //i가 3보다 작아야 대기열인덱스가 오버플로우가 안난다. 
                    //if(i != 3)
                    //{
                    //Debug.Log(i + " :noCompleteMinIndex: " + noCompleteMinIndex + " :i + noCompleteMinIndex + 1: " + (i + noCompleteMinIndex + 1));
                    //대기열에 정보를 넣어주기 위해 리스트 총 갯수보다 넣어줄 리스트인덱스가 작아야한다.
                    if ((i + noCompleteMinIndex + 1) < ordernumberdata_script.orderNumberList.Count)
                        waitingNumber[i].text = ordernumberdata_script.orderNumberList[i + noCompleteMinIndex + 1].orderNum.ToString();
                    //}
                }
            }
        }
        //완료주문번호판에 번호가 있을 경우
        else if (orderNumber.text != "")
        {
            //주문완료열에 있는 번호들 한칸씩 뒤로 뺀다.
            for (int i = 3; i >= 0; i--)
            {
                if (i != 0)
                    completeNumber[i].text = completeNumber[i - 1].text;
                else if (i == 0)
                    completeNumber[i].text = "";
            }
            completeNumber[0].text = orderNumber.text;  //주문완료열 첫번째에 완료주문번호판에 있는 주문번호를 넣어준다.
            orderNumber.text = waitingNumber[0].text;   //완료주문번호판에는 대기열 첫번째에 있는 정보를 넣어준다.

            //대기열에 있는 정보들 한칸씩 옮기기
            for (int i = 0; i < 4; i++)
            {
                if (i != 3)
                    waitingNumber[i].text = waitingNumber[i + 1].text;
                else if (i == 3)
                    waitingNumber[i].text = "";
            }


            //빈칸인 대기열에 리스트가 있으면 정보를 넣어준다.
            for (int i = 0; i < 4; i++)
            {
                if (waitingNumber[i].text == "")
                {
                    index += 1;

                    Debug.Log((i + noCompleteMinIndex + 1) + " ::: " + ordernumberdata_script.orderNumberList.Count);
                    Debug.Log(i + " ::: " + noCompleteMinIndex + " :: " + 1);
                    if ((i + noCompleteMinIndex + 1) <= ordernumberdata_script.orderNumberList.Count-1)
                    {
                        waitingNumber[i].text = ordernumberdata_script.orderNumberList[i + noCompleteMinIndex + 1].orderNum.ToString();
                    }

                }
            }
        }

        //완료음료 주문번호 쿨타임 시작하기
        textOrderCoolTime_script.orderNum_CoolStart = true;
        textComplete1CoolTime_script.orderNum_CoolStart = true;
        textComplete2CoolTime_script.orderNum_CoolStart = true;
        textComplete3CoolTime_script.orderNum_CoolStart = true;
        textComplete4CoolTime_script.orderNum_CoolStart = true;
    }


    /// 통신 한번만 받기 위한 이중조건식 바로하는 함수
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
}
