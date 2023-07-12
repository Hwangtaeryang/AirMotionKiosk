using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupCtrl : MonoBehaviour
{
    public static CupCtrl instance { get; private set; }

    public GameObject cup;
    public GameObject trayPos;

    //bool cupTouch;
    public bool trayTouch; //트레이에 컵이 올려져 있으면
    private InteractionBehaviour interactionBehaviour;
    private Rigidbody cup_rigidbody;

    string drinkName;
    //public List<string> drinkList = new List<string>();
    //public List<int> price = new List<int>();

    bool cupHold;   //컵을 들고 잇는 여부

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        drinkName = "라임 레몬에이드";      // 임시
        //drinkList.Add("라임 레몬에이드");   // 임시

        cup_rigidbody = GetComponent<Rigidbody>();
        interactionBehaviour = GetComponent<InteractionBehaviour>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tray"))
        {
            Debug.Log("여기?????????");
            for (int i = 0; i < MenuData.instance.menuDataList.Count; i++)
            {
                //Debug.Log(MenuData.instance.menuDataList.Count);
                // 추후에 태그나 레이어로 음료 확인
                if (MenuData.instance.menuDataList[i].drinkName == drinkName)
                {
                    //drinkList.Add(drinkName);
                    //price.Add(MenuData.instance.menuDataList[i].price);

                    //// OrderJsonData 저장용도
                    //OrderMenuJsonData.instance.order.drinkname = drinkList;   // 음료
                    //OrderMenuJsonData.instance.order.price = price;     // 가격

                    PlayerPrefs.SetString("DrinkName", drinkName);  // 음료 저장
                    PlayerPrefs.SetInt("Price", MenuData.instance.menuDataList[i].price);
                }
            }

                //cup.transform.parent = trayPos.transform;
                //cup_rigidbody.isKinematic = true;
                cup.transform.position = trayPos.transform.position;
                interactionBehaviour.enabled = false;
                trayTouch = true;
        }

    }


    public void CupReSet()
    {
        cup_rigidbody.isKinematic = false;
        interactionBehaviour.enabled = true;
        cup.transform.localPosition = new Vector3(-7.5926f, 0.8376f, 0.6754f); //컵 초기화 위치
    }

    private void Update()
    {
        //컵 위치를 벗어나면 초기 위치로 셋팅
        if(cup.transform.localPosition.y <= 0.4f || cup.transform.localPosition.x >= -7.4f || cup.transform.localPosition.x <= -7.77f)
        {
            CupReSet();
        }
    }
}