using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Playerの操作に関するクラス
public class PlayerController : MonoBehaviour
{
    #region 他スクリプト参照
    /// <summary>
    /// Playerの入力を取得
    /// </summary>
    private PlayerInput _playerinput = default;

    /// <summary>
    /// 配列クラス
    /// </summary>
    private Map _map = default;

    /// <summary>
    /// よく使う変数などを定義したクラス
    /// </summary>
    private Variables _variables = default;
    #endregion

    #region Vector
    /// <summary>
    /// Playerの移動の入力値
    /// </summary>
    private Vector2 _player_Input_Vector = default;
    #endregion

    #region int
    /// <summary>
    /// 回転の向き
    /// </summary>
    private int _turn_Direction = default;
    #endregion

    private void Awake()
    {
        _playerinput = GetComponent<PlayerInput>();
        _map = GetComponent<Map>();
        _variables = new Variables();
    }

    private void OnEnable()
    {
        _playerinput.actions[_variables._move].performed += Move;
        _playerinput.actions[_variables._turn_Key].started += TurnKey;
        _playerinput.actions[_variables._turn_Pad].performed += TurnPad;
    }


    private void OnDisable()
    {
        _playerinput.actions[_variables._move].performed -= Move;
        _playerinput.actions[_variables._turn_Key].started -= TurnKey;
        _playerinput.actions[_variables._turn_Pad].performed -= TurnPad;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        _player_Input_Vector = obj.ReadValue<Vector2>();
        _map.Move(_player_Input_Vector);
    }
    private void TurnKey(InputAction.CallbackContext obj)
    {
        _map.Turn(_variables._one);
    }
    private void TurnPad(InputAction.CallbackContext obj)
    {
        _map.Turn(obj.ReadValue<int>());
    }
}
