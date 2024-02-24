using UnityEngine.InputSystem;

internal class MultiTapAndHoldInteraction : IInputInteraction
{
    public float tapTime;       // 最大のタップ時間[s]
    public float tapDelay;      // 次のタップまでの最大待機時間[s]
    public int tapCount = 2;    // 必要なタップ数
    public float pressPoint;    // 入力判定の閾値(0でデフォルト値)
    public float releasePoint;  // リリース判定の閾値(0でデフォルト値)
    public float endDelay;      // マルチタップ＆ホールド後、入力がなくなってから終了するまでの時間

    /// <summary>
    /// タップ状態の内部フェーズ
    /// </summary>
    enum TapPhase
    {
        None,
        WaitingForNextRelease,
        WaitingForNextPress,
        WaitingForRelease,
        WaitingForEnd,
    }

    // 設定値かデフォルト値の値を格納するフィールド
    float tapTimeOrDefault => tapTime > 0.0 ? tapTime : InputSystem.settings.defaultTapTime;
    float tapDelayOrDefault => tapDelay > 0.0 ? tapDelay : InputSystem.settings.multiTapDelayTime;
    float pressPointOrDefault => pressPoint > 0 ? pressPoint : InputSystem.settings.defaultButtonPressPoint;
    float releasePointOrDefault => pressPointOrDefault * InputSystem.settings.buttonReleaseThreshold;

    // Interactionの内部状態
    TapPhase _currentTapPhase = TapPhase.None;
    double _currentTapStartTime;
    double _lastTapReleaseTime;
    int _currentTapCount;

    /// <summary>
    /// 初期化
    /// </summary>
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoadMethod]
#else
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
    public static void Initialize()
    {
        // 初回にInteractionを登録する必要がある
        InputSystem.RegisterInteraction<MultiTapAndHoldInteraction>();
    }

    /// <summary>
    /// Interactionの内部処理
    /// </summary>
    public void Process(ref InputInteractionContext context)
    {
        // タイムアウト判定
        if (context.timerHasExpired)
        {
            // 最大許容時間を超えてタイムアウトになった場合はキャンセル
            context.Canceled();
            return;
        }

        switch (_currentTapPhase)
        {
            case TapPhase.None:
                // 初期状態

                // タップされたかチェック
                if (context.ControlIsActuated(pressPointOrDefault))
                {
                    _currentTapStartTime = context.time;

                    if (++_currentTapCount >= tapCount)
                    {
                        // 必要なタップ数に達したらPerformedコールバック実行
                        _currentTapPhase = TapPhase.WaitingForRelease;
                        context.Started();
                        context.PerformedAndStayPerformed();
                    }
                    else
                    {
                        // 入力がなくなるまで待機
                        _currentTapPhase = TapPhase.WaitingForNextRelease;
                        context.Started();
                        context.SetTimeout(tapTimeOrDefault);
                    }
                }

                break;

            case TapPhase.WaitingForNextRelease:
                // 入力がなくなるまで待機している状態
                if (!context.ControlIsActuated(releasePointOrDefault))
                {
                    if (context.time - _currentTapStartTime > tapTimeOrDefault)
                    {
                        // 最大許容時間を超えたのでキャンセル
                        context.Canceled();
                        break;
                    }

                    // 次の入力待ち状態に遷移
                    _lastTapReleaseTime = context.time;
                    _currentTapPhase = TapPhase.WaitingForNextPress;
                    context.SetTimeout(tapDelayOrDefault);
                }

                break;

            case TapPhase.WaitingForNextPress:
                // 次の入力待ちの状態
                if (context.ControlIsActuated(pressPointOrDefault))
                {
                    if (context.time - _lastTapReleaseTime > tapDelayOrDefault)
                    {
                        // 最大許容時間を超えたのでキャンセル
                        context.Canceled();
                        break;
                    }

                    ++_currentTapCount;
                    _currentTapStartTime = context.time;

                    if (_currentTapCount >= tapCount)
                    {
                        // 必要なタップ数に達したので、Performedコールバック通知
                        // 終了まで待機する状態に遷移
                        _currentTapPhase = TapPhase.WaitingForRelease;
                        context.PerformedAndStayPerformed();
                    }
                    else
                    {
                        // 必要タップ数に達していないので、入力がなくなるまで待機
                        _currentTapPhase = TapPhase.WaitingForNextRelease;
                        context.SetTimeout(tapTimeOrDefault);
                    }

                    _currentTapStartTime = context.time;
                }

                break;

            case TapPhase.WaitingForRelease:
                // マルチタップ判定後、入力がなくなるまで待機している状態

                // 入力チェック
                if (!context.ControlIsActuated(releasePointOrDefault))
                {
                    // 入力がなくなったので終了
                    _currentTapPhase = TapPhase.WaitingForEnd;
                    _lastTapReleaseTime = context.time;
                    context.SetTimeout(endDelay);
                }

                break;

            case TapPhase.WaitingForEnd:
                // 入力がなくなってからInteractionを終了するまで待機している状態
                if (context.time - _lastTapReleaseTime >= endDelay)
                {
                    // 一定時間経過したので終了する
                    context.Canceled();
                }
                else if (context.ControlIsActuated(pressPointOrDefault))
                {
                    // 再び入力があった
                    // 一定時間経過していないので、継続とみなす
                    _currentTapPhase = TapPhase.WaitingForRelease;
                    context.PerformedAndStayPerformed();
                }

                break;
        }
    }

    /// <summary>
    /// Interactionの状態リセット
    /// </summary>
    public void Reset()
    {
        _currentTapPhase = TapPhase.None;
        _currentTapStartTime = 0;
        _lastTapReleaseTime = 0;
        _currentTapCount = 0;
    }
}