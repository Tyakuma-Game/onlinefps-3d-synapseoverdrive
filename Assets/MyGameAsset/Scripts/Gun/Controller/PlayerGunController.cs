using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

namespace Guns
{
    /// <summary>
    /// 銃の種類を表す列挙体
    /// </summary>
    enum GunType
    {
        HandGun,        // ハンドガン
        AssaultRifle,   // アサルトライフル
        SniperRifle     // スナイパーラフル
    }

    /// <summary>
    /// プレイヤーの銃管理クラス
    /// </summary>
    public class PlayerGunController : MonoBehaviourPunCallbacks
    {
        //−−−−−−−−−−−−−−−−−−−−−−−−−−−/
        //　改善部分
        //−−−−−−−−−−−−−−−−−−−−−−−−−−−/

        [SerializeField] Animator gunAnimator;                  // アクションに統合する感じでリファクタリングする！
        [SerializeField] CameraController cameraController;     // アクションに統合する感じでリファクタリングする!
        [SerializeField] PlayerAnimator playerAnimator;         // アクションに統合する感じでリファクタリングする！

        //−−−−−−−−−−−−−−−−−−−−−−−−−−−/

        [Header(" データ関連 ")]
        [SerializeField] GunData[] gunDates;                // 銃データ一覧
        List<GameObject> guns = new List<GameObject>();     // 銃Data管理用
        List<int> ammunition = new List<int>();             // 現在の所持弾薬
        List<int> ammoClip = new List<int>();               // マガジン内の弾薬
        GunType selectedGunType = GunType.HandGun;          // 現在選択中の銃種類
        float shotTimer;                                    // 射撃間隔

        [Header(" 見た目関連 ")]
        [SerializeField] GameObject[] gunsHolder;       // 自分視点の銃
        [SerializeField] GameObject[] otherGunsHolder;  // 相手視点の銃


        void Start()
        {
            // 使用する銃ホルダー選択（自分視点か相手視点かを基に決定）
            GameObject[] selectedGunsHolder = photonView.IsMine ? gunsHolder : otherGunsHolder;

            // 銃登録
            foreach (GameObject gun in selectedGunsHolder)
                guns.Add(gun);

            // 自身の場合のみ、弾薬とマガジンの初期化
            if (photonView.IsMine)
            {
                foreach (var gun in gunDates)
                {
                    ammunition.Add(gun.MaxAmmunition);  // 所持弾薬
                    ammoClip.Add(gun.MaxAmmoClip);      // マガジン内弾薬
                }
            }

            // 銃の表示切替
            ChangeActiveGun();
        }

        void Update()
        {
            // 自分以外なら処理終了
            if (!photonView.IsMine)
                return;

            // 銃の切り替え
            SwitchingGuns();

            // 覗き込み
            Aim();

            // 射撃関数
            Fire();

            // リロード関数
            if (Input.GetKeyDown(KeyCode.R))
                Reload();

            // 弾薬テキスト更新
            UIManager.instance.SettingBulletsText(gunDates[(int)selectedGunType].MaxAmmoClip,
                ammoClip[(int)selectedGunType], ammunition[(int)selectedGunType]);
        }

        //−−−−−−−−−−−−−−−−−−−−−−−−−−−/
        //　武器切り替え
        //−−−−−−−−−−−−−−−−−−−−−−−−−−−/

        /// <summary>
        /// 銃の切り替えキー入力を検知
        /// </summary>
        public void SwitchingGuns()
        {
            int gunCount = Enum.GetValues(typeof(GunType)).Length;

            // マウスホイールでの銃の切り替え
            float mouseScroll = Input.GetAxisRaw("Mouse ScrollWheel");
            if (mouseScroll != 0f)
                UpdateSelectedGunType((int)Mathf.Sign(mouseScroll), gunCount);

            // 数値キーでの銃の切り替え
            for (int i = 0; i < guns.Count; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    SetGunTypeAndNotify((GunType)i);
                    break; // キーが押されたらループを抜ける
                }
            }
        }

        /// <summary>
        /// 銃のタイプを更新し、変更を通知
        /// </summary>
        void UpdateSelectedGunType(int direction, int gunCount)
        {
            // 銃のタイプを更新
            selectedGunType += direction;

            // 範囲外にならないように調整
            if (selectedGunType < 0)
            {
                selectedGunType = (GunType)(gunCount - 1);
            }
            else if ((int)selectedGunType >= gunCount)
            {
                selectedGunType = GunType.HandGun;
            }

            // 更新後の銃のタイプを設定し、通知
            SetGunTypeAndNotify(selectedGunType);
        }

        /// <summary>
        /// 銃のタイプを設定し、変更を通知する
        /// </summary>
        void SetGunTypeAndNotify(GunType gunType)
        {
            selectedGunType = gunType;
            gunAnimator.SetTrigger("WeaponChange");
            photonView.RPC("SetGun", RpcTarget.All, (int)selectedGunType);
        }

