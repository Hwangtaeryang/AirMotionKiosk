using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemManager : MonoBehaviour
{

    
    public void UseAppBtnOn()
    {
        //NetworkManager.instance.loadSceneName = "AriMotion_Kiosk";
        SceneManager.LoadScene("AriMotion_Kiosk");
    }

    public void ManagerAppBtnOn()
    {
        //NetworkManager.instance.loadSceneName = "AriMotion_Kiosk_Manager";
        SceneManager.LoadScene("AriMotion_Kiosk_Manager");
    }

    public void MoriterAppBtnOn()
    {
        //NetworkManager.instance.loadSceneName = "AriMotion_Kiosk_Moriter";
        SceneManager.LoadScene("AriMotion_Kiosk_Moriter");
    }


}
