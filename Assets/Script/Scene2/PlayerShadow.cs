using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{

    private PlayerController player;
    private Animator playerAnimator;
    private PlayerState _playerState;
    public PlayerState _PlayerState
    {
        get
        {
            return _playerState;
        }

        set
        {
            _playerState = value;
            ChangePlayerState(value);
        }
    }
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        _PlayerState = player._PlayerState;
    }
    private void ChangePlayerState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                playerAnimator.SetBool("idle", true);
                playerAnimator.SetBool("walk", false);
                playerAnimator.SetBool("xise", false);


                break;
            case PlayerState.Walk:
                playerAnimator.SetBool("idle", false);
                playerAnimator.SetBool("walk", true);
                playerAnimator.SetBool("xise", false);

                break;
            case PlayerState.Xise:
                playerAnimator.SetBool("idle", false);
                playerAnimator.SetBool("walk", false);
                playerAnimator.SetBool("xise", true);

                break;
            default:
                break;
        }
    }
}
