using System.Collections;
using SteamAudio;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate void TimerEndedAction();
    public static event TimerEndedAction OnTimerEnded;
    
    public bool IsActive { get; private set; }
    public string CurrentTime { get { return duration.ToString(); }}
    public string TotalTime { get { return simulationDuration.ToString(); }}
    private bool didStart = false;
    private float duration;
    private float simulationDuration;

    public void Begin(float duration)
    {
        this.duration = duration;
        IsActive = true;
        
        if (!didStart)
        {
            simulationDuration = Calculator.CalculateRenderDuration(duration, SteamAudioManager.Singleton.SOFACount());
            didStart = true;
        } 

        StartCoroutine(Countdown());
    }

    public void Stop()
    {
        IsActive = false;
        didStart = false;
    }

    private IEnumerator Countdown()
    {
        while (duration > 0)
        {
            yield return new WaitForSeconds(1.0f);
            duration--;
            simulationDuration--;
        }
        
        IsActive = false;

        OnTimerEnded.Invoke();
    }
}