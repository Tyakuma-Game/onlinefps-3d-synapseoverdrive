using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���_�̃Y�[���Ɋւ��鏈��
/// </summary>
public class PlayerViewPointZoom : MonoBehaviour, IPlayerViewPointZoom
{
    /// <summary>
    /// �J�n�n�_���珙�X�ɃY�[������
    /// </summary>
    /// <param name="camera">�Ώۂ̃J����</param>
    /// <param name="adsZoom">�Y�[���{��</param>
    /// <param name="adsSpeed">�Y�[�����x</param>
    public void GunZoomIn(Camera camera, float adsZoom, float adsSpeed)
    {
        camera.fieldOfView = Mathf.Lerp(
            camera.fieldOfView,         //�J�n�n�_
            adsZoom,                    //�ړI�n�_
            adsSpeed * Time.deltaTime); //�⊮���l
    }

    /// <summary>
    /// ���̒n�_�ɏ��X�ɖ߂�
    /// </summary>
    /// <param name="camera">�Ώۂ̃J����</param>
    /// <param name="adsZoom">�Y�[���O�J�����{��</param>
    /// <param name="adsSpeed">�Y�[�����x</param>
    public void GunZoomOut(Camera camera, float CameraBaseFactor, float adsSpeed)
    {
        camera.fieldOfView = Mathf.Lerp(
            camera.fieldOfView,         //�J�n�n�_
            CameraBaseFactor,           //�ړI�n�_
            adsSpeed * Time.deltaTime); //�⊮���l
    }
}