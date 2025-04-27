using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class InServer : MonoBehaviour
{
    private Image image;
    void Awake()
    {
        image = GetComponent<Image>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsConnected)
            image.color = Color.green;
        else
            image.color = Color.red;
    }
}
