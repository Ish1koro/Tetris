using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 配列のクラス
public class Map : MonoBehaviour
{
    #region 他スクリプト参照
    private Variables _variables = new Variables();
    #endregion

    #region Stage配列(縦21×横12の配列)
    /// <summary>
    /// 0,空気　1,壁　2,動かせなくなったmino　3,現在動かしているmino　4,落とせる位置
    /// </summary>
    private int[,] _stage = new int[21, 12]
    {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,},
    };

    /// <summary>
    /// Stage配列のコピー
    /// 回転などで使用
    /// </summary>
    private int[,] _copy_Stage = default;
    #endregion

    #region int
    /// <summary>
    /// 生成するminoを1～7に設定する
    /// </summary>
    private int _generate_Mino = default;

    private int _generate_Position_X = 5;
    private int _generate_Position_Y = 12;
    #endregion

    #region bool
    #endregion

    #region minoの種類
    private enum _mino_Type
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
    #endregion

    #region minoの回転の向き
    private const int _right_Turn = 1;
    private const int _left_Turn = 2;
    #endregion

    /// <summary>
    /// minoの生成
    /// </summary>
    public void Generate()
    {
        // Randomで0～6を格納
        _generate_Mino = Random.Range(_variables._zero, (int)_mino_Type.Length);
        // 0～6で処理を分ける
        switch (_generate_Mino)
        {
            // Tminoの場合
            // 333
            //  3
            case (int)_mino_Type.Tmino:
                _stage[_generate_Position_X, _generate_Position_Y] = _variables._now_Mino;
                _stage[_generate_Position_X - _variables._one, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                _stage[_generate_Position_X, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                _stage[_generate_Position_X + _variables._one, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                break;
            
            // Iminoの場合
            // 3333
            case (int)_mino_Type.Imino:
                _stage[_generate_Position_X, _generate_Position_Y] = _variables._now_Mino;
                _stage[_generate_Position_X + _variables._one, _generate_Position_Y] = _variables._now_Mino;
                _stage[_generate_Position_X + _variables._two, _generate_Position_Y] = _variables._now_Mino;
                _stage[_generate_Position_X - _variables._one, _generate_Position_Y] = _variables._now_Mino;
                break;

            // Ominoの場合
            //  33
            //  33
            case (int)_mino_Type.Omino:
                _stage[_generate_Position_X, _generate_Position_Y] = _variables._now_Mino;
                _stage[_generate_Position_X + _variables._one, _generate_Position_Y] = _variables._now_Mino;
                _stage[_generate_Position_X, _generate_Position_Y -_variables._one] = _variables._now_Mino;
                _stage[_generate_Position_X + _variables._one, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                break;

            // Jminoの場合
            // 333
            // 3
            case (int)_mino_Type.Jmino:
                _stage[_generate_Position_X - _variables._one, _generate_Position_Y] = _variables._now_Mino;
                _stage[_generate_Position_X, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                _stage[_generate_Position_X - _variables._one, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                _stage[_generate_Position_X + _variables._one, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                break;

            // Lminoの場合
            // 333
            //   3
            case (int)_mino_Type.Lmino:
                _stage[_generate_Position_X + _variables._one, _generate_Position_Y] = _variables._now_Mino;
                _stage[_generate_Position_X - _variables._one, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                _stage[_generate_Position_X, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                _stage[_generate_Position_X + _variables._one, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                break;

            // Zmino
            //  33
            // 33
            case (int)_mino_Type.Zmino:
                _stage[_generate_Position_X, _generate_Position_Y] = _variables._now_Mino;
                _stage[_generate_Position_X - _variables._one, _generate_Position_Y] = _variables._now_Mino;
                _stage[_generate_Position_X, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                _stage[_generate_Position_X + _variables._one, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                break;

            // Smino
            // 33
            //  33
            case (int)_mino_Type.Smino:
                _stage[_generate_Position_X, _generate_Position_Y] = _variables._now_Mino;
                _stage[_generate_Position_X + _variables._one, _generate_Position_Y] = _variables._now_Mino;
                _stage[_generate_Position_X, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                _stage[_generate_Position_X - _variables._one, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                break;
        }
    }

    /// <summary>
    /// Fall
    /// </summary>
    public void Fall()
    {
        for (int y = 0; y < _variables._stage_Y_Length; y++)
        {
            for (int x = 0; x < _variables._stage_X_Length; x++)
            {
                if (_stage[y, x] == _variables._now_Mino && _stage[y - _variables._one, x] != _variables._cant_Move_Area)
                {
                    _stage[y - _variables._one, x] = _stage[y, x];
                    _stage[y, x] = _variables._air;
                }
            }
        }
    }

    /// <summary>
    /// 配列の移動。入力値を引数でもらってくる
    /// </summary>
    /// <param name="Input">i移動の入力値</param>
    public void Move(Vector2 Input)
    {

    }

    /// <summary>
    /// minoの回転時に配列を更新する
    /// 1が右に回転2が左に回転
    /// </summary>
    /// <param name="turn_direction">回転の向き</param>
    public void Turn(int turn_direction)
    {
        _copy_Stage = _stage;
        switch (turn_direction)
        {
            // 右回転
            case _right_Turn:
                break;

            // 左回転
            case _left_Turn:
                break;
        }
    }
}
