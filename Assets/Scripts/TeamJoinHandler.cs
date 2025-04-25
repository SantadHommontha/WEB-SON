using Photon.Pun;
using TMPro;

using UnityEngine;


public class TeamJoinHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField codeInput;


    // เรียกใช้โดยตัว UI Buttom
    public void FirstTeam()
    {
        JoinTeam(TeamName.FirstTeam);
    }
    // เรียกใช้โดยตัว UI Buttom
    public void SecondTeam()
    {
        JoinTeam(TeamName.SecondTeam);
    }
    int num = 0;
    private void JoinTeam(string _teamName)
    {
        PlayerData playerData = new PlayerData();

        playerData.playerName = nameInput.text;
        playerData.code = codeInput.text;
        playerData.teamName = _teamName;
        // playerData.playerID = PhotonNetwork.LocalPlayer.UserId;
        playerData.playerID = num.ToString();
        num++;
        TeamManager.instance.JoinTeam(playerData);
    }
}
