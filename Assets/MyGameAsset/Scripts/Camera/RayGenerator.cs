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
        /// ƒJƒƒ‰‚©‚çêŠ‚ğw’è‚µ‚ÄRay‚ğ¶¬
        /// </summary>
        /// <param name="generationPos">¶¬‚·‚éÀ•W</param>
        /// <returns>¶¬‚µ‚½Ray</returns>
        public Ray GenerateRay(Vector2 generationPos)
        {
            return myCamera.ViewportPointToRay(generationPos);
        }
    }
}