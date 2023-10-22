using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;

/// <summary>
/// �v���C���[�̏e�Ǘ��N���X
/// </summary>
public class PlayerGunController : MonoBehaviourPunCallbacks
{
    [Header("�Q��")]
    [Tooltip("Player�̎��_�Ɋւ���N���X")]
    [SerializeField] CameraController cameraController;

    [Header("�e�֘A")]
    [SerializeField] GameObject hitEffect;              //���̃G�t�F�N�g
    [SerializeField] List<Gun> guns = new List<Gun>();  //����̊i�[�z��
    int selectedGun = 0;                                //�I�𒆂̕���Ǘ��p���l
    float shotTimer;                                    //�ˌ��Ԋu

    [Tooltip("�����e��")]
    [SerializeField] int[] ammunition;

    [Tooltip("�ō������e��")]
    [SerializeField] int[] maxAmmunition;

    [Tooltip("�}�K�W�����̒e��")]
    [SerializeField] int[] ammoClip;

    [Tooltip("�}�K�W���ɓ���ő�̐�")]
    [SerializeField] int[] maxAmmoClip;

    [Tooltip("�e�z���_�[ �������_�p")]
    [SerializeField] Gun[] gunsHolder;

    [Tooltip("�e�z���_�[ ���莋�_�p")]
    [SerializeField] Gun[] OtherGunsHolder;


    [SerializeField] List<GunStatus> gunStatus = new List<GunStatus>();

    //UI�Ǘ�
    UIManager uIManager;

    void Start()
    {
        //�����Ȃ̂�
        if (photonView.IsMine)
        {
            //�e�̐������[�v
            foreach (Gun gun in gunsHolder)
            {
                //���X�g�ɒǉ�
                guns.Add(gun);
            }

            //�^�O����UIManager��T��
            uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        }
        else//���l��������OtherGunsHolder��\��
        {
            //�e�̐������[�v
            foreach (Gun gun in OtherGunsHolder)
            {
                //���X�g�ɒǉ�
                guns.Add(gun);
            }
        }

        //�e�̕\���ؑ�
        switchGun();
    }

    void Update()
    {
        //�����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }
        
        //�e�̐؂�ւ�
        SwitchingGuns();

        //�`������
        Aim();

        //�ˌ��֐�
        Fire();

        //�����[�h�֐�
        Reload();

        //�e��e�L�X�g�X�V
        uIManager.SettingBulletsText(GetGunAmmoClipMax(), GetGunAmmoClip(), GetGunAmmunition());

        //�T�E���h�~�߂����
        if (Input.GetMouseButtonUp(0) || ammoClip[2] <= 0)
        {
            //�T�E���h��~
            photonView.RPC("SoundStop", RpcTarget.All);
        }
    }

    /// <summary>
    /// �ݒ莞�Ԗ��Ɏ��s
    /// </summary>
    void FixedUpdate()
    {
        //�����ȊO�Ȃ�
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }

