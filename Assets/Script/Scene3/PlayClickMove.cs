using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PlayClickMove : MonoBehaviour
{

    private PlayerController player;
    private GameObject clickFx;
    public bool canClick = true;
    public PolyNavAgent agent;
    void Start()
    {
        clickFx = Resources.Load<GameObject>("FX/clickFx");
        player = FindObjectOfType<PlayerController>();

    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
            agent.repath = true;
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if (agent.SetDestination(clickPos))
            {
                player.PlayerMove(clickPos);
                Destroy(Instantiate(clickFx, clickPos, Quaternion.identity), 1f);
            }
        }
        if (agent.dir.x > 0)
            player.SetPlayerDir(true);
        else if (agent.dir.x < 0)
            player.SetPlayerDir(false);

    }


}
