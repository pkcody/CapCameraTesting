using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSoundEffect : MonoBehaviour
{
    public AudioSource _as;

    private void OnTriggerEnter(Collider cc)
    {
        _as.PlayOneShot(_as.clip, 0.5f);
    }
}
