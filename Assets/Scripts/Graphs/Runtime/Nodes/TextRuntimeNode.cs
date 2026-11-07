using System;
using UnityEngine.Localization;
using UnityEngine;

[Serializable]
public class TextRuntimeNode : GenericRuntimeNode
{
    public LocalizedString localizedText;
    public string text;
    public int waitTime;
}
