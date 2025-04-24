
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class WebMVideoPlayer : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "anim_alpha.webm");
        videoPlayer.isLooping = true;
        videoPlayer.renderMode = VideoRenderMode.APIOnly;

        videoPlayer.prepareCompleted += Prepared;
        videoPlayer.Prepare();
    }

    void Prepared(VideoPlayer vp)
    {
        rawImage.texture = vp.texture;
        vp.Play();
    }
}
