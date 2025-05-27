using UnityEngine;

public class RotateCrown : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    void Update()
    {
        transform.Rotate(Vector3.up*rotationSpeed*Time.deltaTime);
    }
}
