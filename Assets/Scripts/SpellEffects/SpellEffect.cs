using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellEffect : MonoBehaviour {
    public AudioSource entryAudio;
    public AudioSource exitAudio;
    protected Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public virtual IEnumerator Activate() {
        GetComponent<Animator>().SetTrigger("Active");
        entryAudio?.Play();
        yield break;
    }

    public virtual void Deactivate() {
        exitAudio?.Play();
        GetComponent<Animator>().SetTrigger("Inactive");
    }

    public void OnDestroyAnimationEnd() {
        gameObject.SetActive(false);
    }
}
