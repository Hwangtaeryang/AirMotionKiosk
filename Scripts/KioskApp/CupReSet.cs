using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupReSet : MonoBehaviour
{
    


    void Start()
    {
    }

    
    void Update()
    {
        
    }

    

    private void OnTriggerExit(Collider other)
    {
        
        if(other.CompareTag("Cup"))
        {
            other.transform.localPosition = new Vector3(-7.5926f, 0.8376f, 0.6754f); //컵 초기화 위치
        }
    }
}
