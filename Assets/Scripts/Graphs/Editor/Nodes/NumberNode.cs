using System;
using UnityEngine;

[Serializable]
public class NumberNode : GenericNode
{
    public static readonly string INPUT_PORT_NUMBER = "Number";
    public static readonly string INPUT_PORT_WAIT_TIME = "WaitTime";

    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort(INPUT_PORT_NAME).Build();
        context.AddInputPort<float>(INPUT_PORT_NUMBER).Build();
        context.AddInputPort<int>(INPUT_PORT_WAIT_TIME).Build();

        context.AddOutputPort(OUTPUT_PORT_NAME).Build();
    }
}
