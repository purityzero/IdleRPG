
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySound(string name, AudioSource source)
    {
        if (audioClips.TryGetValue(name, out AudioClip clip))
            source.PlayOneShot(clip);
    }
}
