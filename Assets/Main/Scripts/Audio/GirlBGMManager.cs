using UnityEngine;

public class GirlBGMManager : MonoBehaviour {
    
    public AudioSource bgmSrc;

    void OnEnable () {
        SelectionsManager.GirlSelected += OnGirlSelected;
        SelectionsManager.GirlLost += OnGirlLost;
    }

    void OnDisable () {
        SelectionsManager.GirlSelected -= OnGirlSelected;
        SelectionsManager.GirlLost -= OnGirlLost;
        
    }


    void OnGirlSelected () {
        bgmSrc.Play();
    }

    void OnGirlLost () {
        bgmSrc.Stop();
    }
}