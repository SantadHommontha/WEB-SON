using UnityEngine;


public class PlayerListBtn : MonoBehaviour
{
    [SerializeField] private GameObject playerList;
    [SerializeField] private bool startShow;
    private bool show = false;

    void Start()
    {
        if (startShow) Show();
        else Hide();
    }
    private void Show()
    {
        playerList.SetActive(true);
        show = true;
    }
    private void Hide()
    {
        playerList.SetActive(false);
        show = false;
    }
    public void Toggle()
    {
        if (!show) Show();
        else Hide();
    }
}