        /// <summary>
        /// 銃の切り替え処理
        /// </summary>
        [PunRPC]
        public void SetGun(int gunNo)
        {
            //銃の切り替え
            if (gunNo < Enum.GetValues(typeof(GunType)).Length)
            {
                //銃の番号をセット
                selectedGunType = (GunType)gunNo;

                // アニメーション
                playerAnimator.IsWeaponChange();

                // 指定時間後切り替え
                StartCoroutine(DelayedSwitchGun(1f));
            }
        }

        /// <summary>
        /// Photonで呼び出す武器変更処理
        /// </summary>
        /// <param name="waitTime">待ち時間</param>
        IEnumerator DelayedSwitchGun(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            ChangeActiveGun();
        }

        /// <summary>
        /// 銃の表示切り替え
        /// </summary>
        void ChangeActiveGun()
        {
            // 全ての銃を非表示に
            foreach (GameObject gun in guns)
                gun.gameObject.SetActive(false);

            // 選択中の銃のみ表示する
            guns[(int)selectedGunType].SetActive(true);
        }

        //−−−−−−−−−−−−−−−−−−−−−−−−−−−/
        //　ズーム処理
        //−−−−−−−−−−−−−−−−−−−−−−−−−−−/

        /// <summary>
        /// 右クリックで覗き込み
        /// </summary>
        public void Aim()
        {
            //マウス右ボタン押しているとき
            if (Input.GetMouseButton(1))
            {
                // アニメーション
                gunAnimator.SetBool("IsZoom", true);

                //ズームイン
                cameraController.AdjustCameraZoom(gunDates[(int)selectedGunType].AdsZoom, gunDates[(int)selectedGunType].AdsSpeed);
            }
            else
            {
                // アニメーション
                gunAnimator.SetBool("IsZoom", false);

                //ズームアウト(60f = カメラのデフォルト絞り値 後ほど修正する)
                cameraController.AdjustCameraZoom(60f,gunDates[(int)selectedGunType].AdsSpeed);
            }
        }


        //−−−−−−−−−−−−−−−−−−−−−−−−−−−/
        //　発射処理とリロード
        //−−−−−−−−−−−−−−−−−−−−−−−−−−−/

        /// <summary>
        /// 左クリックの検知
        /// </summary>
        public void Fire()
        {
            if (Input.GetMouseButton(0) && Time.time > shotTimer)
            {
                // 弾薬の残りがあるか判定
                if (ammoClip[(int)selectedGunType] == 0)
                {
                    // 弾切れの音を鳴らす
                    // アニメーションを使用する方法に分ける
                    //photonView.RPC("NotShotSound", RpcTarget.All);

                    // オートリロード
                    Reload();

                    // 処理終了
                    return;
                }

                //銃の発射処理
                FiringBullet();
            }
        }

        /// <summary>
        /// 弾丸の発射
        /// </summary>
        void FiringBullet()
        {
            // アニメーション
            gunAnimator.SetTrigger("Attack");

            // アニメーション
            playerAnimator.Attack(AttackType.Short);

            //Ray(光線)をカメラの中央から設定
            Vector2 pos = new Vector2(.5f, .5f);
            Ray ray = cameraController.GenerateRay(pos);

            //レイを発射
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //プレイヤーにぶつかった場合
                if (hit.collider.gameObject.tag == "Player")
                {
                    //血のエフェクトをネットワーク上に生成
                    PhotonNetwork.Instantiate(gunDates[(int)selectedGunType].PlayerHitEffect.name, hit.point, Quaternion.identity);

                    // ヒット関数を全プレイヤーで呼び出して撃たれたプレイヤーのHPを同期
                    hit.collider.gameObject.GetPhotonView().RPC("Hit",
                        RpcTarget.All,
                        gunDates[(int)selectedGunType].ShotDamage,
                        photonView.Owner.NickName,
                        PhotonNetwork.LocalPlayer.ActorNumber);
                }
                else
                {
                    //弾痕エフェクト生成 
                    GameObject bulletImpactObject = Instantiate(gunDates[(int)selectedGunType].NonPlayerHitEffect,
                        hit.point + (hit.normal * .002f),                   //オブジェクトから少し浮かしてちらつき防止
                        Quaternion.LookRotation(hit.normal, Vector3.up));   //直角の方向を返してその方向に回転させる

                    //時間経過で削除
                    Destroy(bulletImpactObject, 10f);
                }
            }

            //射撃間隔を設定
            shotTimer = Time.time + gunDates[(int)selectedGunType].ShootInterval;

            //選択中の銃の弾薬減らす
            ammoClip[(int)selectedGunType]--;
        }


        /// <summary>
        /// リロード
        /// </summary>
        void Reload()
        {
            int gunTypeIndex = (int)selectedGunType;

            // リロード補充分の弾数計算
            int ammoToReload = Math.Min(gunDates[gunTypeIndex].MaxAmmoClip - ammoClip[gunTypeIndex], ammunition[gunTypeIndex]);

            // 弾薬がある場合のみリロード
            if (ammoToReload > 0)
            {
                //　TODO: 時間がある際にここを調整する
                // アニメーション
                // gunAnimator.SetTrigger("Reload");

                // 所持弾薬を更新と弾薬装填
                ammunition[gunTypeIndex] -= ammoToReload;
                ammoClip[gunTypeIndex] += ammoToReload;
            }
        }
    }
}