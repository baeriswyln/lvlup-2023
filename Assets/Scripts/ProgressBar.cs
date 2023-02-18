using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public Gradient barGradient;
    public Transform barTransform;
    public SpriteRenderer bar;

    private void Start()
    {
        bar.color = barGradient.Evaluate(1);
    }

    public void SetProgress(float progress, float on)
    {
        float percent = progress / on;
        Debug.Log(percent);
        barTransform.localScale = new Vector3(percent, transform.localScale.y, transform.localScale.z);
        bar.color = barGradient.Evaluate(percent);
    }
}
