using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffect : MonoBehaviour {
    public AudioSource entryAudio;
    public AudioSource exitAudio;
    protected Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public virtual IEnumerator Activate() {
        gameObject.SetActive(true);
        GetComponent<Animator>().SetTrigger("Active");
        if (entryAudio != null) {
            Debug.Log("Play audio");
            entryAudio.Play();
        }
        //entryAudio?.Play();
        yield break;
    }

    public virtual void Deactivate() {
        StartCoroutine(DeactivateRoutine());
    }

    public void OnDestroyAnimationEnd() {
        gameObject.SetActive(false);
    }

    public bool IsDone() {
        return gameObject.activeSelf ? false : true;
    }

    IEnumerator DeactivateRoutine() {
        if (exitAudio != null) {
            exitAudio.Play();
            yield return new WaitForSeconds(exitAudio.clip.length);
        }
        gameObject.SetActive(false);
        GetComponent<Animator>().SetTrigger("Inactive");
    }
}
