using System.Threading.Tasks;
using UnityEngine;

public class TextResolver : INodeResolver<TextRuntimeNode>
{
    public async Task Resolve(DialogController controller, TextRuntimeNode node)
    {
        controller.DisplayText(node.text);
        await Task.Delay(node.waitTime);
    }

}
