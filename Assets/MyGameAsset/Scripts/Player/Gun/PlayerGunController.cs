using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;

/// <summary>
/// プレイヤーの銃管理クラス
/// </summary>
public class PlayerGunController : MonoBehaviourPunCallbacks
{
    //－－－－－－－－－－－－－－－－－－－－－/
    // 変更なし
    //－－－－－－－－－－－－－－－－－－－－－/

    [SerializeField] Animator gunAnimator;

    [Header("参照")]
    [Tooltip("Playerの視点に関するクラス")]
    [SerializeField] CameraController cameraController;

    TestAnimatorController testAnimatorController;

    //－－－－－－－－－－－－－－－－－－－－－/

    [Header("銃関連")]
    [Tooltip("被弾時のエフェクト")]
    [SerializeField] GameObject hitEffect;

    [Tooltip("銃の管理配列")]
    [SerializeField] List<GunStatus> guns = new List<GunStatus>();
    
    [Tooltip("銃ホルダー 自分視点用")]
    [SerializeField] GunStatus[] gunsHolder;

    [Tooltip("銃ホルダー 相手視点用")]
    [SerializeField] GunStatus[] OtherGunsHolder;

    


    int selectedGun = 0;                                //選択中の武器管理用数値
    float shotTimer;                                    //射撃間隔

    [Tooltip("所持弾薬")]
    [SerializeField] int[] ammunition;

    [Tooltip("最高所持弾薬数")]
    [SerializeField] int[] maxAmmunition;

    [Tooltip("マガジン内の弾数")]
    [SerializeField] int[] ammoClip;

    [Tooltip("マガジンに入る最大の数")]
    [SerializeField] int[] maxAmmoClip;

    //UI管理
    UIManager uIManager;

    void Start()
    {
        //自分なのか
        if (photonView.IsMine)
        {
            //銃の数分ループ
            foreach (GunStatus gun in gunsHolder)
            {
                //リストに追加
                guns.Add(gun);
            }

            //タグからUIManagerを探す
            uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        }
        else//他人だったらOtherGunsHolderを表示
        {
            //銃の数分ループ
            foreach (GunStatus gun in OtherGunsHolder)
            {
                //リストに追加
                guns.Add(gun);
            }
        }

        testAnimatorController = GetComponent<TestAnimatorController>();

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
        uIManager.SettingBulletsText(GetGunAmmoClipMax(), GetGunAmmoClip(), GetGunAmmunition());
    }

    /// <summary>
    /// 設定時間毎に実行
    /// </summary>
    void FixedUpdate()
    {
        //自分以外なら
        if (!photonView.IsMine)
        {
            //処理終了
            return;
        }

        //弾薬テキスト更新
        uIManager.SettingBulletsText(GetGunAmmoClipMax(),ammoClip[selectedGun], ammunition[selectedGun]);
    }

    /// <summary>
    /// 銃の切り替えキー入力を検知
    /// </summary>
    public void SwitchingGuns()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            //扱う銃を管理する数値を増やす
            selectedGun++;

            //リストより大きな数値になれば０に戻す
            if (selectedGun >= guns.Count)
            {
                selectedGun = 0;
            }

            //銃の切り替え（ルーム内のプレイヤー全員呼び出し）
            photonView.RPC("SetGun", RpcTarget.All, selectedGun);
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            //扱う銃を管理する数値を減らす
            selectedGun--;

            //0より小さければリストの最大数－１の数値に設定
            if (selectedGun < 0)
            {
                selectedGun = guns.Count - 1;
            }

