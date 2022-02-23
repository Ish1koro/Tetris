using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Playerの操作に関するクラス
public class PlayerController : Map
{
    #region 他スクリプト参照
    /// <summary>
    /// Playerの入力を取得
    /// </summary>
    private PlayerInput _playerinput = default;
    #endregion

    #region Vector
    /// <summary>
    /// Playerの移動の入力値
    /// </summary>
    private Vector2 _player_Input_Vector = default;
    #endregion

    #region int
    #endregion

    private void Awake()
    {
        _playerinput = GetComponent<PlayerInput>();
        _viewController = GetComponent<ViewController>();
        Generate();
    }

    private void Upadate()
    {
        Fall();
    }

    private void OnEnable()
    {
        _playerinput.actions[Variables._move].performed += Move;
        _playerinput.actions[Variables._turn_Key].started += TurnKey;
        _playerinput.actions[Variables._turn_Pad].performed += TurnPad;
    }

    private void OnDisable()
    {
        _playerinput.actions[Variables._move].performed -= Move;
        _playerinput.actions[Variables._turn_Key].started -= TurnKey;
        _playerinput.actions[Variables._turn_Pad].performed -= TurnPad;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        _player_Input_Vector = obj.ReadValue<Vector2>();
        Move(_player_Input_Vector);
    }

    private void TurnKey(InputAction.CallbackContext obj)
    {
        Turn(Variables._one);
    }

    private void TurnPad(InputAction.CallbackContext obj)
    {
        Turn(obj.ReadValue<int>());
    }
}
