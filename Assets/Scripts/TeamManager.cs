using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections.Generic;
using ExitGames.Client.Photon;

public class TeamManager : MonoBehaviourPunCallbacks
{
    public static TeamManager instance;
    [SerializeField] private int maxTeamCount = 3;
    [SerializeField] private TMP_Text report;
    [SerializeField] private string code;
    private Team team = new Team();

    public Team Team_Script => team;
    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }
    void Start()
    {
        team.OnPlayerTeamChange += UpdateTeamToOther;
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
        if (playerData.code == code)
        {
            if (team.PlayerCount(playerData.teamName) < maxTeamCount && team.TryToAddPlayer(playerData))
            {
                report.text = "Add Team Complete";

            }
            else
            {
                report.text = "Add Team Fail";

            }
        }
        else
        {
            report.text = "Code Not Correct";
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



    private void UpdateTeamToOther()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        TeamsWrapper teamsWrapper = new TeamsWrapper(team.GetAllPlayer());

        ExitGames.Client.Photon.Hashtable playerdata = new ExitGames.Client.Photon.Hashtable()
    {
        {RoomPropertiesName.TeamData,JsonUtility.ToJson(teamsWrapper)}
    };

        PhotonNetwork.CurrentRoom.SetCustomProperties(playerdata);
    }



    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {

        Debug.Log((string)propertiesThatChanged[RoomPropertiesName.TeamData]);
        if (propertiesThatChanged.ContainsKey(RoomPropertiesName.TeamData))
        {

            TeamsWrapper teamsWrapper = JsonUtility.FromJson<TeamsWrapper>((string)propertiesThatChanged[RoomPropertiesName.TeamData]);

            if (PhotonNetwork.IsMasterClient) return;
            team.ClearAll();

            foreach (var T in teamsWrapper.playerDatas)
            {
                team.AddPlayer(T);
            }
        }
    }




}

[System.Serializable]
public class TeamsWrapper
{
    public List<PlayerData> playerDatas;
    public TeamsWrapper(List<PlayerData> _playerDatas)
    {
        this.playerDatas = _playerDatas;
    }
}
