using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager_Moriter : MonoBehaviour
{
    public static Sound_Manager_Moriter instance { get; private set; }


    public AudioSource myAudio;


    public AudioClip dingdong_sound;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    //주문이 들어왔다는 사운드(매니저앱)
    //음료 준비가 완료됬다는 사운드(모니터앱)
    public void DingdongSound()
    {
        myAudio.PlayOneShot(dingdong_sound);
    }

    
}
