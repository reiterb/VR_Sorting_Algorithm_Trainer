using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrayManager : MonoBehaviour
{
    public static ArrayManager Instance;

    public GameObject[] vBalls;
    public GameObject[] sockets;
    public TMP_Text arrayText;
    
    [Header("Swap velocity")]
    public float moveSpeed = 5.0f;

    private int[] arrVal;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeArray();
    }

    public void UpdateArrayValue(int value, int index)
    {
        arrVal[index] = value;
        string text = "Current Array \n \n";
        for (int i = 0; i < arrVal.Length; i++)
        {
            text += "[" + arrVal[i] + "] ";
        }
        arrayText.text = text; 
    }

    void InitializeArray()
    {
        arrVal = new int[sockets.Length];
        for (int i = 0; i < arrVal.Length; i++)
        {
            arrVal[i] = 0;
        }

        arrayText.text = "Current Array \n \n[0] [0] [0]";
    }

    public void SwapBallPositions(int indexA, int indexB)
    {
        if (indexA < 0 || indexA >= sockets.Length || indexB < 0 || indexB >= sockets.Length)
        {
            Debug.LogError("Invalid indices for swapping ball positions.");
            return;
        }
        
        GameObject socketA = sockets[indexA];
        GameObject socketB = sockets[indexB];

        // Get the initial positions of the balls
        Vector3 originPositionA = socketA.transform.position; //(0.7,0.46,-2,66)
        Vector3 originPositionB = socketB.transform.position; //(0.4,0.46,-2,66)
        
        Debug.Log("originA = " + originPositionA);
        Debug.Log("originB = " + originPositionB);
        
        Vector3 currPosA = originPositionA; 
        Vector3 currPosB = originPositionB;
        
        Debug.Log("currPosA = " + currPosA);
        Debug.Log("currPosB = " + currPosB);

        // Move the balls up
        Debug.Log("move up");
        currPosA.y += 0.3f;
        currPosB.y += 0.5f;
        
        Debug.Log("currPosA = " + currPosA);
        Debug.Log("currPosB = " + currPosB);

        StartCoroutine(MoveObject(socketA.transform, currPosA, moveSpeed)); 
        // socketA.transform.position = currPosA;
        StartCoroutine(MoveObject(socketB.transform, currPosB, moveSpeed)); 
        // socketB.transform.position = currPosB;
        
        
        // Todo Move the balls sidewards  
        // Todo BUG: the Button will activate event 2 times !!!! -> thus nothing works after that point! 
        
        // Debug.Log("move sidewards");
        // currPosA.x = originPositionB.x; 
        // currPosA.z = originPositionB.z; 
        // currPosB.x = originPositionA.x;
        // currPosB.z = originPositionA.z;
        //
        // Debug.Log("currPosA = " + currPosA);
        // Debug.Log("currPosB = " + currPosB);
        //
        // Debug.Log("originA = " + originPositionA);
        // Debug.Log("originB = " + originPositionB);
        //
        // socketA.transform.position = currPosA;
        // socketB.transform.position = currPosB;
        // StartCoroutine(MoveObject(socketA.transform, currPosA, moveSpeed));
        // StartCoroutine(MoveObject(socketB.transform, currPosB, moveSpeed));
        
        // Todo Move the balls Down
        // currPosA.y = originPositionB.y; 
        // currPosB.y = originPositionA.y; 
        //
        // Debug.Log("move down");
        // socketA.transform.position = currPosA;
        // socketB.transform.position = currPosB;
        //
        // // Swap index
        // (sockets[indexA], sockets[indexB]) = (sockets[indexB], sockets[indexA]);
    }

    public void testSwap()
    {
        Debug.Log("testSwap initialized");
        SwapBallPositions(0,2);
    }
    
    private IEnumerator MoveObject (Transform objTransform, Vector3 targetPosition, float speed)
    {
        float t = 0f;
        Vector3 startPosition = objTransform.position;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            objTransform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        
        objTransform.position = targetPosition;
    }
    
}