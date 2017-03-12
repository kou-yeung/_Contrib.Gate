using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Logger;
using Network;
using IO;
using Entity;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        // スタンドアロン版ではスタンドアロンサーバーを追加します
        if (connectionType == ConnectionType.Standalone)
        {
            gameObject.AddComponent<StandaloneServer>();
        }
    }

    void Start()
    {
        // サービスロケータセットアップ
        LoggerService.SetLocator(new UnityLogger());
        InitSocketService();
        SocketService.Locator.AddReceivedEvent(Receive);
        FileLoaderServer.SetLocator(new UnityResourceLoader());

        // 初期化
        GameEnities.CreateInstance().Load();

        // ログイン
        {
            var communication = new Communication(Command.Auth);
            communication.Pack("ID");
            communication.Pack("PW");
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
                }
                break;
            case Command.Auth:
                {
                    SceneManager.LoadScene("Ball");
                }
                break;

        }
    }
}

