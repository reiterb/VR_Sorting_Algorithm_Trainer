using UnityEngine;

namespace Interaction_Part
{
    public class Socket : MonoBehaviour
    {
        private ValueBall _currValBall;

        private void OnTriggerEnter(Collider other)
        {
            ValueBall vBall = other.GetComponent<ValueBall>();
        
            // Check if there's already a ball in the socket
            if (_currValBall != null)
            {
                return; 
            }
        
            if (vBall != null)
            {
                // Update the ArrayManager
                ArrayManager.Instance.UpdateArrayValue(vBall.GetValue(), GetSocketIndex());
                _currValBall = vBall;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // If exits, update the ArrayManager with default(0)
            if (_currValBall != null && other.gameObject == _currValBall.gameObject)
            {
                ArrayManager.Instance.UpdateArrayValue(0, GetSocketIndex());
                _currValBall = null;
            }
        }

        private int GetSocketIndex()
        {
            for (int i = 0; i < ArrayManager.Instance.sockets.Length; i++)
            {
                if (ArrayManager.Instance.sockets[i].transform == transform)
                {
                    return i;
                }
            }
            return -1; 
        }
    
        public bool ContainsBall()
        {
            return _currValBall != null;
        }

        public ValueBall GetVBall()
        {
            return _currValBall;
        }
    }
}