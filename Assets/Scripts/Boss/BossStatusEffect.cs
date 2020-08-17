using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatusEffect : MonoBehaviour {
    public AudioSource entryAudio;
    public AudioSource exitAudio;
    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Enable() {
        gameObject.SetActive(true);
        if (entryAudio != null) {
            entryAudio.Play();
        }
    }

    public void Disable() {
        StartCoroutine(DisableRoutine());
    }

    IEnumerator DisableRoutine() {
        if (exitAudio != null) {
            exitAudio.Play();
            yield return new WaitForSeconds(exitAudio.clip.length);
        }
        gameObject.SetActive(false);
    }
}
