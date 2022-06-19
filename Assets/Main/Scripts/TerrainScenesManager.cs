using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using DoubleHeat.Common;

public class TerrainScenesManager : SingletonMonoBehaviour<TerrainScenesManager> {
    
    public bool loadInitSceneOnStart;
    public string initSceneName = "Cars Showing";

    public string currentTerrainSceneName;


    protected override void Awake () {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }


    void OnEnable () {
        GlobalEventManager.AddListener("terrain end", OnTerrainEnd);
        GlobalEventManager.AddListener("happy to win", OnWin);
        GlobalEventManager.AddListener("unhappy to lose", OnLose);
    }

    void OnDisable () {
        GlobalEventManager.RemoveListener("terrain end", OnTerrainEnd);
        GlobalEventManager.RemoveListener("happy to win", OnWin);
        GlobalEventManager.RemoveListener("unhappy to lose", OnLose);
    }

    void Start () {
        if (loadInitSceneOnStart) {
            LoadScene(initSceneName);
        }
    }

    void OnTerrainEnd () {
        print("terrain end: " + currentTerrainSceneName);
        switch (currentTerrainSceneName) {
            case "Cars Showing": 
                LoadScene("Terrain Farm");
                break;
            case "Terrain Farm":
                GlobalEventManager.TriggerEvent("exit farm");

                string nextSceneName = SelectionsManager.girlNumber == -1 ? "Terrain Desert" : "Terrain Track";

                DOTween.Sequence()
                    .AppendInterval(3f)
                    .AppendCallback(() => LoadScene(nextSceneName));
                break;
            case "Terrain Track":
                LoadScene("Terrain Desert");
                break;
            case "Terrain Desert":
                LoadScene("Game Complete");
                break;
        }
    }

    void OnWin () {
        DOTween.Sequence()
            .AppendInterval(3f)
            .AppendCallback(GoToEndScene);
    }

    void OnLose () {
        DOTween.Sequence()
            .AppendInterval(3f)
            .AppendCallback(RestartGame);
    }

    void GoToEndScene () {
        LoadScene("Terrain Desert");
    }


    public void LoadScene (string name) {
        SceneManager.LoadScene(name);
        currentTerrainSceneName = name;
    }


    [ContextMenu("Reload Terrain Scene")]
    public void ReloadTerrainScene () {
        SceneManager.LoadScene(currentTerrainSceneName);
    }


    [ContextMenu("Restart Game")]
    public void RestartGame () {
        SelectionsManager.Reset();
        LoadScene(initSceneName);
    }

}