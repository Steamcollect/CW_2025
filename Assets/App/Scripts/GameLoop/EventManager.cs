using System.Collections.Generic;
using UnityEngine;
public class EventManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float timeBetweenEvents = 5f;
    [Header("Input")]
    [SerializeField] private RSE_SendEvent rseSendEvent;
    
    private readonly Queue<Event> _events = new();
    private Event _currentEvent;
    private bool _eventWaiting;
    private float _internalTime;
    private float _timeSinceLastEvent;

    private void OnEnable() => rseSendEvent.action += ReceiveEvent;
    private void OnDisable() => rseSendEvent.action -= ReceiveEvent;
    
    private void Update()
    {
        _internalTime += Time.deltaTime;
        _timeSinceLastEvent += Time.deltaTime;
        if( !_eventWaiting ) return;
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
            ProcessEvent(e);
            return;
        }
        _eventWaiting = true;
        _events.Enqueue(new Event{eventType = e.eventType, time = _internalTime + e.time});
    }

    private void ProcessEvent(Event e)
    {
        switch (e.eventType)
        {
            case EventType.EventWorld:
                break;
            case EventType.QTE:
                break;
            case EventType.Question:
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