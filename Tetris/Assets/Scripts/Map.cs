using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 配列のクラス
public class Map : MonoBehaviour
{
    #region 他スクリプト参照

    /// <summary>
    /// 見た目を配列に同期する
    /// </summary>
    protected ViewController _viewController = default;
    #endregion

    #region Stage配列(縦21×横12の配列)
    /// <summary>
    /// 0,空気　1,壁　2,動かせなくなったmino　3,現在動かしているmino　4,minoの中心　5,落とせる位置
    /// </summary>
    protected int[,] _stage = new int[21, 12]
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
    /// Stage配列の取得用
    /// </summary>
    public int[,] _public_Stage
    {
        get { return _stage; }
        set { _stage = value; }
    }

    #endregion

    #region int
    /// <summary>
    /// 生成するminoを1～7に設定する
    /// </summary>
    protected int _generate_Mino = default;
    public int _Generate_mino
    {
        get { return _generate_Mino; }
        set { _generate_Mino = value; }
    }
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
    private const int _RIGHT_TURN = 1;
    private const int _LEFT_TURN = 2;
    #endregion

    /// <summary>
    /// minoの生成
    /// </summary>
    public void Generate()
    {
        // Randomで0～6を格納
        _generate_Mino = Random.Range(Variables._zero, (int)_mino_Type.Length);
        // 0～6で処理を分ける
        switch (_generate_Mino)
        {
            // Tminoの場合
            // 343
            //  3
            case (int)_mino_Type.Tmino:
                _stage[_stage.GetLength(Variables._zero) - Variables._one, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two] = Variables._mino_Center;
                _stage[_stage.GetLength(Variables._zero) - Variables._two, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two - Variables._one] = Variables._now_Mino;
                _stage[_stage.GetLength(Variables._zero) - Variables._two, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two] = Variables._now_Mino;
                _stage[_stage.GetLength(Variables._zero) - Variables._two, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two + Variables._one] = Variables._now_Mino;
                break;
            
            // Iminoの場合
            // 3433
            case (int)_mino_Type.Imino:
                _stage[_stage.GetLength(Variables._zero) - Variables._one, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two] = 4;
                _stage[_stage.GetLength(Variables._zero) - Variables._one, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two + Variables._one] = 3;
                _stage[_stage.GetLength(Variables._zero) - Variables._one, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two + Variables._two] = 3;
                _stage[_stage.GetLength(Variables._zero) - Variables._one, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two - Variables._one] = 3;
                break;

            // Ominoの場合
            //  43
            //  33
            case (int)_mino_Type.Omino:
                _stage[_stage.GetLength(Variables._zero) - Variables._one, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two] = Variables._mino_Center;
                _stage[_stage.GetLength(Variables._zero) - Variables._one, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two + Variables._one] = Variables._now_Mino;
                _stage[_stage.GetLength(Variables._zero) - Variables._two, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two] = Variables._now_Mino;
                _stage[_stage.GetLength(Variables._zero) - Variables._two, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two + Variables._one] = Variables._now_Mino;
                break;

            // Jminoの場合
            // 343
            // 3
            case (int)_mino_Type.Jmino:
                _stage[_stage.GetLength(Variables._zero) - Variables._one, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two - Variables._one] = Variables._now_Mino;
                _stage[_stage.GetLength(Variables._zero) - Variables._two, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two] = Variables._mino_Center;
                _stage[_stage.GetLength(Variables._zero) - Variables._two, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two - Variables._one] = Variables._now_Mino;
                _stage[_stage.GetLength(Variables._zero) - Variables._two, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two + Variables._one] = Variables._now_Mino;
                break;

            // Lminoの場合
            // 343
            //   3
            case (int)_mino_Type.Lmino:
                _stage[_stage.GetLength(Variables._zero) - Variables._one, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two + Variables._one] = Variables._now_Mino;
                _stage[_stage.GetLength(Variables._zero) - Variables._two, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two - Variables._one] = Variables._now_Mino;
                _stage[_stage.GetLength(Variables._zero) - Variables._two, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two] = Variables._mino_Center;
                _stage[_stage.GetLength(Variables._zero) - Variables._two, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two + Variables._one] = Variables._now_Mino;
                break;

            // Zminoの場合
            //  43
            // 33
            case (int)_mino_Type.Zmino:
                _stage[_stage.GetLength(Variables._zero) - Variables._one, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two] = Variables._mino_Center;
                _stage[_stage.GetLength(Variables._zero) - Variables._one, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two - Variables._one] = Variables._now_Mino;
                _stage[_stage.GetLength(Variables._zero) - Variables._two, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two] = Variables._now_Mino;
                _stage[_stage.GetLength(Variables._zero) - Variables._two, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two + Variables._one] = Variables._now_Mino;
                break;

            // Sminoの場合
            // 34
            //  33
            case (int)_mino_Type.Smino:
                _stage[_stage.GetLength(Variables._zero) - Variables._one, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two] = Variables._mino_Center;
                _stage[_stage.GetLength(Variables._zero) - Variables._one, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two + Variables._one] = Variables._now_Mino;
                _stage[_stage.GetLength(Variables._zero) - Variables._two, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two] = Variables._now_Mino;
                _stage[_stage.GetLength(Variables._zero) - Variables._two, (_stage.GetLength(Variables._one) - Variables._two) / Variables._two - Variables._one] = Variables._now_Mino;
                break;
        }
    }

