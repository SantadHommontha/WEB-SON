using TMPro;
using UnityEngine;


public class TeamJoinHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField codeInput;
     [SerializeField] private TMP_Text report;

    // เรียกใช้โดยตัว UI Buttom
    public void FirstTeam()
    {
        JoinTeam(TeamName.FirstTeam);
    }
    // เรียกใช้โดยตัว UI Buttom
    public void SecondTeam()
    {
        JoinTeam(TeamName.SecondTeamTeam);
    }

    private void JoinTeam(string _teamName)
    {
        PlayerData playerData = new PlayerData();

        playerData.playerName =nameInput.text;
        playerData.code = codeInput.text;
        playerData.teamName = _teamName;

    }
}
