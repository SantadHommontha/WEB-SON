using Photon.Pun;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    public GameObject connectToServer_Canva;
    public GameObject selectTeam_Canva;
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
   //     selectTeam_Canva.SetActive(false);
       // connectToServer_Canva.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connect To Master Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Join a Lobby");
        JoinRoom();
    //    selectTeam_Canva.SetActive(true);
     //   connectToServer_Canva.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Join a RoomJoinRoom()");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("GameRoom", null, null);
    }

}
