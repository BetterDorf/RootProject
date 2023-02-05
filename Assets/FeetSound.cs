using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetSound : MonoBehaviour
{
    [SerializeField] private List<AudioClip> bootClips;
    [SerializeField] private AudioClip landClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip specialClip;
    private int curBootClip = 0;

    public void PlayBootSound()
    {
        if (bootClips.Count == 0)
        {
            return;
        }

        GetComponent<AudioSource>().PlayOneShot(bootClips[curBootClip]);

        curBootClip = (curBootClip + 1) % bootClips.Count;
    }

    public void PlayJumpClip()
    {
        GetComponent<AudioSource>().PlayOneShot(jumpClip);
    }

    public void PlayLandClip()
    {
        GetComponent<AudioSource>().PlayOneShot(landClip);
    }

    public void PlaySpecialClip()
    {
        GetComponent<AudioSource>().PlayOneShot(specialClip);
    }
}
