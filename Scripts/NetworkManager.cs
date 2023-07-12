using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance { get; private set; }


    //string systemVersion = "1";

    //public Text connectInfoText;    //네트워크 정보 표시할 텍스트
    //public string loadSceneName;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        //모든 플레이어 마스터가 있는 방으로 자동 로드한다.
        //PhotonNetwork.AutomaticallySyncScene = true;
    }

    //void Start()
    //{
    //    //접속에 필요한 정보 설정
    //    PhotonNetwork.GameVersion = systemVersion;

    //    //설정한 정보를 가지고 마스터 서버 접속 시도
    //    PhotonNetwork.ConnectUsingSettings();

    //    //connectInfoText.text = "서버에 접속 중...";
    //    Debug.Log("서버에 접속 중...");

    //    //Connect();
    //}


    ////마스터 서버 접속 성공시 자동실행
    //public override void OnConnectedToMaster()
    //{
    //    //connectInfoText.text = "서버와 연결됨!!";
    //    Debug.Log("서버와 연결됨!!");
    //}

    ////마스터 서버 접속 실패시 자동 실행
    //public override void OnDisconnected(DisconnectCause cause)
    //{
    //    connectInfoText.text = "연걸되지 않음..\n" + "접속 재시도 중. ..";
    //    Debug.Log("연걸되지 않음..\n" + "접속 재시도 중. ..");
    //    //마스터 서버로의 재접속 시도
    //    PhotonNetwork.ConnectUsingSettings();
    //}

    ////룸 접속 시도
    //public void Connect()
    //{
    //    //마스터 서버에 접속 중이라면
    //    if (PhotonNetwork.IsConnected)
    //    {
    //        //룸 접속 실행
    //        connectInfoText.text = "룸에 접속...";
    //        PhotonNetwork.JoinRandomRoom();
    //    }
    //    else
    //    {
    //        //마스터 서버로 재접속 시도
    //        connectInfoText.text = "서버와 연결되지 않음/n" + "접속 재시도 중...";
    //        PhotonNetwork.ConnectUsingSettings();
    //    }
    //}

    ////방없을 때 랜덤 름 참가에 실패한 경우 자동 실행
    //public override void OnJoinRandomFailed(short returnCode, string message)
    //{
    //    connectInfoText.text = "빈 방 없음 / 새로운 방 생성..";
    //    PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 3 });
    //}

    ////룸에 참가 완료된 경우 자동 실행
    //public override void OnJoinedRoom()
    //{
    //    connectInfoText.text = "방 참가 성공";
    //    Debug.Log("방 참가 성공");
    //    //모든 룸 참가자들이 씬 로드
    //    PhotonNetwork.LoadLevel(loadSceneName);
    //}


    private void Start()
    {
        //PhotonNetwork.GameVersion = "1";
        //PhotonNetwork.ConnectUsingSettings();
        Connect();
    }

    public void Connect()
    {
        if(PhotonNetwork.IsConnected)
        {
            //존재하는 방이 있으면 OnJoinedRoom(), 없으면 OnJoinRandomFailed() 호출
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    //포톤 서버에 접속
    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 3;

        //새방을 만든다
        PhotonNetwork.CreateRoom("room", roomOptions);
    }

    ////방 입장
    public override void OnJoinedRoom()
    {
        Debug.Log("방입장");
    }

    void Update()
    {
        
    }
}
