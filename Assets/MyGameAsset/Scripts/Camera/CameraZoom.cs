using UnityEngine;

namespace CameraControl
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;
        Camera myCamera;

        void Start()
        {
            myCamera = Camera.main;
        }

        public void ZoomIn(float adsZoom, float adsSpeed)
        {
            myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, adsZoom, adsSpeed * Time.deltaTime);
        }

        public void ZoomOut(float adsSpeed)
        {
            myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, CAMERA_APERTURE_BASE_FACTOR, adsSpeed * Time.deltaTime);
        }
    }
}