using UnityEngine;
using UnityEngine.SceneManagement;
using DoubleHeat.Common;

public class TerrainScenesManager : SingletonMonoBehaviour<TerrainScenesManager> {
    

    public string currentTerrainSceneName;


    protected override void Awake () {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }


    void OnEnable () {
        GlobalEventManager.AddListener("terrain end", OnTerrainEnd);
    }

    void OnDisable () {
        GlobalEventManager.RemoveListener("terrain end", OnTerrainEnd);
    }


    void OnTerrainEnd () {
        switch (currentTerrainSceneName) {
            case "Cars Showing": 
                LoadScene("Terrain Farm");
                break;
            case "Terrain Farm":
                LoadScene("Terrain Track");
                break;
            case "Terrain Track":
                LoadScene("Terrain Desert");
                break;
        }
    }


    public void LoadScene (string name) {
        SceneManager.LoadScene(name);
        currentTerrainSceneName = name;
    }


    [ContextMenu("Reload Terrain Scene")]
    public void ReloadTerrainScene () {
        SceneManager.LoadScene(currentTerrainSceneName);
    }


}