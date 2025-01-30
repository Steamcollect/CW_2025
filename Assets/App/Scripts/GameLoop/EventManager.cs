using System.Collections.Generic;
using UnityEngine;
public class EventManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float timeBetweenEvents = 5f;
    [Header("Input")]
    [SerializeField] private RSE_SendEvent rseSendEvent;
    [SerializeField] private RSE_EventFinished rseEventFinished;
    
    private readonly Queue<Event> _events = new();
    private Event _currentEvent;
    private bool _eventWaiting;
    private bool _eventRunning;
    private float _internalTime;
    private float _timeSinceLastEvent;

    private void OnEnable()
    {
        rseSendEvent.action += ReceiveEvent;
        rseEventFinished.action += EventFinished;
    }

    private void OnDisable()
    {
        rseSendEvent.action -= ReceiveEvent;
        rseEventFinished.action -= EventFinished;
    }

    private void EventFinished() => _eventRunning = false;
    
    private void Update()
    {
        _internalTime += Time.deltaTime;
        _timeSinceLastEvent += Time.deltaTime;
        if( !_eventWaiting || _eventRunning) return;
        if (_currentEvent.time <= _internalTime && _timeSinceLastEvent >= timeBetweenEvents)
        {
            _timeSinceLastEvent = 0f;
            ProcessEvent(_currentEvent);
            if (_events.Count <= 0)
            {
                _eventWaiting = false;
                return;
            }
            _currentEvent = _events.Dequeue();
        }
    }
    
    private void ReceiveEvent(Event e, bool priority = false)
    {
        if (priority)
        {
            if (_eventRunning)
            {
                Queue<Event> queue = new();
                queue.Enqueue(e);
                for (int i = 0; i < _events.Count; i++)
                {
                    queue.Enqueue(_events.Dequeue());
                }
                _events.Clear();
                for (int i = 0; i < queue.Count; i++)
                {
                    _events.Enqueue(queue.Dequeue());
                }
                return;
            }
            ProcessEvent(e);
            return;
        }
        _eventWaiting = true;
        _events.Enqueue(new Event{eventType = e.eventType, time = _internalTime + e.time});
    }

    private void ProcessEvent(Event e)
    {
        _eventRunning = true;
        switch (e.eventType)
        {
            case EventType.QTE:
                Debug.Log("Spawn QTE Event");
                //Start QTE
                break;
            case EventType.Question:
                //Start Question
                Debug.Log("Ask Question !");
                break;
            default:
                Debug.LogWarning($"Event type not supported:{e.eventType}");
                break;
        }
    }
    
}

public struct Event
{
    public float time;
    public EventType eventType;
}

public enum EventType
{
    QTE,
    EventWorld,
    Question
}