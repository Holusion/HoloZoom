using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
 
[Serializable]
[PostProcess(typeof(CameraFlopRenderer), PostProcessEvent.AfterStack, "CameraFlop")]
public sealed class CameraFlop : PostProcessEffectSettings
{
    public BoolParameter horizontal = new BoolParameter {value = true};
    public BoolParameter vertical = new BoolParameter {value = false};
}

public sealed class CameraFlopRenderer : PostProcessEffectRenderer<CameraFlop>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/CameraFlop"));
        sheet.properties.SetInt("_FlopH", settings.horizontal ? 1 : 0);
        sheet.properties.SetInt("_FlopV", settings.vertical ? 1 : 0);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}