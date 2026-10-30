using System.Threading.Tasks;
using UnityEngine;

public class NumberResolver : INodeResolver<NumberRuntimeNode>
{
    public async Task Resolve(DialogController controller, NumberRuntimeNode node)
    {
        controller.DisplayNumber(node.number);
        await Task.Delay(node.waitTime);
    }

}
