using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageInButtonCtrl : MonoBehaviour
{
    GameObject orderBase;    //페이지수를 가져오기 위한 부모역할
    GameObject buttonPanel; //버튼 자식 껏다켜기위한 부모변수


    int btnPage;
    int pageShare = 0;  //페이지 몫
    int currentBtn; //현재 버튼
    int networkCount = 0;   //통신 한번만 들어오게 하기 위한 이중 조건 변수

    void Start()
    {
        orderBase = GameObject.Find("OrderBase");
        buttonPanel = GameObject.Find("ButtonPanel");
        StartCoroutine(SetUp());
    }

    IEnumerator SetUp()
    {
        yield return new WaitForSeconds(0.06f);

        Debug.Log("여기 두번인가 ?!?!?!?!?!?!?!?!");
        transform.FindChild("NextButton").SetAsLastSibling();

        //Button btn = transform.GetChild(1).GetComponent<Button>();
        //btn.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Texture/ManagerApp/Btn_Color");

    }

    void Update()
    {
        //숫자 버튼 눌렀을 때 일어나는 이벤트
        ButtonState();
        NextBtnState();
        PreviousBtnState();
    }


    //이전버튼 눌렀을 때 이벤트
    public void PreviousBtnState()
    {
        if(ManagerApp.instance.previousBtnOn)
        {
            ManagerApp.instance.previousBtnOn = false;

            //현재클릭 된 버튼 다음 버튼 색변경을 위한 인덱스 구하기
            for (int i = 0; i < OrderBasePage.instance.pageParent; i++)
            {
                if (OrderBasePage.instance.btnOnState[i])
                {
                    if (i == 0)
                        i = 0;
                    else
                    {
                        currentBtn = i - 1; //현재 클릭해져있는 버튼 인덱스 + 다음 버튼 색변경하기 위해(+1)
                    }
                    
                    PreviousButtonPageChange();
                }
            }

            //버튼 다음걸로 변경해주고 숫자버튼 눌러줬다고 변경해줘서 함수 들어가게 한다.
            for (int i = 0; i < OrderBasePage.instance.pageParent; i++)
            {
                if (i == currentBtn)
                {
                    OrderBasePage.instance.btnOnState[i] = true;    //다음버튼 변경
                    ManagerApp.instance.btnOn = true;
                }
                else
                {
                    OrderBasePage.instance.btnOnState[i] = false;
                }
            }
        }
    }

    //이전버튼 눌렀을때 페이지 전환
    public void PreviousButtonPageChange()
    {
        btnPage = int.Parse(gameObject.name.Substring(7)) - 1;
        //Debug.Log("BtnPage" + (btnPage - 1));
        if (currentBtn % 4 == 3)
        {
            GameObject page = buttonPanel.transform.Find("BtnPage" + (btnPage)).gameObject;
            //Debug.Log(page.name);
            page.SetActive(true);

            this.gameObject.SetActive(false);
        }
    }


    //다음버튼 눌렀을때 이벤트
    public void NextBtnState()
    {
        if (ManagerApp.instance.nextBtnOn)
        {
            ManagerApp.instance.nextBtnOn = false;
            //현재클릭 된 버튼 다음 버튼 색변경을 위한 인덱스 구하기
            for (int i = 0; i < OrderBasePage.instance.pageParent; i++)
            {
                if (OrderBasePage.instance.btnOnState[i])
                {
                    if(i >= OrderBasePage.instance.pageParent - 1)
                    {
                        i = OrderBasePage.instance.pageParent - 1;
                    }
                    else
                    {
                        currentBtn = i + 1; //현재 클릭해져있는 버튼 인덱스 + 다음 버튼 색변경하기 위해(+1)
                    }
                    NextButtonPageChange();
                }
            }

            //버튼 다음걸로 변경해주고 숫자버튼 눌러줬다고 변경해줘서 함수 들어가게 한다.
            for (int i = 0; i < OrderBasePage.instance.pageParent; i++)
            {
                if (i == currentBtn)
                {
                    OrderBasePage.instance.btnOnState[i] = true;    //다음버튼 변경
                    ManagerApp.instance.btnOn = true;   //숫자버튼 눌렀다는 표시
                }
                else
                {
                    OrderBasePage.instance.btnOnState[i] = false;
                }
            }
        }
    }

    //넥스트버튼 눌렀을 때 페이지 전환
    public void NextButtonPageChange()
    {
        btnPage = int.Parse(gameObject.name.Substring(7)) - 1;
        if (currentBtn % 4 == 0)
        {
            GameObject page = buttonPanel.transform.Find("BtnPage" + (btnPage + 2)).gameObject;
            //Debug.Log(page.name);
            page.SetActive(true);

            this.gameObject.SetActive(false);
        }
    }



    //숫자버튼 눌렀을때 이벤트
    public void ButtonState()
    {
        //버튼 눌렀다! //숫자번호 버튼 눌렀을 때
        if (ManagerApp.instance.btnOn)
        {
            ManagerApp.instance.btnOn = false;

            for (int i = 0; i < OrderBasePage.instance.pageParent; i++)
            {
                if (OrderBasePage.instance.btnOnState[i])
                {
                    pageShare = i / 4;
                    //Debug.Log("왜 ???" + pageShare);
                }
            }

            for(int i = 0; i < OrderBasePage.instance.pageParent; i++)
            {
                if(OrderBasePage.instance.btnOnState[i])
                {
                    OrderBasePage.instance.btnSetActive = i;
                    //ButtonPanelCtrl.instance.btnSetActive = i;
                    Debug.Log("활성화 버튼 " + i);
                }
            }



            for (int i = 0; i < OrderBasePage.instance.pageParent; i++)
            {
                if (OrderBasePage.instance.btnOnState[i])
                {
                    btnPage = int.Parse(gameObject.name.Substring(7)) - 1;
                    //Debug.Log(btnPage + ":::" + i);

                    //Debug.Log("Btn" + (i + 1));
                    //선택 버튼 활성화
                    //Button btn = transform.Find("Btn" + ((i + 1) + (btnPage * 4))).GetComponent<Button>();
                    Button btn = transform.Find("Btn" + (i + 1)).GetComponent<Button>();
                    btn.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Texture/ManagerApp/Btn_Color");


                    //선택버튼에 대한 페이지 오픈
                    GameObject page = orderBase.transform.Find("Page" + (OrderBasePage.instance.pageParent - i)).gameObject;
                    //Debug.Log(page.name);
                    page.SetActive(true);


                }
                else if (!OrderBasePage.instance.btnOnState[i])
                {

                    btnPage = int.Parse(gameObject.name.Substring(7)) - 1;
                    Debug.Log(btnPage + "여기-  " + i + "__" + gameObject.name + "::::" + pageShare);

                    //현재 내 페이지에 있는 버튼들만 비활성화 시킨다.
                    if (i / 4 == pageShare)
                    {
                        Button btn = transform.Find("Btn" + (i + 1)).GetComponent<Button>();
                        btn.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Texture/ManagerApp/Btn_Btn");
                    }

                    //선택버튼에 대한 페이지 비오픈
                    GameObject page = orderBase.transform.Find("Page" + (OrderBasePage.instance.pageParent - i)).gameObject;
                    page.SetActive(false);
                }
            }

        }



        //for (int i = 0; i < OrderBasePage.instance.pageParent; i++)
        //{
        //    if (OrderBasePage.instance.btnOnState[i])
        //    {
        //        if (gameObject.name.Substring(7) == (i + 1).ToString())
        //        {
        //            btnPage = (i);
        //            Debug.Log(btnPage + "여기");
        //        }

        //        Debug.Log("Btn" + (i + 1) + (btnPage * 4));
        //        //선택 버튼 활성화
        //        Button btn = transform.Find("Btn" + (i + 1)).GetComponent<Button>();
        //        btn.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Texture/ManagerApp/Btn_Color");

        //        //선택버튼에 대한 페이지 오픈
        //        GameObject page = orderBase.transform.Find("Page" + (OrderBasePage.instance.pageParent - i)).gameObject;
        //        Debug.Log(page.name);
        //        page.SetActive(true);

        //        //if (_btnPrefab.name == "Btn" + (i + 1))
        //        //{
        //        //    Debug.Log("=_+" + i);
        //        //    btn.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Texture/ManagerApp/Btn_Color");
        //        //}
        //    }
        //    else if (!OrderBasePage.instance.btnOnState[i])
        //    {
        //        if (gameObject.name.Substring(7) == (i + 1).ToString())
        //        {
        //            btnPage = (i + 1);
        //        }
        //        //선택하지 않은 버튼 비활성화
        //        //페이지 
        //        if (i <= 3)
        //        {
        //            Debug.Log("Btn" + ((i + 1) + (btnPage * 4)));
        //            Button btn = transform.Find("Btn" + (i + 1)).GetComponent<Button>();
        //            btn.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Texture/ManagerApp/Btn_Btn");

        //            //선택버튼에 대한 페이지 비오픈
        //            GameObject page = orderBase.transform.Find("Page" + (OrderBasePage.instance.pageParent - i)).gameObject;
        //            page.SetActive(false);
        //        }

        //    }
        //}

        //ManagerApp.instance.btnOn = false;
    }

}
