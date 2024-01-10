using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // grades
    [SerializeField] GameObject A;
    [SerializeField] GameObject B;
    [SerializeField] GameObject C;
    [SerializeField] GameObject D;
    [SerializeField] GameObject E;
    [SerializeField] GameObject F;

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
        ShowGrade();
    }

    private void ShowGrade()
    {
        if (elapsedTime < 10f)
        {
            A.SetActive(true);
        }
        else if (elapsedTime >= 10f && elapsedTime < 20f)
        {
            B.SetActive(true );
        }
        else if (elapsedTime >= 20f && elapsedTime < 30f)
        {
            C.SetActive(true);
        }
        else if (elapsedTime >= 30f && elapsedTime < 40f)
        {
            D.SetActive(true);
        }
        else if (elapsedTime >= 50f && elapsedTime < 60f)
        {
            E.SetActive(true);
        }
        else if (elapsedTime >= 60f)
        {
            F.SetActive(true);
        }
    }

}
