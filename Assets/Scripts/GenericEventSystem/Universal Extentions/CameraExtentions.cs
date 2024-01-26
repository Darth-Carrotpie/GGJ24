using UnityEngine;

namespace GenericEventSystem {

    public static class CameraExtensions {
        public static bool IsVisibleFrom(this Renderer renderer, Camera camera) {
            var planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }
    }
}