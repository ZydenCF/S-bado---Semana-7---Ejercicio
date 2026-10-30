using System.Collections.Generic;
using System.Linq;
using Unity.GraphToolkit.Editor;
using UnityEditor.AssetImporters;
using UnityEngine;

[ScriptedImporter(1, GenericGraph.AssetExtension)]
public class GraphImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext context)
    {
        GenericGraph graph = GraphDatabase.LoadGraphForImporter<GenericGraph>(context.assetPath);
        StartNode startNode = graph.GetNodes().OfType<StartNode>().FirstOrDefault();

        GenericRuntimeGraph runtimeGraph = ScriptableObject.CreateInstance<GenericRuntimeGraph>();

      
        INode currentNode = GetNextNode(startNode);

     
        while (currentNode != null)
        {
            List<GenericRuntimeNode> runtimeNodes = ConvertNodesToRuntimeNodes(currentNode);
            runtimeGraph.nodes.AddRange(runtimeNodes);

       
            currentNode = GetNextNode(currentNode);
        }

        context.AddObjectToAsset("RuntimeGraph", runtimeGraph);
        context.SetMainObject(runtimeGraph);
    }

    static INode GetNextNode(INode currentNode)
    {
        if (currentNode == null) return null;

        IPort outputPort = currentNode.GetOutputPortByName(GenericNode.OUTPUT_PORT_NAME);

      
        if (outputPort == null || !outputPort.isConnected) return null;

        IPort nextNodePort = outputPort.firstConnectedPort;
        return nextNodePort?.GetNode();
    }

    static List<GenericRuntimeNode> ConvertNodesToRuntimeNodes(INode currentNode)
    {
        List<GenericRuntimeNode> runtimeNodes = new List<GenericRuntimeNode>();

        switch (currentNode)
        {
            case TextNode textNode:
                runtimeNodes.Add(new TextRuntimeNode
                {
                    text = GetInputPortValue<string>(textNode.GetInputPortByName(TextNode.INPUT_PORT_TEXT)),
                    waitTime = GetInputPortValue<int>(textNode.GetInputPortByName(TextNode.INPUT_PORT_WAIT_TIME))
                });
                break;

            case NumberNode numberNode:
                runtimeNodes.Add(new NumberRuntimeNode
                {
                    number = GetInputPortValue<float>(numberNode.GetInputPortByName(NumberNode.INPUT_PORT_NUMBER)),
                    waitTime = GetInputPortValue<int>(numberNode.GetInputPortByName(NumberNode.INPUT_PORT_WAIT_TIME))
                });
                break;
        }
        return runtimeNodes;
    }

    static T GetInputPortValue<T>(IPort port)
    {
        if (port == null) return default;

        T value = default;
        if (port.isConnected)
        {
            switch (port.firstConnectedPort.GetNode())
            {
                case IVariableNode variableNode:
                    variableNode.variable.TryGetDefaultValue<T>(out value);
                    return value;
                case IConstantNode constantNode:
                    constantNode.TryGetValue<T>(out value);
                    return value;
                default:
                    break;
            }
        }
        else
        {
            port.TryGetValue(out value);
        }
        return value;
    }
}