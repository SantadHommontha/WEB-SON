using UnityEngine;
using UnityEngine.UI;

public class TeamJoinHandler : MonoBehaviour
{
    [SerializeField] private InputField nameInput;
    [SerializeField] private InputField codeInput;

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


    }
}
