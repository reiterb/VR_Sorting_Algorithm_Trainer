using UnityEngine;

namespace Player
{
    public class VRNoPeek : MonoBehaviour
    {
        [SerializeField] private LayerMask collisionLayer; 
        [SerializeField] private float fadeSpeed; 
        [SerializeField] private float sphereChecksize;

        private Material cameraFadeMat;
        private bool isFadeOut;
        private static readonly int AlphaValue = Shader.PropertyToID("_AlphaValue");
        private static readonly int Value = Shader.PropertyToID("__AlphaValue");

        private void Awake()
        {
            cameraFadeMat = GetComponent<Renderer>().material;
        }
    
        void Update()
        {
            if (Physics.CheckSphere(transform.position, sphereChecksize, collisionLayer, QueryTriggerInteraction.Ignore))
            {
                CameraFade(1f);
                isFadeOut = true; 
            }
            else
            {
                if (!isFadeOut)
                {
                    return;
                }

                CameraFade(0f);
            }
        }

        private void CameraFade(float targetAlpha)
        {
            var fadeValue =
                Mathf.MoveTowards(cameraFadeMat.GetFloat(AlphaValue), targetAlpha, Time.deltaTime * fadeSpeed); 
            cameraFadeMat.SetFloat(AlphaValue,fadeValue);

            if (fadeValue <= 0.01f)
            {
                isFadeOut = false; 
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.1f, 0.7f,0.5f, 0.75f); 
            Gizmos.DrawSphere(transform.position,sphereChecksize);
        }
    }
}
