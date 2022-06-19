using UnityEngine;
using UnityEngine.UI;

public class TrackHUDManager : MonoBehaviour {
    
    public Text speedText;
    public Text happyText;
    public HappyValueHandler happyValueHandler;


    string _speedInitText;
    string _happyInitText;


    void Awake () {
        _speedInitText = speedText.text;
        _happyInitText = happyText.text;
    }

    void Update () {
        if (PlayerCar.current) {
            speedText.text = _speedInitText + (int) PlayerCar.current.carController.speed;
        }

        happyText.text = GetHappyPrefix() + (int) happyValueHandler.happyValue;
    }


    string GetHappyPrefix () {
        string girlName = SelectionsManager.GirlName;
        return _happyInitText.Replace("<name>", girlName);
    }

}