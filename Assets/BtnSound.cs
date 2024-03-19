using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSound : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip clickFk;

    // Update is called once per frame
    public void ClickSound()
    {
        myFx.PlayOneShot(clickFk);
    }
}
