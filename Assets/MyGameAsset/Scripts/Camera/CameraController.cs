using UnityEngine;
using System;
using Photon.Pun;

// TODO: ‚±‚ê‚àEvent‹ì“®Œ^‚É•ÏX‚µ‚ÄŒø—¦‰»‚·‚éI

/// <summary>
/// ƒJƒƒ‰‚ÉŠÖ‚·‚éˆ—‚ğ‚Ü‚Æ‚ß‚ÄŠÇ—‚·‚éƒNƒ‰ƒX
/// </summary>
public class CameraController : MonoBehaviourPunCallbacks
{
    [Tooltip("ƒJƒƒ‰‚ÌˆÊ’uƒIƒuƒWƒFƒNƒg")]
    [SerializeField] Transform viewPoint;
    Camera myCamera;

    void Start()
    {
        // ©g‚ª‘€ì‚·‚éƒIƒuƒWƒFƒNƒg‚Å‚È‚¯‚ê‚Îˆ—‚ğƒXƒLƒbƒv
        if (!photonView.IsMine)
            return;

        // æ“¾
        myCamera = Camera.main;
    }

    void Update()
    {
        // ©g‚ª‘€ì‚·‚éƒIƒuƒWƒFƒNƒg‚Å‚È‚¯‚ê‚Îˆ—‚ğƒXƒLƒbƒv
        if (!photonView.IsMine)
            return;

        // ˆÊ’uXV
        myCamera.transform.position = viewPoint.position;
        myCamera.transform.rotation = viewPoint.rotation;
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