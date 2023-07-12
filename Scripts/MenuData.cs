using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class Menu
{
    //public int orderNum;    //주문 번호
    //public DateTime orderTime;  //주문시간
    public string drinkName;    //메뉴
    public int price;   //단가
    //public int amount;  //수량
    //public int payment; //결제금액
    //public bool progress;   //진행여부

    //public void InfoData(string _drinkName, int _price)
    //{
    //    drinkName = _drinkName;
    //    price = _price;
    //}

    public Menu(string _drinkName, int _price)
    {
        drinkName = _drinkName;
        price = _price;
    }
}

public class MenuData : MonoBehaviour
{
    public static MenuData instance { get; private set; }
    public List<Menu> menuDataList = new List<Menu>();

    //public Menu limeLemon_Ade = new Menu();
    //public Menu ice_americano = new Menu();

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    //private void Start()
    //{
    //    MenuDataSet();
    //}

    //void MenuDataSet()
    //{
    //    limeLemon_Ade.InfoData("라임레몬에이드", 4500);
    //    ice_americano.InfoData("아이스아메리카노", 2500);
    //}

    private void Start()
    {
        //menuDataList.Add(new Menu("라임레몬에이드", 4500));
        //menuDataList.Add(new Menu("아이스아메리카노", 2500));
        //Invoke("GetMenu", 1f);
    }

    void GetMenu()
    {
        foreach (Menu Menu in menuDataList)
        {
            Debug.Log(Menu.drinkName + " ::: " + Menu.price);
        }
    }
}
