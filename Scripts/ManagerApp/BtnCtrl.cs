using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnCtrl : MonoBehaviour
{
    Button meBtn;
    bool meState;

    void Start()
    {
        meBtn = gameObject.GetComponent<Button>();
    }

    
    void Update()
    {
        //if(meState)
        //{
        //    meBtn.image.sprite = Resources.Load<Sprite>("Texture/ManagerApp/Btn_Color");
        //}
        //else
        //{
        //    meBtn.image.sprite = Resources.Load<Sprite>("Texture/ManagerApp/Btn_Btn");
        //}
    }

    public void ButtonOnClick()
    {
        ManagerApp.instance.btnOn = true;   //버튼 눌렀음.

        for(int i = 0; i < OrderBasePage.instance.pageParent; i++)
        {
            if (this.gameObject.name == "Btn" + (i + 1))
            {
                //Debug.Log("Btn" + (i + 1) + "누름");
                OrderBasePage.instance.btnOnState[i] = true;
            }
            else
            {
                //Debug.Log("Btn" + (i + 1) + "해제");
                OrderBasePage.instance.btnOnState[i] = false;
            }
        }
    }


    //넥스트버튼 이벤트
    public void NextButtonOnClick()
    {
        ManagerApp.instance.nextBtnOn = true;
    }


    //이전버튼 이벤트
    public void PreviousButtonOnClick()
    {
        ManagerApp.instance.previousBtnOn = true;
    }




}
