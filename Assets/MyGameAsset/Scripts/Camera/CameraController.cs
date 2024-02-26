using UnityEngine;
using Photon.Pun;

/// <summary>
/// ƒJƒƒ‰‚ÉŠÖ‚·‚éˆ—‚ğ‚Ü‚Æ‚ß‚ÄŠÇ—‚·‚éƒNƒ‰ƒX
/// </summary>
public class CameraController : MonoBehaviourPunCallbacks
{
    [Header(" Elements ")]
    [SerializeField] Transform viewPoint;
    Camera myCamera;

    void Start()
    {
        if (!photonView.IsMine)
            return;

        myCamera = Camera.main;
    }

    void Update()
    { 
        if (!photonView.IsMine)
            return;

        // ˆÊ’uXV
        myCamera.transform.position = viewPoint.position;
        myCamera.transform.rotation = viewPoint.rotation;
    }

    //|||||||||||||||||||||||||||/
    //@Ray¶¬
    //|||||||||||||||||||||||||||/

    // TODO:
    // ‚±‚Ìˆ—‚ğ•ÊƒNƒ‰ƒX‚É•ªŠ„‚·‚éI

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