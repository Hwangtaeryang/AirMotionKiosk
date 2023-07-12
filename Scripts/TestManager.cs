using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static TestManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<TestManager>();
            }
            return m_instance;
        }
    }

    private static TestManager m_instance;
    public PhotonView photonview;
    public GameObject playerPrefab;
    public TextMeshProUGUI text;

    public int score = 0;
    public bool touch;



    void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonview.IsMine && touch)
        {
            photonview.RPC("AddScore", RpcTarget.AllBuffered, 0);
            touch = false;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            Debug.Log("---");

            stream.SendNext(score);
        }
        else
        {
            Debug.Log("???");
            score = (int)stream.ReceiveNext();
            text.text = score.ToString();
        }
    }

    [PunRPC]
    public void AddScore(int newScore)
    {

        touch = true;
        score += 100;
        text.text = score.ToString();
    }

    public void Test()
    {
        SceneManager.LoadScene("AriMotion_Kiosk_Manager");
    }
}
