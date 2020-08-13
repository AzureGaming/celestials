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
        if (entryAudio != null) {
            entryAudio.Play();
        }
        //entryAudio?.Play();
        yield break;
    }

    public virtual void Deactivate() {
        if (exitAudio != null) {
            exitAudio.Play();
        }
        GetComponent<Animator>().SetTrigger("Inactive");
    }

    public void OnDestroyAnimationEnd() {
        gameObject.SetActive(false);
    }
}
