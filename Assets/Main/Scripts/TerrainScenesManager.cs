using UnityEngine;
using UnityEngine.SceneManagement;
using DoubleHeat.Common;

public class TerrainScenesManager : SingletonMonoBehaviour<TerrainScenesManager> {
    

    public string currentTerrainSceneName;


    protected override void Awake () {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }



    public void LoadScene (string name) {
        SceneManager.LoadScene(name);
    }


    [ContextMenu("Reload Terrain Scene")]
    public void ReloadTerrainScene () {
        SceneManager.LoadScene(currentTerrainSceneName);
    }


}