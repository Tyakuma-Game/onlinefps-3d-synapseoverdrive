using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Playables;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

/// <summary>
/// PlayerŠÇ—ƒNƒ‰ƒX
/// </summary>
public class PlayerController : MonoBehaviourPunCallbacks
{
    UIManager uIManager;        //UIŠÇ—
    SpawnManager spawnManager;  //ƒXƒ|[ƒ“ƒ}ƒl[ƒWƒƒ[ŠÇ—
    GameManager gameManager;    //ƒQ[ƒ€ƒ}ƒl[ƒWƒƒ[

    //|||||||||||||||||||||/
    //@Œø—¦‰»’†
    //|||||||||||||||||||||/

    [SerializeField] Animator gunAnimator;

    [Tooltip("ƒvƒŒƒCƒ„[‚ÌƒXƒe[ƒ^ƒXî•ñ")]
    [SerializeField] PlayerStatus playerStatus;

    // Player‹@”\
    [Tooltip("Player‚ÌˆÚ“®ˆ—")]
    IPlayerMove playerMove;

    [Tooltip("Player‚Ì‰ñ“]ˆ—")]
    IPlayerRotation playerRotation;

    [Tooltip("Player‚ÌƒWƒƒƒ“ƒvˆ—")]
    IPlayerJump playerJump;

    [Tooltip("Player‚ÌƒAƒjƒ[ƒVƒ‡ƒ“ˆ—")]
    IPlayerAnimator playerAnimator;

    [Tooltip("’…’n‚µ‚Ä‚¢‚é‚©”»’èˆ—")]
    PlayerLandDetector playerLandDetector;

    [SerializeField] PlayerSoundManager playerSoundManager;


    // “ü—ÍƒVƒXƒeƒ€
    [Tooltip("ƒL[ƒ{[ƒh‚Ì“ü—Íˆ—")]
    IKeyBoardInput keyBoardInput;
    
    [Tooltip("ƒ}ƒEƒX‚Ì“ü—Íˆ—")]
    IMouseInput mouseInput;

    IMouseCursorLock mouseCursorLock;

    Rigidbody myRigidbody;
    Camera myCamera;

    [SerializeField] CameraController cameraController;


    TestAnimatorController testAnimatorController;
    [SerializeField] GameObject spawnEffect;


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


    //|||||||||||||||||||||/

    void Awake()
    {
        //©•ªˆÈŠO‚Ìê‡‚Í
        if (!photonView.IsMine)
        {
            //ˆ—I—¹
            return;
        }

        //ƒ^ƒO‚©‚çUIManager‚ğ’T‚·
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //ƒ^ƒO‚©‚çUIManager‚ğ’T‚·
        uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        //ƒ^ƒO‚©‚çSpawnManager‚ğ’T‚·
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();

    }

    void Start()
    {
        // w’èŠÔŒã‚É‰‰o‚ğ’â~‚³‚¹‚é
        Invoke("SpawnEffectNotActive", 1.5f);

        //©•ªˆÈŠO‚Ìê‡‚Í
        if (!photonView.IsMine)
        {
            //ˆ—I—¹
            return;
        }

        myRigidbody = GetComponent<Rigidbody>();
        myCamera = Camera.main;

        // “ü—ÍƒVƒXƒeƒ€
        keyBoardInput = GetComponent<IKeyBoardInput>();
        mouseInput = GetComponent<IMouseInput>();
        mouseCursorLock = GetComponent<IMouseCursorLock>();
        mouseCursorLock.LockScreen();

        // PlayerƒVƒXƒeƒ€
        playerLandDetector = GetComponent<PlayerLandDetector>();
        playerAnimator = GetComponent<IPlayerAnimator>();
        playerMove = GetComponent<IPlayerMove>();
        playerJump = GetComponent<IPlayerJump>();
        playerRotation = GetComponent<IPlayerRotation>();

        // ƒXƒe[ƒ^ƒX‰Šú‰»
        playerMove.Init(myRigidbody);
        playerJump.Init(myRigidbody);
        playerStatus.Init();

        //HPƒXƒ‰ƒCƒ_[”½‰f
        uIManager.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);

