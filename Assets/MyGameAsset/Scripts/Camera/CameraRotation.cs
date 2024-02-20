using UnityEngine;

namespace CameraControl
{
    public class CameraRotation : MonoBehaviour
    {
        [SerializeField] Transform viewPoint;
        float verticalMouseInput;

        public void Rotation(Vector2 rotationInput, float rotationSpeed, float rotationRange)
        {
            verticalMouseInput += rotationInput.y * rotationSpeed;
            verticalMouseInput = Mathf.Clamp(verticalMouseInput, -rotationRange, rotationRange);

            viewPoint.rotation = Quaternion.Euler(-verticalMouseInput, viewPoint.rotation.eulerAngles.y, viewPoint.rotation.eulerAngles.z);
        }
    }
}