using UnityEngine;

[CreateAssetMenu]
public class ParamsSO : ScriptableObject
{
    [Header("初期のボールカウント")]
    public int initBallCount;

    [Header("ボールを消した時の得点")]
    public int scorePoint;

    [Header("ボールの判定距離")]
    public float ballDistance;

    [Header("ボールの大きさ")]
    public float defaultBallScale;

    [Header("ボールクリック時の大きさ")]
    public float activeBallScale;

    [Header("スコアの残る時間")]
    public float pointEffectRemain;

    [Header("爆発の残る時間")]
    public float explosionRemain;

    [Header("爆弾の発生確率")]
    [Range(0.0f, 1.0f)]
    public float bombRatio;

    [Header("爆弾の爆発範囲")]
    [Range(0.5f, 3.0f)]
    public float BombExplosionRadius;

    //Scripts for easy loading

    //MyScriptableObjectが保存してある場所のパス
    public const string PATH = "ParamsSO";

    //MyScriptableObjectの実体
    private static ParamsSO _entity;

    public static ParamsSO Entity
    {
        get
        {
            //初アクセス時にロードする
            if (_entity == null)
            {
                _entity = Resources.Load<ParamsSO>(PATH);

                //ロード出来なかった場合はエラーログを表示
                if (_entity == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }
            return _entity;
        }
    }
}