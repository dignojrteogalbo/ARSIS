using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.Events;
using MixedReality.Toolkit.UX;
using UnityEngine.UI;
using Microsoft.MixedReality.GraphicsTools;
using TMPro;

public class Button : IButton
{
    public GameObject gameObject;
    float offsetZ = 5f;
    float depth = 25f;
    string name = "Button";
    string text;
    int fontSize = 8;

    public Button(string name)
    {
        this.name = name;
    }

    public void OnPress()
    {
        throw new System.NotImplementedException();
    }

    public void SetText(string text)
    {
        this.text = text;
    }

    void AddFrontplate(GameObject gameObject)
    {
        RectTransform transform = gameObject.GetComponent<RectTransform>();
        GameObject frontplate = new GameObject("Frontplate");
        frontplate.transform.SetParent(gameObject.transform, false);
        RectTransform rectTransform = frontplate.AddComponent<RectTransform>();
        frontplate.AddComponent<CanvasRenderer>();
        TextMeshProUGUI content = frontplate.AddComponent<TextMeshProUGUI>();
        rectTransform.transform.position = Vector3.back * offsetZ;
        rectTransform.sizeDelta = transform.sizeDelta;
        content.text = text ?? name;
        content.fontSize = fontSize;
        content.alignment = TextAlignmentOptions.Center;
    }

    public GameObject Build()
    {
        gameObject = new GameObject(name);
        gameObject.AddComponent<RectTransform>();
        gameObject.AddComponent<PressableButton>();
        gameObject.AddComponent<UGUIInputAdapter>();
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        RectTransformColliderFitter fitter = gameObject.AddComponent<RectTransformColliderFitter>();
        fitter.ThisCollider = collider;
        collider.center = new Vector3(collider.center.x, collider.center.y, offsetZ);
        collider.size = new Vector3(collider.size.x, collider.size.y, depth);
        Animator animator = gameObject.AddComponent<Animator>();
        animator.enabled = false;
        gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<StateVisualizer>();
        gameObject.AddComponent<InteractablePulse>();
        AddFrontplate(gameObject);
        return gameObject;
    }

    public void Destroy()
    {
        if (gameObject) Object.Destroy(gameObject);
    }
}
