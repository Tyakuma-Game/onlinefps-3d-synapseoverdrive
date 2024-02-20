using UnityEngine;

namespace CameraControl
{
    public class RayGenerator : MonoBehaviour
    {
        Camera myCamera;

        void Start()
        {
            myCamera = Camera.main;
        }

        /// <summary>
        /// カメラから場所を指定してRayを生成
        /// </summary>
        /// <param name="generationPos">生成する座標</param>
        /// <returns>生成したRay</returns>
        public Ray GenerateRay(Vector2 generationPos)
        {
            return myCamera.ViewportPointToRay(generationPos);
        }
    }
}