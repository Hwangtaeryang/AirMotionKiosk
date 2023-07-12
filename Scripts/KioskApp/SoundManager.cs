using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }


    public AudioSource wave;    //파도
    public AudioSource seagull; //갈매기
    public AudioSource paymentEnd; //결제
    public AudioSource myAudio;

    public AudioClip btn_sound; //버튼 사운드
    public AudioClip choice_sound;  //선택
    public AudioClip back_sound;    //백
    public AudioClip bell_sound;    //종
    public AudioClip enterPhone_sound;   //휴대폰 들어가는 소리
    public AudioClip enterCard_sound;   //카드 들어가는 소리
    public AudioClip endPayment_sound;  //결제 완료 소리


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        Screen.SetResolution(1080, 1920, true);
    }

    void Start()
    {
    }

    public void SeaGull_WaveStartSound()
    {
        seagull.Play();
        wave.Play();
    }

    public void SeaGull_WaveEndSound()
    {
        seagull.Stop();
        wave.Stop();
    }

    public void MenuButtonClick()
    {
        myAudio.PlayOneShot(btn_sound);
    }


    //선택 사운드
    public void ChoiceSound()
    {
        myAudio.PlayOneShot(choice_sound);
    }

    //백버튼
    public void BackSound()
    {
        myAudio.PlayOneShot(back_sound);
    }

    //종소리
    public void BellSound()
    {
        myAudio.PlayOneShot(bell_sound);
    }

    //폰,카드 들어가는 소리
    public void EnterPhoneSound()
    {
        myAudio.PlayOneShot(enterPhone_sound);
    }

    public void EnterCardSound()
    {
        myAudio.PlayOneShot(enterCard_sound);
    }

    //결제완료소리
    public void PayMentEndSound()
    {
        //myAudio.PlayOneShot(endPayment_sound);
        paymentEnd.PlayOneShot(endPayment_sound);
    }

    public void CardMentEndSound()
    {
        paymentEnd.PlayOneShot(endPayment_sound);
    }
}