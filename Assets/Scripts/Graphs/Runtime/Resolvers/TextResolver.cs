using System.Threading.Tasks;
using UnityEngine;

public class TextResolver : INodeResolver<TextRuntimeNode>
{
    public async Task Resolve(DialogController controller, TextRuntimeNode node)
    {
        string localizedString = string.Empty;

        if (node.localizedText != null && !node.localizedText.IsEmpty)
        {
            var handle = node.localizedText.GetLocalizedStringAsync();
            await handle.Task;
            localizedString = handle.Result;
        }

        controller.DisplayText(localizedString);
        await Task.Delay(node.waitTime);
    }
}
