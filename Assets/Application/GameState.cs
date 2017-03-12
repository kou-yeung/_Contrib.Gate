using System;
using System.Collections;
using System.Collections.Generic;
using Network;
using Entity;
using Logic;
using Logger;

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
                session.Userdata(new UserData()); // とりあえず新規ユーザデータを追加
                session.SetCommandExec(ReceivedEvent);  //通常イベント処理
                session.Send(Communication.Create(Command.Auth).GetBytes());
                break;
        }
    }
    void SendMissionStates(ISession session)
    {
        List<MissionState> res = new List<MissionState>();
        foreach (var state in session.Userdata().MissionStates)
        {
            if (state.IsOrder())
            {
                res.Add(state);
            }
        }
        var c = Communication.Create(Command.MissionState);
        c.Pack(res.ToArray());
        session.Send(c.GetBytes());
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
            case Command.GetBall:
                var ball = c.Unpack<IdWithType>();
                MissionLogic.CheckBallGet(GameEnities.Instance.missions, session.Userdata().MissionStates, ball);
                LoggerService.Locator.Info("Rece : Command.GetBall {0}", IdWithType.GetId(ball));
                SendMissionStates(session);
                break;
            case Command.MissionState:
                SendMissionStates(session);
                break;
        }
    }

}
