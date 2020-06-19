using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTracking : MonoBehaviour {
    Transform sunrise;
    Transform sunset;
    public bool doneMoving = false;
    public float journeyTime = 1.0f;
    private float startTime;

    private void Start() {
        startTime = Time.time;
        sunrise = GetComponentInParent<SummonController>().transform;
        sunset = FindObjectOfType<Boss>().GetTransformToHit();
    }

    private void Update() {
        // center of arc
        Vector3 center = (sunrise.position + sunset.position) * 0.5f;

        // make arc vertical
        center -= new Vector3(0, 3f, 0);

        // interpolate over the arc relative to center
        Vector3 riseRelCenter = sunrise.position - center;
        Vector3 setRelCenter = sunset.position - center;

        float fracComplete = (Time.time - startTime) / journeyTime;

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;

        if (Vector3.Distance(transform.position, sunset.position) <= 0.1f) {
            doneMoving = true;
        }
    }
}
