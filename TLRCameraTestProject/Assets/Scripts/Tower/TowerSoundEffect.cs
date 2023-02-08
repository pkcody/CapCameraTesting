using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSoundEffect : MonoBehaviour
{
    public AudioSource _as;
    public List<AudioClip> audioClips;
    public bool ImTower1;


    private void OnTriggerEnter(Collider cc)
    {
        if(ImTower1)
            _as.PlayOneShot(_as.clip, 0.5f);
    }

    public void PlayTower2Sound()
    {

        _as.clip = audioClips.Find(clipName => clipName.name == "MS_halfSound");
        _as.PlayOneShot(_as.clip, 0.5f);

    }
    public void PlayTower3Sound()
    {
        _as.clip = audioClips.Find(clipName => clipName.name == "MS_allVoice");
        _as.PlayOneShot(_as.clip, 0.5f);
    }
}
