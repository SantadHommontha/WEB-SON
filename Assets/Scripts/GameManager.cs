
using Photon.Pun;
using UnityEngine;



public class PlayerScoreData
{
    public string playerName;
    public string teamName;
    public int clickCount;
}

public class GameManager : MonoBehaviourPun
{
    [SerializeField] private IntValue score;


    private void UpdateGameScore()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        photonView.RPC("SendClickCount", RpcTarget.Others);
    }


    [PunRPC]
    private void SendClickCount()
    {
           photonView.RPC("ReciveClickCount", RpcTarget.MasterClient);
    }

    [PunRPC]
    private void ReciveClickCount(string _playerScoreData)
    {
        if (!PhotonNetwork.IsMasterClient) return;
    }
}
