using UnityEngine;
using System.Collections;
public class Timer : MonoBehaviour
{
    [SerializeField] private FloatValue timer;
    [SerializeField] private float countdownTime = 10f; 
    private float currentTime;


    public void StartTimer()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        currentTime = countdownTime;

        while (currentTime > 0)
        {

            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        Debug.Log("Time UP!");
      
    }

}
