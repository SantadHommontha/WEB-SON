using UnityEngine;
using Photon.Pun;
public class TeamManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private int maxTeamCount = 3;
    private Team team = new Team();
    

    private void TryJoinTeam(string _teamName)
    {


        // if (_team)



        //     if (PhotonNetwork.IsMasterClient)
        //     {
        //         // เป็น MasterClient
        //     }



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
