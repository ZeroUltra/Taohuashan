using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region 人物状态

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
    #endregion
    [SerializeField]
    private float moveSpeed;

    private Animator playerAnimator;
    private Coroutine moveCoroutine = null;
    private GameObject getColorFX;
    private AudioSource audioSource;
    private Vector2 baseScale;

    public bool canMove = true;
    public bool horizaltalMove = false;
    public float disStop = 0.3f;
    public static bool isLock = false;
    public PolyNavAgent navAgent;
    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        getColorFX = Resources.Load<GameObject>("FX/getcolorFx");
        audioSource = GetComponent<AudioSource>();
        baseScale = transform.localScale;
        isLock = false;
    }


    private void ChangePlayerState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                playerAnimator.SetBool("idle", true);
                playerAnimator.SetBool("walk", false);
                playerAnimator.SetBool("xise", false);
                audioSource.Stop();

                break;
            case PlayerState.Walk:
                playerAnimator.SetBool("idle", false);
                playerAnimator.SetBool("walk", true);
                playerAnimator.SetBool("xise", false);
                audioSource.Play();
                break;
            case PlayerState.Xise:
                playerAnimator.SetBool("idle", false);
                playerAnimator.SetBool("walk", false);
                playerAnimator.SetBool("xise", true);
                audioSource.Stop();
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 玩家移动
    /// </summary>
    /// <param name="pos"></param>
    public void PlayerMove(Vector2 pos, System.Action action = null)
    {
        if (isLock) return;
        //pos = new Vector2(pos.x, transform.position.y-0.5f);
        if (_playerState != PlayerState.Walk)
            _PlayerState = PlayerState.Walk;
        if (moveCoroutine == null)
            moveCoroutine = StartCoroutine(IEMove(pos,action));
        else
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
            moveCoroutine = StartCoroutine(IEMove(pos));
        }
    }
    private IEnumerator IEMove(Vector2 pos, System.Action action = null)
    {
        bool isFlip = pos.x - transform.position.x < 0;//是否转向
        if (isFlip) transform.localScale = new Vector2(-baseScale.x, baseScale.y);
        else transform.localScale = baseScale;
        if (horizaltalMove)
        {
            pos = new Vector2(pos.x,transform.position.y);
        }
        while (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), pos) >= disStop)
        {
            Vector2 dir = (pos - new Vector2(transform.position.x, transform.position.y));
            dir.Normalize();
            if (canMove)
            {
                if (horizaltalMove)
                {
                    dir = isFlip ? Vector2.left : Vector2.right;
                    transform.Translate(dir * Time.deltaTime * moveSpeed);
                }
                else
                {
                    transform.Translate(dir * Time.deltaTime * moveSpeed);
                }
            }
            yield return null;
        }
        if (action != null) action.Invoke();
        _PlayerState = PlayerState.Idle;
        moveCoroutine = null;
    }

    public void SetLock(bool islock)
    {
        isLock = islock;
    }
    public void StopMove()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
            _PlayerState = PlayerState.Idle;
            if (navAgent != null)
            {
                navAgent.Stop();
            }
        }
    }
    /// <summary>
    /// 设置朝向
    /// </summary>
    /// <param name="right"></param>
    public void SetPlayerDir(bool right)
    {
        if (right)
        {
            transform.localScale = baseScale;
        }
        else
        {
            transform.localScale = new Vector2(-baseScale.x, baseScale.y);
        }
    }
    /// <summary>
    /// 当获取到颜色播放特效
    /// </summary>
    public void GetColor(Color color)
    {
        GameObject go = Instantiate(getColorFX, transform.position + Vector3.up * 1.35f, Quaternion.identity);
        go.GetComponent<SpriteRenderer>().color = color;
        _PlayerState = PlayerState.Xise;
        GameTools.WaitDoSomeThing(this, 0.8f, () =>
        {
            if (_PlayerState != PlayerState.Walk)
                _PlayerState = PlayerState.Idle;
        });
        Destroy(go, 0.7f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Trigger1")
        {
            Destroy(other.gameObject);
            (UIManagerFirst.Instance as UIManagerFirst).GuideFocusOn();
        }
        else if (other.name == "Trigger2")
        {
            if (!Scene01Manager.isGuide) return;
            Destroy(other.gameObject);
            (UIManagerFirst.Instance as UIManagerFirst).ShowEndPrompt();
        }
        //-------------第二关-------------
        //else if (other.name == "cloud_after")
        //{
        //    if (!Scene01Manager.isGuide) return;
        //    Destroy(other.gameObject);
        //    (UIManagerFirst.Instance as UIManagerFirst).ShowEndPrompt();
        //}
    }
}
public enum PlayerState
{
    Idle, Walk, Xise
}