    /// <summary>
    /// 時間経過で落ちる
    /// </summary>
    protected void Fall()
    {
        for (int y = 0; y < _stage.GetLength(Variables._zero); y++)
        {
            for (int x = 0; x < _stage.GetLength(Variables._one); x++)
            {
                if (_stage[y, x] == Variables._mino_Center && _stage[y - Variables._one, x] != Variables._cant_Move_Area)
                {
                    _stage[y - Variables._one, x] = _stage[y, x];
                    _stage[y, x] = Variables._air;
                }
            }
        }
    }

    /// <summary>
    /// 配列の移動。入力値を引数でもらってくる
    /// </summary>
    /// <param name="Input">移動の入力値</param>
    protected void Move(Vector2 Input)
    {
        // 入力値のyが0より大きかったら0にする
        if (Input.y > Variables._zero)
        {
            Input.y = Variables._zero;
        }

        /*
        for (int y = 0; y < _variables._stage_Y_Length; y++)
        {
            for (int x = 0; x < _variables._stage_X_Length; x++)
            {
                if (_stage[y, x] == _variables._now_Mino && _stage[y + (int)Input.y, x + (int)Input.x] != _variables._cant_Move_Area)
                {
                    switch (_generate_Mino)
                    {
                        case (int)_mino_Type.Tmino:
                            _stage[_generate_Position_X, _generate_Position_Y] = _variables._now_Mino;
                            _stage[_generate_Position_X - _variables._one, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                            _stage[_generate_Position_X, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                            _stage[_generate_Position_X + _variables._one, _generate_Position_Y - _variables._one] = _variables._now_Mino;
                            break;

                        case (int)_mino_Type.Imino:
                            break;

                        case (int)_mino_Type.Omino:
                            break;

                        case (int)_mino_Type.Jmino:
                            break;

                        case (int)_mino_Type.Lmino:
                            break;

                        case (int)_mino_Type.Zmino:
                            break;

                        case (int)_mino_Type.Smino:
                            break;
                    }
                }
            }
        }
        */
    }

    /// <summary>
    /// minoの回転時に配列を更新する
    /// 1が右に回転2が左に回転
    /// </summary>
    /// <param name="turn_direction">回転の向き</param>
    protected void Turn(int turn_direction)
    {
        for (int y = 0; y < _stage.GetLength(Variables._zero); y++)
        {
            for (int x = 0; x < _stage.GetLength(Variables._one); x++)
            {
                if (_stage[y, x] == Variables._mino_Center && _stage[y - Variables._one, x] != Variables._cant_Move_Area)
                {
                    switch (_generate_Mino)
                    {
                        case (int)_mino_Type.Tmino:

                            break;
                    }
                }
            }
        }
    }
}
