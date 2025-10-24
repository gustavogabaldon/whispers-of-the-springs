using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class UnderwaterRenderFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public Material underwaterMaterial;
    }

    public Settings settings = new Settings();

    class PassData
    {
        internal UnityEngine.Rendering.RenderGraphModule.TextureHandle source;
        internal Material material;
    }

    public override void Create() { }

    public override void RecordRenderGraph(UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, ContextContainer frameData)
    {
        if (settings.underwaterMaterial == null) return;

        var res = frameData.Get<UniversalResourceData>();
        var camera = frameData.Get<UniversalCameraData>();

        using (var builder = renderGraph.AddRasterRenderPass<PassData>("Underwater Tint", out var passData))
        {
            passData.source = res.activeColorTexture;
            passData.material = settings.underwaterMaterial;

            builder.UseTexture(passData.source);
            builder.SetRenderAttachment(res.activeColorTexture, 0);
            builder.SetRenderAttachmentDepth(res.activeDepthTexture);

            builder.SetRenderFunc((PassData data, UnityEngine.Rendering.RenderGraphModule.RasterGraphContext ctx) =>
            {
                Blitter.BlitTexture(ctx.cmd, data.source, new Vector4(1, 1, 0, 0), data.material, 0);
            });
        }
    }
}
