using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public static CameraMoving instacne { get; private set; }

    Transform camera;
    public GameObject paymentpageBack;  //결제 선택 페이지

    [Header("카메라 층 이동")]
    public bool GetFloor3, GetFloor2, GetFloor1;   //3층, 2층, 1층

    

    Animator animator;
    Vector3 startCameraPos; //카메라 시작 위치
    Vector3 targetPos2, targetPos1; //카메라 2층, 1층 위치

    private void Awake()
    {
        if (instacne != null)
            Destroy(this);
        else instacne = this;



        startCameraPos = new Vector3(0.352f, 1.461f, -4.163f);//this.transform.localPosition; //0.352, 1.461, -4.163
        this.transform.localPosition = startCameraPos;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        camera = GetComponent<Transform>();

        targetPos2 = new Vector3(0.352f, 1.019f, -4.163f);
        targetPos1 = new Vector3(0.352f, 0.557f, -4.163f);

        paymentpageBack.SetActive(false);

    }


    void Update()
    {
        //카메라 2층으로 이동
        if(GetFloor2)
        {
            GetFloor3 = false;
            StartCoroutine(Camera3_2Moving());
            SoundManager.instance.SeaGull_WaveEndSound();
        }

        //카메라 1층으로 이동
        else if(GetFloor1)
        {
            StartCoroutine(Camera2_1Moving());
        }

        //카메라 3층으로 이동
        else if(GetFloor3)
        {
            
            StartCoroutine(Camera1_3Moving());
            SoundManager.instance.SeaGull_WaveStartSound();
        }


        //2층 결제 페이지에 있는 백버튼 클릭 (2층->3층)
        if(PayMentPageBack.instance.paymentPageBack)
        {
            StartCoroutine(Camera2_3Moving());
        }
        
    }

    //카메라 3층->2층 이동 함수
    IEnumerator Camera3_2Moving()
    {
        paymentpageBack.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        transform.position = Vector3.Lerp(transform.position, targetPos2, Time.deltaTime * 2f);

        yield return new WaitForSeconds(3f);
        transform.localPosition = new Vector3(0.352f, 1.019f, -4.163f);
        GetFloor2 = false;
        paymentpageBack.SetActive(true);
        paymentpageBack.transform.localPosition = new Vector3(0.02406761f, -0.295f, 0.2504f);
    }

    IEnumerator Camera2_3Moving()
    {
        yield return new WaitForSeconds(1.2f);
        camera.transform.localPosition = new Vector3(0.352f, 1.461f, -4.163f);
        //animator.SetTrigger("Idle");
        PayMentPageBack.instance.paymentPageBack = false;

        yield return new WaitForSeconds(1.5f);
        CupCtrl.instance.trayTouch = true;
    }

    //카메라 2층에서 1층으로 이동
    IEnumerator Camera2_1Moving()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos1, Time.deltaTime * 2f);

        yield return new WaitForSeconds(2.5f);
        GetFloor1 = false;
    }

    IEnumerator Camera1_3Moving()
    {
        yield return null;// new WaitForSeconds(2f);
        camera.transform.localPosition = new Vector3(0.352f, 1.461f, -4.163f);
        PayMentPageBack.instance.paymentPageBack = false;
        GetFloor3 = false;

        yield return new WaitForSeconds(1f);
        //Debug.Log("-_-????");
    }

    void PayMentPageObjUnSetActive()
    {
        paymentpageBack.SetActive(false);
    }
}
