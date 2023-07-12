using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDisappearCoolTime : MonoBehaviour
{
    public TextMeshProUGUI text;

    float coolTime;

    public bool orderNum_CoolStart;
    public bool orderNum_CoolEnd;
    public bool complete_CoolEnd;

    void Start()
    {
    }


    void Update()
    {
        if (orderNum_CoolStart)//MoniterAppManager2.instance.completeCoolTime)
        {
            StopAllCoroutines();
            orderNum_CoolStart = false; // MoniterAppManager2.instance.completeCoolTime = false;

            //Debug.Log("?????  " + text.text + "::: " + this.gameObject.name);
            coolTime = 30f;

            if (text.text != "")
            {
                //Debug.Log("들어왔쥬~!" + this.gameObject.name);
                StartCoroutine(TextCoolTime(coolTime));
            }
            else
            {
                StartCoroutine(TextCoolTime(coolTime));
            }
        }

        //주문완료 대기열에 5분 쿨타임 주기 위한
        if(complete_CoolEnd)
        {
            //Debug.Log("!!!!!!!!!!!!!!!" + this.gameObject.name);
            complete_CoolEnd = false;

            StartCoroutine(CompleteCoolTime());
        }
    }

    //주문완료 메인 뷰에 5분 쿨타임 주는 함수
    IEnumerator TextCoolTime(float _cool)
    {
        //5분동안 쿨타임
        while (_cool > 1.0f)
        {
            _cool -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        //주문번호 성공 메인 뷰에 글자가 있으면
        if (this.gameObject.name == "OrderNumber")
        {
            orderNum_CoolEnd = true;
            StartCoroutine(DeleteText());
        }

        complete_CoolEnd = true;
    }

    //공백으로 만들기 위한 함수
    IEnumerator DeleteText()
    {
        yield return new WaitForSeconds(0.2f);
        text.text = "";
    }

    //완료한 주문번호 대기열에 글자가 잇을경우 5분 쿨타임 주는 함수
    IEnumerator CompleteCoolTime()
    {
        yield return new WaitForSeconds(0.2f);

        if (this.gameObject.name == "Text (TMP)")
        {
            if (text.text != "")
            {
                coolTime = 30f;

                //5분동안 쿨타임
                while (coolTime > 1.0f)
                {
                    coolTime -= Time.deltaTime;
                    yield return new WaitForFixedUpdate();
                }

                text.text = "";
            }
        }
    }
}
