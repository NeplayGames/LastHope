
using TMPro;
using UnityEngine;

public class Oscilate : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] float period = 2f;
    [SerializeField] Vector3 movement;

    [Range(0, 1)] [SerializeField] float movementFactor;
    private void Start()
    {

        startPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        float cycle = Time.time / period;
        const float tau = Mathf.PI * 2;
        float value = Mathf.Sin(cycle * tau);
        movementFactor = value / 2 + 0.5f;
        Vector3 offset = movementFactor * movement;
        transform.position = startPos + offset;
    }
}
