using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalEventManager {
    
    static Dictionary<string, List<Action>> _events = new Dictionary<string, List<Action>>();

    public static void TriggerEvent (string name) {
        if (_events.ContainsKey(name)) {
            _events[name].ForEach(action => action?.Invoke());
        }
    }

    public static void AddListener (string name, Action action) {
        if (!_events.ContainsKey(name)) {
            _events.Add(name, new List<Action>());
        }
        _events[name].Add(action);
    }

    public static void RemoveListener (string name, Action action) {
        if (_events.ContainsKey(name)) {
            _events[name].Remove(action);
        }
    }

}