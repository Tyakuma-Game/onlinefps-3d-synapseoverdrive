using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.InputSystem;

namespace Guns
{
    /// <summary>
    /// e‚Ìí—Ş‚ğ•\‚·—ñ‹“‘Ì
    /// </summary>
    enum GunType
    {
        HandGun,        // ƒnƒ“ƒhƒKƒ“
        AssaultRifle,   // ƒAƒTƒ‹ƒgƒ‰ƒCƒtƒ‹
        SniperRifle     // ƒXƒiƒCƒp[ƒ‰ƒtƒ‹
    }

    /// <summary>
    /// ƒvƒŒƒCƒ„[‚ÌeŠÇ—ƒNƒ‰ƒX
    /// </summary>
    public class PlayerGunController : MonoBehaviourPunCallbacks
    {
        //|||||||||||||||||||||||||||/
        //@‰ü‘P•”•ª
        //|||||||||||||||||||||||||||/


        [SerializeField] Animator gunAnimator;                  // ƒAƒNƒVƒ‡ƒ“‚É“‡‚·‚éŠ´‚¶‚ÅƒŠƒtƒ@ƒNƒ^ƒŠƒ“ƒO‚·‚éI
        [SerializeField] CameraController cameraController;     // ƒAƒNƒVƒ‡ƒ“‚É“‡‚·‚éŠ´‚¶‚ÅƒŠƒtƒ@ƒNƒ^ƒŠƒ“ƒO‚·‚é!
        [SerializeField] PlayerAnimator playerAnimator;         // ƒAƒNƒVƒ‡ƒ“‚É“‡‚·‚éŠ´‚¶‚ÅƒŠƒtƒ@ƒNƒ^ƒŠƒ“ƒO‚·‚éI

        //|||||||||||||||||||||||||||/

        [Header(" ƒf[ƒ^ŠÖ˜A ")]
        [SerializeField] GunData[] gunDates;                // eƒf[ƒ^ˆê——
        List<GameObject> guns = new List<GameObject>();     // eDataŠÇ——p
        List<int> ammunition = new List<int>();             // Œ»İ‚ÌŠ’e–ò
        List<int> ammoClip = new List<int>();               // ƒ}ƒKƒWƒ““à‚Ì’e–ò
        GunType selectedGunType = GunType.HandGun;          // Œ»İ‘I‘ğ’†‚Ìeí—Ş
        float shotTimer;                                    // ËŒ‚ŠÔŠu

        [Header(" Œ©‚½–ÚŠÖ˜A ")]
        [SerializeField] GameObject[] gunsHolder;       // ©•ª‹“_‚Ìe
        [SerializeField] GameObject[] otherGunsHolder;  // ‘Šè‹“_‚Ìe


        void Start()
        {
            // g—p‚·‚éeƒzƒ‹ƒ_[‘I‘ği©•ª‹“_‚©‘Šè‹“_‚©‚ğŠî‚ÉŒˆ’èj
            GameObject[] selectedGunsHolder = photonView.IsMine ? gunsHolder : otherGunsHolder;

            // e“o˜^
            foreach (GameObject gun in selectedGunsHolder)
                guns.Add(gun);

            // ©g‚Ìê‡‚Ì‚İA’e–ò‚Æƒ}ƒKƒWƒ“‚Ì‰Šú‰»
            if (photonView.IsMine)
            {
                foreach (var gun in gunDates)
                {
                    ammunition.Add(gun.MaxAmmunition);  // Š’e–ò
                    ammoClip.Add(gun.MaxAmmoClip);      // ƒ}ƒKƒWƒ““à’e–ò
                }

                // ƒY[ƒ€ŠÖ˜Aˆ—“o˜^
                InputManager.Controls.Gun.Zoom.started += ZoomIn;
                InputManager.Controls.Gun.Zoom.canceled += ZoomOut;
            }

            // e‚Ì•\¦Ø‘Ö
            ChangeActiveGun();
        }
        void OnDestroy()
        {
            // ©g‚ª‘€ì‚·‚éƒIƒuƒWƒFƒNƒg‚Å‚È‚¯‚ê‚Îˆ—‚ğƒXƒLƒbƒv
            if (!photonView.IsMine)
                return;

            // ƒY[ƒ€ŠÖ˜Aˆ—‰ğœ
            InputManager.Controls.Gun.Zoom.started -= ZoomIn;
            InputManager.Controls.Gun.Zoom.canceled -= ZoomOut;
        }

        void Update()
        {
            // ©•ªˆÈŠO‚È‚çˆ—I—¹
            if (!photonView.IsMine)
                return;

            // e‚ÌØ‚è‘Ö‚¦
            SwitchingGuns();

            // ËŒ‚ŠÖ”
            Fire();

            // ƒŠƒ[ƒhŠÖ”
            if (Input.GetKeyDown(KeyCode.R))
                Reload();

            // ’e–òƒeƒLƒXƒgXV
            UIManager.instance.SettingBulletsText(gunDates[(int)selectedGunType].MaxAmmoClip,
                ammoClip[(int)selectedGunType], ammunition[(int)selectedGunType]);
        }

