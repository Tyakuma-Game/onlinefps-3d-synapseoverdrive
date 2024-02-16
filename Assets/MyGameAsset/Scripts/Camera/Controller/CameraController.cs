using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// ƒJƒƒ‰‚ÉŠÖ‚·‚éˆ—‚ğ‚Ü‚Æ‚ß‚ÄŠÇ—‚·‚éƒNƒ‰ƒX
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("ƒJƒƒ‰‚ÌŒ³‚Ìi‚è”{—¦")]
    [SerializeField] float CAMERA_APERTURE_BASE_FACTOR = 60f;

    [Tooltip("ƒJƒƒ‰‚ÌˆÊ’uƒIƒuƒWƒFƒNƒg")]
    [SerializeField] Transform viewPoint;

    [Tooltip("ƒJƒƒ‰‚ÌˆÊ’uƒIƒuƒWƒFƒNƒg‚Ì—\”õ")]
    [SerializeField] Transform sabViewPoint;

    // ‘€ì‚·‚éƒJƒƒ‰ƒIƒuƒWƒFƒNƒg
    Camera myCamera;


    ICameraZoom cameraZoom;
    ICameraRay cameraRay;

    void Start()
    {
        // ƒJƒƒ‰Ši”[
        myCamera = Camera.main;

        // ˆ—æ“¾
        cameraZoom = GetComponent<ICameraZoom>();
        cameraRay = GetComponent<ICameraRay>();
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
    // Damage‚Ì—h‚êˆ—
    //|||||||||||||||||||||/

    public void Shake()
    {
        shakeCount = 0;
        StartCoroutine(ViewPointShake());
    }

    float shakeMagnitude = 0.2f;
    float shakeTime = 0.1f;
    float shakeCount = 0;

    IEnumerator ViewPointShake()
    {
        while(shakeCount < shakeTime)
        {
            float x = sabViewPoint.transform.position.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = sabViewPoint.transform.position.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            viewPoint.transform.position = new Vector3(x,y, sabViewPoint.transform.position.z);
            myCamera.transform.position = viewPoint.transform.position;

            shakeCount += Time.deltaTime;

            yield return null;
        }
        viewPoint.transform.position = sabViewPoint.transform.position;
    }

    //|||||||||||||||||||||/

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

    /// <summary>
    /// ŠJn’n“_‚©‚ç™X‚ÉƒY[ƒ€‚·‚é
    /// </summary>
    /// <param name="adsZoom">ƒY[ƒ€”{—¦</param>
    /// <param name="adsSpeed">ƒY[ƒ€‘¬“x</param>
    public void GunZoomIn(float adsZoom,float adsSpeed)
    {
        cameraZoom.GunZoomIn(myCamera,adsZoom,adsSpeed);
    }

    /// <summary>
    /// Œ³‚Ì’n“_‚É™X‚É–ß‚·
    /// </summary>
    /// <param name="adsSpeed">ƒY[ƒ€‘¬“x</param>
    public void GunZoomOut(float adsSpeed)
    {
        cameraZoom.GunZoomOut(myCamera, CAMERA_APERTURE_BASE_FACTOR, adsSpeed);
    }

    /// <summary>
    /// ƒJƒƒ‰‚©‚çêŠ‚ğw’è‚µ‚ÄRay‚ğ¶¬
    /// </summary>
    /// <param name="camera">¶¬‚·‚éƒJƒƒ‰</param>
    /// <param name="generationPos">¶¬‚·‚éÀ•W</param>
    /// <returns>¶¬‚µ‚½Ray</returns>
    public Ray GenerateRay(Vector2 generationPos)
    {
         return cameraRay.GenerateRay(myCamera, generationPos);
    }
}