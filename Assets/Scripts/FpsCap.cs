using UnityEngine;

public class FpsCap : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
