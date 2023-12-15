using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyEnabled : MonoBehaviour
{
    [SerializeField] Material disabled_Mat;
    [SerializeField] Material enabledMat;
    private XRGrabInteractable keyGrabInteractable;
    private Renderer keyRenderer;
    // Start is called before the first frame update
    void Start()
    {
        keyGrabInteractable = GetComponent<XRGrabInteractable>();
        keyRenderer = GetComponent<Renderer>();

        keyRenderer.sharedMaterial = disabled_Mat;

        if(keyGrabInteractable.enabled)
        {
            Debug.Log("ENABLED");
        }
        else
        {
            Debug.Log("DISABLED");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (keyGrabInteractable.enabled)
        {
            Debug.Log("ENABLED");
            keyRenderer.sharedMaterial = enabledMat;
        }
        else
        {
            Debug.Log("DISABLED");
            keyRenderer.sharedMaterial = disabled_Mat;

        }
    }
}
