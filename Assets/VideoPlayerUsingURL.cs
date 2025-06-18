using UnityEngine;

public class VideoPlayerUsingURL : MonoBehaviour
{
    [SerializeField]
    private string videoFileName;
    [SerializeField]
    private UnityEngine.Video.VideoPlayer videoPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        PlayVideo();
    }

    private void PlayVideo()
    {
        string url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        videoPlayer.url = url;
        videoPlayer.Play();
    }
}
