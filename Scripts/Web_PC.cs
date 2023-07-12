using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using System;

//[System.Serializable]
//public class DrinkList
//{
//    public string[] drinkname;
//}

//[System.Serializable]
//public class Amount
//{
//    public int[] amount;
//}

//[System.Serializable]
//public class Price
//{
//    public int[] price;
//}


public class Web_PC : MonoBehaviour
{
    //public DrinkList drinkList;
    //public Amount amount;
    //public Price price;
    public List<OrderMeun> orderLists = new List<OrderMeun>();
    
    public int orderNumber;
    public string ordertime;
    public string drinkname;
    public int price;
    public int amount;
    public int payment;
    public bool progress;
    public bool save_completed = false;
    
    public static Web_PC Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // 메뉴 초기화
        StartCoroutine(GetMenu());
        // 메뉴리스트 초기화 : 클래스에 넣기
        //App_OrderListUpdate();
    }

    //---------------------------------------------------------------------------
    // 클래스에 음료, 가격 초기화 용도 : SELECT
    IEnumerator GetMenu()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://52.78.74.47/amk_getmenu.php"))
        {
            yield return www.SendWebRequest();
            //yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                //byte[] results = www.downloadHandler.data;
                //text.text = www.downloadHandler.text;
                GetMenuJSON(www.downloadHandler.text);
            }
        }
    }
    
    // Get Menu
    public void GetMenuJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            for (int i = 0; i < Array.Count; i++)
            {
                //Debug.Log("i : " + i);
                //Debug.Log("DRINKNAME Value : " + Array[i]["DRINKNAME"].Value);  // 음료이름
                //Debug.Log("PRICE Value : " + Array[i]["PRICE"].Value);          // 가격
                MenuData.instance.menuDataList.Add(new Menu(Array[i]["DRINKNAME"].Value, Int32.Parse(Array[i]["PRICE"].Value)));
            }
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
        }
    }
    //---------------------------------------------------------------------------

    //----------------------------------------------------------------------------
    // 주문 접수 내역 UPDATE : 주문한 목록을 "시간 순서대로" "모두" 불러온다.
    public void App_OrderListUpdate()
    {
        //StartCoroutine(_App_OrderStatusUpdate());
        StartCoroutine(_App_OrderListUpdate());
    }

    // 구성목록 : DrinkName, Amount, Payment App Update
    IEnumerator _App_OrderListUpdate()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://52.78.74.47/amk_app_orderlist_update.php"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                App__App_OrderListUpdateJSON(www.downloadHandler.text);
            }
        }
    }

    public void App__App_OrderListUpdateJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        Debug.Log("::::::1 : " + _jsonData);

        var N = JSON.Parse(_jsonData);
        Debug.Log("::::::2 : "+N);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            for (int i = 0; i < Array.Count; i++)
            {
                //Debug.Log("i : " + i);
                //Debug.Log("2. ORDERNUMBER Value : " + Array[i]["ORDERNUMBER"].Value);          // 주문번호
                //Debug.Log("2. ORDERTIME Value : " + Array[i]["ORDERTIME"].Value);          // 주문시간
                //Debug.Log("2. DRINKNAME Value : " + Array[i]["DRINKNAME"].Value);          // 음료
                //Debug.Log("2. PRICE Value : " + Array[i]["PRICE"].Value);          // 가격
                //Debug.Log("2. AMOUNT Value : " + Array[i]["AMOUNT"].Value);          // 갯수
                //Debug.Log("2. PAYMENT Value : " + Array[i]["PAYMENT"].Value);          // 결제
                //Debug.Log("2. PROGRESS Value : " + Array[i]["PROGRESS"].Value);          // 완료/진행

                //Debug.Log("Count " + Array[i]["DRINKNAME"].Count);
                //drinkList = JsonUtility.FromJson<DrinkList>(Array[i]["DRINKNAME"].Value);
                //Debug.Log("2-1 : " + drinkList.drinkname[0]);
                //Debug.Log("2-2 : " + drinkList.drinkname[1]);
                //Debug.Log("2-3 : " + drinkList.drinkname[2]);
                if (int.TryParse(Array[i]["ORDERNUMBER"].Value, out int temp_ordernumber))
                {
                    orderNumber = temp_ordernumber;
                }

                ordertime = Array[i]["ORDERTIME"].Value;
                drinkname = Array[i]["DRINKNAME"].Value;

                if (int.TryParse(Array[i]["PRICE"].Value, out int temp_price))
                {
                    price = temp_price;
                }

                if (int.TryParse(Array[i]["AMOUNT"].Value, out int num_amount))
                {
                    amount = num_amount;
                }

                if (int.TryParse(Array[i]["PAYMENT"].Value, out int temp_payment))
                {
                    payment = temp_payment;
                }

                if (int.TryParse(Array[i]["PROGRESS"].Value, out int temp_progress))
                {
                    progress = temp_progress != 0;
                }

                orderLists.Add(new OrderMeun(orderNumber, ordertime, drinkname, price, amount, payment, progress));
            }
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
        }
    }
    //----------------------------------------------------------------------------


    //---------------------------------------------------------------------------
    // 주문하기INSERT
    public void SendOrder()
    {
        StartCoroutine(_SendOrderHistory());
    }

    IEnumerator _SendOrderHistory()
    {
        WWWForm form = new WWWForm();

        // json 형식으로 보내주기!!
        //string[] drinkName = { "아메리카노", "라임 레몬에이드", "카페라떼" };
        //int[] amount = { 1, 2, 1 };
        //int[] price = { 2500, 4500, 3500 };
        //int payment = 15000;

        //orderMeun = new Order(drinkName, amount, price, payment);

        //string jsonData = JsonUtility.ToJson(OrderMenuJsonData.instance.order);
        //form.AddField("jsonData", jsonData);
        //Debug.Log("jsonData::::: " + jsonData);

        //drinkList = JsonUtility.FromJson<DrinkList>(Array[0]["DRINKLIST"].Value);

        //PlayerPrefs.GetString("DrinkName");
        //PlayerPrefs.GetInt("Price").ToString();
        //PlayerPrefs.GetInt("Amount").ToString();
        //PlayerPrefs.GetInt("TotalPrice").ToString();
        //PlayerPrefs.GetString("Progress", "N");

        form.AddField("drinkName", PlayerPrefs.GetString("DrinkName"));
        form.AddField("amount", PlayerPrefs.GetInt("Amount"));
        form.AddField("price", PlayerPrefs.GetInt("Price"));
        form.AddField("payment", PlayerPrefs.GetInt("TotalPrice"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/amk_insert_order_history2.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.text);
                GetOrderNumberJSON(www.downloadHandler.text);
            }
        }
    }
    
    public void GetOrderNumberJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            for (int i = 0; i < Array.Count; i++)
            {
                //Debug.Log("i : " + i);
                //Debug.Log("주문시간 : " + Array[i]["ORDERTIME"].Value);  // 추가 주문시간
                //Debug.Log("추가된 주문번호 : " + Array[i]["ORDERNUMBER"].Value);  // 추가 주문번호

                if (int.TryParse(Array[i]["ORDERNUMBER"].Value, out int ordernumber))
                {
                    Debug.Log("::::::::::::::::  " + ordernumber);
                    PlayerPrefs.SetInt("OrderNumber", ordernumber);
                }
                
                //if (int.TryParse(Array[i]["ORDERTIME"].Value, out int ordertime))
                //{
                //    PlayerPrefs.SetString("OrderTime", ordertime.ToString());
                //    Debug.Log("::::::::::::: " + ordertime);
                //}
                PlayerPrefs.SetString("OrderTime", Array[i]["ORDERTIME"].Value);

            }
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
        }

        save_completed = true;
    }
    //---------------------------------------------------------------------------


    //---------------------------------------------------------------------------
    // 주문완료
    public void OrderComplete()
    {
        StartCoroutine(_OrderComplete());
    }

    // 주문완료
    // 주문완료를 눌렀을 때 주문번호를 가져와서 테이블내의 Progress 를 완료(1)로 바꿔주고, 완료된 시점의 TimeStamp 를 최근으로 업데이트
    IEnumerator _OrderComplete()
    {
        WWWForm form = new WWWForm();

        // 주문완료할때 주문번호를 가져온다.
        int orderNumber = 23;

        //form.AddField("orderNumber", PlayerPrefs.GetString("orderNumber"));

        form.AddField("orderNumber", orderNumber);
        
        using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/amk_app_order_complete.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
    //---------------------------------------------------------------------------
}
