
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;


[System.Serializable]
public class OrderMeun
{
    public int ordernumber;    //주문 번호
    public string orderTime;  //주문시간
    public string drinkname;    //메뉴
    public int price;   //단가
    public int amount;  //수량
    public int payment; //결제금액
    public bool progress;   //진행여부

    //public OrderMeun(string _drinkname, int _amount, int _price, int _payment)
    //{
    //    drinkname = _drinkname;
    //    amount = _amount;
    //    price = _price;
    //    payment = _payment;
    //}

    public OrderMeun(int _ordernumber, string _ordertime, string _drinkname, int _price, int _amount, int _payment, bool _progress)
    {
        ordernumber = _ordernumber;
        orderTime = _ordertime;
        drinkname = _drinkname;
        price = _price;
        amount = _amount;
        payment = _payment;
        progress = _progress;
    }
}


public class ToDayMenuData : MonoBehaviour
{
    public List<OrderMeun> orderMenuList = new List<OrderMeun>();
    //string[] drinkName = { "라임 레몬에이드" };
    //int[] amount = { 2 };
    //int[] price = { 4500 };
    //int payment = 9000;

    void Start()
    {
        Invoke("StackOrderMenu",0.2f);
        //orderMenuList.Add(new OrderMeun(1, "2020-07-27 15:01:23", "라임레몬에이드", 4500, 3, 13500, false));
        //orderMenuList.Add(new OrderMeun(1, "2020-07-27 15:01:23", "라임레몬에이드", 4500, 3, 13500, false));
        //orderMenuList.Add(new OrderMeun(1, "2020-07-27 15:01:23", "라임레몬에이드", 4500, 3, 13500, false));
        //orderMenuList.Add(new OrderMeun(1, "2020-07-27 15:01:23", "라임레몬에이드", 4500, 3, 13500, false));
        //orderMenuList.Add(new OrderMeun(1, "2020-07-27 15:01:23", "라임레몬에이드", 4500, 3, 13500, false));


        //orderMenuList.Add(new OrderMeun(2, Convert.ToDateTime("2020-07-27 15:01:23"), "라임레몬에이드", 4500, 1, 4500, false));
        //orderMenuList.Add(new OrderMeun(2, Convert.ToDateTime("2020-07-27 15:01:23"), "라임레몬에이드", 4500, 10, 45000, false));
        //orderMenuList.Add(new OrderMeun(3, Convert.ToDateTime("2020-07-27 16:41:20"), "라임레몬에이드", 4500, 1, 4500, false));
        //orderMenuList.Add(new OrderMeun(4, Convert.ToDateTime("2020-07-28 01:01:23"), "라임레몬에이드", 4500, 4, 18000, false));
        //orderMenuList.Add(new OrderMeun(5, Convert.ToDateTime("2020-07-28 22:01:23"), "라임레몬에이드", 4500, 5, 22500, false));
        //orderMenuList.Add(new OrderMeun(6, Convert.ToDateTime("2020-07-28 10:41:20"), "라임레몬에이드", 4500, 3, 13500, false));

        //orderMenuList.Add(new OrderMeun(7, Convert.ToDateTime("2020-07-27 12:01:23"), "라임레몬에이드", 4500, 2, 9000, false));
        //orderMenuList.Add(new OrderMeun(8, Convert.ToDateTime("2020-07-27 15:01:23"), "라임레몬에이드", 4500, 10, 45000, false));
        //orderMenuList.Add(new OrderMeun(9, Convert.ToDateTime("2020-07-27 16:41:20"), "라임레몬에이드", 4500, 1, 4500, false));
        //orderMenuList.Add(new OrderMeun(10, Convert.ToDateTime("2020-07-28 01:01:23"), "라임레몬에이드", 4500, 4, 18000, false));
        //orderMenuList.Add(new OrderMeun(11, Convert.ToDateTime("2020-07-28 22:01:23"), "라임레몬에이드", 4500, 5, 22500, false));
        //orderMenuList.Add(new OrderMeun(12, Convert.ToDateTime("2020-07-28 10:41:20"), "라임레몬에이드", 4500, 3, 13500, false));

        //orderMenuList.Add(new OrderMeun(13, Convert.ToDateTime("2020-07-27 12:01:23"), "라임레몬에이드", 4500, 2, 9000, false));
        //orderMenuList.Add(new OrderMeun(14, Convert.ToDateTime("2020-07-27 15:01:23"), "라임레몬에이드", 4500, 10, 45000, false));
        //orderMenuList.Add(new OrderMeun(15, Convert.ToDateTime("2020-07-27 16:41:20"), "라임레몬에이드", 4500, 1, 4500, false));
        //orderMenuList.Add(new OrderMeun(16, Convert.ToDateTime("2020-07-28 01:01:23"), "라임레몬에이드", 4500, 4, 18000, false));
        //orderMenuList.Add(new OrderMeun(17, Convert.ToDateTime("2020-07-28 22:01:23"), "라임레몬에이드", 4500, 5, 22500, false));
        //orderMenuList.Add(new OrderMeun(18, Convert.ToDateTime("2020-07-28 10:41:20"), "라임레몬에이드", 4500, 3, 13500, false));

        //orderMenuList.Add(new OrderMeun(19, Convert.ToDateTime("2020-07-27 12:01:23"), "라임레몬에이드", 4500, 2, 9000, false));
        //orderMenuList.Add(new OrderMeun(20, Convert.ToDateTime("2020-07-27 15:01:23"), "라임레몬에이드", 4500, 10, 45000, false));
        //orderMenuList.Add(new OrderMeun(21, Convert.ToDateTime("2020-07-27 16:41:20"), "라임레몬에이드", 4500, 1, 4500, false));
        //orderMenuList.Add(new OrderMeun(22, Convert.ToDateTime("2020-07-28 01:01:23"), "라임레몬에이드", 4500, 4, 18000, false));
        //orderMenuList.Add(new OrderMeun(23, Convert.ToDateTime("2020-07-28 22:01:23"), "라임레몬에이드", 4500, 5, 22500, false));
        //orderMenuList.Add(new OrderMeun(24, Convert.ToDateTime("2020-07-28 10:41:20"), "라임레몬에이드", 4500, 3, 13500, false));

        //orderMenuList.Add(new OrderMeun(25, Convert.ToDateTime("2020-07-27 12:01:23"), "라임레몬에이드", 4500, 2, 9000, false));
        //orderMenuList.Add(new OrderMeun(26, Convert.ToDateTime("2020-07-27 15:01:23"), "라임레몬에이드", 4500, 10, 45000, false));
        //orderMenuList.Add(new OrderMeun(27, Convert.ToDateTime("2020-07-27 16:41:20"), "라임레몬에이드", 4500, 1, 4500, false));
        //orderMenuList.Add(new OrderMeun(28, Convert.ToDateTime("2020-07-28 01:01:23"), "라임레몬에이드", 4500, 4, 18000, false));
        //orderMenuList.Add(new OrderMeun(29, Convert.ToDateTime("2020-07-28 22:01:23"), "라임레몬에이드", 4500, 5, 22500, false));
        //orderMenuList.Add(new OrderMeun(30, Convert.ToDateTime("2020-07-28 10:41:20"), "라임레몬에이드", 4500, 3, 13500, false));

        //orderMenuList.Add(new OrderMeun(31, Convert.ToDateTime("2020-07-27 12:01:23"), "라임레몬에이드", 4500, 2, 9000, false));
        //orderMenuList.Add(new OrderMeun(32, Convert.ToDateTime("2020-07-27 15:01:23"), "라임레몬에이드", 4500, 10, 45000, false));
        //orderMenuList.Add(new OrderMeun(33, Convert.ToDateTime("2020-07-27 16:41:20"), "라임레몬에이드", 4500, 1, 4500, false));
        //orderMenuList.Add(new OrderMeun(34, Convert.ToDateTime("2020-07-28 01:01:23"), "라임레몬에이드", 4500, 4, 18000, false));
        //orderMenuList.Add(new OrderMeun(35, Convert.ToDateTime("2020-07-28 22:01:23"), "라임레몬에이드", 4500, 5, 22500, false));
        //orderMenuList.Add(new OrderMeun(36, Convert.ToDateTime("2020-07-28 10:41:20"), "라임레몬에이드", 4500, 3, 13500, false));

        //orderMenuList.Add(new OrderMeun(37, Convert.ToDateTime("2020-07-27 12:01:23"), "라임레몬에이드", 4500, 2, 9000, false));
        //orderMenuList.Add(new OrderMeun(38, Convert.ToDateTime("2020-07-27 15:01:23"), "라임레몬에이드", 4500, 10, 45000, false));
        //orderMenuList.Add(new OrderMeun(39, Convert.ToDateTime("2020-07-27 16:41:20"), "라임레몬에이드", 4500, 1, 4500, false));
        //orderMenuList.Add(new OrderMeun(40, Convert.ToDateTime("2020-07-28 01:01:23"), "라임레몬에이드", 4500, 4, 18000, false));
        //orderMenuList.Add(new OrderMeun(41, Convert.ToDateTime("2020-07-28 22:01:23"), "라임레몬에이드", 4500, 5, 22500, false));
        //orderMenuList.Add(new OrderMeun(42, Convert.ToDateTime("2020-07-28 10:41:20"), "라임레몬에이드", 4500, 3, 13500, false));

        //orderMenuList.Add(new OrderMeun(43, Convert.ToDateTime("2020-07-27 12:01:23"), "라임레몬에이드", 4500, 2, 9000, false));
        //orderMenuList.Add(new OrderMeun(44, Convert.ToDateTime("2020-07-27 15:01:23"), "라임레몬에이드", 4500, 10, 45000, false));
        //orderMenuList.Add(new OrderMeun(45, Convert.ToDateTime("2020-07-27 16:41:20"), "라임레몬에이드", 4500, 1, 4500, false));
        //orderMenuList.Add(new OrderMeun(46, Convert.ToDateTime("2020-07-28 01:01:23"), "라임레몬에이드", 4500, 4, 18000, false));
        //orderMenuList.Add(new OrderMeun(47, Convert.ToDateTime("2020-07-28 22:01:23"), "라임레몬에이드", 4500, 5, 22500, false));
        //orderMenuList.Add(new OrderMeun(48, Convert.ToDateTime("2020-07-28 10:41:20"), "라임레몬에이드", 4500, 3, 13500, false));
    }

    void StackOrderMenu()
    {
        orderMenuList = Web_Manager.Instance.orderLists.ToList();
        //Debug.Log("count : " + orderMenuList.Count);
        //Debug.Log("count : " + orderMenuList[0].ordernumber);
        //Debug.Log("count : " + orderMenuList[0].orderTime);
        //Debug.Log("count : " + orderMenuList[0].drinkname);
        //Debug.Log("count : " + orderMenuList[0].amount);
        //Debug.Log("count : " + orderMenuList[0].price);
        //Debug.Log("count : " + orderMenuList[0].payment);
        //Debug.Log("count : " + orderMenuList[0].progress);
    }
}
