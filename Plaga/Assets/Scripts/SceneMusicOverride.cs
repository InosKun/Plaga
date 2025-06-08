using UnityEngine;

public class SceneMusicOverride : MonoBehaviour
{
    public AudioClip sceneTrack; // Assign new music for this scene

    void Start()
    {
        if (MusicManager.instance != null)
        {
            MusicManager.instance.StopMusic();

            if (sceneTrack != null)
                MusicManager.instance.PlayMusic(sceneTrack);
        }
    }
}