        testAnimatorController = GetComponent<TestAnimatorController>();
        testAnimatorController.TestSetHP(playerStatus.CurrentHP);
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
                    playerStatus.IsRunning();
            }
            else
            {
                if (playerStatus.AnimationState != PlayerAnimationState.Walk)
                    playerStatus.IsWalking();
            }
        }

        //|||||||||||||||||||||/
        // PLAYERˆ—
        //|||||||||||||||||||||/
        {
            // ‰ñ“]
            Vector2 roteDirection = mouseInput.GetMouseMove();
            if (roteDirection != Vector2.zero)
            {
                playerRotation.Rotation(roteDirection, playerStatus.Constants.RotationSpeed);
                cameraController.Rotation(roteDirection, playerStatus.Constants.RotationSpeed, playerStatus.Constants.VerticalRotationRange);
            }

            // ˆÚ“®
            Vector3 moveDirection = keyBoardInput.GetWASDAndArrowKeyInput();
            if (moveDirection != Vector3.zero)
            {
                playerMove.Move(moveDirection, playerStatus.ActiveMoveSpeed);
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
            testAnimatorController.TestIsGround(playerLandDetector.IsGrounded);
            if (testAnimatorController == null)
            {
                Debug.Log("testAnimatorController‚ªNULL!!");
            }
            else
            {
                float moveSpeed = moveDirection.magnitude*playerStatus.ActiveMoveSpeed;
                testAnimatorController.TestMove(moveSpeed);

                gunAnimator.SetFloat("MoveSpeed",moveSpeed);
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
            uIManager.IsRunning();
        }
        else
        {
            uIManager.IsNotRunning();
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
            testAnimatorController.TestSetHP(playerStatus.CurrentHP);
            testAnimatorController.Damage();

            //ƒJƒƒ‰‚ğ—h‚ç‚·
            cameraController.Shake();

            //Œ»İ‚ÌHP‚ª0ˆÈ‰º‚Ìê‡
            if (playerStatus.CurrentHP <= 0 && isShowDeath == false)
            {
                //€–SŠÖ”‚ğŒÄ‚Ô
                Death(name, actor);
            }

            //HP‚ğƒXƒ‰ƒCƒ_[‚É”½‰f
            uIManager.UpdateHP(playerStatus.Constants.MaxHP, playerStatus.CurrentHP);
        }
    }

    /// <summary>
    /// €–Sˆ—
    /// </summary>
    public void Death(string name, int actor)
    {
        //€–SUI‚ğXV
        uIManager.UpdateDeathUI(name);

        //©•ª‚ÌƒfƒX”‚ğã¸(©•ª‚Ì¯•Ê”Ô†AƒfƒXA‰ÁZ”’l)
        gameManager.ScoreGet(PhotonNetwork.LocalPlayer.ActorNumber, 1, 1);

        //Œ‚‚Á‚Ä‚«‚½‘Šè‚ÌƒLƒ‹”‚ğã¸(Œ‚‚Á‚Ä‚«‚½“G‚Ì¯•Ê”Ô†AƒLƒ‹A‰ÁZ”’l)
        gameManager.ScoreGet(actor, 0, 1);

        // €–S‰‰o•ÏX
        isShowDeath = true;

        // Á–Åƒp[ƒeƒBƒNƒ‹oŒ»
        photonView.RPC("SpawnEffectActive",RpcTarget.All);

        //€–SŠÖ”‚ğŒÄ‚Ño‚µ
        spawnManager.Die();
    }


    /// <summary>
    /// Player‚Ìn––ˆ—
    /// </summary>
    public void OutGame()
    {
        // GameManagerƒIƒuƒWƒFƒNƒg‚ğQÆ
        gameManager = GameObject.FindObjectOfType<GameManager>();

        //ƒvƒŒƒCƒ„[ƒf[ƒ^íœ
        gameManager.OutPlayerGet(PhotonNetwork.LocalPlayer.ActorNumber);

        //“¯Šú‚ğØ’f
        PhotonNetwork.AutomaticallySyncScene = false;

        //ƒ‹[ƒ€‚©‚ç‘Şo
        PhotonNetwork.LeaveRoom();
    }
}