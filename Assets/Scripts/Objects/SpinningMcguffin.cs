using UnityEngine;

public class SpinningMcguffin : MonoBehaviour
{
    [SerializeField] private float spinCycleSpeed;

    void Update()
    {
        transform.Rotate(spinCycleSpeed * Time.deltaTime, 0, 0);
    }
}
