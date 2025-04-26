using UnityEngine;
using UnityEngine.EventSystems;
public class TouchArea : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private IntValue currentClick;
    [SerializeField] private IntValue allClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        Click();
        Debug.Log("Click");
    }

    public void Click()
    {
        currentClick.Value++;
        allClick.Value++;
    }
    
    public void Reset()
    {
        currentClick.Value = 0;
        allClick.Value = 0;
    }
}



