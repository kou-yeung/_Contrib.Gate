
namespace Network
{
    enum Command
    {
        Auth, // 認証
        GetBall,                // 検証用:ボールを取得した
        MissionState,           // ミッションステート
        Admin = int.MaxValue,   // 管理者
    }
}