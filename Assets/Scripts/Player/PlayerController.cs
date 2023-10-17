using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Playables;

public struct PlayerStatus
{
    public float activeMoveSpeed;                       // Œ»İ‚ÌˆÚ“®‘¬“x
    public Vector3 jumpForth;                           // ƒWƒƒƒ“ƒv—Í
    public PlayerAnimationState playerAnimationState;   // Œ»İ‚Ìó‘Ô
}

/// <summary>
/// PlayerŠÇ—ƒNƒ‰ƒX
/// </summary>
public class PlayerController : MonoBehaviourPunCallbacks
{
    [Header("’è”")]
    [Tooltip("Player‚ÌHPÅ‘å’l")]
    [SerializeField] int PLAYER_MAX_HP = 100;   //Å‘åHP
    int currentHp;                              //Œ»İ‚ÌHP

    [Header("QÆ")]

    [Tooltip("Player‚Ì‹“_ˆÚ“®‚ÉŠÖ‚·‚éƒNƒ‰ƒX")]
    [SerializeField] PlayerViewpointShift playerViewpointShift;

    [Tooltip("Player‚ÌeŠÇ—ƒNƒ‰ƒX")]
    [SerializeField] PlayerGunController playerGunController;

    UIManager uIManager;        //UIŠÇ—
    SpawnManager spawnManager;  //ƒXƒ|[ƒ“ƒ}ƒl[ƒWƒƒ[ŠÇ—
    GameManager gameManager;    //ƒQ[ƒ€ƒ}ƒl[ƒWƒƒ[

    //|||||||||||||||||||||/
    //@Œø—¦‰»’†‚ÌProgram
    //|||||||||||||||||||||/

    [Tooltip("ƒvƒŒƒCƒ„[‚ÌStatusî•ñ")]
    PlayerStatus playerStatus;


    // Player‹@”\
    [Tooltip("Player‚ÌˆÚ“®ˆ—")]
    IPlayerMove playerMove;

    [Tooltip("Player‚ÌƒWƒƒƒ“ƒvˆ—")]
    IPlayerJump playerJump;

    [Tooltip("Player‚ÌƒAƒjƒ[ƒVƒ‡ƒ“ˆ—")]
    IPlayerAnimator playerAnimator;

    [Tooltip("’…’n‚µ‚Ä‚¢‚é‚©”»’èˆ—")]
    PlayerLandDetector playerLandDetector;


    // “ü—ÍƒVƒXƒeƒ€
    [Tooltip("ƒL[ƒ{[ƒh‚Ì“ü—Íˆ—")]
    IKeyBoardInput keyBoardInput;


    [SerializeField] float PLAYER_WALK_SPEED = 4f;
    [SerializeField] float PLAYER_RUN_SPEED = 8f;
    [SerializeField] Vector3 PLAYER_JUMP_FORTH = new Vector3(0,3f,0);

    //|||||||||||||||||||||/

    private void Awake()
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
        //Œ»İ‚ÌHP‚ğMAXHP‚Ì”’l‚Éİ’è
        currentHp = PLAYER_MAX_HP;

        //HP‚ğƒXƒ‰ƒCƒ_[‚É”½‰f
        uIManager.UpdateHP(PLAYER_MAX_HP, currentHp);


        // “ü—ÍƒVƒXƒeƒ€
        keyBoardInput = GetComponent<IKeyBoardInput>();

        // PlayerƒVƒXƒeƒ€
        playerAnimator = GetComponent<PlayerAnimator>();
        playerMove = GetComponent<IPlayerMove>();
        playerJump = GetComponent<IPlayerJump>();
        playerJump = GetComponent<IPlayerJump>();
        playerLandDetector = GetComponent<PlayerLandDetector>();

