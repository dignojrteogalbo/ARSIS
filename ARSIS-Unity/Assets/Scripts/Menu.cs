using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.Events;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Menu : MonoBehaviour
{
    [SerializeField]
    public Camera camera;
    public bool performBuild;
    public bool performDestroy;
    public bool addButtons;
    public bool removeButtons;

    Backplate canvas;
    float width = 200f;
    float height = 300f;
    float cellX = 85f;
    float cellY = 85f;
    GridLayoutGroup.Constraint constraint = GridLayoutGroup.Constraint.FixedColumnCount;
    int constraintCount = 2;

    public void SetActive(bool active)
    {
        if (canvas == null) return;
        canvas.SetEnabled(active);
    }

    void CreateBackplate()
    {
        canvas = new Backplate(gameObject, camera);
        canvas.SetName("Canvas");
        canvas.SetSize(new Vector2(width, height));
        canvas.SetCellSize(new Vector2(cellX, cellY));
        canvas.SetConstraint(constraint, constraintCount);
        canvas.Build();
    }

    void AddButtons()
    {
        IButton biometrics = new Biometrics();
        canvas.AddElement(biometrics.Build());
    }

    void RemoveButtons()
    {
        canvas.RemoveAllElements();
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateBackplate();
        AddButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if (performBuild)
        {
            CreateBackplate();
        }
        if (performDestroy)
        {
            canvas.Destroy();
        }
        if (addButtons)
        {
            AddButtons();
        }
        if (removeButtons)
        {
            RemoveButtons();
        }

        performBuild = false;
        performDestroy = false;
        addButtons = false;
        removeButtons = false;
    }
}
