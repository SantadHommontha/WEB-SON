using UnityEngine.UI;
using Photon.Pun;
using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    [SerializeField] private Image loadBar;
    [SerializeField] private GameObject connect_canvas;
    [SerializeField] private GameObject chooseTeam_canvas;
    [SerializeField] private GameObject play_canvas;
    [SerializeField] private GameObject gameEnd_canvas;
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    void Start()
    {
        connect_canvas.SetActive(true);
        play_canvas.SetActive(false);
        chooseTeam_canvas.SetActive(false);
        gameEnd_canvas.SetActive(false);
        loadBar.fillAmount = 0;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        loadBar.fillAmount = 0.35f;
        Debug.Log("Connect To Master Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Join a Lobby");
        loadBar.fillAmount = 0.65f;
        PhotonNetwork.JoinOrCreateRoom("GameRoom", null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Join a RoomJoinRoom()");
        loadBar.fillAmount = 1;
        StartCoroutine(COuntDownToChoostTeam());
    }

    private IEnumerator COuntDownToChoostTeam()
    {
        yield return new WaitForSeconds(0.7f);
        connect_canvas.SetActive(false);
        play_canvas.SetActive(false);
        chooseTeam_canvas.SetActive(true);
        gameEnd_canvas.SetActive(false);

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
