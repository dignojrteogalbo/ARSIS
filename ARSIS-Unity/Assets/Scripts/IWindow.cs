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
        void SetSize(Vector2 size);
        void SetName(string name);
        void AddElement(GameObject element);
        void RemoveAllElements();
        void SetAnchorMax(Vector2 anchorMax);
        void SetAnchorMin(Vector2 anchorMin);
        void SetPivot(Vector2 pivot);
        void SetScale(Vector3 scale);
        void SetCellSize(Vector2 cellSize);
        void SetConstraint(GridLayoutGroup.Constraint constraint, int count);
        void SetPadding(RectOffset padding);
        void SetSpacing(Vector2 spacing);
        Transform GetTransform();
        void Build();
        void Destroy();
    }
}
