using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class InitialVideoManager : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject gameObj;
    [SerializeField] GameObject videoPlay;
    [SerializeField] GameObject missionHelper;
    [SerializeField] GameObject skipButton;
    [SerializeField] GameObject environmentSound;
    bool stopVideo = false;
    private void Start()
    {
        StartCoroutine(CheckVideo());
        environmentSound.SetActive(false);
    }
    // Start is called before the first frame update
    private void LateUpdate()
    {
        if ((stopVideo && !videoPlayer.isPlaying) || skipVideo)
        {
            //Video has finshed playing!
            gameObj.SetActive(true);
            Destroy(videoPlay);
            missionHelper.SetActive(true);
            skipButton.SetActive(false);
            environmentSound.SetActive(true);

            this.gameObject.SetActive(false);
        }
       
    }
    bool skipVideo = false;
    public void SkipVideo()
    {
        skipVideo = true;
    }
   IEnumerator CheckVideo()
    {
        yield return new WaitForSeconds(2f);
        stopVideo = true;
    }
}
