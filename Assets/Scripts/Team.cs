using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using System;


public class PlayerData
{
    public PhotonMessageInfo info;
    public string playerID;
    public string teamName;
    public string playerName;
    public string code;
}

public class Team
{
    private Dictionary<string, PlayerData> playerdata = new Dictionary<string, PlayerData>();

    // จะถูก Invoke ตอนที่ค่า playerdata มีการเปลี่ยนแปลง
    public Action OnPlayerTeamChange;



    // ลองเพิ่มข้อมูลลงใน playerdata ถ้าเพื่มสำเร็จจะ return ค่า True ถ้าเพื่มไม่ได้จะ return false
    public bool TryToAddPlayer(PlayerData _data)
    {
        if (playerdata.ContainsKey(_data.playerID))
        {
            AddPlayer(_data);
            return true;
        }
        else
        {
            return false;
        }
    }
    // เพิ่มข้อมูลลงใน playerdata
    public void AddPlayer(PlayerData _data)
    {
        if (!playerdata.ContainsKey(_data.playerID))
        {

            playerdata.Add(_data.playerID, _data);
            OnPlayerTeamChange?.Invoke();
        }
    }
    // ลบข้อมูลออกจาก playerdata
    public void RemovePlayer(string _playerID)
    {
        if (playerdata.ContainsKey(_playerID))
        {
            Debug.Log("Remove Player Complete");
            playerdata.Remove(_playerID);
            OnPlayerTeamChange?.Invoke();
        }
    }
    // ล้างข้อมูลทั้งหมดใน PlayerData
    public void ClearAll()
    {
        playerdata.Clear();
        OnPlayerTeamChange?.Invoke();
    }

    /* public void SetPlayerData(PlayerData[] _playerData)
     {
         Dictionary<string, PlayerData> playerdatas = new Dictionary<string, PlayerData>();
         foreach (var v in _playerData)
         {
             playerdatas.Add(v.playerID, v);
         }
         playerdata = playerdatas;
         OnPlayerTeamChange?.Invoke();
     }

 */
    /* #region Team Count
     public int AddTeamCount()
     {
         int count = 0;
         foreach (var v in playerdata)
         {
             if (v.Value.teamName == ValueName.ADD_TEAM) count++;
         }
         return count;
     }
     public int MinusTeamCount()
     {
         int count = 0;
         foreach (var v in playerdata)
         {
             if (v.Value.teamName == ValueName.MINUS_TEAM) count++;
         }
         return count;
     }
     public void TeamCount(out int _addTeam, out int _minusTeam)
     {
         _addTeam = AddTeamCount();
         _minusTeam = MinusTeamCount();
     }
     #endregion



     //#region ChangeData
 */

    /*   public void Change(ChangePlayerData _playerData)
       {
           if (HavePlayer(_playerData.playerTargetID, out var _player))
           {
               if (_playerData.playerName != null)
               {
                   _player.playerName = _playerData.playerName;
               }
               if (_playerData.teamName != null)
               {
                   _player.teamName = _playerData.teamName;
               }

           }
       }

       #endregion
       */
    //   #region  Add Player And Remove Player
    /* public bool TryToAddPlayer(PlayerData _data)
     {

         if (!HavePlayer(_data.playerID))
         {
             AddPlayer(_data);
             return true;
         }
         else
         {
             return false;
         }
     }*/



    /*  #region  HavePlayer
      public bool HavePlayer(string _playerID)
      {
          return playerdata.ContainsKey(_playerID);
      }
      public bool HavePlayer(string _playerID, out PlayerData _playerData)
      {
          if (HavePlayer(_playerID))
          {
              _playerData = GetPlayerData(_playerID);
              return true;
          }
          else
          {
              _playerData = new PlayerData();
              return false;
          }

      }
      #endregion

  */



}
