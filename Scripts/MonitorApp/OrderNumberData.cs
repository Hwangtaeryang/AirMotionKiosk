using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;




[System.Serializable]
public class OrderNumber
{
    public int orderNum;    //주문번호
    public bool progress;   //진행여부


    public OrderNumber(int _orderNum, bool _progress)
    {
        orderNum = _orderNum;
        progress = _progress;
    }

}



public class OrderNumberData : MonoBehaviour
{

    public List<OrderNumber> orderNumberList = new List<OrderNumber>();

    void Start()
    {
        Invoke("GetOrderNumber", 0.2f);
        //orderNumberList.Add(new OrderNumber(1234, false));
        //orderNumberList.Add(new OrderNumber(1235, false));
        //orderNumberList.Add(new OrderNumber(1236, false));
        //orderNumberList.Add(new OrderNumber(1237, false));
        //orderNumberList.Add(new OrderNumber(1238, false));
        //orderNumberList.Add(new OrderNumber(1239, false));
        //orderNumberList.Add(new OrderNumber(1240, false));
        //orderNumberList.Add(new OrderNumber(1241, false));
        //orderNumberList.Add(new OrderNumber(1242, false));
    }

    void GetOrderNumber()
    {
        orderNumberList = Web_Monitor.Instance.orderNumbers.ToList();
    }
}
