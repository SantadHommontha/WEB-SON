using UnityEngine.UI;
using Photon.Pun;
using UnityEngine;
using System.Collections;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    [SerializeField] private Image loadBar;
    [SerializeField] private GameObject connect_canvas;
    [SerializeField] private GameObject chooseTeam_canvas;
    [SerializeField] private GameObject play_canvas;
    [SerializeField] private GameObject gameEnd_canvas;
    [SerializeField] private BoolValue isConnectToRoom;

    [SerializeField] private GameObject loadBar_ground;
    [SerializeField] private GameObject clickBtn;
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    void Start()
    {
        isConnectToRoom.Value = false;
        connect_canvas.SetActive(true);
        play_canvas.SetActive(false);
        chooseTeam_canvas.SetActive(false);
        gameEnd_canvas.SetActive(false);
        loadBar_ground.SetActive(true);
        clickBtn.SetActive(false);
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
        isConnectToRoom.Value = true;
        Debug.Log("Join a RoomJoinRoom()");
        loadBar.fillAmount = 1;
        StartCoroutine(COuntDownToChoostTeam());
    }

    private IEnumerator COuntDownToChoostTeam()
    {
        yield return new WaitForSeconds(0.7f);
        loadBar_ground.SetActive(false);
        clickBtn.SetActive(true);
        clickBtn.GetComponent<UIBlink>().Play();

    }

    public void OpenChooseTeme()
    {
        connect_canvas.SetActive(false);
        play_canvas.SetActive(false);
        chooseTeam_canvas.SetActive(true);
        gameEnd_canvas.SetActive(false);
        clickBtn.GetComponent<UIBlink>().Stop();
    }
    public void DisconnectPlayer(PhotonMessageInfo _target)
    {


    }

    [PunRPC]
    private void Disconnect()
    {
        PhotonNetwork.LeaveRoom();
        chooseTeam_canvas.SetActive(false);
        chooseTeam_canvas.SetActive(true);
    }

    public void ChangeMaster(Player _player)
    {
        PhotonNetwork.SetMasterClient(_player);
    }

}
