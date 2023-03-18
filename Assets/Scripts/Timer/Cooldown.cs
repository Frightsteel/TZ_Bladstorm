using System;
using UnityEngine;

public class Cooldown
{
    private TimerType _type;
    private Timer _timer;

    public float CooldownSeconds;
    public bool IsCooldowned;
    public string TimerName;

    public Cooldown(float cooldownSeconds, string name)
    {
        IsCooldowned = true;
        TimerName = name + "_Timer";
        CooldownSeconds = cooldownSeconds;

        _type = TimerType.OnSecTick;
        _timer = new Timer(_type, CooldownSeconds);

        _timer.OnTimerValueChangedEvent += OnTimerValueChanged;
        _timer.OnTimerFinishedEvent += OnTimerFinished;
    }

    private void OnTimerFinished()
    {
        IsCooldowned = true;
        Debug.Log("Timer Finished");
    }

    private void OnTimerValueChanged(float remainingSeconds)
    {
        Debug.Log($"{TimerName} - {remainingSeconds}");
    }

    public void StartCooldown()
    {
        IsCooldowned = false;
        _timer.Start(CooldownSeconds);
    }

    public void PauseCooldown()
    {
        if (_timer.IsPaused)
            _timer.Resume();
        else
            _timer.Paused();
    }

    public void StopCooldown()
    {
        IsCooldowned = true;
        _timer.Stop();
    }
}