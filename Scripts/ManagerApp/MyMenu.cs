using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyMenu : MonoBehaviour
{
    GameObject dataManager;
    ToDayMenuData todaymenu_script;

    public TextMeshProUGUI orderNumber;    //주문번호
    public TextMeshProUGUI brinkName;  //음료이름
    public TextMeshProUGUI amount; //수량
    public TextMeshProUGUI orderTime;  //주문시간
    public TextMeshProUGUI totalPrice; //총액
    public Button completeBtn;

    int pageParent = 0;
    string str;

    void Start()
    {
        todaymenu_script = GameObject.Find("DataManager").GetComponent<ToDayMenuData>();

        StartCoroutine(MyMeunSetting());
    }

    IEnumerator MyMeunSetting()
    {
        yield return new WaitForSeconds(0.05f);

        for (int i = todaymenu_script.orderMenuList.Count - 1; i >= 0; i--)
        {
            if (this.gameObject.name == "Content" + (i))
            {
                if (todaymenu_script.orderMenuList[i].progress)
                {
                    completeBtn.image.sprite = Resources.Load<Sprite>("Texture/ManagerApp/CompleteBtn");
                    completeBtn.interactable = false;
                }
            }
        }

        if (todaymenu_script.orderMenuList.Count % 5 == 0)
        {
            pageParent = todaymenu_script.orderMenuList.Count / 5;
        }
        else if (todaymenu_script.orderMenuList.Count % 5 != 0)
        {
            pageParent = (todaymenu_script.orderMenuList.Count / 5) + 1;
        }




        //for (int i = 0; i < todaymenu_script.orderMenuList.Count; i++)
        //{
        //    if (this.gameObject.name == "Content" + (i + 1))
        //    {
        //        //TextMeshProUGUI orderNumber = transform.Find("OrderNumber").GetComponentInChildren<TextMeshProUGUI>();
        //        orderNumber.text = todaymenu_script.orderMenuList[i].orderNum.ToString();
        //        brinkName.text = todaymenu_script.orderMenuList[i].drinkName.ToString();
        //        amount.text = todaymenu_script.orderMenuList[i].amount.ToString();
        //        orderTime.text = todaymenu_script.orderMenuList[i].orderTime.ToString();
        //        totalPrice.text = string.Format("{0:#,###}", todaymenu_script.orderMenuList[i].payment) + "원";//todaymenu_script.orderMenuList[i].payment.ToString() + " 원";
        //    }
        //}


        for (int i = todaymenu_script.orderMenuList.Count - 1; i >= 0; i--)
        {
            if (this.gameObject.name == "Content" + (i))
            {
                //TextMeshProUGUI orderNumber = transform.Find("OrderNumber").GetComponentInChildren<TextMeshProUGUI>();
                orderNumber.text = todaymenu_script.orderMenuList[i].ordernumber.ToString();
                brinkName.text = todaymenu_script.orderMenuList[i].drinkname.ToString();
                amount.text = todaymenu_script.orderMenuList[i].amount.ToString();
                orderTime.text = todaymenu_script.orderMenuList[i].orderTime.ToString();
                
                totalPrice.text = string.Format("{0:#,###}", todaymenu_script.orderMenuList[i].payment) + "원";//todaymenu_script.orderMenuList[i].payment.ToString() + " 원";
            }
        }
    }

    //주문완료버튼 눌렀을 때
    public void OrderComplete(GameObject gameObject)
    {
        completeBtn.image.sprite = Resources.Load<Sprite>("Texture/ManagerApp/CompleteBtn");
        completeBtn.interactable = false;

        //주문 완료한거 true로 변경해서 완료되었다고 리스트에 업로드
        for (int i = todaymenu_script.orderMenuList.Count - 1; i >= 0; i--)
        {
            if (this.gameObject.name == "Content" + (i))
            {
                ManagerApp.instance.completeBtn = true;
                All_AppManager.instance.SetCompleteDrink(true, i);

                todaymenu_script.orderMenuList[i].progress = true;

                // 서버에 갱신
                TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();
                Web_Manager.Instance.OrderComplete(text.text.ToString());
            }
        }
    }


    void Update()
    {

    }
}
