using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 読み取り専用の変数を定義するクラス
// マジックナンバー防止用
public class Variables : MonoBehaviour
{
    #region int 
    public int _zero { get; } = 0;
    public int _one { get; } = 1;
    public int _two { get; } = 2;
    public int _three { get; } = 3;
    public int _four { get; } = 4;
    public int _five { get; } = 5;
    public int _six { get; } = 6;
    public int _seven { get; } = 7;
    public int _eight { get; } = 8;
    #endregion

    #region float
    #endregion

    #region InputSystemのActionsの名前
    public string _move { get; } = "Move";
    public string _turn_Key { get; } = "TurnKey";
    public string _turn_Pad { get; } = "TurnPad";
    #endregion

    #region 配列関係
    public int _stage_X_Length { get; } = 12;
    public int _stage_Y_Length { get; } = 21;
    public int _cant_Move_Area { get; } = 1 << 1 | 1 << 2;
    public int _air { get; } = 0;
    public int _wall { get; } = 1;
    public int _old_Mino { get; } = 2;
    public int _now_Mino { get; } = 3;
    public int _can_Fall_Position { get; } = 4;
    #endregion
}
