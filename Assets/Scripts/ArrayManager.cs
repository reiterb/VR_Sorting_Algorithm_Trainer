using TMPro;
using UnityEngine;

public class ArrayManager : MonoBehaviour
{
    public static ArrayManager Instance;

    public GameObject[] vBalls;
    public Transform[] sockets;
    public TMP_Text arrayText; 

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
        Debug.Log("Updated Array Values:");
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
}