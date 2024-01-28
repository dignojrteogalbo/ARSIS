using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.Events;
using MixedReality.Toolkit.UX;
using UnityEngine.UI;

public class Biometrics : IButton
{
    GameObject gameObject;
    GameObject backplate;

    public void OnPress()
    {
        throw new System.NotImplementedException();
    }

    public void SetText(string text)
    {
        throw new System.NotImplementedException();
    }

    public GameObject Build()
    {
        Destroy();
        gameObject = new GameObject("Biometrics");
        gameObject.AddComponent<PressableButton>();
        gameObject.AddComponent<UGUIInputAdapter>();
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        RectTransformColliderFitter fitter = gameObject.AddComponent<RectTransformColliderFitter>();
        fitter.ThisCollider = collider;
        gameObject.AddComponent<Animator>();
        gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<StateVisualizer>();
        gameObject.AddComponent<InteractablePulse>();
        gameObject.AddComponent<LayoutElement>();
        return gameObject;
    }

    public void Destroy()
    {
        if (gameObject) Object.Destroy(gameObject);
    }
}
