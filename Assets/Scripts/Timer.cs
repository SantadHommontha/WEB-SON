using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
#if UNITY_EDITOR //เอาไว้ว่านับเวลาของอะไร
    [SerializeField][TextArea] private string description;
#endif
    [SerializeField] protected FloatValue timer;
    [SerializeField] protected BoolValue startTimer;

    [SerializeField] protected float time = 10f;
    protected Coroutine timerCoroutine;

    void Start()
    {
        startTimer.OnValueChange += StartChange;
    }

    protected void StartChange(bool _b)
    {
        if (_b) StartTimer();
        else StopTime();
    }

    [ContextMenu("Start Timer")]
    public void StartTimer()
    {
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        timerCoroutine = StartCoroutine(Countdown(time));
    }

    public void StopTime()
    {
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
    }

    protected virtual IEnumerator Countdown(float _countdownTime)
    {
        timer.Value = _countdownTime;

        while (timer.Value > 0)
        {
            yield return new WaitForSeconds(1f);
            timer.Value--;
        }
        timerCoroutine = null;

        Debug.Log("timer UP!");
    }
}
