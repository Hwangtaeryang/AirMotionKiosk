using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnTest : MonoBehaviour
{
    public static BtnTest instance { get; private set; }


    public bool orderBtnOn;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        
    }

    public void OrderBtnOn()
    {
        orderBtnOn = true;
    }

    
}
