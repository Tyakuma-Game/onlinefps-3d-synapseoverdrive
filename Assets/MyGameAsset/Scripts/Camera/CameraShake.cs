using System.Collections;
using UnityEngine;

namespace CameraControl
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] Transform viewPoint;
        [SerializeField] Transform sabViewPoint;
        [SerializeField] float shakeMagnitude = 0.2f;
        [SerializeField] float shakeTime = 0.1f;
        Camera myCamera;
        
        void Start()
        {
            myCamera = Camera.main;
        }

        public void Shake()
        {
            StartCoroutine(ViewPointShake());
        }

        IEnumerator ViewPointShake()
        {
            float shakeCount = 0;
            while (shakeCount < shakeTime)
            {
                float x = sabViewPoint.transform.position.x + Random.Range(-shakeMagnitude, shakeMagnitude);
                float y = sabViewPoint.transform.position.y + Random.Range(-shakeMagnitude, shakeMagnitude);
                viewPoint.transform.position = new Vector3(x, y, sabViewPoint.transform.position.z);
                myCamera.transform.position = viewPoint.transform.position;

                shakeCount += Time.deltaTime;
                yield return null;
            }
            viewPoint.transform.position = sabViewPoint.transform.position;
        }
    }
}