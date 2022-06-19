using System;
using UnityEngine;

public static class SelectionsManager {
    
    public static GameObject carPrefab = null;

    static int _gNum = -1;  // 0 for tomato, 1 for cabbage
    public static int girlNumber {
        get => _gNum;
        set {
            if (_gNum != value) {
                _gNum = value;
                if (value == -1) {
                    GirlLost?.Invoke();
                }
                else {
                    GirlSelected?.Invoke();
                }
            }
        }
    }


    public static string GirlName {
        get {
            switch (girlNumber) {
                case 0:
                    return "阿茄";
                case 1:
                    return "阿菜";
                default:
                    return "NONE";
            }
        }
    }


    public static event Action GirlSelected;
    public static event Action GirlLost;


    public static void Reset () {
        carPrefab = null;
        girlNumber = -1;
    }

}