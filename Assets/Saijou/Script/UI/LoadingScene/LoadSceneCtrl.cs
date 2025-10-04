using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LoadSceneCtrl : MonoBehaviour
{
    public PlayableDirector timeline;
    void Start()
    {
        timeline.stopped += OnTimelineFinished;
    }

    void OnTimelineFinished(PlayableDirector pd)
    {
        //ï€ë∂Ç≥ÇÍÇΩéüÇÃÉVÅ[ÉìñºÇ…ëJà⁄
        SceneManager.LoadScene(StageLoader.NextStageName);
    }
    public void LoadStage(string stageName)
    {
        StageLoader.NextStageName = stageName;
        SceneManager.LoadScene("LoadingScene");
    }
    private void OnDestroy()
    {
        timeline.stopped -= OnTimelineFinished;
    }
}
