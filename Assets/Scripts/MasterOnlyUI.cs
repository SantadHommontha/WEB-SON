using Photon.Pun;
using UnityEngine;

public class MasterOnlyUI : MonoBehaviour
{
    [SerializeField] private BoolValue isConnectToRoom;
    void OnEnable()
    {
        if (isConnectToRoom.Value)
            gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }
}
