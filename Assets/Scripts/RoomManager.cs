using Photon.Pun;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    public GameObject connectToServer_Canva;
    public GameObject selectTeam_Canva;


    [SerializeField] private GameObject chooseTeam_canvas;
    [SerializeField] private GameObject play_canvas;
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    void Start()
    {

        play_canvas.SetActive(false);
        chooseTeam_canvas.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
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
        PhotonNetwork.JoinOrCreateRoom("GameRoom", null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Join a RoomJoinRoom()");
    }

    public void DisconnectPlayer(PhotonMessageInfo _target)
    {


    }

    [PunRPC]
    private void Disconnect()
    {
        PhotonNetwork.LeaveRoom();
    }

}
