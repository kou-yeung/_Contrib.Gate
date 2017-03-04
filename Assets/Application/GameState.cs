using System;
using System.Collections;
using System.Collections.Generic;
using Network;

class GameState
{
    static public GameState Instance { get; private set; }

    static public GameState CreateInstance()
    {
        if (Instance == null)
        {
            Instance = new GameState();
        }
        return Instance;
    }

    List<ISession> authSessions = new List<ISession>();                         // 認証待ちセッション
    Dictionary<string, ISession> sessions = new Dictionary<string, ISession>(); // 認証済セッション

    private GameState()
    {
    }

    public void Update()
    {
        // 認証済セッションPoll
        foreach (var session in sessions)
        {
            session.Value.Poll();
        }
        // 認証待ちセッションPoll
        foreach (var session in authSessions.ToArray())
        {
            session.Poll();
        }
    }

    public void AddSession(ISession session)
    {
        session.SetCommandExec(ReceivedAuth);
        authSessions.Add(session);
    }

    // イベント:必ず認証完了
    void ReceivedEvent(ISession session, byte[] bytes)
    {
        var c = Communication.Create(bytes);
        switch (c.command)
        {
            case Command.Admin:
                var message = c.Unpack<string>();
                session.Send(Communication.Create(Command.Admin).Pack(message).GetBytes());
                break;
        }
    }

    // 認証情報受け取り
    void ReceivedAuth(ISession session, byte[] bytes)
    {
        var c = Communication.Create(bytes);
        switch (c.command)
        {
            case Command.Auth:
                string id = c.Unpack<string>();
                string pw = c.Unpack<string>();
                // TODO : 実際の認証を行う
                sessions[id] = session;
                authSessions.Remove(session);           // 認証待ちリストから削除
                session.SetCommandExec(ReceivedEvent);  //通常イベント処理
                break;
        }
    }
}
