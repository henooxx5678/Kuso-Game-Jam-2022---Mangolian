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
        GlobalEventManager.AddListener("got driver license", OnGotDriverLicense);
        GlobalEventManager.AddListener("happy to win", OnWin);
        GlobalEventManager.AddListener("unhappy to lose", OnLose);
    }

    void OnDisable () {
        GlobalEventManager.RemoveListener("too far to pick", OnTooFarToPick);
        GlobalEventManager.RemoveListener("try to pick multiple girls", OnTryToPickMultipleGirl);
        GlobalEventManager.RemoveListener("got driver license", OnGotDriverLicense);
        GlobalEventManager.RemoveListener("happy to win", OnWin);
        GlobalEventManager.RemoveListener("unhappy to lose", OnLose);
    }


    [ContextMenu("OnTooFarToPick")]
    void OnTooFarToPick () {
        Show(messages.tooFarToPick);
    }

    [ContextMenu("OnTryToPickMultipleGirl")]
    void OnTryToPickMultipleGirl () {
        Show(messages.tryToPickMultipleGirl);
    }

    [ContextMenu("OnGotDriverLicense")]
    void OnGotDriverLicense () {
        Show(messages.gotDriverLicense);
    }

    void OnWin () {
        Show(messages.happyToWin);
    }

    void OnLose() {
        Show(messages.unhappyToLose);
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
        
        [TextArea]
        public string gotDriverLicense;

        [TextArea]
        public string happyToWin;

        [TextArea]
        public string unhappyToLose;
    }

}