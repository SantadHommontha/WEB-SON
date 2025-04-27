using UnityEngine;
using System.Linq;
using System;
using TMPro;
public class RoomCodeUI : MonoBehaviour
{

    private string GenerateRandomString(int length)
    {
        return new string(Guid.NewGuid().ToString("N").ToUpper().Take(length).ToArray());
    }
    [SerializeField] private TMP_InputField input;
    [SerializeField] private StringValue roomCode;
    void Start()
    {

        input.text = roomCode.Value;
    }
    public void GenerateRandom(int _length)
    {
        input.text = GenerateRandomString(_length);
        roomCode.Value = input.text;
    }
    public void CharacterUpdate(string _input)
    {
        if (_input.Length <= 4)
        {
            input.text = _input.ToUpper();
        }
        else
        {
            input.text = _input.Substring(0, 4).ToUpper();
        }
       if(roomCode) roomCode.Value = input.text;
    }







}
