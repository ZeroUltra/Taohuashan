using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPlus : Button {
    public AudioClip clickClip;

    private AudioSource audioSource;
    protected override void Start()
    {
        base.Start();
        if (clickClip != null)
        {
            audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
            audioSource.clip = clickClip;
            audioSource.playOnAwake = false;
        }
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        audioSource.Play();
    }

}
