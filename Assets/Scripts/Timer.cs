using UnityEngine;
using System.Collections;
public class Timer : MonoBehaviour
{
#if UNITY_EDITOR //เอาไว้ว่านับเวลาของอะไร
    [SerializeField][TextArea] private string description;
#endif
    [SerializeField] private FloatValue time;
    [SerializeField] private float countdownTime = 10f;
  
    [ContextMenu("Start Timer")]
    public void StartTimer()
    {
        StartCoroutine(StartCountdown(countdownTime));
    }

    private IEnumerator StartCountdown(float _countdownTime)
    {
        time.Value = _countdownTime;

        while (time.Value > 0)
        {
            yield return new WaitForSeconds(1f);
            time.Value--;
        }
        Debug.Log("Time UP!");
    }
}
