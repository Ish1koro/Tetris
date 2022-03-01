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

    private Queue<int> _generate_Queue = new Queue<int>();

    #region Stage配列(縦21×横12の配列)
    /// <summary>
    /// 0,空気　1,壁　2,動かせなくなったmino　3,現在動かしているmino　4,minoの中心　5,落とせる位置
    /// </summary>
    protected int[,] _stage = new int[20, 10]
    {
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},
    };

    /// <summary>
    /// Stage配列の取得用
    /// </summary>
    public int[,] _public_Stage
    {
        get { return _stage; }
        set { _stage = value; }
    }

    /// <summary>
    /// 現在のminoの位置(y, x)の順番
    /// </summary>
    protected int[,] _now_Mino_Position = new int[4, 2]
    {
        { 0, 0},
        { 0, 0},
        { 0, 0},
        { 0, 0}
    };

    /// <summary>
    /// 現在のminoの位置の取得用
    /// </summary>
    public int[,] _Mino_Position
    {
        get { return _now_Mino_Position; }
        set { _now_Mino_Position = value; }
    }

    /// <summary>
    /// minoの生成位置
    /// </summary>
    private int[,] _Generate_Position = new int[7, 8]
    {
        // Tmino
        {19, 18, 18, 18, 5, 4, 5, 6 },
        // Imino
        {19, 19, 19, 19, 5, 6, 7, 4 },
        // Omino
        {19, 19, 18, 18, 5, 6, 5, 6 },
        // Jmino
        {19, 18, 18, 18, 5, 5, 4, 6 },
        // Lmino
        {19, 18, 18, 18, 6, 5, 4, 6 },
        // Zmino
        {19, 19, 18, 18, 5, 4, 5, 6 },
        // Smino
        {19, 19, 18, 18, 5, 6, 5, 4 }
    };

    private int[,] _delete_Queue = new int[4, 1]
    {
        { 0 },
        { 0 },
        { 0 },
        { 0 },
    };
    #endregion

    #region int
    /// <summary>
    /// 生成するminoを1～7に設定する
    /// </summary>
    protected int _generate_Mino = default;

    private int _now_Mino = default;

    public int _now_Generate_Mino
    {
        get { return _now_Mino; }
        set { _now_Mino = value; }
    }

    /// <summary>
    /// 回転した数
    /// </summary>
    protected int _now_Directon = default;
    #endregion

    #region bool
    /// <summary>
    /// 落下できるかどうか
    /// </summary>
    protected bool _can_Fall = default;

    protected bool _isGameOver = default;
    public bool _GameOver
    {
        get { return _isGameOver; }
        set { _isGameOver = value; }
    }
    #endregion

    #region minoの回転の向き
    private const int _RIGHT_TURN = 1;
    private const int _LEFT_TURN = -1;
    #endregion

    protected virtual void Awake()
    {
        for (int count = Variables._zero; count < Variables._five; count++)
        {
            _generate_Mino = Random.Range(Variables._zero, (int)Variables._mino_Type.Length);
            _generate_Queue.Enqueue(_generate_Mino);
        }
        Generate();
    }

    /// <summary>
    /// minoの生成
    /// </summary>
    public void Generate()
    {
        _now_Mino = _generate_Queue.Dequeue();

        // Randomで0～6をキューに格納
        _generate_Mino = Random.Range(Variables._zero, (int)Variables._mino_Type.Length);
        _generate_Queue.Enqueue(_generate_Mino);

        #region// 0～6で処理を分ける
        //switch (_generate_Mino)
        //{
        //    // Tminoの場合
        //    // 343
        //    //  3
        //    case (int)Variables._mino_Type.Tmino:
        //        _stage[Variables._mino_Generate_Position_Y, Variables._mino_Generate_Position_X] = Variables._mino_Center;
        //        _stage[Variables._mino_Generate_Position_Y - Variables._one, Variables._mino_Generate_Position_X - Variables._one] = Variables._now_Mino;
        //        _stage[Variables._mino_Generate_Position_Y - Variables._one, Variables._mino_Generate_Position_X] = Variables._now_Mino;
        //        _stage[Variables._mino_Generate_Position_Y - Variables._one, Variables._mino_Generate_Position_X + Variables._one] = Variables._now_Mino;
        //        break;

        //    // Iminoの場合
        //    // 3433
        //    case (int)Variables._mino_Type.Imino:
        //        _stage[Variables._mino_Generate_Position_Y, Variables._mino_Generate_Position_X] = 4;
        //        _stage[Variables._mino_Generate_Position_Y, Variables._mino_Generate_Position_X + Variables._one] = 3;
        //        _stage[Variables._mino_Generate_Position_Y, Variables._mino_Generate_Position_X + Variables._two] = 3;
        //        _stage[Variables._mino_Generate_Position_Y, Variables._mino_Generate_Position_X - Variables._one] = 3;
        //        break;

        //    // Ominoの場合
        //    //  43
        //    //  33
        //    case (int)Variables._mino_Type.Omino:
        //        _stage[Variables._mino_Generate_Position_Y, Variables._mino_Generate_Position_X] = Variables._mino_Center;
        //        _stage[Variables._mino_Generate_Position_Y, Variables._mino_Generate_Position_X + Variables._one] = Variables._now_Mino;
        //        _stage[Variables._mino_Generate_Position_Y - Variables._one, Variables._mino_Generate_Position_X] = Variables._now_Mino;
        //        _stage[Variables._mino_Generate_Position_Y - Variables._one, Variables._mino_Generate_Position_X + Variables._one] = Variables._now_Mino;
        //        break;

        //    // Jminoの場合
        //    // 343
        //    // 3
        //    case (int)Variables._mino_Type.Jmino:
        //        _stage[Variables._mino_Generate_Position_Y , Variables._mino_Generate_Position_X - Variables._one] = Variables._now_Mino;
        //        _stage[Variables._mino_Generate_Position_Y - Variables._one, Variables._mino_Generate_Position_X] = Variables._mino_Center;
        //        _stage[Variables._mino_Generate_Position_Y - Variables._one, Variables._mino_Generate_Position_X - Variables._one] = Variables._now_Mino;
        //        _stage[Variables._mino_Generate_Position_Y - Variables._one, Variables._mino_Generate_Position_X + Variables._one] = Variables._now_Mino;
        //        break;

        //    // Lminoの場合
        //    // 343
        //    //   3
        //    case (int)Variables._mino_Type.Lmino:
        //        _stage[Variables._mino_Generate_Position_Y, Variables._mino_Generate_Position_X + Variables._one] = Variables._now_Mino;
        //        _stage[Variables._mino_Generate_Position_Y - Variables._one, Variables._mino_Generate_Position_X - Variables._one] = Variables._now_Mino;
        //        _stage[Variables._mino_Generate_Position_Y - Variables._one, Variables._mino_Generate_Position_X] = Variables._mino_Center;
        //        _stage[Variables._mino_Generate_Position_Y - Variables._one, Variables._mino_Generate_Position_X + Variables._one] = Variables._now_Mino;
        //        break;

        //    // Zminoの場合
        //    //  43
        //    // 33
        //    case (int)Variables._mino_Type.Zmino:
        //        _stage[Variables._mino_Generate_Position_Y, Variables._mino_Generate_Position_X] = Variables._now_Mino;
        //        _stage[Variables._mino_Generate_Position_Y, Variables._mino_Generate_Position_X - Variables._one] = Variables._now_Mino;
        //        _stage[Variables._mino_Generate_Position_Y - Variables._one, Variables._mino_Generate_Position_X] = Variables._mino_Center;
        //        _stage[Variables._mino_Generate_Position_Y - Variables._one, Variables._mino_Generate_Position_X + Variables._one] = Variables._now_Mino;
        //        break;

        //    // Sminoの場合
        //    // 34
        //    //  33
        //    case (int)Variables._mino_Type.Smino:
        //        _stage[Variables._mino_Generate_Position_Y, Variables._mino_Generate_Position_X] = Variables._now_Mino;
        //        _stage[Variables._mino_Generate_Position_Y, Variables._mino_Generate_Position_X + Variables._one] = Variables._now_Mino;
        //        _stage[Variables._mino_Generate_Position_Y - Variables._one, Variables._mino_Generate_Position_X] = Variables._mino_Center;
        //        _stage[Variables._mino_Generate_Position_Y - Variables._one, Variables._mino_Generate_Position_X - Variables._one] = Variables._now_Mino;
        //        break;
        //}
        #endregion

        // 配列上にminoを生成
        for (int block_count = default; block_count < _now_Mino_Position.GetLength(Variables._zero); block_count++)
        {
            _stage[_Generate_Position[_generate_Mino, block_count], _Generate_Position[_generate_Mino, block_count + Variables._four]] = Variables._now_Mino;
            _now_Mino_Position[block_count, Variables._zero] = _Generate_Position[_generate_Mino, block_count];
            _now_Mino_Position[block_count, Variables._one] = _Generate_Position[_generate_Mino, block_count + Variables._four];
            Debug.Log(block_count);
        }
        _can_Fall = true;
    }

    /// <summary>
    /// 時間経過で落ちる
    /// </summary>
    protected void Fall()
    {
        // 落とせるとき
        if (_can_Fall)
        {
            for (int block_count = default; block_count < _now_Mino_Position.GetLength(Variables._zero); block_count++)
            {
                Debug.Log(_stage[_now_Mino_Position[block_count, Variables._zero], _now_Mino_Position[block_count, Variables._one]]);
                // 現在の配列の位置を空気にする
                _stage[_now_Mino_Position[block_count, Variables._zero], _now_Mino_Position[block_count, Variables._one]] = Variables._air;
                // 現在の-1の配列の位置を今動かしているブロックにする
                _stage[_now_Mino_Position[block_count, Variables._zero] - Variables._one, _now_Mino_Position[block_count, Variables._one]] = Variables._now_Mino;
                // 現在動かしているminoの位置の配列を更新
                _now_Mino_Position[block_count, Variables._zero] = _now_Mino_Position[block_count, Variables._zero] - Variables._one;

                // 壁か古いminoに触れているとき
                if (_stage[_now_Mino_Position[block_count, Variables._zero] - Variables._one, _now_Mino_Position[block_count, Variables._one]] == Variables._cant_Move_Area)
                {
                    // 落下できなくする
                    _can_Fall = false;
                }
            }
        }
        // 落とせないとき
        else
        {
            for (int block_count = default; block_count < _now_Mino_Position.GetLength(Variables._zero); block_count++)
            {
                // 
                if (_now_Mino_Position[block_count, Variables._zero] == _stage.GetLength(Variables._zero) - Variables._one)
                {
                    _isGameOver = true;
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

        for (int block_count = default; block_count < _now_Mino_Position.GetLength(Variables._zero); block_count++)
        {
            _stage[_now_Mino_Position[block_count, Variables._zero], _now_Mino_Position[block_count, Variables._one]] = Variables._air;
            _stage[_now_Mino_Position[block_count, Variables._zero], _now_Mino_Position[block_count, Variables._one] + Variables._one] = Variables._now_Mino;
            _now_Mino_Position[block_count, Variables._one] = _now_Mino_Position[block_count, Variables._one] + Variables._one;
        }
    }

    /// <summary>
    /// minoの回転時に配列を更新する
    /// 1が右に回転2が左に回転
    /// </summary>
    /// <param name="turn_direction">回転の向き</param>
    protected void Turn(int turn_direction)
    {
        switch (turn_direction)
        {
            case _RIGHT_TURN:
                if (_now_Directon == Variables._three)
                {
                    _now_Directon = Variables._zero;
                }
                else
                {
                    _now_Directon += turn_direction;
                }
                break;

            case _LEFT_TURN:
                if (_now_Directon == Variables._zero)
                {
                    _now_Directon = Variables._zero;
                }
                else
                {
                    _now_Directon += turn_direction;
                }
                break;
        }

        for (int y = default; y < _stage.GetLength(Variables._zero); y++)
        {
            for (int x = default; x < _stage.GetLength(Variables._one); x++)
            {
                if (_stage[y, x] == Variables._mino_Center && _stage[y - Variables._one, x] != Variables._cant_Move_Area)
                {
                    switch (_generate_Mino)
                    {
                        case (int)Variables._mino_Type.Tmino:
                            break;
                    }
                }
            }
        }


    }

    protected void Delete()
    {
        for (int y = default; y < _stage.GetLength(Variables._zero); y++)
        {
            int delete_line = default;
            for (int x = Variables._zero; x < _stage.GetLength(Variables._one); x++)
            {
                int block_count = default;
                if (_stage[y, x] == Variables._old_Mino)
                {
                    block_count++;
                }

                if (block_count == _stage.GetLength(Variables._one))
                {
                    _delete_Queue[delete_line, Variables._zero] = y;
                    delete_line++;
                }
            }
        }

        // 上で格納した座標を取り出し
        for (int y = default; y < _stage.GetLength(Variables._zero); y++)
        {
            // 削除する列の高さをとる
            int delete_line = _delete_Queue[y, Variables._zero];
            
            // 削除した分行を下げる削除した分はyに1足した数
            for (int x = default; x < _stage.GetLength(Variables._zero); x++)
            {
                if (delete_line + y + Variables._one < _stage.GetLength(Variables._zero)) 
                {
                    _stage[delete_line, x] = _stage[delete_line + y + Variables._one, x];
                }
                else
                {
                    _stage[delete_line, x] = Variables._air;
                }
            }
        }
    }
}
