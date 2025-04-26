
using System;
using NUnit.Framework;
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
    [SerializeField] private StringValue myName;
    [SerializeField] private StringValue teamName;
    [SerializeField] private IntValue clickCount;

    [SerializeField] private FloatValue timer;
    [SerializeField] private FloatValue fetchtimer;
    [SerializeField] private BoolValue startTimer;
    [SerializeField] private BoolValue startFetchTimer;
    void Start()
    {
        timer.OnValueChange += TimeChange;
        fetchtimer.OnValueChange += CheckFetchTime;
    }

    [ContextMenu("Start Game")]
    private void StartGame()
    {
        startTimer.Value = true;
        startFetchTimer.Value = true;
    }


    protected void TimeChange(float _time)
    {
        if (_time <= 0)
        {
            startTimer.Value = false;
            startFetchTimer.Value = false;
            UpdateGameScore();
        }
    }
    private void CheckFetchTime(float _b)
    {
        if (_b <= 0)
        {
            UpdateGameScore();
            if (timer.Value <= 0)
                startFetchTimer.Value = false;
            else
                startFetchTimer.Value = true;
        }
    }


    [ContextMenu("UpdateGameScore")]
    private void UpdateGameScore()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        photonView.RPC("SendClickCount", RpcTarget.All);
    }


    [PunRPC]
    private void SendClickCount()
    {
        PlayerScoreData playerScoreData = new()
        {
            playerName = myName.Value,
            teamName = teamName.Value,
            clickCount = clickCount.Value
        };
        clickCount.SetValue(0);
        string jsonData = JsonUtility.ToJson(playerScoreData);
        photonView.RPC("ReciveClickCount", RpcTarget.MasterClient, jsonData);
    }

    [PunRPC]
    private void ReciveClickCount(string _playerScoreData)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        PlayerScoreData playerScoreData = JsonUtility.FromJson<PlayerScoreData>(_playerScoreData);


        if (playerScoreData.teamName == TeamName.FirstTeam)
        {
            score.Value += playerScoreData.clickCount;
        }
        else
        {
            score.Value -= playerScoreData.clickCount;
        }

    }
}
