using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestApp : MonoBehaviour
{
    public static TestApp instance { get; private set; }


    public int orderNum;
    public bool orderState;



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

    public void OrderButton()
    {
        orderState = true;
        MoniterAppManager.instance.one = true;
        MoniterAppManager2.instance.one = true;
    }

    
}
