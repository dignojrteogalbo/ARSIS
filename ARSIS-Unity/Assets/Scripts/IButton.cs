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
    internal interface IButton
    {
        void OnPress();
        void SetText(string text);
        GameObject Build();
        void Destroy();
    }
}
