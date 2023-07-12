using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Order
{
    //public int ordernumber;    //주문 번호
    //public DateTime ordertime;  //주문시간
    public List<string> drinkname;    //메뉴
    public List<int> price;   //단가
    public List<int> amount;  //수량
    public int payment; //결제금액
    //public bool progress;   //진행여부

    public Order(List<string> _drinkname, List<int> _amount, List<int> _price, int _payment)
    {
        drinkname = _drinkname;
        amount = _amount;
        price = _price;
        payment = _payment;
    }

    //public Order(int _ordernumber, DateTime _ordertime, List<string> _drinkname, List<int> _price, List<int> _amount, int _payment, bool _progress)
    //{
    //    ordernumber = _ordernumber;
    //    ordertime = _ordertime;
    //    drinkname = _drinkname;
    //    price = _price;
    //    amount = _amount;
    //    payment = _payment;
    //    progress = _progress;
    //}
}

public class OrderMenuJsonData : MonoBehaviour
{
    public static OrderMenuJsonData instance { get; private set; }
    public Order order;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    public void SaveOrderDataToJson()
    {
        string jsonData = JsonUtility.ToJson(order, true);
        string path = Path.Combine(Application.dataPath, "orderMenu.json");
        File.WriteAllText(path, jsonData);
    }

    public void LoadOrderDataFromJson()
    {
        string path = Path.Combine(Application.dataPath, "orderMenu.json");
        string jsonData = File.ReadAllText(path);
        order = JsonUtility.FromJson<Order>(jsonData);
    }

}
