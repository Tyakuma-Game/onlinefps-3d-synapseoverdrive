using System.Collections;
using UnityEngine;

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
        // ƒJƒƒ‰Ši”[
        myCamera = Camera.main;
    }

    /// <summary>
    /// ƒJƒƒ‰‚ÌXVˆ—
    /// </summary>
    public void UpdatePosition()
    {
        // ƒJƒƒ‰ˆÊ’uXV
        myCamera.transform.position = viewPoint.position;//À•W
        myCamera.transform.rotation = viewPoint.rotation;//‰ñ“]
    }

    //|||||||||||||||||||||/
    // ‹“_‚Ì‰ñ“]—pProgram
    //|||||||||||||||||||||/

    // y²‚Ì‰ñ“]‚ğŠi”[@‰ñ“]§Œä—p
    float verticalMouseInput;

    /// <summary>
    /// Player‚Ì‹“_‰ñ“]ˆ—
    /// </summary>
    /// <param name="rotaInput">‰ñ“]‚Ì‚½‚ß‚Ì“ü—Íî•ñ</param>
    /// <param name="rotaSpeed">‰ñ“]‘¬“x</param>
    /// <param name="rotationRange">‰ñ“]”ÍˆÍ</param>
    public void Rotation(Vector2 rotaInput, float rotaSpeed, float rotationRange)
    {
        //•Ï”‚Éy²‚Ìƒ}ƒEƒX“ü—Í•ª‚Ì”’l‚ğ‘«‚·
        verticalMouseInput += rotaInput.y * rotaSpeed;

        //•Ï”‚Ì”’l‚ğŠÛ‚ß‚éiã‰º‚Ì‹“_”ÍˆÍ§Œäj
        verticalMouseInput = Mathf.Clamp(verticalMouseInput, -rotationRange, rotationRange);

        //c‚Ì‹“_‰ñ“]‚ğ”½‰f
        viewPoint.rotation = Quaternion.Euler
            (-verticalMouseInput,                       //-‚ğ•t‚¯‚È‚¢‚Æã‰º”½“]
            viewPoint.transform.rotation.eulerAngles.y,
            viewPoint.transform.rotation.eulerAngles.z);
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