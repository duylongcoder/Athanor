using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Athanor.Game
{
    /// <summary>
    /// One flat thing of the bench placed in depth: it is moved to its depth and enlarged by (D - z) / D, so that from the
    /// camera at rest it covers exactly the art pixels it covers in the flat game. Sorting stays by sortingOrder.
    /// </summary>
    public sealed class DepthLayer : MonoBehaviour
    {
        /// <summary>SPEC depth: 0 is the art plane, negative is away from the camera.</summary>
        public float z;
        /// <summary>World x, y of the object in the flat game (its position on the plane z = 0).</summary>
        public Vector2 rest;
        /// <summary>Its scale in the flat game.</summary>
        public Vector3 restScale = Vector3.one;
        /// <summary>A point light on this object, or null. URP builds a point light's matrix from its radius and ignores
        /// the transform's scale, so the radius is what gets enlarged.</summary>
        public Light2D pointLight;
        /// <summary>The point light's inner and outer radius in the flat game.</summary>
        public float restInner, restOuter;

        /// <summary>Takes the object's present position, scale and point light as its flat (rest) state and sets its depth.</summary>
        public static DepthLayer Add(GameObject go, float z)
        {
            var layer = go.GetComponent<DepthLayer>();
            if (layer == null) layer = go.AddComponent<DepthLayer>();
            var t = go.transform;
            layer.z = z;
            layer.rest = t.position;
            layer.restScale = t.localScale;
            var light = go.GetComponent<Light2D>();
            if (light != null && light.lightType == Light2D.LightType.Point)
            {
                layer.pointLight = light;
                layer.restInner = light.pointLightInnerRadius;
                layer.restOuter = light.pointLightOuterRadius;
            }
            return layer;
        }

        /// <summary>Places the object for a camera at distance d from the art plane.</summary>
        public void Apply(float d)
        {
            float s = DepthMath.Scale(z, d);
            transform.position = DepthMath.Place(rest, z, d);
            transform.localScale = restScale * s;
            if (pointLight != null)
            {
                pointLight.pointLightInnerRadius = restInner * s;
                pointLight.pointLightOuterRadius = restOuter * s;
            }
        }
    }
}