        // ‰Šú‰»ˆ—
        playerStatus.activeMoveSpeed = PLAYER_WALK_SPEED;               // ˆÚ“®‘¬“x
        playerStatus.jumpForth = PLAYER_JUMP_FORTH;                     // ƒWƒƒƒ“ƒv—Í
        playerStatus.playerAnimationState = PlayerAnimationState.Idol;  // ó‘Ô
    }


    void Update()
    {
        //©•ªˆÈŠO‚Ìê‡‚Í
        if (!photonView.IsMine)
        {
            //ˆ—I—¹
            return;
        }

        //@–ˆ‰ñ‘–‚Á‚Ä‚¢‚Ä‚à•à‚«‚É‚È‚èA‚ë‚­‚ÈğŒ®‚Å‚È‚¢‚©‚çC³‚·‚é
        //|||||||||||||||||||||/

        // ‘–‚èó‘Ô‘JˆÚ ‘–‚èƒL[“ü—Í‚³‚ê‚Ä‚¢‚é•‘–‚èó‘Ô‚Å‚È‚¢‚È‚ç
        if (keyBoardInput.GetRunKeyInput() && !(playerStatus.playerAnimationState == PlayerAnimationState.Run))
        {
            playerStatus.activeMoveSpeed = PLAYER_RUN_SPEED;
            playerStatus.playerAnimationState = PlayerAnimationState.Run;
        }// ‘–‚éƒL[‚ª‰Ÿ‚³‚ê‚Ä‚¢‚È‚¢ & ó‘Ô‚ª•à‚«ó‘Ô‚Å‚Í‚È‚¢
        else if (!keyBoardInput.GetRunKeyInput() && !(playerStatus.playerAnimationState == PlayerAnimationState.Walk))
        {
            playerStatus.activeMoveSpeed = PLAYER_WALK_SPEED;
            playerStatus.playerAnimationState = PlayerAnimationState.Walk;
        }

        //|||||||||||||||||||||/

        // ˆÚ“®
        Vector3 moveDirection = keyBoardInput.GetWASDAndArrowKeyInput();
        if (moveDirection != Vector3.zero)
        {
            playerMove.Move(moveDirection, playerStatus.activeMoveSpeed);
        }
        else
        {
            playerStatus.playerAnimationState = PlayerAnimationState.Idol;
        }

        // ƒWƒƒƒ“ƒv
        if (playerLandDetector.IsGrounded)
        {
            Debug.Log("’n–Ê‚É‚¢‚Ü‚·");
            if (keyBoardInput.GetJumpKeyInput())
            {
                playerJump.Jump(playerStatus.jumpForth);
            }
        }
        else
        {
            Debug.Log("’n–Ê‚É‚¢‚Ü‚¹‚ñ");
        }

        // ƒAƒjƒ[ƒVƒ‡ƒ“XV
        playerAnimator.AnimationUpdate(playerStatus.playerAnimationState);
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
            //ƒ_ƒ[ƒW
            currentHp -= damage;

            //Œ»İ‚ÌHP‚ª0ˆÈ‰º‚Ìê‡
            if (currentHp <= 0)
            {
                //€–SŠÖ”‚ğŒÄ‚Ô
                Death(ref currentHp, name, actor);
            }

            //HP‚ğƒXƒ‰ƒCƒ_[‚É”½‰f
            uIManager.UpdateHP(PLAYER_MAX_HP, currentHp);
        }
    }


    /// <summary>
    /// €–Sˆ—
    /// </summary>
    public void Death(ref int currentHp, string name, int actor)
    {
        //Œ»İ‚ÌHP‚ğ‚O‚É’²®
        currentHp = 0;

        //€–SŠÖ”‚ğŒÄ‚Ño‚µ
        spawnManager.Die();

        //€–SUI‚ğXV
        uIManager.UpdateDeathUI(name);

        //©•ª‚ÌƒfƒX”‚ğã¸(©•ª‚Ì¯•Ê”Ô†AƒfƒXA‰ÁZ”’l)
        gameManager.ScoreGet(PhotonNetwork.LocalPlayer.ActorNumber, 1, 1);

        //Œ‚‚Á‚Ä‚«‚½‘Šè‚ÌƒLƒ‹”‚ğã¸(Œ‚‚Á‚Ä‚«‚½“G‚Ì¯•Ê”Ô†AƒLƒ‹A‰ÁZ”’l)
        gameManager.ScoreGet(actor, 0, 1);
    }


    /// <summary>
    /// €–Sˆ—
    /// </summary>
    public void Death(string name, int actor)
    {
        //Œ»İ‚ÌHP‚ğ‚O‚É’²®
        currentHp = 0;

        //€–SŠÖ”‚ğŒÄ‚Ño‚µ
        spawnManager.Die();

        //€–SUI‚ğXV
        uIManager.UpdateDeathUI(name);

        //©•ª‚ÌƒfƒX”‚ğã¸(©•ª‚Ì¯•Ê”Ô†AƒfƒXA‰ÁZ”’l)
        gameManager.ScoreGet(PhotonNetwork.LocalPlayer.ActorNumber, 1, 1);

        //Œ‚‚Á‚Ä‚«‚½‘Šè‚ÌƒLƒ‹”‚ğã¸(Œ‚‚Á‚Ä‚«‚½“G‚Ì¯•Ê”Ô†AƒLƒ‹A‰ÁZ”’l)
        gameManager.ScoreGet(actor, 0, 1);
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