        //|||||||||||||||||||||||||||/
        //@•ŠíØ‚è‘Ö‚¦
        //|||||||||||||||||||||||||||/

        /// <summary>
        /// e‚ÌØ‚è‘Ö‚¦ƒL[“ü—Í‚ğŒŸ’m
        /// </summary>
        public void SwitchingGuns()
        {
            int gunCount = Enum.GetValues(typeof(GunType)).Length;

            // ƒ}ƒEƒXƒzƒC[ƒ‹‚Å‚Ìe‚ÌØ‚è‘Ö‚¦
            float mouseScroll = Input.GetAxisRaw("Mouse ScrollWheel");
            if (mouseScroll != 0f)
                UpdateSelectedGunType((int)Mathf.Sign(mouseScroll), gunCount);

            // ”’lƒL[‚Å‚Ìe‚ÌØ‚è‘Ö‚¦
            for (int i = 0; i < guns.Count; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    SetGunTypeAndNotify((GunType)i);
                    break; // ƒL[‚ª‰Ÿ‚³‚ê‚½‚çƒ‹[ƒv‚ğ”²‚¯‚é
                }
            }
        }

        /// <summary>
        /// e‚Ìƒ^ƒCƒv‚ğXV‚µA•ÏX‚ğ’Ê’m
        /// </summary>
        void UpdateSelectedGunType(int direction, int gunCount)
        {
            // e‚Ìƒ^ƒCƒv‚ğXV
            selectedGunType += direction;

            // ”ÍˆÍŠO‚É‚È‚ç‚È‚¢‚æ‚¤‚É’²®
            if (selectedGunType < 0)
            {
                selectedGunType = (GunType)(gunCount - 1);
            }
            else if ((int)selectedGunType >= gunCount)
            {
                selectedGunType = GunType.HandGun;
            }

            // XVŒã‚Ìe‚Ìƒ^ƒCƒv‚ğİ’è‚µA’Ê’m
            SetGunTypeAndNotify(selectedGunType);
        }

        /// <summary>
        /// e‚Ìƒ^ƒCƒv‚ğİ’è‚µA•ÏX‚ğ’Ê’m‚·‚é
        /// </summary>
        void SetGunTypeAndNotify(GunType gunType)
        {
            selectedGunType = gunType;
            gunAnimator.SetTrigger("WeaponChange");
            photonView.RPC("SetGun", RpcTarget.All, (int)selectedGunType);
        }

        /// <summary>
        /// e‚ÌØ‚è‘Ö‚¦ˆ—
        /// </summary>
        [PunRPC]
        public void SetGun(int gunNo)
        {
            //e‚ÌØ‚è‘Ö‚¦
            if (gunNo < Enum.GetValues(typeof(GunType)).Length)
            {
                //e‚Ì”Ô†‚ğƒZƒbƒg
                selectedGunType = (GunType)gunNo;

                // ƒAƒjƒ[ƒVƒ‡ƒ“
                playerAnimator.IsWeaponChange();

                // w’èŠÔŒãØ‚è‘Ö‚¦
                StartCoroutine(DelayedSwitchGun(1f));
            }
        }

        /// <summary>
        /// Photon‚ÅŒÄ‚Ño‚·•Ší•ÏXˆ—
        /// </summary>
        /// <param name="waitTime">‘Ò‚¿ŠÔ</param>
        IEnumerator DelayedSwitchGun(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            ChangeActiveGun();
        }

        /// <summary>
        /// e‚Ì•\¦Ø‚è‘Ö‚¦
        /// </summary>
        void ChangeActiveGun()
        {
            // ‘S‚Ä‚Ìe‚ğ”ñ•\¦‚É
            foreach (GameObject gun in guns)
                gun.gameObject.SetActive(false);

            // ‘I‘ğ’†‚Ìe‚Ì‚İ•\¦‚·‚é
            guns[(int)selectedGunType].SetActive(true);
        }

        //|||||||||||||||||||||||||||/
        //@ƒY[ƒ€ˆ—
        //|||||||||||||||||||||||||||/

        /// <summary>
        /// e‚ÌƒY[ƒ€ó‘Ô•ÏX‚ÌƒCƒxƒ“ƒgƒnƒ“ƒhƒ‰
        /// </summary>
        public static Action<bool> OnGunZoomStateChanged;

        /// <summary>
        /// ƒY[ƒ€ŠJn
        /// </summary>
        void ZoomIn(InputAction.CallbackContext context)
        {
            OnGunZoomStateChanged?.Invoke(true);
            CameraZoom.OnZoomStateChanged?.Invoke(gunDates[(int)selectedGunType].AdsZoom, gunDates[(int)selectedGunType].AdsSpeed);
        }

        /// <summary>
        /// ƒY[ƒ€I—¹
        /// </summary>
        void ZoomOut(InputAction.CallbackContext context)
        {
            OnGunZoomStateChanged?.Invoke(false);
            CameraZoom.OnZoomStateChanged?.Invoke(60f, gunDates[(int)selectedGunType].AdsSpeed);
        }