        //�e��e�L�X�g�X�V
        uIManager.SettingBulletsText(GetGunAmmoClipMax(),ammoClip[selectedGun], ammunition[selectedGun]);
    }

    /// <summary>
    /// �e�̐؂�ւ��L�[���͂����m����
    /// </summary>
    public void SwitchingGuns()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            //�����e���Ǘ����鐔�l�𑝂₷
            selectedGun++;

            //���X�g���傫�Ȑ��l�ɂȂ�΂O�ɖ߂�
            if (selectedGun >= guns.Count)
            {
                selectedGun = 0;
            }

            //�e�̐؂�ւ��i���[�����̃v���C���[�S���Ăяo���j
            photonView.RPC("SetGun", RpcTarget.All, selectedGun);
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            //�����e���Ǘ����鐔�l�����炷
            selectedGun--;

            //0��菬������΃��X�g�̍ő吔�|�P�̐��l�ɐݒ�
            if (selectedGun < 0)
            {
                selectedGun = guns.Count - 1;
            }

            //�e�̐؂�ւ��i���[�����̃v���C���[�S���Ăяo���j
            photonView.RPC("SetGun", RpcTarget.All, selectedGun);
        }

        //���l�L�[�̓��͌��m�ŕ����؂�ւ���
        for (int i = 0; i < guns.Count; i++)
        {
            //���[�v�̐��l�{�P�����ĕ�����ɕϊ��B���̌�A�����ꂽ������
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                //�e���������l��ݒ�
                selectedGun = i;

                //�e�̐؂�ւ��i���[�����̃v���C���[�S���Ăяo���j
                photonView.RPC("SetGun", RpcTarget.All, selectedGun);
            }
        }
    }


    /// <summary>
    /// �e�̕\���؂�ւ�
    /// </summary>
    void switchGun()
    {
        //���X�g�����[�v����
        foreach (Gun gun in guns)
        {
            //�e���\��
            gun.gameObject.SetActive(false);
        }

        //�I�𒆂̏e�̂ݕ\��
        guns[selectedGun].gameObject.SetActive(true);
    }


    /// <summary>
    /// �E�N���b�N�Ŕ`������
    /// </summary>
    public void Aim()
    {
        //�}�E�X�E�{�^�������Ă���Ƃ�
        if (Input.GetMouseButton(1))
        {
            //�Y�[���C��
            cameraController.GunZoomIn(guns[selectedGun].adsZoom, guns[selectedGun].adsSpeed);
        }
        else
        {
            //�Y�[���A�E�g
            cameraController.GunZoomOut(guns[selectedGun].adsSpeed);
        }
    }


    /// <summary>
    /// ���N���b�N�̌��m
    /// </summary>
    public void Fire()
    {
        if (Input.GetMouseButton(0) && Time.time > shotTimer)
        {
            // �e��̎c�肪���邩����
            if (ammoClip[selectedGun] == 0)
            {
                photonView.RPC("SoundGeneration", RpcTarget.All);   //����炷�i�e�؂ꉹ�j
                return;                                             //�����I��
            }

            //�e�̔��ˏ���
            FiringBullet();
        }
    }

    //�@���C���̃G�t�F�N�g����
    void ShotEffect()
    {
        //�@���ʉ��̍Đ�
        gunStatus[selectedGun].GetShotSE().Stop();
        gunStatus[selectedGun].GetShotSE().Play();

        //�@����\��
        gunStatus[selectedGun].GetShotLight().enabled = false;
        gunStatus[selectedGun].GetShotLight().enabled = true;

        // ����Object��0.2f�̂ݐ���
        gunStatus[selectedGun].ActiveShotEffect();

        //�@�R���[�`���ŏ������������s
        StartCoroutine(DisableLight());
        StartCoroutine(DisableEffect());

        //�@�e��Effect������
        IEnumerator DisableEffect()
        {
            yield return new WaitForSeconds(0.1f);
            gunStatus[selectedGun].ShotEffectNotActive();
        }

        //�@���C�g����������
        IEnumerator DisableLight()
        {
            yield return new WaitForSeconds(0.1f);
            gunStatus[selectedGun].GetShotLight().enabled = false;
        }
    }


    /// <summary>
    /// �e�ۂ̔���
    /// </summary>
    private void FiringBullet()
    {
        // Effect���U�炷
        ShotEffect();

        Vector2 pos = new Vector2(.5f, .5f);

        //Ray(����)���J�����̒�������ݒ�
        Ray ray = cameraController.GenerateRay(pos);

        //���C�𔭎�
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //�v���C���[�ɂԂ������ꍇ
            if (hit.collider.gameObject.tag == "Player")
            {
                //���̃G�t�F�N�g���l�b�g���[�N��ɐ���
                PhotonNetwork.Instantiate(hitEffect.name, hit.point, Quaternion.identity);

                // �q�b�g�֐���S�v���C���[�ŌĂяo���Č����ꂽ�v���C���[��HP�𓯊�
                hit.collider.gameObject.GetPhotonView().RPC("Hit",
                    RpcTarget.All,
                    guns[selectedGun].shotDamage,
                    photonView.Owner.NickName,
                    PhotonNetwork.LocalPlayer.ActorNumber);
            }
            else
            {
                //�e���G�t�F�N�g���� 
                GameObject bulletImpactObject = Instantiate(guns[selectedGun].bulletImpact,
                    hit.point + (hit.normal * .002f),                   //�I�u�W�F�N�g���班���������Ă�����h�~
                    Quaternion.LookRotation(hit.normal, Vector3.up));   //���p�̕�����Ԃ��Ă��̕����ɉ�]������

                //���Ԍo�߂ō폜
                Destroy(bulletImpactObject, 10f);
            }
        }

        //�ˌ��Ԋu��ݒ�
        shotTimer = Time.time + guns[selectedGun].shootInterval;

        //����炷�i���ˉ��j
        photonView.RPC("SoundGeneration", RpcTarget.All);

        //�I�𒆂̏e�̒e�򌸂炷
        ammoClip[selectedGun]--;
    }


    /// <summary>
    /// �����[�h
    /// </summary>
    private void Reload()
    {
        //�����[�h�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.R))
        {
            //�����[�h�ŕ�[����e�����擾
            int amountNeed = maxAmmoClip[selectedGun] - ammoClip[selectedGun];

            //�K�v�Ȓe��ʂƏ����e��ʂ��r
            int ammoAvailable = amountNeed < ammunition[selectedGun] ? amountNeed : ammunition[selectedGun];

            //�e�򂪖��^���̎��̓����[�h�ł��Ȃ�&�e����������Ă���Ƃ�
            if (amountNeed != 0 && ammunition[selectedGun] != 0)
            {
                //�����e�򂩂烊���[�h����e�򕪂�����
                ammunition[selectedGun] -= ammoAvailable;

                //�e�ɑ��U����
                ammoClip[selectedGun] += ammoAvailable;
            }
        }
    }


    /// <summary>
    /// �e�̐؂�ւ�����
    /// </summary>
    [PunRPC]
    public void SetGun(int gunNo)
    {
        //�e�̐؂�ւ�
        if (gunNo < guns.Count)
        {
            //�e�̔ԍ����Z�b�g
            selectedGun = gunNo;

            //�e�̐؂�ւ�����
            switchGun();
        }
    }


    /// <summary>
    /// ���̔���
    /// </summary>
    [PunRPC]
    public void SoundGeneration()
    {
        //�}�K�W�����ɒe�򂪎c���Ă��邩����
        if (ammoClip[selectedGun] == 0)
        {
            guns[selectedGun].SoundGunOutOfBullets();   //�e�؂�̉���炷
            return;                                     //�I��
        }

        //�A�T���g���C�t����
        if (selectedGun == 2)
        {
            guns[selectedGun].LoopON_SubmachineGun();//���[�v���Ė炷
        }
        else//�Ⴄ�Ȃ�
        {
            guns[selectedGun].SoundGunShot();//�P���Ŗ炷
        }
    }


    /// <summary>
    /// �����~�߂�֐�
    /// </summary>
    [PunRPC]
    public void SoundStop()
    {
        //�A�T���g���C�t����SE���[�v���~
        guns[2].LoopOFF_SubmachineGun();
    }


    /// <summary>
    /// �I�����Ă���e�̏����e��
    /// </summary>
    public int GetGunAmmunition()
    {
        return ammunition[selectedGun];
    }


    /// <summary>
    /// �I�����Ă���e�̃}�K�W�����e��
    /// </summary>
    public int GetGunAmmoClip()
    {
        return ammoClip[selectedGun];
    }


    /// <summary>
    /// �I�����Ă���e�̃}�K�W�����e��ő吔
    /// </summary>
    public int GetGunAmmoClipMax()
    {
        return maxAmmoClip[selectedGun];
    }
}