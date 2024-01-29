using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    internal interface IWindow
    {
        void SetEnabled(bool enabled);
        void SetName(string name);
        void AddElement(GameObject element);
        void SetMaterial(Material material);
        void SetSize(Vector2 rectTransform);
        void SetParent(GameObject parent);
        void SetCellSize(Vector2 cellSize);
        void SetConstraint(GridLayoutGroup.Constraint constraint, int constraintCount);
        void SetSpacing(Vector2 spacing);
        void SetPadding(RectOffset padding);
        void Build();
        void Destroy();
    }
}
