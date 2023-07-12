using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class All_AppManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static All_AppManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<All_AppManager>();

            return m_instance;
        }
    }

    static All_AppManager m_instance;
    public PhotonView photonView;
    
    public bool networkShow;
    public bool completeBtn;
    public int index;

    public int orderNumber; //주문번호
    public string drinkName;    //음료이름
    public int amount;  //수량
    public int price;   //단가
    public int totalPrice;  //총액
    public string time; //날짜

    int testi = 0;

    void Awake()
    {
        if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        if(!photonView.IsMine && networkShow)
        {
            Debug.Log("여기는 들어옴???" + networkShow);
            //photonView.RPC("SetOrderNumber", RpcTarget.AllBuffered, null);

            if (SceneManager.GetActiveScene().name == "AriMotion_Kiosk_Manager")
            {
                //ManagerApp.instance.networkOn = true;
                OrderBasePage.instance.networkOn = true;
            }
            else if(SceneManager.GetActiveScene().name == "AriMotion_Kiosk_Moriter")
            {
                MoniterAppManager2.instance.networkOn = true;
                MoniterAppManager2.instance.one = true;
            }

            networkShow = false;
        }

        if(!photonView.IsMine && completeBtn)
        {
            if (SceneManager.GetActiveScene().name == "AriMotion_Kiosk_Manager")
            {
                Debug.Log("완료 버튼1 ");
                photonView.RPC("SetCompleteDrink", RpcTarget.AllBuffered, true, index);
            }
            else if (SceneManager.GetActiveScene().name == "AriMotion_Kiosk_Moriter")
            {
                Debug.Log("완료 버튼2 ");
                MoniterAppManager2.instance.completeBtn = true;
                //MoniterAppManager.instance.i_index = index;
                
            }
            completeBtn = false;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            //다른 사람들에게 데이터를 보낸다
            stream.SendNext(networkShow);   //통신상태
            stream.SendNext(orderNumber);   //주문번호
            stream.SendNext(drinkName); //음료이름
            stream.SendNext(amount);    //수량
            stream.SendNext(price); //단가
            stream.SendNext(totalPrice);    //총액
            stream.SendNext(time);  //시간
            Debug.Log(networkShow + "보내드림");
            Debug.Log(orderNumber + "보내드림");
            Debug.Log(amount + "보내드림");
            Debug.Log(totalPrice + "보내드림");
            Debug.Log(time + "보내드림");
        }
        else
        {
            //데이터를 받는다
            networkShow = (bool)stream.ReceiveNext();
            orderNumber = (int)stream.ReceiveNext();
            Debug.Log("ordernumber ::::::: " + orderNumber);
            drinkName = (string)stream.ReceiveNext();
            amount = (int)stream.ReceiveNext();
            price = (int)stream.ReceiveNext();
            totalPrice = (int)stream.ReceiveNext();
            time = (string)stream.ReceiveNext();
            Debug.Log(networkShow + "====");
            Debug.Log(orderNumber + "====");
            Debug.Log(totalPrice + "===");
            Debug.Log(time + "===");

            if (SceneManager.GetActiveScene().name == "AriMotion_Kiosk_Manager")
            {
                OrderBasePage.instance.orderNumber = orderNumber;
                OrderBasePage.instance.drinkname = drinkName;    
                OrderBasePage.instance.amount = amount;          
                OrderBasePage.instance.price = price;            
                OrderBasePage.instance.totalPrice = totalPrice;
                OrderBasePage.instance.time = time;
            }
            else if (SceneManager.GetActiveScene().name == "AriMotion_Kiosk_Moriter")
            {
                MoniterAppManager2.instance.orderNum = orderNumber;
            }
        }
    }

    [PunRPC]
    public void SetOrderNumber()
    {

        Debug.Log("SetOrderNumber()");
        networkShow = true;
        orderNumber = PlayerPrefs.GetInt("OrderNumber");
        drinkName = PlayerPrefs.GetString("DrinkName");
        amount = PlayerPrefs.GetInt("Amount");
        price = PlayerPrefs.GetInt("Price");
        totalPrice = PlayerPrefs.GetInt("TotalPrice");
        time = PlayerPrefs.GetString("OrderTime");
    }

    [PunRPC]
    public void SetCompleteDrink(bool _completeBtn, int _index)
    {
        Debug.Log("음료 완료"+ _completeBtn + "::: "+ _index);
        completeBtn = true;
        index = _index;
    }
}
