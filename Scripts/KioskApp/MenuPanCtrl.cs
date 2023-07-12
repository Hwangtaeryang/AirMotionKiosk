using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanCtrl : MonoBehaviour
{
    public static MenuPanCtrl instance { get; private set; }

    public TextMeshProUGUI amountText;
    public GameObject bell;

    public int cupAmount = 1;   //수량

    Vector3 target;
    Animator animator;
    //List<int> amount = new List<int>();

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        amountText.text = cupAmount.ToString();
        target = new Vector3(0.0781f, 0.05f, 0.0027f);
    }

    void Update()
    {
        if(CupCtrl.instance.trayTouch) //TrayTouch.instance.trayTouch)//
        {
            animator.SetBool("MenuDown", true);
            StartCoroutine(BellMoving());
        }
    }

    IEnumerator BellMoving()
    {
        yield return new WaitForSeconds(0.5f);
        //Debug.Log(bell.transform.localPosition);
        bell.transform.localPosition = Vector3.Lerp(bell.transform.localPosition, target, Time.deltaTime * 2f);
    }


    //음료 결제 버튼 클릭 
    public void BellChoiceBteOn()
    {
        //animator.SetBool("MenuUp", true);
        if(CupCtrl.instance.trayTouch) //TrayTouch.instance.trayTouch)//
        {
            SoundManager.instance.BellSound();
            CameraMoving.instacne.GetFloor2 = true; //카메라 2층으로 이동
            
            PlayerPrefs.SetInt("Amount", int.Parse(amountText.text));

            //amount.Add(int.Parse(amountText.text));           
            //OrderMenuJsonData.instance.order.amount = amount;   // 음료 갯수 저장

            CupCtrl.instance.trayTouch = false;
            //TrayTouch.instance.trayTouch = false;
            Debug.Log("선택 온");
        }
        
    }

    //수량 빼기 버튼
    public void MinusBtnOn()
    {
        if (cupAmount > 1)
            cupAmount -= 1;
        else
            cupAmount = 1;


        SoundManager.instance.MenuButtonClick();

        amountText.text = cupAmount.ToString();
        Debug.Log("마이너스 온" + cupAmount);
    }

    //수량 더하기 버튼
    public void PlusBtnOn()
    {
        if (cupAmount > 10)
            cupAmount = 10;
        else
            cupAmount += 1;

        SoundManager.instance.MenuButtonClick();

        amountText.text = cupAmount.ToString();
        Debug.Log("플러스 온" + cupAmount);
    }


    public void MenuReSet()
    {
        cupAmount = 1;
        amountText.text = cupAmount.ToString();
        bell.transform.localPosition = new Vector3(0.0781f, 0.07f, 0.0027f);
    }
}
