using UnityEngine;

public class EndWordsInitiator : MonoBehaviour {
    
    public GameObject withGirl;
    public GameObject noGirl;

    void Start () {
        bool hasGirl = SelectionsManager.girlNumber != -1;
        withGirl.SetActive(hasGirl);
        noGirl.SetActive(!hasGirl);
    }

}