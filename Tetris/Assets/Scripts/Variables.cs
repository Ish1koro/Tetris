using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 読み取り専用の変数を定義するクラス
// マジックナンバー防止用
public static class Variables
{
    #region int 
    public static int _zero { get; } = 0;
    public static int _one { get; } = 1;
    public static int _two { get; } = 2;
    public static int _three { get; } = 3;
    public static int _four { get; } = 4;
    public static int _five { get; } = 5;
    public static int _six { get; } = 6;
    public static int _seven { get; } = 7;
    public static int _eight { get; } = 8;
    #endregion

    #region InputSystemのActionsの名前
    public static string _move { get; } = "Move";
    public static string _turn_Key { get; } = "TurnKey";
    public static string _turn_Pad { get; } = "TurnPad";
    #endregion

    #region 配列関係
    public static int _mino_Generate_Position_X { get; } = 5;
    public static int _mino_Generate_Position_Y { get; } = 20;
    public static int _cant_Move_Area { get; } = 1 << 1 | 1 << 2;
    public static int _air { get; } = 0;
    public static int _wall { get; } = 1;
    public static int _old_Mino { get; } = 2;
    public static int _now_Mino { get; } = 3;
    public static int _mino_Center { get; } = 4;
    public static int _can_Fall_Position { get; } = 5;
    #endregion

    public enum _mino_Type
    {
        Tmino,
        Imino,
        Omino,
        Jmino,
        Lmino,
        Zmino,
        Smino,
        Length
    }

    public enum _mino_Rotate
    {
        Up,
        Right,
        Down,
        Left
    }
}
