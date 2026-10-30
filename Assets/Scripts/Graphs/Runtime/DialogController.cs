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
        await Task.Yield();
        INodeResolver<TextRuntimeNode> textResolver = new TextResolver();
        INodeResolver<NumberRuntimeNode> numberResolver= new NumberResolver();

        foreach(GenericRuntimeNode node in graph.nodes)
        {
            switch(node)
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

    public void DisplayText(string text)
    {
        dialogText.text = text;
    }

    public void DisplayNumber(float number)
    {
        dialogText.text = $"{number}";
    }
}
