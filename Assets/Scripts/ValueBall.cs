using UnityEngine;

public class ValueBall : MonoBehaviour
{
    [SerializeField] private int value;

    public void SetValue(int val)
    {
        value = val;
    }
    
    public int GetValue()
    {
        return value;
    }
}
