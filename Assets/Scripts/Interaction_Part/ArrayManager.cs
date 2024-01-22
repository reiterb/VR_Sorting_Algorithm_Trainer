using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Interaction_Part
{
    public class ArrayManager : MonoBehaviour
    {
        public static ArrayManager Instance;

        // public GameObject[] vBalls;
        public GameObject[] sockets;
        public TMP_Text arrayText;

        [Header("Swap velocity")] public float moveSpeed = 2.0f;
        private float _defaultSpeed = 2f;
        private float _maxSpeed = 15f;
        private float _minSpeed = 1f; 

        private int[] _arrVal;      // last updated values in the array 
    
        private Queue<Tuple<int, int>> _swapQueue = new();
        private bool _isSwapping;   // one swap process is active
        private bool _isBusy;       // when algorithm is active
        private bool _shuffling;   // one swap process is active
        private Queue<Tuple<int, int>> _remainingSwapQueue = new();

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

        // Updates the Array Value of TMP_Text arrayText on a specific index to an specific value 
        public void UpdateArrayValue(int value, int index)
        {
            _arrVal[index] = value;
            string text = "Current Array \n \n";
            foreach (var t in _arrVal)
            {
                text += "[" + t + "] ";
            }

            arrayText.text = text;
        }
    
        // Updates the Array Value of TMP_Text arrayText by going through every socket
        private void UpdateArray()
        {
            // Update sockets
            for (int i = 0; i < sockets.Length; i++)
            {
                Socket valueSocket = sockets[i].GetComponent<Socket>();
                if (valueSocket.ContainsBall())
                {
                    {
                        _arrVal[i] = valueSocket.GetVBall().GetValue();
                    }
                }
                else
                {
                    _arrVal[i] = -1;
                }
            }

            string text = "Current Array \n \n";
            foreach (var t in _arrVal)
            {
                text += "[" + t + "] ";
            }

            arrayText.text = text;
        }

        // Puts Brackets around the tuples about to be swapped 
        private void VisualizeSwap(int idx1, int idx2)
        {
            var text = "Current Array \n \n";
            for (int i = 0; i < sockets.Length; i++)
            {
                if (i == idx1)
                {
                    text += " (";
                }

                text += "[" + _arrVal[i] + "]";
                if (i == idx2)
                {
                    text += ") ";
                }
                else
                {
                    text += " ";
                }
                    
            }
            arrayText.text = text;
        }

        // Initialize the TMP_Text to default
        void InitializeArray()
        {
            _arrVal = new int[sockets.Length];
            for (int i = 0; i < _arrVal.Length; i++)
            {
                _arrVal[i] = 0;
            }

            arrayText.text = "Current Array \n \n[0] [0] [0] [0] [0] [0] [0] [0] [0] [0]";
        }

        private void SetMoveSpeed(float value)
        {
            moveSpeed += value;
        }


        // Callable Functions
        // -----------------------------------------------------------------------------
    
        public void TestSwap()
        {
            if (_isBusy) { return; }
            Debug.Log("testSwap initialized");
            SwapBallPositions(0, 1);
            SwapBallPositions(2, 3);
            SwapBallPositions(4, 5);
            SwapBallPositions(0, 4);
        }
    
        public void DoBubbleSort()
        {
            if (_isBusy) { return; }
            UpdateArray();
            _isBusy = true;
            List<Tuple<int, int>> swapList = BubbleSortWithSwaps(_arrVal);

            foreach (var t in swapList)
            {
                SwapBallPositions(t.Item1, t.Item2); 
            }
        }
    
        public void DoQuickSort()
        {
            if (_isBusy) { return; }
            UpdateArray();
            _isBusy = true;
            List<Tuple<int, int>> swapList = QuickSortWithSwaps(_arrVal);

            foreach (var t in swapList)
            {
                SwapBallPositions(t.Item1, t.Item2); 
            }
        }
    
        public void ShuffleArray()
        {
            if (_isBusy) { return; }
            UpdateArray();
            _isBusy = true;
            _shuffling = true;
            for (int i = 0; i < sockets.Length; i++)
            {
                int randomIndex = Random.Range(0, sockets.Length);
                SwapBallPositions(i, randomIndex);
            }
        }
    
        public void IncreaseSpeed()
        {
            if (moveSpeed < _maxSpeed)
            {
                SetMoveSpeed(1f);
            }
        }
    
        public void DecreaseSpeed()
        {
            if (moveSpeed > _minSpeed)
            {
                SetMoveSpeed(-1f);
            }
        }

        // Ball swapping
        // -----------------------------------------------------------------------------
        private void SwapBallPositions(int indexA, int indexB)
        {
            if (indexA < 0 || indexA >= sockets.Length || indexB < 0 || indexB >= sockets.Length)
            {
                Debug.LogError("Invalid indices for swapping ball positions.");
                return;
            }
        
            // Enqueue the swap request
            _swapQueue.Enqueue(new Tuple<int, int>(indexA, indexB));
            Debug.Log($"Enqueued swap request: {indexA} <-> {indexB}");

            // If not currently swapping, start the coroutine
            if (!_isSwapping)
            {
                StartCoroutine(SequentialSwapCoroutine());
            }
        }

        private IEnumerator SequentialSwapCoroutine()
        {
            _isSwapping = true;

            while (_swapQueue.Count > 0)
            {
                Tuple<int, int> swapRequest = _swapQueue.Dequeue();
                if (!_shuffling)
                {
                    VisualizeSwap(swapRequest.Item1, swapRequest.Item2);
                }
                else // Shuffle at maxSpeed
                {
                    moveSpeed = _maxSpeed;
                }
                yield return StartCoroutine(SwapCoroutine(swapRequest.Item1, swapRequest.Item2));
            }

            _isSwapping = false;
            _isBusy = false;
            if (_shuffling)
            {
                moveSpeed = _defaultSpeed;
                _shuffling = false; 
            }
            
        }
    
        private IEnumerator SwapCoroutine(int indexA, int indexB)
        {
            yield return StartCoroutine(MoveBalls(indexA, indexB));

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
    
        // Sort Functions
        // -----------------------------------------------------------------------------
        private List<Tuple<int, int>> BubbleSortWithSwaps(int[] arr)
        {
            List<Tuple<int, int>> swapList = new List<Tuple<int, int>>();
            int n = arr.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    // If the element is greater than the next element, swap them
                    if (arr[j] > arr[j + 1])
                    {
                        swapList.Add(new Tuple<int, int>(j, j + 1));
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                    }
                }
            }
            return swapList;
        }
    
        private List<Tuple<int, int>> QuickSortWithSwaps(int[] arr)
        {
            List<Tuple<int, int>> swapList = new List<Tuple<int, int>>();
            QuickSort(arr, 0, arr.Length - 1, swapList);
            return swapList;
        }

        private void QuickSort(int[] arr, int low, int high, List<Tuple<int, int>> swapList)
        {
            if (low >= high) return;
            int pivot = arr[(low + high) / 2];
            int i = low;
            int j = high;

            while (i <= j)
            {
                while (arr[i] < pivot)
                {
                    i++;
                }
                while (arr[j] > pivot)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Add the swap to the list
                    swapList.Add(new Tuple<int, int>(i, j));

                    // Swap arr[i] and arr[j]
                    (arr[i], arr[j]) = (arr[j], arr[i]);

                    i++;
                    j--;
                }
            }

            QuickSort(arr, low, j, swapList);
            QuickSort(arr, i, high, swapList);
        }

    }
}