using UnityEngine;
using Photon.Pun;
using MiniMap;
using System;

public static class PlayerEvent
{
    public static Action onIdol;
    public static Action onWalk;
    public static Action onDash;

    public static Action onDamage;
    public static Action onSpawn;
    public static Action onDisappear;
}

/// <summary>
/// PlayerŠÇ—ƒNƒ‰ƒX
/// </summary>
public class PlayerController : MonoBehaviourPunCallbacks
{
    //|||||||||||||||||||||/
    //@Œø—¦‰»’†
    //|||||||||||||||||||||/

    [SerializeField] Animator gunAnimator;

    [Tooltip("ƒvƒŒƒCƒ„[‚ÌƒXƒe[ƒ^ƒXî•ñ")]
    [SerializeField] PlayerStatus playerStatus;

    [SerializeField] EnemyIconController enemyIcon;

    

    [Tooltip("Player‚ÌƒAƒjƒ[ƒVƒ‡ƒ“ˆ—")]
    PlayerAnimator playerAnimator;

    [Tooltip("’…’n‚µ‚Ä‚¢‚é‚©”»’èˆ—")]
    PlayerLandDetector playerLandDetector;

    [SerializeField] PlayerSoundManager playerSoundManager;


    // “ü—ÍƒVƒXƒeƒ€
    [Tooltip("ƒL[ƒ{[ƒh‚Ì“ü—Íˆ—")]
    KeyBoardInput keyBoardInput;
   

    IMouseCursorLock mouseCursorLock;

    

    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject spawnEffect;


    [Tooltip("Player‚ÌƒWƒƒƒ“ƒvˆ—")]
    IPlayerJump playerJump;


    Rigidbody myRigidbody;
    bool isShowDeath = false;

    [PunRPC]
    public void SpawnEffectActive()
    {
        spawnEffect.SetActive(true);
    }

    public void SpawnEffectNotActive()
    {
        spawnEffect.SetActive(false);
    }

    void Start()
    {
        // w’èŠÔŒã‚É‰‰o‚ğ’â~‚³‚¹‚é
        Invoke("SpawnEffectNotActive", 1.5f);

        //©•ªˆÈŠO‚Ìê‡‚Í
        if (!photonView.IsMine)
        {
            enemyIcon.SetIconVisibility(true);
            //ˆ—I—¹
            return;
        }
        enemyIcon.SetIconVisibility(false);

        MiniMapController.instance.SetMiniMapTarget(this.transform);

        myRigidbody = GetComponent<Rigidbody>();

        // “ü—ÍƒVƒXƒeƒ€
        keyBoardInput = GetComponent<KeyBoardInput>();
        mouseCursorLock = GetComponent<IMouseCursorLock>();
        mouseCursorLock.LockScreen();

        // PlayerƒVƒXƒeƒ€
        playerLandDetector = GetComponent<PlayerLandDetector>();
        playerAnimator = GetComponent<PlayerAnimator>();


        playerJump = GetComponent<IPlayerJump>();

        // ƒXƒe[ƒ^ƒX‰Šú‰»
        playerJump.Init(myRigidbody);
        playerStatus.Init();

        //HPƒXƒ‰ƒCƒ_[”½‰f
        UIManager.instance.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);

