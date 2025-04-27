
using System;
using System.Collections;
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

    [SerializeField] private FloatValue gameTimer;
    [SerializeField] private FloatValue timer;
    [SerializeField] private FloatValue fetchtimer;
    [SerializeField] private BoolValue startTimer;
    [SerializeField] private BoolValue startFetchTimer;


    [SerializeField] private IntValue redTeamScore;
    [SerializeField] private IntValue blueTeamScore;

    [SerializeField] private BoolValue enterGame;

    [SerializeField] private GameObject play_canvas;
    [SerializeField] private GameObject end_canvas;
    [SerializeField] private GameObject gameControl_canvas;
  
    [SerializeField] private GameObject red_ui;
    [SerializeField] private GameObject blue_ui;
    [SerializeField] private GameObject leave;



    [SerializeField] private BoolValue setSpectatorMode;


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
        timer.Value = gameTimer.Value;
        clickCount.SetValue(0);
    }

    [ContextMenu("Start Game")]

    public void StartGame()
    {

        if (!PhotonNetwork.IsMasterClient) return;

        SetUp();
        startTimer.Value = true;
        startFetchTimer.Value = true;
        gamestart = true;
        timer.Value = gameTimer.Value;
        if (setSpectatorMode.Value)
        {
            red_ui.SetActive(false);
            blue_ui.SetActive(false);
            gameControl_canvas.SetActive(false);
            play_canvas.SetActive(true);
            end_canvas.SetActive(false);
        }

        photonView.RPC("GameStart", RpcTarget.Others);

    }

    [PunRPC]
    private void GameStart()
    {
        if (!enterGame.Value) return;
        play_canvas.SetActive(true);
        red_ui.SetActive(false);
        blue_ui.SetActive(false);
        end_canvas.SetActive(false);
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
        if (!enterGame.Value) return;
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
        if (!enterGame.Value) return;
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

    public void LeaveGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {


            // if (setSpectatorMode.Value)
            // {

            // }
            // else
            // {
            //     play_canvas.SetActive(true);
            //     end_canvas.SetActive(false);
            // }
            gameControl_canvas.SetActive(true);
            SetUp();
        }
        else
        {

            TeamManager.instance.Kick(PhotonNetwork.LocalPlayer.UserId);


        }
    }

    public void ResetGame()
    {
        SetUp();
        photonView.RPC("ReciveReset", RpcTarget.Others);

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

        StartCoroutine(ShowLeaveBtn());
        if (PhotonNetwork.IsMasterClient)
            photonView.RPC("ReciveGameEnd", RpcTarget.Others);
    }

    private IEnumerator ShowLeaveBtn()
    {

        leave.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        leave.SetActive(true);
    }
    [PunRPC]
    private void ReciveGameEnd()
    {
        if (!enterGame.Value) return;
        GameEnd();
    }



    [PunRPC]
    private void ReciveTime(float _time)
    {
        if (!enterGame.Value) return;
        if (PhotonNetwork.IsMasterClient) return;
        timer.Value = _time;
    }
    [PunRPC]
    private void ReciveScore(int _score)
    {
        if (!enterGame.Value) return;
        if (PhotonNetwork.IsMasterClient) return;
        score.Value = _score;
    }

    [PunRPC]
    private void ReciveReset()
    {
        if (!enterGame.Value) return;
        SetUp();
        TeamManager.instance.Kick(PhotonNetwork.LocalPlayer.UserId);

    }
}
