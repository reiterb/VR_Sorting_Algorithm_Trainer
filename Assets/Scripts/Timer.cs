using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    private bool isTiming = false;
    private float elapsedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTiming)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log("Elapsed Time: " + elapsedTime.ToString("F2")); // Output to console instead of UI
        }
    }

    public void StartTimer()
    {
        isTiming = true;
    }

    public void StopTimer()
    {
        isTiming = false;
    }

}
