using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PopupMessageManager : MonoBehaviour {
    
    public Text messageText;
    public float defaultShowingDuration;
    public Messages messages;


    void Awake () {
        DontDestroyOnLoad(this.gameObject);
        messageText.text = string.Empty;
    }




    void OnEnable () {
        GlobalEventManager.AddListener("too far to pick", OnTooFarToPick);
        GlobalEventManager.AddListener("try to pick multiple girls", OnTryToPickMultipleGirl);
    }

    void OnDisable () {
        GlobalEventManager.RemoveListener("too far to pick", OnTooFarToPick);
        GlobalEventManager.RemoveListener("try to pick multiple girls", OnTryToPickMultipleGirl);
    }


    [ContextMenu("OnTooFarToPick")]
    void OnTooFarToPick () {
        Show(messages.tooFarToPick);
    }

    [ContextMenu("OnTryToPickMultipleGirl")]
    void OnTryToPickMultipleGirl () {
        Show(messages.tryToPickMultipleGirl);
    }






    public void Show (string text, float overrideDuration = -1f) {
        StopAllCoroutines();
        StartCoroutine(ShowingMessage(text, overrideDuration == -1f ? defaultShowingDuration : overrideDuration));
    }

    
    IEnumerator ShowingMessage (string text, float duration) {
        messageText.text = text;
        yield return new WaitForSeconds(duration);
        messageText.text = string.Empty;
    }



    [Serializable]
    public class Messages {
        public string tooFarToPick;
        public string tryToPickMultipleGirl;
    }

}