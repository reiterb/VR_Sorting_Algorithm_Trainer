using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CodeBlock : MonoBehaviour
{
    [SerializeField] private CodeBlockManager linkedCodeBlockManager;
    [SerializeField] private Transform CorrectCodeBlock;
    private XRSocketInteractor socket;

    private void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    private void OnEnable()
    {
        socket.selectEntered.AddListener(ObjectSnapped);
        socket.selectExited.AddListener(ObjectRemoved);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(ObjectSnapped);
        socket.selectExited.RemoveListener(ObjectRemoved);
    }

    private void ObjectSnapped(SelectEnterEventArgs arg0)
    {
        var snappedObjectName = arg0.interactableObject;
        if(snappedObjectName!= null && snappedObjectName.transform != null && CorrectCodeBlock != null && snappedObjectName.transform.name == CorrectCodeBlock.name)
        {
            linkedCodeBlockManager.CompletedQuizTask();
        }
    }

    private void ObjectRemoved(SelectExitEventArgs arg0)
    {
        var removedObjectName = arg0.interactableObject;
        if (removedObjectName != null && removedObjectName.transform != null && CorrectCodeBlock != null && removedObjectName.transform.name == CorrectCodeBlock.name)
        {
            linkedCodeBlockManager.CodeBlockRemoved();
        }
    }
}
