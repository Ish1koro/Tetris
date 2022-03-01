using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    private PlayerController _playerController = default;
    private AI _aI = default;

    private _type _script_Type = default;

    #region View関係
    private const int _spawn_Postion_X = 12;
    private const int _spawn_Postion_Y = 21;
    private const int _STAGE_DISTANCE = 10;
    #endregion

    private enum _type
    {
        Player,
        AI
    }

    private void Awake()
    {
        if (TryGetComponent(out _playerController))
        {
            _script_Type = _type.Player;
        }
        else if (TryGetComponent(out _aI))
        {
            _script_Type = _type.AI;
        }
    }

    private void GenerateMino()
    {
        switch (_script_Type)
        {
            case _type.Player:
                break;
            case _type.AI:
                break;
        }
    }

    private void MovemMino()
    {

    }
}
