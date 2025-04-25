using UnityEngine;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;
public class TeamManager : MonoBehaviourPunCallbacks
{
    public static TeamManager instance;
    [SerializeField] private int maxTeamCount = 3;
    [SerializeField] private TMP_Text report;
    private Team team = new Team();

    public Team Team_Script => team;
    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    public void JoinTeam(PlayerData _playerData)
    {
        string josnData = JsonUtility.ToJson(_playerData);

        photonView.RPC("TryJoinTeam", RpcTarget.MasterClient, josnData);
    }

    [PunRPC]
    private void TryJoinTeam(string _playerData)
    {
        //    Debug.Log(_playerData);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(_playerData);

        if (team.PlayerCount(playerData.teamName) < maxTeamCount && team.TryToAddPlayer(playerData))
        {
            report.text = "Add Team Complete";

        }
        else
        {
            report.text = "Add Team Fail";

        }


    }
    [ContextMenu("LogShow")]
    private void Log()
    {
        team.LogShow();
    }

    [ContextMenu("Kick")]
    public void Kick(string _playerID)
    {
        team.RemovePlayer(_playerID);
        
    }

    // private void TryJoinTeam(string _jsonData, PhotonMessageInfo _info)
    // {
    //     if (!PhotonNetwork.IsMasterClient) return;

    //     var data = JsonUtility.FromJson<PlayerData>(_jsonData);
    //     data.info = _info;

    //     if (data.teamName == ValueName.RED_TEAM)
    //     {

    //         if (redTeamCount < maxTeamCount && team.TryToAddPlayer(data))
    //         {
    //             // add Complete
    //             Debug.Log($"PLayer Join Add Team:{data.playerName} {data.playerID}");
    //             redTeamCount++;

    //         }
    //         else
    //         {
    //             // fail to addFD

    //         }
    //     }
    //     else if (data.teamName == ValueName.BLUE_TEAM)
    //     {

    //         if (blueTeamCount < maxTeamCount && team.TryToAddPlayer(data))
    //         {
    //             // add Complete
    //             Debug.Log($"PLayer Join Minus Team:{data.playerName} {data.playerID}");
    //             blueTeamCount++;

    //         }
    //         else
    //         {
    //             // fail to add

    //         }
    //     }
    //     else
    //     {
    //         // fail not have this team name

    //     }



    // }
}
