
using System;
using ExitGames.Client.Photon;
using NUnit.Framework;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Rendering.Universal;



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


    [SerializeField] private IntValue redTeamScore;
    [SerializeField] private IntValue blueTeamScore;


    [SerializeField] private GameObject play_canvas;
    [SerializeField] private GameObject end_canvas;
    [SerializeField] private GameObject red_ui;
    [SerializeField] private GameObject blue_ui;


    private bool gamestart = false;
    void Start()
    {
        timer.OnValueChange += TimeChange;

        fetchtimer.OnValueChange += CheckFetchTime;
        SetUp();
    }



    private void SetUp()
    {
        gamestart = false;
        startTimer.Value = false;
        startFetchTimer.Value = false;
        score.Value = 0;
        timer.Value = 0;
    }

    [ContextMenu("Start Game")]

    public void StartGame()
    {

        if (!PhotonNetwork.IsMasterClient) return;

        SetUp();
        startTimer.Value = true;
        startFetchTimer.Value = true;
        gamestart = true;
        photonView.RPC("GameStart", RpcTarget.Others);

    }

    [PunRPC]
    private void GameStart()
    {
        SetUp();
    }
    protected void TimeChange(float _time)
    {
        if (!gamestart) return;
        if (_time <= 0)
        {
            startTimer.Value = false;
            startFetchTimer.Value = false;
            UpdateGameScore();
            GameEnd();
        }

        photonView.RPC("ReciveTime", RpcTarget.Others, timer.Value);
    }
    private void CheckFetchTime(float _b)
    {
        if (!gamestart) return;
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
            redTeamScore.Value += playerScoreData.clickCount;
        }
        else
        {
            score.Value -= playerScoreData.clickCount;
            blueTeamScore.Value += playerScoreData.clickCount;
        }

        photonView.RPC("ReciveScore", RpcTarget.Others, score.Value);

    }




    private void GameEnd()
    {
        end_canvas.SetActive(true);
        play_canvas.SetActive(false);

        if (score.Value > 0)
        {
            red_ui.SetActive(true);
            blue_ui.SetActive(false);
        }
        else
        {
            red_ui.SetActive(false);
            blue_ui.SetActive(true);
        }
        if (PhotonNetwork.IsMasterClient)
            photonView.RPC("ReciveGameEnd", RpcTarget.Others);
    }

    [PunRPC]
    private void ReciveGameEnd()
    {
        GameEnd();
    }



    [PunRPC]
    private void ReciveTime(float _time)
    {
        if (PhotonNetwork.IsMasterClient) return;
        timer.Value = _time;
    }
    [PunRPC]
    private void ReciveScore(int _score)
    {
        if (PhotonNetwork.IsMasterClient) return;
        score.Value = _score;
    }
}
