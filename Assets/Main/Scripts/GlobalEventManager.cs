using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalEventManager {

    static Dictionary<string, List<Action>> _events = new Dictionary<string, List<Action>>();
    static List<EventActionInfo> _pendingAdding = new List<EventActionInfo>();
    static List<EventActionInfo> _pendingremoving = new List<EventActionInfo>();


    static string _currentUsing = "";

    public static void TriggerEvent (string name) {
        if (_events.ContainsKey(name)) {
            _currentUsing = name;
            _events[name].ForEach(action => action?.Invoke());
            _currentUsing = "";
Debug.Log("event: " + name);
            ProcessPending();
        }
    }

    public static void AddListener (string name, Action action) {
        if (_currentUsing == name) {
            _pendingAdding.Add(new EventActionInfo(name, action));
        }
        else {
            if (!_events.ContainsKey(name)) {
                _events.Add(name, new List<Action>());
            }
            _events[name].Add(action);
        }
    }

    public static void RemoveListener (string name, Action action) {
        if (_currentUsing == name) {
            _pendingremoving.Add(new EventActionInfo(name, action));
        }
        else {
            if (_events.ContainsKey(name)) {
                _events[name].Remove(action);
            }
        }
    }


    static void ProcessPending () {
        var pendingAddingCopy = new List<EventActionInfo>(_pendingAdding);
        var pendingRemovingCopy = new List<EventActionInfo>(_pendingremoving);

        _pendingAdding.Clear();
        _pendingremoving.Clear();

        pendingAddingCopy.ForEach(info => AddListener(info.eventName, info.action));
        pendingRemovingCopy.ForEach(info => RemoveListener(info.eventName, info.action));
    }



    public class EventActionInfo {
        public string eventName;
        public Action action;

        public EventActionInfo (string name, Action a) {
            eventName = name;
            action = a;
        }
    }

}