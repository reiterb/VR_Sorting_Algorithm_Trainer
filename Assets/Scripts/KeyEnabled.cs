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
    }

    // Update is called once per frame
    void Update()
    {
        if (keyGrabInteractable.enabled)
        {
            keyRenderer.sharedMaterial = enabledMat;
        }
        else
        {
            keyRenderer.sharedMaterial = disabled_Mat;

        }
    }
}
