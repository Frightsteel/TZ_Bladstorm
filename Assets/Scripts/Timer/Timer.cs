using System;

public class Timer
{
    public event Action<float> OnTimerValueChangedEvent;
    public event Action OnTimerFinishedEvent;

    public TimerType Type { get; }
    public float RemainingSeconds { get; private set; }
    public bool IsPaused { get; private set; }

    public Timer(TimerType type)
    {
        Type = type;
    }

    public Timer(TimerType type, float seconds)
    {
        Type = type;
        SetTime(seconds);
    }

    public void SetTime(float seconds)
    {
        RemainingSeconds = seconds;
        OnTimerValueChangedEvent?.Invoke(RemainingSeconds);
    }

    public void Start()
    {
        if (RemainingSeconds == 0)
        {
            OnTimerFinishedEvent?.Invoke();
        }

        IsPaused = false;
        Subscribe();
        OnTimerValueChangedEvent?.Invoke(RemainingSeconds);
    }

    public void Start(float seconds)
    {
        SetTime(seconds);
        Start();
    }

    public void Paused()
    {
        IsPaused = true;
        Unsubscribe();
        OnTimerValueChangedEvent?.Invoke(RemainingSeconds);
    }

    public void Resume()
    {
        IsPaused = false;
        Subscribe();
        OnTimerValueChangedEvent?.Invoke(RemainingSeconds);
    }

    public void Stop()
    {
        Unsubscribe();
        RemainingSeconds = 0f;

        OnTimerValueChangedEvent?.Invoke(RemainingSeconds);
        OnTimerFinishedEvent?.Invoke();
    }

    private void Subscribe()
    {
        switch (Type)
        {
            case TimerType.UpdateTick:
                TimeInvoker.Instance.OnUpdateTimeTickedEvent += OnUpdateTick;
                break;

            case TimerType.UpdateTickUnscaled:
                TimeInvoker.Instance.OnUpdateTimeUnscaledTickedEvent += OnUpdateTick;
                break;

            case TimerType.OnSecTick:
                TimeInvoker.Instance.OnOneSecondTickedEvent += OnOneSecTick;
                break;

            case TimerType.OnSecTickUnscaled:
                TimeInvoker.Instance.OnOneSecondUnscaledTickedEvent += OnOneSecTick;
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Unsubscribe()
    {
        switch (Type)
        {
            case TimerType.UpdateTick:
                TimeInvoker.Instance.OnUpdateTimeTickedEvent -= OnUpdateTick;
                break;

            case TimerType.UpdateTickUnscaled:
                TimeInvoker.Instance.OnUpdateTimeUnscaledTickedEvent -= OnUpdateTick;
                break;

            case TimerType.OnSecTick:
                TimeInvoker.Instance.OnOneSecondTickedEvent -= OnOneSecTick;
                break;

            case TimerType.OnSecTickUnscaled:
                TimeInvoker.Instance.OnOneSecondUnscaledTickedEvent -= OnOneSecTick;
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnUpdateTick(float deltaTime)
    {
        if (IsPaused)
            return;

        RemainingSeconds -= deltaTime;
        CheckFinish();
    }

    private void OnOneSecTick()
    {
        if (IsPaused)
            return;

        RemainingSeconds -= 1f;
        CheckFinish();
    }

    private void CheckFinish()
    {
        if (RemainingSeconds <= 0)
        {
            Stop();
        }
        else
        {
            OnTimerValueChangedEvent?.Invoke(RemainingSeconds);
        }
    }
}