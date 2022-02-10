using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 読み取り専用の変数を定義するクラス
// マジックナンバー防止用
public class Variables : MonoBehaviour
{
    #region int 
    public int _zero { get; } = 0;
    public int _one { get; }  = 1;
    public int _two { get; } = 2;
    public int _three { get; } = 3;
    #endregion

    #region float
    #endregion

    #region InputSystemのAction名
    public string _move { get; } = "Move";
    public string _turn_Key { get; } = "TurnKey";
    public string _turn_Pad { get; } = "TurnPad";
    #endregion
}
