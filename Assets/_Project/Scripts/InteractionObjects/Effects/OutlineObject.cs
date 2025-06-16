using UnityEngine;

namespace AE
{
    public class OutlineObject : MonoBehaviour
    {
        [SerializeField] private Material outlineMaterial;
        private GameObject outlineInstance;


        void Start()
        {
            var meshFilter = GetComponent<MeshFilter>();
            var meshRenderer = GetComponent<MeshRenderer>();


            if (meshFilter && meshRenderer)
            {
                outlineInstance = new GameObject("Outline");
                outlineInstance.transform.SetParent(transform);
                outlineInstance.transform.localPosition = Vector3.zero;
                outlineInstance.transform.localRotation = Quaternion.identity;
                outlineInstance.transform.localScale = Vector3.one;

                var outlineMF = outlineInstance.AddComponent<MeshFilter>();
                var outlineMR = outlineInstance.AddComponent<MeshRenderer>();

                outlineMF.sharedMesh = meshFilter.sharedMesh;
                outlineMR.material = outlineMaterial;

                outlineInstance.SetActive(false);
            }
        }

        public void EnableOutline(bool enable)
        {
            if (outlineInstance)
                outlineInstance.SetActive(enable);
        }
    }
}
