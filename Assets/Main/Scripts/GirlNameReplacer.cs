using UnityEngine;
using UnityEngine.UI;

public class GirlNameReplacer : MonoBehaviour {
    
    public Text text;


    void Start () {
        text.text = text.text.Replace("<name>", SelectionsManager.GirlName);
    }


}