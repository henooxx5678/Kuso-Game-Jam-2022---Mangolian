using UnityEngine;

public class RestartAtFreeTrackAdvancer : StageAdvacner {
    
    protected override void OnPlayerAdvance () {
        TerrainScenesManager.current.RestartGame();
    }

}