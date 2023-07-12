using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using System.IO;

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

//[System.Serializable]
//public class OrderList
//{
//    public int ordernumber;    //주문 번호
//    public string orderTime;  //주문시간
//    public string drinkname;    //메뉴
//    public int price;   //단가
//    public int amount;  //수량
//    public int payment; //결제금액
//    public bool progress;   //진행여부

//    public OrderList(int _ordernumber, string _ordertime, string _drinkname, int _price, int _amount, int _payment, bool _progress)
//    {
//        ordernumber = _ordernumber;
//        orderTime = _ordertime;
//        drinkname = _drinkname;
//        price = _price;
//        amount = _amount;
//        payment = _payment;
//        progress = _progress;
//    }
//}

public class Web_Monitor : MonoBehaviour
{
    //public DrinkList drinkList;
    //public Amount amount;
    //public Price price;
    //public OrderMeun orderMeun;
    public List<OrderNumber> orderNumbers = new List<OrderNumber>();
    
    public int orderNumber;
    public bool progress;

    public static Web_Monitor Instance { get; private set; }

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
        //StartCoroutine(GetMenu());
        // 메뉴리스트 초기화 : 클래스에 넣기
        //App_OrderListUpdate();
        GetOrderNumbers();
    }

    //---------------------------------------------------------------------------
    // 주문완료
    public void GetOrderNumbers()
    {
        StartCoroutine(_GetOrderNumbers());

    }

    // 주문완료
    // 주문완료를 눌렀을 때 주문번호를 가져와서 테이블내의 Progress 를 완료(1)로 바꿔주고, 완료된 시점의 TimeStamp 를 최근으로 업데이트
    IEnumerator _GetOrderNumbers()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://52.78.74.47/amk_app_ordernumber_update.php"))
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

                _GetOrderNumbersJSON(www.downloadHandler.text);
            }
        }
    }

    public void _GetOrderNumbersJSON(string _jsonData)
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
                if (int.TryParse(Array[i]["ORDERNUMBER"].Value, out int temp_ordernumber))
                {
                    orderNumber = temp_ordernumber;
                }

                if (int.TryParse(Array[i]["PROGRESS"].Value, out int temp_progress))
                {
                    progress = temp_progress != 0;
                }

                orderNumbers.Add(new OrderNumber(orderNumber, progress));
            }
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
        }
    }
    //---------------------------------------------------------------------------
}
