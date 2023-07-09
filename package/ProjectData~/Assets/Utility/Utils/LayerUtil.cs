using UnityEngine;

namespace Utility
{
    public static class LayerUtil
    {
        public static void SetLayerRecursively(GameObject go, int layer)
        {
            go.layer = layer;

            foreach (Transform child in go.transform)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
        }
    }
}