        //|||||||||||||||||||||||||||/
        //@”­Ëˆ—‚ÆƒŠƒ[ƒh
        //|||||||||||||||||||||||||||/

        /// <summary>
        /// ¶ƒNƒŠƒbƒN‚ÌŒŸ’m
        /// </summary>
        public void Fire()
        {
            if (Input.GetMouseButton(0) && Time.time > shotTimer)
            {
                // ’e–ò‚Ìc‚è‚ª‚ ‚é‚©”»’è
                if (ammoClip[(int)selectedGunType] == 0)
                {
                    // ’eØ‚ê‚Ì‰¹‚ğ–Â‚ç‚·
                    // ƒAƒjƒ[ƒVƒ‡ƒ“‚ğg—p‚·‚é•û–@‚É•ª‚¯‚é
                    //photonView.RPC("NotShotSound", RpcTarget.All);

                    // ƒI[ƒgƒŠƒ[ƒh
                    Reload();

                    // ˆ—I—¹
                    return;
                }

                //e‚Ì”­Ëˆ—
                FiringBullet();
            }
        }

        /// <summary>
        /// ’eŠÛ‚Ì”­Ë
        /// </summary>
        void FiringBullet()
        {
            // ƒAƒjƒ[ƒVƒ‡ƒ“
            gunAnimator.SetTrigger("Attack");

            // ƒAƒjƒ[ƒVƒ‡ƒ“
            playerAnimator.Attack(AttackType.Short);

            //Ray(Œõü)‚ğƒJƒƒ‰‚Ì’†‰›‚©‚çİ’è
            Vector2 pos = new Vector2(.5f, .5f);
            Ray ray = cameraController.GenerateRay(pos);

            //ƒŒƒC‚ğ”­Ë
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //ƒvƒŒƒCƒ„[‚É‚Ô‚Â‚©‚Á‚½ê‡
                if (hit.collider.gameObject.tag == "Player")
                {
                    //ŒŒ‚ÌƒGƒtƒFƒNƒg‚ğƒlƒbƒgƒ[ƒNã‚É¶¬
                    PhotonNetwork.Instantiate(gunDates[(int)selectedGunType].PlayerHitEffect.name, hit.point, Quaternion.identity);

                    // ƒqƒbƒgŠÖ”‚ğ‘SƒvƒŒƒCƒ„[‚ÅŒÄ‚Ño‚µ‚ÄŒ‚‚½‚ê‚½ƒvƒŒƒCƒ„[‚ÌHP‚ğ“¯Šú
                    hit.collider.gameObject.GetPhotonView().RPC("Hit",
                        RpcTarget.All,
                        gunDates[(int)selectedGunType].ShotDamage,
                        photonView.Owner.NickName,
                        PhotonNetwork.LocalPlayer.ActorNumber);
                }
                else
                {
                    //’e­ƒGƒtƒFƒNƒg¶¬ 
                    GameObject bulletImpactObject = Instantiate(gunDates[(int)selectedGunType].NonPlayerHitEffect,
                        hit.point + (hit.normal * .002f),                   //ƒIƒuƒWƒFƒNƒg‚©‚ç­‚µ•‚‚©‚µ‚Ä‚¿‚ç‚Â‚«–h~
                        Quaternion.LookRotation(hit.normal, Vector3.up));   //’¼Šp‚Ì•ûŒü‚ğ•Ô‚µ‚Ä‚»‚Ì•ûŒü‚É‰ñ“]‚³‚¹‚é

                    //ŠÔŒo‰ß‚Åíœ
                    Destroy(bulletImpactObject, 10f);
                }
            }

            //ËŒ‚ŠÔŠu‚ğİ’è
            shotTimer = Time.time + gunDates[(int)selectedGunType].ShootInterval;

            //‘I‘ğ’†‚Ìe‚Ì’e–òŒ¸‚ç‚·
            ammoClip[(int)selectedGunType]--;
        }


        /// <summary>
        /// ƒŠƒ[ƒh
        /// </summary>
        void Reload()
        {
            int gunTypeIndex = (int)selectedGunType;

            // ƒŠƒ[ƒh•â[•ª‚Ì’e”ŒvZ
            int ammoToReload = Math.Min(gunDates[gunTypeIndex].MaxAmmoClip - ammoClip[gunTypeIndex], ammunition[gunTypeIndex]);

            // ’e–ò‚ª‚ ‚éê‡‚Ì‚İƒŠƒ[ƒh
            if (ammoToReload > 0)
            {
                //@TODO: ŠÔ‚ª‚ ‚éÛ‚É‚±‚±‚ğ’²®‚·‚é
                // ƒAƒjƒ[ƒVƒ‡ƒ“
                // gunAnimator.SetTrigger("Reload");

                // Š’e–ò‚ğXV‚Æ’e–ò‘•“U
                ammunition[gunTypeIndex] -= ammoToReload;
                ammoClip[gunTypeIndex] += ammoToReload;
            }
        }
    }
}