            //銃の切り替え（ルーム内のプレイヤー全員呼び出し）
            photonView.RPC("SetGun", RpcTarget.All, selectedGun);
        }

        //数値キーの入力検知で武器を切り替える
        for (int i = 0; i < guns.Count; i++)
        {
            //ループの数値＋１をして文字列に変換。その後、押されたか判定
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                //銃を扱う数値を設定
                selectedGun = i;

                //銃の切り替え（ルーム内のプレイヤー全員呼び出し）
                photonView.RPC("SetGun", RpcTarget.All, selectedGun);
            }
        }
    }


    IEnumerator DelayedSwitchGun()
    {
        yield return new WaitForSeconds(1f);

        //リスト分ループを回す
        foreach (GunStatus gun in guns)
        {
            //銃を非表示
            gun.gameObject.SetActive(false);
        }

        //選択中の銃のみ表示
        guns[selectedGun].gameObject.SetActive(true);
    }

    /// <summary>
    /// 銃の表示切り替え
    /// </summary>
    void switchGun()
    {
        //リスト分ループを回す
        foreach (GunStatus gun in guns)
        {
            //銃を非表示
            gun.gameObject.SetActive(false);
        }

        //選択中の銃のみ表示
        guns[selectedGun].gameObject.SetActive(true);
    }


    /// <summary>
    /// 右クリックで覗き込み
    /// </summary>
    public void Aim()
    {
        //マウス右ボタン押しているとき
        if (Input.GetMouseButton(1))
        {
            //ズームイン
            cameraController.GunZoomIn(guns[selectedGun].AdsZoom, guns[selectedGun].AdsSpeed);
        }
        else
        {
            //ズームアウト
            cameraController.GunZoomOut(guns[selectedGun].AdsSpeed);
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
            if (ammoClip[selectedGun] == 0)
            {
                // 弾切れの音を鳴らす
                photonView.RPC("NotShotSound", RpcTarget.All);

                // オートリロード
                Reload();

                // 処理終了
                return;
            }

            // アニメーション
            gunAnimator.SetTrigger("Attack");
            Debug.Log($"銃のアニメーション呼ばれたで！");

            //銃の発射処理
            FiringBullet();
        }
    }

    // 弾切れ音処理
    [PunRPC]
    void NotShotSound()
    {
        //　効果音再生
        guns[selectedGun].GetNotShotSE().Stop();
        guns[selectedGun].GetNotShotSE().Play();
    }


    // 発砲時のエフェクト処理
    [PunRPC]
    void ShotEffect()
    {
        //　効果音の再生
        guns[selectedGun].GetShotSE().Stop();
        guns[selectedGun].GetShotSE().Play();

        //　光を表示
        guns[selectedGun].GetShotLight().enabled = false;
        guns[selectedGun].GetShotLight().enabled = true;

        // 発射エフェクトを表示
        guns[selectedGun].ShotEffectNotActive();
        guns[selectedGun].ActiveShotEffect();

        //　コルーチンで消す処理を実行
        StartCoroutine(DisableLight());
        StartCoroutine(DisableEffect());

        //　銃のEffectを消す
        IEnumerator DisableEffect()
        {
            yield return new WaitForSeconds(0.1f);
            guns[selectedGun].ShotEffectNotActive();
        }

        //　ライトを消す処理
        IEnumerator DisableLight()
        {
            yield return new WaitForSeconds(0.1f);
            guns[selectedGun].GetShotLight().enabled = false;
        }
    }


    /// <summary>
    /// 弾丸の発射
    /// </summary>
    void FiringBullet()
    {
        // アニメーション
        testAnimatorController.Attack(AttackType.Short);

        // Effectを散らす
        photonView.RPC("ShotEffect", RpcTarget.All);

        //Ray(光線)をカメラの中央から設定
        Vector2 pos = new Vector2(.5f, .5f);
        Ray ray = cameraController.GenerateRay(pos);

        // カメラの演出(攻撃時に上を向かせる)
        cameraController.ApplyRecoil();

        //レイを発射
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //プレイヤーにぶつかった場合
            if (hit.collider.gameObject.tag == "Player")
            {
                //血のエフェクトをネットワーク上に生成
                PhotonNetwork.Instantiate(hitEffect.name, hit.point, Quaternion.identity);

                // ヒット関数を全プレイヤーで呼び出して撃たれたプレイヤーのHPを同期
                hit.collider.gameObject.GetPhotonView().RPC("Hit",
                    RpcTarget.All,
                    guns[selectedGun].ShotDamage,
                    photonView.Owner.NickName,
                    PhotonNetwork.LocalPlayer.ActorNumber);
            }
            else
            {
                //弾痕エフェクト生成 
                GameObject bulletImpactObject = Instantiate(guns[selectedGun].GetHitEffect(),
                    hit.point + (hit.normal * .002f),                   //オブジェクトから少し浮かしてちらつき防止
                    Quaternion.LookRotation(hit.normal, Vector3.up));   //直角の方向を返してその方向に回転させる

                //時間経過で削除
                Destroy(bulletImpactObject, 10f);
            }
        }

        //射撃間隔を設定
        shotTimer = Time.time + guns[selectedGun].ShootInterval;

        //選択中の銃の弾薬減らす
        ammoClip[selectedGun]--;
    }


    /// <summary>
    /// リロード
    /// </summary>
    void Reload()
    {
        //リロードで補充する弾数を取得
        int amountNeed = maxAmmoClip[selectedGun] - ammoClip[selectedGun];

        //必要な弾薬量と所持弾薬量を比較
        int ammoAvailable = amountNeed < ammunition[selectedGun] ? amountNeed : ammunition[selectedGun];

        //弾薬が満タンの時はリロードできない&弾薬を所持しているとき
        if (amountNeed != 0 && ammunition[selectedGun] != 0)
        {
            //所持弾薬からリロードする弾薬分を引く
            ammunition[selectedGun] -= ammoAvailable;

            //銃に装填する
            ammoClip[selectedGun] += ammoAvailable;
        }
    }


    /// <summary>
    /// 銃の切り替え処理
    /// </summary>
    [PunRPC]
    public void SetGun(int gunNo)
    {
        //銃の切り替え
        if (gunNo < guns.Count)
        {
            //銃の番号をセット
            selectedGun = gunNo;

            // アニメーション
            testAnimatorController.TestWeaponChange();

            // 1秒待ってから切り替える
            StartCoroutine(DelayedSwitchGun());
        }
    }

    /// <summary>
    /// 選択している銃の所持弾薬
    /// </summary>
    public int GetGunAmmunition()
    {
        return ammunition[selectedGun];
    }


    /// <summary>
    /// 選択している銃のマガジン内弾薬
    /// </summary>
    public int GetGunAmmoClip()
    {
        return ammoClip[selectedGun];
    }


    /// <summary>
    /// 選択している銃のマガジン内弾薬最大数
    /// </summary>
    public int GetGunAmmoClipMax()
    {
        return maxAmmoClip[selectedGun];
    }
}