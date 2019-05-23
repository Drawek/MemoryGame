using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MovieOver : MonoBehaviour
{
    public string sceneName = "Level 1";
    private VideoPlayer video;
    // Start is called before the first frame update
    void Start()
    {
        video = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
            SceneManager.LoadScene(sceneName);
        if ((float)video.frame > video.frameCount - 3)
            SceneManager.LoadScene(sceneName);
    }
}
