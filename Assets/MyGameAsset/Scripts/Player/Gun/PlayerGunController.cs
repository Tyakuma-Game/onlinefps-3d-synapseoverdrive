using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

enum GunType
{
    HandGun,
    AssaultRifle,
    SniperRifle
}

/// <summary>
/// プレイヤーの銃管理クラス
/// </summary>
public class PlayerGunController : MonoBehaviourPunCallbacks
{
    [Header(" 銃毎のステータス ")]
    [SerializeField] GunData[] gunDates;
    GunType selectedGunType = GunType.HandGun;
    List<int> ammunition = new List<int>();    // 現在の所持弾薬
    List<int> ammoClip = new List<int>();      // マガジン内の弾薬


    [Header(" 銃の見た目 ")]
    [Tooltip("自分視点用")]
    [SerializeField] GameObject[] gunsHolder;

    [Tooltip("相手視点用")]
    [SerializeField] GameObject[] OtherGunsHolder;

    //－－－－－－－－－－－－－－－－－－－－－/

    // これはアクションに統合する感じで将来的に修正する
    [SerializeField] Animator gunAnimator;

    [Header("参照")]
    [Tooltip("Playerの視点に関するクラス")]
    [SerializeField] CameraController cameraController;
    [SerializeField] PlayerAnimator playerAnimator; // Callbackを使用する感じでリファクタリングする！

    //－－－－－－－－－－－－－－－－－－－－－/

    List<GameObject> guns = new List<GameObject>();     // 銃Data管理用    
    float shotTimer;                                    // 射撃間隔

    
    void Start()
    {
        //自分なのか
        if (photonView.IsMine)
        {
            //銃の数分ループ
            foreach (GameObject gun in gunsHolder)
            {
                //リストに追加
                guns.Add(gun);
            }

            // GunDataの数だけループして、各銃の弾薬を初期化
            foreach (var gun in gunDates)
            {
                // 所持弾薬を最大に
                ammunition.Add(gun.MaxAmmunition);
                // マガジン内弾薬を最大に
                ammoClip.Add(gun.MaxAmmoClip);
            }
        }
        else//他人だったらOtherGunsHolderを表示
        {
            //銃の数分ループ
            foreach (GameObject gun in OtherGunsHolder)
            {
                //リストに追加
                guns.Add(gun);
            }
        }

        //銃の表示切替
        switchGun();
    }

    void Update()
    {
        //自分以外の場合は
        if (!photonView.IsMine)
        {
            //処理終了
            return;
        }
        
        //銃の切り替え
        SwitchingGuns();

        //覗き込み
        Aim();

        //射撃関数
        Fire();

        //リロード関数
        if (Input.GetKeyDown(KeyCode.R))
            Reload();

        //弾薬テキスト更新
        UIManager.instance.SettingBulletsText(gunDates[(int)selectedGunType].MaxAmmoClip,
            ammoClip[(int)selectedGunType], ammunition[(int)selectedGunType]);
    }

    /// <summary>
    /// 銃の切り替えキー入力を検知
    /// </summary>
    public void SwitchingGuns()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            //扱う銃を管理する数値を増やす
            selectedGunType++;

            //リストより大きな数値になれば０に戻す
            if ((int)selectedGunType >= Enum.GetValues(typeof(GunType)).Length)
            {
                selectedGunType = GunType.HandGun;
            }

            // アニメーション
            gunAnimator.SetTrigger("WeaponChange");

            //銃の切り替え（ルーム内のプレイヤー全員呼び出し）
            photonView.RPC("SetGun", RpcTarget.All, (int)selectedGunType);
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            //扱う銃を管理する数値を減らす
            selectedGunType--;

            //0より小さければリストの最大数－１の数値に設定
            if (selectedGunType < 0)
            {
                selectedGunType = (GunType)guns.Count - 1;
            }
            // アニメーション
            gunAnimator.SetTrigger("WeaponChange");

            //銃の切り替え（ルーム内のプレイヤー全員呼び出し）
            photonView.RPC("SetGun", RpcTarget.All, (int)selectedGunType);
        }

        //数値キーの入力検知で武器を切り替える
        for (int i = 0; i < guns.Count; i++)
        {
            //ループの数値＋１をして文字列に変換。その後、押されたか判定
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                //銃を扱う数値を設定
                selectedGunType = (GunType)i;
                
                // アニメーション
                gunAnimator.SetTrigger("WeaponChange");

                //銃の切り替え（ルーム内のプレイヤー全員呼び出し）
                photonView.RPC("SetGun", RpcTarget.All, (int)selectedGunType);
            }
        }
    }


    IEnumerator DelayedSwitchGun()
    {
        yield return new WaitForSeconds(1f);

        //リスト分ループを回す
        foreach (GameObject gun in guns)
        {
            //銃を非表示
            gun.SetActive(false);
        }

        //選択中の銃のみ表示
        guns[(int)selectedGunType].SetActive(true);
    }

    /// <summary>
    /// 銃の表示切り替え
    /// </summary>
    void switchGun()
    {
        //リスト分ループを回す
        foreach (GameObject gun in guns)
        {
            //銃を非表示
            gun.gameObject.SetActive(false);
        }

        //選択中の銃のみ表示
        guns[(int)selectedGunType].SetActive(true);
    }


    /// <summary>
    /// 右クリックで覗き込み
    /// </summary>
    public void Aim()
    {
        //マウス右ボタン押しているとき
        if (Input.GetMouseButton(1))
        {
            // アニメーション
            gunAnimator.SetBool("IsZoom",true);

            //ズームイン
            cameraController.GunZoomIn(gunDates[(int)selectedGunType].AdsZoom, gunDates[(int)selectedGunType].AdsSpeed);
        }
        else
        {
            // アニメーション
            gunAnimator.SetBool("IsZoom", false);

            //ズームアウト
            cameraController.GunZoomOut(gunDates[(int)selectedGunType].AdsSpeed);
        }
    }


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
        // TODO: 時間がある際にここを調整する
        // アニメーション
        //gunAnimator.SetTrigger("Reload");

        //リロードで補充する弾数を取得
        
        int amountNeed = gunDates[(int)selectedGunType].MaxAmmoClip - ammoClip[(int)selectedGunType];

        //必要な弾薬量と所持弾薬量を比較
        int ammoAvailable = amountNeed < ammunition[(int)selectedGunType] ? amountNeed : ammunition[(int)selectedGunType];

        //弾薬が満タンの時はリロードできない&弾薬を所持しているとき
        if (amountNeed != 0 && ammunition[(int)selectedGunType] != 0)
        {
            //所持弾薬からリロードする弾薬分を引く
            ammunition[(int)selectedGunType] -= ammoAvailable;

            //銃に装填する
            ammoClip[(int)selectedGunType] += ammoAvailable;
        }
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

            // 1秒待ってから切り替える
            StartCoroutine(DelayedSwitchGun());
        }
    }
}