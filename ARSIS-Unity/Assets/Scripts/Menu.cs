using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.Events;
using UnityEngine.UI;
using Unity.XR.CoreUtils;

[ExecuteInEditMode]
public class Menu : MonoBehaviour
{
    [SerializeField]
    public Camera camera;
    [SerializeField]
    Material backplateMaterial;
    public bool performBuild;
    public bool performDestroy;

    GameObject vertical;
    GameObject horizontal;
    IWindow notifications;
    IWindow pinned;
    IWindow menu;
    static float width = 200f;
    static float height = 300f;
    static float uiScale = 0.001f;
    static int padding = 10;
    static int spacing = 5;
    TextAnchor verticalAlign = TextAnchor.UpperLeft;
    static float notifPlateHeight = 60f;
    static float horizontalX = width - (padding * 2f);
    static float horizontalY = height - (padding * 2f) - notifPlateHeight - spacing;

    public void SetActive(bool active)
    {
        if (vertical == null) return;
        vertical.SetActive(active);
    }

    void CreateBackplate()
    {
        notifications = new Backplate("Notifications");
        notifications.SetSize(new Vector2(horizontalX, notifPlateHeight));
        notifications.SetParent(vertical);
        notifications.SetMaterial(backplateMaterial);
        notifications.Build();
        GameObject child = vertical.GetNamedChild("Notifications");
        child.transform.SetAsFirstSibling();

        float plateWidth = (horizontalX - spacing) / 2f;
        GridLayoutGroup.Constraint constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        int numColumns = 2;
        int numRows = 3;
        float buttonWidth = (plateWidth - (spacing * (numColumns + 1))) / numColumns;
        float buttonHeight = (horizontalY - (spacing * (numRows + 1))) / numRows;

        pinned = new Backplate("Pinned");
        pinned.SetSize(new Vector2(plateWidth, horizontalY));
        pinned.SetParent(horizontal);
        pinned.SetMaterial(backplateMaterial);
        pinned.Build();

        menu = new Backplate("Menu");
        menu.SetSize(new Vector2(plateWidth, horizontalY));
        menu.SetParent(horizontal);
        menu.SetConstraint(constraint, numColumns);
        menu.SetPadding(new RectOffset(spacing, spacing, spacing, spacing));
        menu.SetSpacing(new Vector2(spacing, spacing));
        menu.SetCellSize(new Vector2(buttonWidth, buttonHeight));
        menu.SetMaterial(backplateMaterial);
        menu.AddElement(new Button("Procedures").Build());
        menu.AddElement(new Button("Biometrics").Build());
        menu.AddElement(new Button("Spectrometry").Build());
        menu.AddElement(new Button("Drop Breadcrumb").Build());
        menu.AddElement(new Button("Navigation").Build());
        menu.AddElement(new Button("Settings").Build());
        menu.Build();
    }

    void CreateLayouts()
    {
        vertical = new GameObject("VerticalLayout");
        vertical.transform.SetParent(gameObject.transform, false);
        RectTransform vtransform = vertical.AddComponent<RectTransform>();
        VerticalLayoutGroup vlayout = vertical.AddComponent<VerticalLayoutGroup>();
        Canvas vcanvas = vertical.AddComponent<Canvas>();
        vcanvas.renderMode = RenderMode.WorldSpace;
        vcanvas.worldCamera = camera;
        vtransform.sizeDelta = new Vector2(width, height); ;
        vtransform.localScale = Vector3.one * uiScale;
        vlayout.childControlWidth = false;
        vlayout.childControlHeight = false;
        vlayout.childForceExpandWidth = false;
        vlayout.childForceExpandHeight = false;
        vlayout.padding = new RectOffset(padding, padding, padding, padding);
        vlayout.spacing = spacing;
        vlayout.childAlignment = verticalAlign;

        horizontal = new GameObject("HorizontalLayout");
        horizontal.transform.SetParent(vertical.transform, false);
        RectTransform htransform = horizontal.AddComponent<RectTransform>();
        HorizontalLayoutGroup hlayout = horizontal.AddComponent<HorizontalLayoutGroup>();
        htransform.sizeDelta = new Vector2(horizontalX, horizontalY);
        hlayout.spacing = spacing;
        hlayout.childControlWidth = false;
        hlayout.childControlHeight = false;
        hlayout.childForceExpandWidth = false;
        hlayout.childForceExpandHeight = false;
    }

    void Build()
    {
        CreateLayouts();
        CreateBackplate();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!Application.isPlaying) return;
        if (vertical) Destroy(vertical);
        Build();
    }

    void OnApplicationQuit()
    {
        Destroy(vertical);
        Debug.Log("Destroyed menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying) return;
        if (performBuild)
        {
            Build();
        }
        if (performDestroy)
        {
            DestroyImmediate(vertical);
        }

        performBuild = false;
        performDestroy = false;
    }
}