        // Œ»İ‚ÌHP‚ğƒZƒbƒg
        playerAnimator.SetCurrentHP(playerStatus.CurrentHP);
    }


    void Update()
    {
        // ©•ªˆÈŠO‚Ìê‡‚Í
        if (!photonView.IsMine)
        {
            //ˆ—I—¹
            return;
        }

        // €–S‰‰o’†‚È‚ç
        if (isShowDeath)
        {
            Debug.Log("€–S‰‰o‚Åˆ—‚ğ’†’f‚³‚¹‚Ä‚Ü‚·B");

            // ˆ—I—¹
            return;
        }

        //|||||||||||||||||||||/
        // ó‘Ô•ÏXˆ—
        //|||||||||||||||||||||/
        {
            // ƒ}ƒEƒXƒJ[ƒ\ƒ‹‚ÌƒƒbƒNó‘Ô•ÏX
            if (keyBoardInput.GetCursorLockKeyInput())
            {
                if (mouseCursorLock.IsLocked())
                    mouseCursorLock.LockScreen();
                else
                    mouseCursorLock.UnlockScreen();
            }

            // ó‘Ô‘JˆÚ
            if (keyBoardInput.GetRunKeyInput())
            {
                if (playerStatus.AnimationState != PlayerAnimationState.Run)
                {
                    playerStatus.IsRunning();
                    PlayerEvent.onDash?.Invoke();
                }
            }
            else
            {
                if (playerStatus.AnimationState != PlayerAnimationState.Walk)
                {
                    playerStatus.IsWalking();
                    PlayerEvent.onWalk?.Invoke();
                }
                    
            }
        }

        //|||||||||||||||||||||/
        // PLAYERˆ—
        //|||||||||||||||||||||/
        {
            // ˆÚ“®@‚»‚ê‚¼‚ê‚ÉƒAƒjƒ[ƒVƒ‡ƒ“İ’è‚·‚éˆ—‚ğ‚â‚Á‚Ä“K—p‚·‚éŠ´‚¶‚É‚â‚éH@‚»‚ê‚ğƒAƒNƒVƒ‡ƒ“‚É“n‚µ‚ÄˆÚ“®‚Ì‚â‚Â‚ªŒÄ‚Ño‚·‚È‚Ç
            Vector3 moveDirection = keyBoardInput.GetWASDAndArrowKeyInput();
            if (moveDirection != Vector3.zero)
            {
            }
            else
            {
                playerStatus.IsIdol();
            }

            // ƒWƒƒƒ“ƒv
            if (playerLandDetector.IsGrounded)
            {
                if (keyBoardInput.GetJumpKeyInput())
                {
                    playerJump.Jump(playerStatus.ActiveJumpForth);
                    playerLandDetector.OnJunpingChangeFlag();
                }
            }

            //|||||||||||||||||||||/
            // ƒAƒjƒ[ƒVƒ‡ƒ“XV
            //|||||||||||||||||||||/
            {
                playerAnimator.IsGround(playerLandDetector.IsGrounded);
                float moveSpeed = moveDirection.magnitude * playerStatus.ActiveMoveSpeed;
                playerAnimator.UpdateMoveSpeed(moveSpeed);
                gunAnimator.SetFloat("MoveSpeed", moveSpeed);
            }
            
            if (playerLandDetector.IsGrounded == false)
            {
                playerStatus.IsIdol();
                gunAnimator.SetFloat("MoveSpeed", 0f);
            }

            // Soundˆ—
            playerSoundManager.SoundPlays(playerStatus.AnimationState);
        }

        if (playerStatus.AnimationState == PlayerAnimationState.Run)
        {
            UIManager.instance.IsRunning();
        }
        else
        {
            UIManager.instance.IsNotRunning();
        }

        //|||||||||||||||||||||/
        // ƒJƒƒ‰ˆ—
        //|||||||||||||||||||||/

        // ƒJƒƒ‰‚ÌÀ•WXV
        cameraController.UpdatePosition();
    }


    /// <summary>
    /// ’e‚É“–‚½‚Á‚½ŒÄ‚Î‚ê‚éˆ—
    /// </summary>
    /// <param name="damage">ƒ_ƒ[ƒW—Ê</param>
    /// <param name="name">Œ‚‚Á‚½‚â‚Â‚Ì–¼‘O</param>
    /// <param name="actor">Œ‚‚Á‚½‚â‚Â‚Ì”Ô†</param>
    [PunRPC]
    public void Hit(int damage, string name, int actor)
    {
        //ƒ_ƒ[ƒWŠÖ”ŒÄ‚Ño‚µ
        ReceiveDamage(name, damage, actor);
    }


    /// <summary>
    /// ƒ_ƒ[ƒW‚ğó‚¯‚éˆ—
    /// </summary>
    /// <param name="damage">ƒ_ƒ[ƒW—Ê</param>
    /// <param name="name">Œ‚‚Á‚½‚â‚Â‚Ì–¼‘O</param>
    /// <param name="actor">Œ‚‚Á‚½‚â‚Â‚Ì”Ô†</param>
    public void ReceiveDamage(string name, int damage, int actor)
    {
        //©•ª‚È‚ç
        if (photonView.IsMine)
        {
            // Damage‚ğó‚¯‚½Û‚Ì‰¹‚ğ–Â‚ç‚·
            playerSoundManager.DamageSound();

            //ƒ_ƒ[ƒW
            playerStatus.OnDamage(damage);

            // ƒAƒjƒ[ƒVƒ‡ƒ“
            playerAnimator.SetCurrentHP(playerStatus.CurrentHP);
            playerAnimator.Damage();

            //ƒJƒƒ‰‚ğ—h‚ç‚·
            PlayerEvent.onDamage?.Invoke();

            //Œ»İ‚ÌHP‚ª0ˆÈ‰º‚Ìê‡
            if (playerStatus.CurrentHP <= 0 && isShowDeath == false)
            {
                //€–SŠÖ”‚ğŒÄ‚Ô
                Death(name, actor);
            }

            //HP‚ğƒXƒ‰ƒCƒ_[‚É”½‰f
            UIManager.instance.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);
        }
    }

    /// <summary>
    /// €–Sˆ—
    /// </summary>
    public void Death(string name, int actor)
    {
        //€–SUI‚ğXV
        UIManager.instance.UpdateDeathUI(name);

        //©•ª‚ÌƒfƒX”‚ğã¸(©•ª‚Ì¯•Ê”Ô†AƒfƒXA‰ÁZ”’l)
        GameManager.instance.ScoreGet(PhotonNetwork.LocalPlayer.ActorNumber, 1, 1);

        //Œ‚‚Á‚Ä‚«‚½‘Šè‚ÌƒLƒ‹”‚ğã¸(Œ‚‚Á‚Ä‚«‚½“G‚Ì¯•Ê”Ô†AƒLƒ‹A‰ÁZ”’l)
        GameManager.instance.ScoreGet(actor, 0, 1);

        // €–S‰‰o•ÏX
        isShowDeath = true;

        // Á–Åƒp[ƒeƒBƒNƒ‹oŒ»
        photonView.RPC("SpawnEffectActive",RpcTarget.All);

        //€–SŠÖ”‚ğŒÄ‚Ño‚µ
        SpawnManager.instance.StartRespawnProcess();
    }

    /// <summary>
    /// Player‚Ìn––ˆ—
    /// </summary>
    public void OutGame()
    {
        GameManager.instance.OutPlayerGet(PhotonNetwork.LocalPlayer.ActorNumber); // ƒvƒŒƒCƒ„[ƒf[ƒ^íœ
        PhotonNetwork.AutomaticallySyncScene = false;                             // “¯ŠúØ’f
        PhotonNetwork.LeaveRoom();                                                // ƒ‹[ƒ€‘Şo
    }
}