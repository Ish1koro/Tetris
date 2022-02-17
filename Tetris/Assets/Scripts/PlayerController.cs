using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Playerの操作に関するクラス
public class PlayerController : MonoBehaviour
{
    #region 他スクリプト参照
    // Playerの入力を取得
    private PlayerInput _playerinput = default;

    // 配列クラス
    private Map _map = default;

    // よく使う変数などを定義したクラス
    private Variables _variables = default;
    #endregion

    #region Vector
    // Playerの移動の入力値
    private Vector2 _player_Input_Vector = default;
    #endregion

    #region int
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
