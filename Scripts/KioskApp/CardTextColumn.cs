using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTextColumn : MonoBehaviour
{
    public static CardTextColumn instance { get; private set; }

    public bool cardMentChoice; //카드 결제 선택(2층 원통 카드결제 화면으로 돌리는 변수)
    public bool cardMentComplete;   //카드결제 완료하는 변수
    public GameObject cardBack; //카드결제 백버튼

    Animator animator;
    Renderer cardRend;  //텍스트 렌더러

    [Header("텍스트 색상변경")]
    public float speed;
    public Color startColor;
    public Color endColor;
    float timeLeft = 0;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this; 
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        cardRend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("L_Hand") || other.CompareTag("R_Hand"))
        {
            SoundManager.instance.ChoiceSound();
            cardMentChoice = true;   //카드화면으로 돌리는 변수
            cardMentComplete = true; //카드 결제 변수
            animator.SetBool("CardRot", true);   //페이 돌리기

            cardBack.GetComponent<Animator>().SetBool("CardBackBtn", false);  //카드백버튼 초기화 돌려놓기
            cardBack.transform.localPosition = new Vector3(-0.2727f, 0.1271f, 0.2508f);
            StartCoroutine("ChangeColor");
        }
    }


    IEnumerator ChangeColor()
    {
        while (timeLeft < 3f)
        {
            timeLeft = (timeLeft + Time.deltaTime) * speed;

            if (timeLeft >= 3f)
                timeLeft = 3.1f;

            cardRend.material.color = Color.Lerp(startColor, endColor, timeLeft);
            ///Debug.Log(cardRend.material.color);

            yield return new WaitForEndOfFrame();
        }
    }

    public void WhileColor()
    {
        cardMentChoice = false; //결제선택 화면에 있는 선택 해지
        cardMentComplete = false;
        timeLeft = 0f;  //색변경 초 초기화
        cardRend.material.color = startColor;
    }
}
