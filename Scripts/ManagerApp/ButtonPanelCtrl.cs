using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanelCtrl : MonoBehaviour
{
    public static ButtonPanelCtrl instance { get; private set; }


    public GameObject btnPrefab;    //버튼 프리팹
    public GameObject butPanel; //인스터스 시킬 위치
    public GameObject btnPagePrefab;    //버튼 페이지 프리팹
    public GameObject nextBtn;  //버튼을 마지막으로 보내기 위해

    //public int btnSetActive;   //버튼 클릭해놓은거 업데이트되어도 그대로 하기위한 변수
    

    GameObject _btnPrefab;  //복사될 오브젝트
    GameObject _btnPagePrefab;  //복사될 오브젝트

    private List<GameObject> poolBtnPage;
    int btnPage;
    int networkCount = 0;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {
        poolBtnPage = new List<GameObject>();
        StartCoroutine(ButtonSetting());
        
    }

    
    IEnumerator ButtonSetting()
    {
        yield return new WaitForSeconds(0.5f);

        //버튼 페이지 설정 위한 부분
        if(OrderBasePage.instance.pageParent % 4 == 0)
        {
            btnPage = OrderBasePage.instance.pageParent / 4;
        }
        else if(OrderBasePage.instance.pageParent % 4 != 0)
        {
            btnPage = (OrderBasePage.instance.pageParent / 4) + 1;
        }

        Debug.Log("버튼 페이지 수"+ btnPage + "왜"+ OrderBasePage.instance.pageParent);

        for(int i  = 0; i < btnPage; i++)
        {
            _btnPagePrefab = Instantiate(btnPagePrefab, butPanel.transform);
            _btnPagePrefab.name = "BtnPage"+(i + 1).ToString(); //버튼 페이지 이름 

            poolBtnPage.Add(_btnPagePrefab);

            for (int j = 0; j < OrderBasePage.instance.pageParent; j++)
            {
                if((j + 1) / 4 == i && (j + 1) % 4 != 0 || ((j + 1) / 4 == (i + 1) && (j + 1) % 4 == 0))
                {
                    _btnPrefab = Instantiate(btnPrefab, _btnPagePrefab.transform);
                    _btnPrefab.name = "Btn" + (j+1);
                    _btnPrefab.GetComponent<Button>();
                    TextMeshProUGUI text = _btnPrefab.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
                    text.text = (j + 1).ToString();
                }
            }
            //nextBtn.transform.SetAsLastSibling();
        }

        for(int i = 0; i < transform.childCount; i++)
        {
            if (i == 0)
                transform.GetChild(i).gameObject.SetActive(true);
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }



        //Debug.Log(OrderBasePage.instance.pageParent +"::");
        //for (int i=0; i< OrderBasePage.instance.pageParent; i++)
        //{
        //    _btnPrefab = Instantiate(btnPrefab, butPanel.transform);
        //    _btnPrefab.name = "Btn" + (i + 1);
        //    _btnPrefab.GetComponent<Button>();
        //    TextMeshProUGUI text = _btnPrefab.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        //    text.text = (i+1).ToString();
        //}
        //nextBtn.transform.SetAsLastSibling();
    }


    private void Update()
    {
        if(OrderBasePage.instance.networkOn && networkCount == 0)//All_AppManager.instance.networkShow)//if(BtnTest.instance.orderBtnOn)
        {
            networkCount = 1;
            //BtnTest.instance.orderBtnOn = false;

            //기존에 있던걸 지운다
            foreach (var obj in poolBtnPage)
            {
                Destroy(obj);
            }
            poolBtnPage.Clear();

            //새롭게 세팅해준다
            StartCoroutine(ButtonSetting());
            StartCoroutine(NetworkCount());
        }
    }

    //네트워크에서 정보 하나만 들고오기 위한 코루틴
    IEnumerator NetworkCount()
    {
        yield return new WaitForSeconds(6.1f);
        networkCount = 0;
    }

}
