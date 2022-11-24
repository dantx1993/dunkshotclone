using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameMenuAnimation : MonoBehaviour
{
    public Action uiAnimation;
    public Action stopUiAnimation;

    private void OnEnable()
    {
        StartCoroutine(StartAnimation());
    }

    private void OnDisable()
    {
        if (stopUiAnimation != null)
            stopUiAnimation.Invoke();
    }

    private IEnumerator StartAnimation()
    {
        yield return new WaitWhile(() => uiAnimation == null);
        uiAnimation.Invoke();
    }
}
