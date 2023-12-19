using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CodeBlockManager : MonoBehaviour
{
    [SerializeField] private int numberOfTasksToComplete;
    private int currentlyCompletedTasks = 0;

    [Header("Completion Events")]
    public UnityEvent onQuizzCompletion;

    public void CompletedQuizTask()
    {
        currentlyCompletedTasks++;
        checkForQuizCompletion();
    }

    private void checkForQuizCompletion()
    {
        if(currentlyCompletedTasks >= numberOfTasksToComplete)
        {
            onQuizzCompletion.Invoke();
        }
    }

    public void CodeBlockRemoved()
    {
        currentlyCompletedTasks--;
    }
}
