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
    
    public void UpdateArray()
    {
        // Update sockets
        // for (int i = 0; i < sockets.Length; i++)
        // {
        //     if (sockets[i].ContainsBall()){
        //     {
        //         arrVal[i] = sockets[i].GetVBall().GetValue(); 
        //     }
        //     else
        //     {
        //         arrVal[i] = -1; 
        //     }
        // }
        
        //     string text = "Current Array \n \n";
        //     foreach (var t in arrVal)
        //     {
        //         text += "[" + t + "] ";
        //     }
        //     arrayText.text = text;
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
    
    
    public void testSwap()
    {
        Debug.Log("testSwap initialized");
        SwapBallPositions(0,2);
    }
    
     public void SwapBallPositions(int indexA, int indexB)
     {
         if (indexA < 0 || indexA >= sockets.Length || indexB < 0 || indexB >= sockets.Length)
         {
             Debug.LogError("Invalid indices for swapping ball positions.");
             return;
         }
         Debug.Log("called");

         StartCoroutine(MoveBalls(indexA, indexB));
        
         // Swap sockets 
         (sockets[indexA], sockets[indexB]) = (sockets[indexB], sockets[indexA]);

         UpdateArray(); // Update components after swapping positions
     }
     
    private IEnumerator MoveBalls(int indexA, int indexB)
    {
        float upA = 0.3f;
        float upB = 0.5f;

        GameObject socketA = sockets[indexA];
        GameObject socketB = sockets[indexB];

        Vector3 originPositionA = socketA.transform.position;
        Vector3 originPositionB = socketB.transform.position;

        Vector3 upPosA = originPositionA + new Vector3(0, upA, 0);
        Vector3 upPosB = originPositionB + new Vector3(0, upB, 0);

        // Move the balls up
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            socketA.transform.position = Vector3.Lerp(originPositionA, upPosA, t);
            socketB.transform.position = Vector3.Lerp(originPositionB, upPosB, t);
            yield return new WaitForEndOfFrame();
        }

        Vector3 sidePosA = new Vector3(originPositionB.x, upPosA.y, originPositionB.z);
        Vector3 sidePosB = new Vector3(originPositionA.x, upPosB.y, originPositionA.z);

        // Move sidewards
        t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            socketA.transform.position = Vector3.Lerp(upPosA, sidePosA, t);
            socketB.transform.position = Vector3.Lerp(upPosB, sidePosB, t);
            yield return new WaitForEndOfFrame();
        }

        Vector3 downPosA = sidePosA - new Vector3(0, upA, 0);
        Vector3 downPosB = sidePosB - new Vector3(0, upB, 0);

        // Move down
        t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            socketA.transform.position = Vector3.Lerp(sidePosA, downPosA, t);
            socketB.transform.position = Vector3.Lerp(sidePosB, downPosB, t);
            yield return new WaitForEndOfFrame();
        }

        // Finally, make sure sockets changed exact place
        socketA.transform.position = originPositionB;
        socketB.transform.position = originPositionA;
    }
    
}