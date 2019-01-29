using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
 
[Serializable]
[PostProcess(typeof(CameraFlopRenderer), PostProcessEvent.AfterStack, "CameraFlop")]
public sealed class CameraFlop : PostProcessEffectSettings
{
}

public sealed class CameraFlopRenderer : PostProcessEffectRenderer<CameraFlop>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/CameraFlop"));
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}