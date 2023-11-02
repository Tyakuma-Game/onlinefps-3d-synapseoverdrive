using Photon.Pun;
using UnityEngine;

///<summary>
/// ���̃v���C���[�̍��W�Ɖ�]���Ԃ���N���X
///</summary>
public class AvatarTransformView : MonoBehaviourPunCallbacks, IPunObservable
{
    // ��Ԃɂ����鎞��
    const float InterpolationPeriod = 0.1f;

    Vector3 p1;         // ��Ԃ̊J�n���W
    Vector3 p2;         // ��Ԃ̏I�����W
    Vector3 v1;         // ��Ԃ̊J�n���x
    Vector3 v2;         // ��Ԃ̏I�����x
    Quaternion r1;      // ��Ԃ̊J�n��]���
    Quaternion r2;      // ��Ԃ̏I����]���
    float elapsedTime;  // �o�ߎ���

    // ���̃v���C���[����~���Ă��邩
    bool isOtherPlayerMoving = true;

    ///<summary>
    /// ����������
    ///</summary>
    void Start()
    {
        p1 = transform.position;
        p2 = p1;
        v1 = Vector3.zero;
        v2 = v1;
        r1 = transform.rotation;
        r2 = r1;
        elapsedTime = Time.deltaTime;
    }

    ///<summary>
    /// �t���[�����̍X�V����
    ///</summary>
    void Update()
    {
        if (photonView.IsMine)
        {
            // ���g�̃l�b�g���[�N�I�u�W�F�N�g�́A���t���[���̈ړ��ʂƌo�ߎ��Ԃ��L�^
            p1 = p2;
            p2 = transform.position;
            r1 = r2;
            r2 = transform.rotation;
            elapsedTime = Time.deltaTime;
        }
        else
        {
            // ���v���C���[�̃l�b�g���[�N�I�u�W�F�N�g�́A��ԏ���
            elapsedTime += Time.deltaTime;

            // ���W�ړ��̕��
            if (isOtherPlayerMoving)
            {
                if (elapsedTime < InterpolationPeriod)
                {
                    transform.position = HermiteSpline.Interpolate(p1, p2, v1, v2, elapsedTime / InterpolationPeriod);
                }
                else
                {
                    transform.position = Vector3.LerpUnclamped(p1, p2, elapsedTime / InterpolationPeriod);
                }
            }

            // ��]�����̕��
            transform.rotation = Quaternion.SlerpUnclamped(r1, r2, elapsedTime / InterpolationPeriod);
        }
    }

    ///<summary>
    /// �l�b�g���[�N��������
    ///</summary>
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // ���g�̃f�[�^���M
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

            // ���t���[���̈ړ��ʂƌo�ߎ��Ԃ���A�b�������߂đ��M
            stream.SendNext((p2 - p1) / elapsedTime);
        }
        else
        {
            // ���v���C���[�̃f�[�^����M
            var networkPosition = (Vector3)stream.ReceiveNext();
            var networkRotation = (Quaternion)stream.ReceiveNext();
            var networkVelocity = (Vector3)stream.ReceiveNext();
            var lag = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - info.SentServerTimestamp) / 1000f);

            // ��M���̍��W���A��Ԃ̊J�n���W��
            p1 = transform.position;
            // ���ݎ����ɂ�����\�����W���A��Ԃ̏I�����W��
            p2 = networkPosition + networkVelocity * lag;
            // �O��̕�Ԃ̏I�����x���A��Ԃ̊J�n���x��
            v1 = v2;
            // ��M�����b�����A��Ԃɂ����鎞�Ԃ�����̑��x�ɕϊ����āA��Ԃ̏I�����x�ɂ���
            v2 = networkVelocity * InterpolationPeriod;
            // ��M���̉�]�����A��Ԃ̊J�n��]�ɂ���
            r1 = transform.rotation;
            // ���ݎ����ɂ�����\����]�����A��Ԃ̏I����]�ɂ���
            r2 = networkRotation;
            // �o�ߎ��Ԃ����Z�b�g
            elapsedTime = 0f;

            // ���̃v���C���[����~���Ă��邩�ǂ����𔻒�
            isOtherPlayerMoving = networkVelocity.magnitude > 0.01f;
        }
    }
}