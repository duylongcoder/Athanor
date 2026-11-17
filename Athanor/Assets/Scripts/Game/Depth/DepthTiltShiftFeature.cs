using UnityEngine.Rendering.Universal;

namespace Athanor.Game
{
    /// <summary>
    /// A full-screen pass of the tilt-shift (one blur direction per feature) that runs only for cameras with an enabled
    /// <see cref="TiltShift"/>, so the HUD camera and the main game's camera are never blurred.
    /// </summary>
    public class DepthTiltShiftFeature : FullScreenPassRendererFeature
    {
        /// <inheritdoc/>
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (!TiltShift.IsOn(renderingData.cameraData.camera)) return;
            base.AddRenderPasses(renderer, ref renderingData);
        }
    }
}
