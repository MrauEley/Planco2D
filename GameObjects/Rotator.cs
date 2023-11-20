using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]private float rotationSpeed;

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
