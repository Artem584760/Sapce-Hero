using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI TimerText;
    private int seconds, minutes;

    private void Start()
    {
        TimerText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);

            if (seconds < 59)
            {
                seconds++;
            }
            else
            {
                seconds = 0;
                minutes++;
            }
            
            TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}