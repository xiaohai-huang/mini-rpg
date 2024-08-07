using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private static Timer _instance;
    public static Timer Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject timerObject = new GameObject("Timer");
                _instance = timerObject.AddComponent<Timer>();
            }
            return _instance;
        }
    }

    private Dictionary<int, Coroutine> _timers = new Dictionary<int, Coroutine>();
    private int _nextTimerId = 0;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public int SetTimeout(Action task, float delayInMilliseconds)
    {
        float delayInSeconds = delayInMilliseconds / 1000f;
        int timerId = _nextTimerId++;
        _timers[timerId] = StartCoroutine(ExecuteAfterTime(delayInSeconds, task));
        return timerId;
    }

    public void ClearTimeout(int timerId)
    {
        if (_timers.ContainsKey(timerId))
        {
            StopCoroutine(_timers[timerId]);
            _timers.Remove(timerId);
        }
    }

    public int SetInterval(Action task, float intervalInMilliseconds, bool immediate = false)
    {
        float intervalInSeconds = intervalInMilliseconds / 1000f;
        int timerId = _nextTimerId++;
        _timers[timerId] = StartCoroutine(ExecuteRepeatedly(intervalInSeconds, task, immediate));
        return timerId;
    }

    public void ClearInterval(int timerId)
    {
        ClearTimeout(timerId);
    }

    private IEnumerator ExecuteAfterTime(float time, Action task)
    {
        yield return new WaitForSeconds(time);
        task();
    }

    private IEnumerator ExecuteRepeatedly(float interval, Action task, bool immediate = false)
    {
        while (true)
        {
            if (immediate)
            {
                task();
                yield return new WaitForSeconds(interval);
            }
            else
            {
                yield return new WaitForSeconds(interval);
                task();
            }
        }
    }
}
