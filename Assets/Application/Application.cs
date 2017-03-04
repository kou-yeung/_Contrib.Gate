using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Logger;
using Network;
using UnityEngine;

public class Application : MonoBehaviour
{
    enum ConnectionType
    {
        Standalone, // スタンドアロン版
        Local,      // ローカル : 127.0.0.1:2007
        AWS,        // AWS : 未定
    }
    [SerializeField] ConnectionType connectionType;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // 初期化
        GameState.CreateInstance();

        // サービスロケータセットアップ
        LoggerService.SetLocator(new UnityLogger());
        InitSocketService();

        SocketService.Locator.AddReceivedEvent(Receive);

        // ログイン
        {
            var communication = new Communication(Command.Auth);
            communication.Pack("ID");
            communication.Pack("PW");
            SocketService.Locator.Send(communication.GetBytes());
        }

        // メッセージ
        {
            var communication = new Communication(Command.Admin);
            communication.Pack("Hello");
            SocketService.Locator.Send(communication.GetBytes());
        }
    }
    void InitSocketService()
    {
        switch (connectionType)
        {
            case ConnectionType.Standalone:
                SocketService.SetLocator(new StandaloneSocketService());
                SocketService.Locator.Connect("Standalone", 0);
                break;
            case ConnectionType.Local:
                SocketService.SetLocator(new TcpClientService());
                SocketService.Locator.Connect("127.0.0.1", 2007);
                break;
            //case ConnectionType.AWS:
            //    // TODO : 
            //    //SocketService.SetLocator(new TcpClientService());
            //    //SocketService.Locator.Connect("127.0.0.1", 2007);
            //    break;
        }
    }
    void Update()
    {
        // メインループ
        if (GameState.Instance != null)
        {
            GameState.Instance.Update();
        }
        SocketService.Locator.Poll();
    }

    void Receive(byte[] bytes)
    {
        var c = Communication.Create(bytes);
        switch (c.command)
        {
            case Command.Admin:
                {
                    var message = c.Unpack<string>();
                    LoggerService.Locator.Info("Receive : {0}", message);
                    break;
                }
        }
    }
}

class UnityLogger : ILoggerService
{
    public override void Log(Level level, string message)
    {
        switch (level)
        {
            case Level.Error:
                Debug.LogError(message);
                break;
            case Level.Warning:
                Debug.LogWarning(message);
                break;
            case Level.Info:
            case Level.Config:
            case Level.Fine:
                Debug.Log(message);
                break;
        }
    }
    public override void Log(Level level, string format, params object[] args)
    {
        switch (level)
        {
            case Level.Error:
                Debug.LogErrorFormat(format, args);
                break;
            case Level.Warning:
                Debug.LogWarningFormat(format, args);
                break;
            case Level.Info:
            case Level.Config:
            case Level.Fine:
                Debug.LogFormat(format, args);
                break;
        }
    }
}