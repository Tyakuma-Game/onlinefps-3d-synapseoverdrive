using UnityEngine;

// TODO: ‚±‚ê‚àEvent‹ì“®Œ^‚É•ÏX‚µ‚ÄŒø—¦‰»‚·‚éI

/// <summary>
/// ƒJƒƒ‰‚ÉŠÖ‚·‚éˆ—‚ğ‚Ü‚Æ‚ß‚ÄŠÇ—‚·‚éƒNƒ‰ƒX
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("ƒJƒƒ‰‚ÌˆÊ’uƒIƒuƒWƒFƒNƒg")]
    [SerializeField] Transform viewPoint;
    Camera myCamera;

    void Start()
    {
        // æ“¾
        myCamera = Camera.main;
    }

    void Update()
    { 
        // ˆÊ’uXV
        myCamera.transform.position = viewPoint.position;
        myCamera.transform.rotation = viewPoint.rotation;
    }

    //|||||||||||||||||||||/
    // ƒY[ƒ€ŠÖ˜A
    //|||||||||||||||||||||/

    //[Tooltip("ƒJƒƒ‰‚ÌŒ³‚Ìi‚è”{—¦")]
    //[SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    /// <summary>
    /// ƒJƒƒ‰‚ÌƒY[ƒ€‚ğ’²®‚·‚é
    /// </summary>
    /// <param name="targetZoom">–Ú•W‚ÌƒY[ƒ€”{—¦</param>
    /// <param name="zoomSpeed">ƒY[ƒ€‘¬“x</param>
    public void AdjustCameraZoom(float targetZoom, float zoomSpeed)
    {
        myCamera.fieldOfView = Mathf.Lerp(
            myCamera.fieldOfView,      //ŠJn’n“_
            targetZoom,                //–Ú“I’n“_
            zoomSpeed * Time.deltaTime //•âŠ®”’l
        );
    }

    //|||||||||||||||||||||||||||/
    //@Ray¶¬
    //|||||||||||||||||||||||||||/

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