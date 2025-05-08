using UnityEngine;
using Photon.Pun;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    [SerializeField] private TMP_Text Text;


    [SerializeField][Range(0,20)] private int number;
    [SerializeField] private IntValue numberV;
    [System.Serializable]
    public class PLayerData
    {
        public string name;
        public int age;
        public string sex;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        numberV.Value = number;
        if(Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(1);
        }
    }

    [ContextMenu("Send")]
    public void Send(string _text)
    {

    //     ExitGames.Client.Photon.Hashtable data = new ExitGames.Client.Photon.Hashtable()
    //   {
    //     {"Key Name",10000},
    //     {"Key Name2", "string"}
    //   };


    //     PhotonNetwork.CurrentRoom.SetCustomProperties(data);



    //     if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("Key Name"))
    //     {
    //         string value = (string)PhotonNetwork.CurrentRoom.CustomProperties["Key Name"];
    //     }




    }


    [PunRPC]
    public void Receive(string _pLayerData)
    {
        PLayerData pLayerData = JsonUtility.FromJson<PLayerData>(_pLayerData);

        Debug.Log(_pLayerData);
    }
}
