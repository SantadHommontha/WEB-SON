using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class PlayerNameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private int nameIndex;
    [SerializeField] private string teamName;
    private string playerID;

    void Start()
    {
        ChangeName("");
        TeamManager.instance.Team_Script.OnPlayerTeamChange += TeamChange;

    }
    void OnDisable()
    {
        TeamManager.instance.Team_Script.OnPlayerTeamChange -= TeamChange;
    }
    private void TeamChange()
    {
        ChangeName("");
        List<PlayerData> p = new List<PlayerData>();
        if (teamName == TeamName.FirstTeam)
            p = TeamManager.instance.Team_Script.GetPlayerByTeam(TeamName.FirstTeam);
        else if (teamName == TeamName.SecondTeam)
            p = TeamManager.instance.Team_Script.GetPlayerByTeam(TeamName.SecondTeam);

        if (nameIndex < p.Count)
        {
            ChangeName(p[nameIndex].playerName);
            playerID = p[nameIndex].playerID;
        }
    }


    public void ChangeName(string _name)
    {
        playerName.text = _name;
    }
    public void Kick()
    {
        TeamManager.instance.Kick(playerID);
    }
}
