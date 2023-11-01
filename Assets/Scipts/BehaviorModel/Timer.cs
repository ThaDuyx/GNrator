using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool _isActive = false;
    private bool _didStart = false;
    private float duration;
    private float simulationDuration;

    public void Begin(float duration, RenderMethod method)
    {
        this.duration = duration;
        _isActive = true;
        
        if (!_didStart)
        {
            simulationDuration = SetSimulationDuration(method);
            _didStart = true;
        } 

        StartCoroutine(Countdown());
    }

    public void Stop()
    {
        _isActive = false;
        _didStart = false;
    }
    private IEnumerator Countdown()
    {
        while (duration > 0)
        {
            yield return new WaitForSeconds(1.0f);
            duration--;
            simulationDuration--;
        }
        
        _isActive = false;
    }

    public bool IsActive()
    {
        return _isActive;
    }

    public float GetTimeLeft()
    {
        return duration;
    }

    public float GetTimeLeftOfSimulation()
    {
        return simulationDuration;
    }

    private float SetSimulationDuration(RenderMethod method)
    {
        return method switch
        {
            RenderMethod.AllAtOnce => RenderManager.Instance.SOFACount * duration,
            RenderMethod.OneByOne => RenderManager.Instance.SpeakerCount * RenderManager.Instance.SimulationLength,
            _ => 0.0f,
        };
    }
}