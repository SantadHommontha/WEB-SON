using System.Collections;
using UnityEngine;

public class FetchTimer : Timer
{
    protected override IEnumerator Countdown(float _countdownTime)
    {
        timer.Value = 1;
        yield return new WaitForSeconds(_countdownTime);
        timer.Value = 0;
        Debug.Log("FFFF");
        timerCoroutine = null;
    }
}
