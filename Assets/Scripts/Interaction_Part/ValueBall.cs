using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Interaction_Part
{
    public class ValueBall : MonoBehaviour
    {
        [SerializeField] private int value;
        private XRGrabInteractable keyGrabInteractable;
        private InteractionLayerMask originalLayer = 0;

        void Start()
        {
            keyGrabInteractable = GetComponent<XRGrabInteractable>();
            originalLayer = keyGrabInteractable.interactionLayers;
        }
        
        public void SetValue(int val)
        {
            value = val;
        }
    
        public int GetValue()
        {
            return value;
        }
    }
}
