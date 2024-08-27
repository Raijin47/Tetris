using UnityEngine;

public enum ClipType
{
    click,
    shot,
    recharge,
    mummyDeath,
    bonus,
    upgrade,
    gameOver,
    tetrominoLine,
}

public class Audio : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioClip[] clips;

    public static Audio Instance;

    private void Awake()
    {
        Instance = this;
    }

    public static void Play(ClipType type)
    {
        Play((int)type); 
    }

    public static void Play(int clip)
    {
        if (Instance == null)
            return;

        AudioSource source = Instance.audioSources[0];

        if (Instance.clips.Length > 0 && clip < Instance.clips.Length)
            source.PlayOneShot(Instance.clips[clip]);
    }

    public void ActiveAudio(bool activ)
    {
        foreach (AudioSource source in audioSources)
        {
            source.mute = !activ;
        }

        Play(0);
    }

    public void ActiveAudioMusic(bool activ)
    {
        audioSources[1].mute = !activ;

        Play(0);
    }

    public void ActiveAudioSound(bool activ)
    {
        audioSources[0].mute = !activ;

        Play(0);
    }
}
