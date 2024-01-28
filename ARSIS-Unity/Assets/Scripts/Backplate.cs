using Microsoft.MixedReality.GraphicsTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using Unity.XR.CoreUtils;

public class Backplate : IWindow
{
    GameObject parent;
    Camera camera;
    GameObject gameObject;
    Vector2 size = new(200f, 200f);
    Vector2 anchorMax = new(0f, 1f);
    Vector2 anchorMin = new(0f, 1f);
    string name = "Canvas";
    Vector2 pivot = new(0.5f, 0.5f);
    Vector3 scale = new(0.001f, 0.001f, 0.001f);
    Vector2 cellSize = new(180f, 180f);
    GridLayoutGroup.Constraint constraint = GridLayoutGroup.Constraint.FixedColumnCount;
    int constraintCount = 2;
    RectOffset padding = new(10, 10, 10, 10);
    Vector2 spacing = new(10f, 10f);

    public Backplate(GameObject parent, Camera camera)
    {
        this.camera = camera;
        this.parent = parent;
    }

    public void AddElement(GameObject element)
    {
        if (!gameObject) return;
        element.transform.SetParent(gameObject.transform, false);
    }

    public void RemoveAllElements()
    {
        if (!gameObject) return;
        foreach (Transform element in gameObject.transform)
        {
            Object.DestroyImmediate(element.gameObject);
        }
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetEnabled(bool enabled)
    {
        if (gameObject == null) return;
        gameObject.SetActive(enabled);
    }

    public void SetSize(Vector2 size) { this.size = size; }
    public void SetAnchorMax(Vector2 anchorMax) { this.anchorMax = anchorMax; }
    public void SetAnchorMin(Vector2 anchorMin) { this.anchorMin = anchorMin; }
    public void SetPivot(Vector2 pivot) { this.pivot = pivot; }
    public void SetScale(Vector3 scale) { this.scale=scale; }
    public void SetCellSize(Vector2 cellSize) { this.cellSize = cellSize; }
    public void SetConstraint(GridLayoutGroup.Constraint constraint, int constraintCount)
    {
        this.constraint = constraint;
        this.constraintCount = constraintCount;
    }
    public void SetPadding(RectOffset padding) { this.padding = padding; }
    public void SetSpacing(Vector2 spacing) { this.spacing = spacing; }
    public Transform GetTransform() { return gameObject.transform; }

    void AddTransform()
    {
        gameObject.transform.parent = parent.transform;
        RectTransform transform = gameObject.AddComponent<RectTransform>();
        transform.anchorMax = anchorMax;
        transform.anchorMin = anchorMin;
        transform.pivot = pivot;
        transform.localScale = scale;
        transform.sizeDelta = size;
    }

    void AddCanvas()
    {
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = camera;
        gameObject.AddComponent<CanvasScaler>();
        CanvasRenderer renderer = gameObject.AddComponent<CanvasRenderer>();
        renderer.cullTransparentMesh = true;
        gameObject.AddComponent<CanvasElementBeveledRect>();
    }

    void AddGrid()
    {
        GridLayoutGroup grid = gameObject.AddComponent<GridLayoutGroup>();
        grid.cellSize = cellSize;
        grid.constraint = constraint;
        grid.constraintCount = constraintCount;
        grid.padding = padding;
        grid.spacing = spacing;
    }

    public void Build()
    {
        Debug.Log("Backplate: Building backplate");
        gameObject = new GameObject(name);
        AddTransform();
        AddCanvas();
        AddGrid();
    }

    public void Destroy()
    {
        Debug.Log("Backplate: Destroying backplate");
        if (!gameObject) return;
        Object.DestroyImmediate(gameObject);
    }
}
