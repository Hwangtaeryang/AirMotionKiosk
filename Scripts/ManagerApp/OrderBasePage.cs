using Photon.Pun.UtilityScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class OrderBasePage : MonoBehaviour
{
    public static OrderBasePage instance { get; private set; }


    public GameObject menuPrefab; //메뉴서 프리팹
    public GameObject pagePrefab;   //페이지 프리팹
    public GameObject orderBase;    //페이지만들 부모

    GameObject _menuPrefab;  //복사될 오브젝트
    GameObject _pagePrefab; //복사될 페이지 오브젝트
    Transform parent;
    public ToDayMenuData todayData_script;


    public  int pageParent = 0; //페이지 수
    public int pageRemainder = 0;   //페이지 나머지 수    


    public bool[] btnOnState;   //버튼 누른 상태
    public bool networkOn;  //네트워크 상태
    int networkCount = 0;   //정보 한번만 들어가기 위한 이중 조건변수
    public int btnSetActive;   //페이지버튼 활성화 번호


    private List<GameObject> poolContent;
    private List<GameObject> poolPage;


    public int orderNumber; //주문번호
    public string drinkname;    //음료이름        // 수정
    public int amount;  //수량
    public int price;   //단가
    public int totalPrice;  //총액
    public string time; //날짜

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {
        //todayData_script = GameObject.Find("DataManager").GetComponent<ToDayMenuData>();
        //parent = GameObject.Find("Page1").GetComponent<Transform>();
        poolContent = new List<GameObject>();
        poolPage = new List<GameObject>();
        //Invoke("Delay", 2f);
        StartCoroutine(SetUp());
    }

    void Delay()
    {
    }

    IEnumerator SetUp()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("???" + todayData_script.orderMenuList.Count);

        //for (int i = todayData_script.orderMenuList.Count - 1; i >= 0; i--)
        //{
        //    _menuPrefab = Instantiate(menuPrefab, parent);
        //    _menuPrefab.name = "Content" + (i);
        //}


        //주문메뉴올라갈 페이지 수 설정하는 부분
        if (todayData_script.orderMenuList.Count % 5 == 0)
        {
            pageParent = todayData_script.orderMenuList.Count / 5;
        }
        else if (todayData_script.orderMenuList.Count % 5 != 0)
        {
            pageRemainder = todayData_script.orderMenuList.Count % 5;
            pageParent = (todayData_script.orderMenuList.Count / 5) + 1;
        }

        //페이지만큼 버튼이 생겨서 버튼 누른 상태 갯수만큼 만들기
        btnOnState = new bool[pageParent];
        for (int k = 0; k < pageParent; k++)
        {
            if (k == 0)
                btnOnState[k] = true;
            else
                btnOnState[k] = false;
        }


        Debug.Log("pageParent???" + pageParent);
        //부모 페이지 생성
        for (int i = 0; i < pageParent; i++)
        {
            _pagePrefab = Instantiate(pagePrefab, orderBase.transform);
            _pagePrefab.name = "Page" + (i + 1);
            poolPage.Add(_pagePrefab);


            for (int j = todayData_script.orderMenuList.Count-1; j >= 0 ; j--)
            {
                if ((j + 1) / 5 == i && (j + 1) % 5 != 0 || ((j + 1) / 5 == (i + 1) && (j + 1) % 5 == 0))
                {
                    _menuPrefab = Instantiate(menuPrefab, _pagePrefab.transform); //transform.Find("Page"+(i+1)).transform);
                    _menuPrefab.name = "Content" + (j);
                    poolContent.Add(_menuPrefab);
                    //if ((j + 1) % 5 == 0)
                    //{
                    //    Debug.Log("___" + (j - 1) + "::" + _pagePrefab.name + ">>>" + i);
                    //    _menuPrefab = Instantiate(menuPrefab, _pagePrefab.transform); //transform.Find("Page"+(i+1)).transform);
                    //    _menuPrefab.name = "Content" + (j );
                    //}
                    //else if ((j + 1) % 5 == 1)
                    //{
                    //    _menuPrefab = Instantiate(menuPrefab, _pagePrefab.transform);
                    //    _menuPrefab.name = "Content" + (j );
                    //}
                    //else if ((j + 1) % 5 == 2)
                    //{
                    //    _menuPrefab = Instantiate(menuPrefab, _pagePrefab.transform);
                    //    _menuPrefab.name = "Content" + (j);
                    //}
                    //else if ((j + 1) % 5 == 3)
                    //{
                    //    _menuPrefab = Instantiate(menuPrefab, _pagePrefab.transform);
                    //    _menuPrefab.name = "Content" + (j);
                    //}
                    //else if ((j + 1) % 5 == 4)
                    //{
                    //    _menuPrefab = Instantiate(menuPrefab, _pagePrefab.transform);
                    //    _menuPrefab.name = "Content" + (j );
                    //}
                }
            }
        }

        //처음 페이지 활성화/비활성화 할 오브젝트 적용시킨다.
        for(int i = 0; i < orderBase.transform.childCount; i++)
        {
            if(i == orderBase.transform.childCount -1)
            {
                orderBase.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                orderBase.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        //부모 페이지 생성
        //for (int i = 0; i < pageParent; i++)
        //{
        //    _pagePrefab = Instantiate(pagePrefab, orderBase.transform);
        //    _pagePrefab.name = "Page" + (i + 1);

        //    for (int j = 0; j < todayData_script.orderMenuList.Count; j++)
        //    {
        //        if ((j + 1) / 5 == i && (j + 1) % 5 != 0 || ((j + 1) / 5 == (i + 1) && (j + 1) % 5 == 0))
        //        {
        //            if ((j + 1) % 5 == 0)
        //            {
        //                Debug.Log("___" + (j + 1) + "::" + _pagePrefab.name + ">>>" + i);
        //                _menuPrefab = Instantiate(menuPrefab, _pagePrefab.transform); //transform.Find("Page"+(i+1)).transform);
        //                _menuPrefab.name = "Content" + (j + 1);
        //            }
        //            else if ((j + 1) % 5 == 1)
        //            {
        //                _menuPrefab = Instantiate(menuPrefab, _pagePrefab.transform);
        //                _menuPrefab.name = "Content" + (j + 1);
        //            }
        //            else if ((j + 1) % 5 == 2)
        //            {
        //                _menuPrefab = Instantiate(menuPrefab, _pagePrefab.transform);
        //                _menuPrefab.name = "Content" + (j + 1);
        //            }
        //            else if ((j + 1) % 5 == 3)
        //            {
        //                _menuPrefab = Instantiate(menuPrefab, _pagePrefab.transform);
        //                _menuPrefab.name = "Content" + (j + 1);
        //            }
        //            else if ((j + 1) % 5 == 4)
        //            {
        //                _menuPrefab = Instantiate(menuPrefab, _pagePrefab.transform);
        //                _menuPrefab.name = "Content" + (j + 1);
        //            }
        //        }
        //    }

        //}
    }

    IEnumerator PageUpdate()
    {
        yield return new WaitForSeconds(0.05f);
        Debug.Log("???" + todayData_script.orderMenuList.Count);

        //주문메뉴올라갈 페이지 수 설정하는 부분
        if (todayData_script.orderMenuList.Count % 5 == 0)
        {
            pageParent = todayData_script.orderMenuList.Count / 5;
        }
        else if (todayData_script.orderMenuList.Count % 5 != 0)
        {
            pageRemainder = todayData_script.orderMenuList.Count % 5;
            pageParent = (todayData_script.orderMenuList.Count / 5) + 1;
        }

        Debug.Log("pageParent???" + pageParent);
        //부모 페이지 생성
        for (int i = 0; i < pageParent; i++)
        {
            _pagePrefab = Instantiate(pagePrefab, orderBase.transform);
            _pagePrefab.name = "Page" + (i + 1);
            poolPage.Add(_pagePrefab);


            for (int j = todayData_script.orderMenuList.Count - 1; j >= 0; j--)
            {
                if ((j + 1) / 5 == i && (j + 1) % 5 != 0 || ((j + 1) / 5 == (i + 1) && (j + 1) % 5 == 0))
                {
                    _menuPrefab = Instantiate(menuPrefab, _pagePrefab.transform); //transform.Find("Page"+(i+1)).transform);
                    _menuPrefab.name = "Content" + (j);
                    poolContent.Add(_menuPrefab);
                }
            }
        }


        for (int i = 0; i < orderBase.transform.childCount; i++)
        {
            int a;
            a = (orderBase.transform.childCount - 1) - btnSetActive;
            if(i == a)
            {
                orderBase.transform.GetChild(a).gameObject.SetActive(true);
            }
            else
            {
                orderBase.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

    }

    
    void Update()
    {
        //주문 들어왓을 때
        if (networkOn && networkCount == 0)//if(BtnTest.instance.orderBtnOn)
        {
            networkOn = false;
            networkCount = 1;
            //BtnTest.instance.orderBtnOn = false;

            todayData_script.orderMenuList.Add(
                new OrderMeun(orderNumber, time, drinkname, price, amount, totalPrice, false));

            //foreach (var obj in poolContent)
            //{
            //    Destroy(obj);
            //}

            //기존에 있던것들을 전부 지운다
            foreach (var obj in poolPage)
            {
                Destroy(obj);
            }
            poolPage.Clear();

            Debug.Log("+++++++++" + todayData_script.orderMenuList.Count);
            StartCoroutine(PageUpdate());
            StartCoroutine(NetworkCount());
        }
    }

    //데이터 중복으로 받는거 막기 위한 함수
    IEnumerator NetworkCount()
    {
        yield return new WaitForSeconds(6f);
        networkOn = false;
        networkCount = 0;
    }

}
