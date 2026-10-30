using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GenericRuntimeGraph graph;

    private void Start()
    {
        Resolve();
    }

    private async void Resolve()
    {
        try
        {
            await Task.Yield();

            if (graph == null)
            {
                Debug.LogWarning("No se asignó ningún GenericRuntimeGraph en DialogController.");
                return;
            }

            INodeResolver<TextRuntimeNode> textResolver = new TextResolver();
            INodeResolver<NumberRuntimeNode> numberResolver = new NumberResolver();

            foreach (GenericRuntimeNode node in graph.nodes)
            {
                switch (node)
                {
                    case TextRuntimeNode textNode:
                        await textResolver.Resolve(this, textNode);
                        break;
                    case NumberRuntimeNode numberNode:
                        await numberResolver.Resolve(this, numberNode);
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Excepción capturada en DialogController: {ex.Message}");
        }
    }

    public void DisplayText(string text)
    {
        if (dialogText != null)
        {
            dialogText.text = text;
        }
    }

    public void DisplayNumber(float number)
    {
        if (dialogText != null)
        {
            dialogText.text = $"{number}";
        }
    }
}