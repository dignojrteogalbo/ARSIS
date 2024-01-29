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
    public GameObject gameObject;
    string name = "Plate";
    IList<GameObject> elements = new List<GameObject>();
    Vector2 size = new(100f, 100f);
    Vector2 cellSize = new(50f, 50f);
    GridLayoutGroup.Constraint constraint = GridLayoutGroup.Constraint.Flexible;
    int constraintCount = 0;
    Vector2 spacing = new(5f, 5f);
    RectOffset padding = new(5, 5, 5, 5);
    Material material;

    public Backplate(string name) { this.name = name; }

    public void AddElement(GameObject element) { elements.Add(element); }
    public void SetName(string name) { this.name = name; }
    public void SetEnabled(bool enabled) { if (gameObject) gameObject.SetActive(enabled); }
    public void SetMaterial(Material material) { this.material = material; }
    public void SetSize(Vector2 size) { this.size = size; }
    public void SetParent(GameObject parent) { this.parent = parent; }
    public void SetCellSize(Vector2 cellSize) { this.cellSize = cellSize; }
    public void SetConstraint(GridLayoutGroup.Constraint constraint, int constraintCount) { 
        this.constraint = constraint;
        this.constraintCount = constraintCount;
    }
    public void SetSpacing(Vector2 spacing) { this.spacing = spacing; }
    public void SetPadding(RectOffset padding) {  this.padding = padding; }

    void AddTransform()
    {
        if (parent) gameObject.transform.SetParent(parent.transform, false);
        RectTransform transform = gameObject.AddComponent<RectTransform>();
        transform.sizeDelta = size;
    }

    void AddCanvas()
    {
        CanvasRenderer renderer = gameObject.AddComponent<CanvasRenderer>();
        renderer.cullTransparentMesh = true;
        CanvasElementRoundedRect plate = gameObject.AddComponent<CanvasElementRoundedRect>();
        if (material) plate.material = material;
    }

    void AddLayout()
    {
        GridLayoutGroup grid = gameObject.AddComponent<GridLayoutGroup>();
        grid.cellSize = cellSize;
        grid.constraint = constraint;
        grid.constraintCount = constraintCount;
        grid.padding = padding;
        grid.spacing = spacing;
    }

    void AddElements()
    {
        foreach (GameObject element in elements)
        {
            element.transform.SetParent(gameObject.transform, false);
        }
    }

    public void Build()
    {
        Debug.Log("Backplate: Building backplate");
        gameObject = new GameObject(name);
        AddTransform();
        AddCanvas();
        AddLayout();
        AddElements();
    }

    public void Destroy()
    {
        Debug.Log("Backplate: Destroying backplate");
        if (!gameObject) return;
        Object.DestroyImmediate(gameObject);
    }
}
