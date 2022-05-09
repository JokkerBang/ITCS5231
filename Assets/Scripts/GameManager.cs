using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text TextHUD;
    public Text TextLevel;

    public GameObject panel_menu;
    public GameObject panel_hud;
    public GameObject panel_level_completed;
    public GameObject panel_game_over;
    public GameObject level1;
    public GameObject level2;
    public Ghost player;

    float level_countdown;
    int level_current;

    public enum State { MENU, INIT, PLAY, LEVELCOMPLETED, LOADLEVEL, GAMEOVER }
    State _state;

    private int _health;

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        SwitchState(State.MENU);
        level_countdown = 10f;
        level_current = 1;
        level2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                level_countdown -= Time.deltaTime;
                if (level_countdown <= 0)
                {
                    SwitchState(State.PLAY);
                }
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                break;
        }
    }

    void BeginState(State new_state)
    {
        switch (new_state)
        {
            case State.MENU:
                panel_menu.SetActive(true);
                break;
            case State.INIT:
                panel_hud.SetActive(true);
                break;
            case State.PLAY:
                panel_level_completed.SetActive(false);
                panel_game_over.SetActive(false);
                if (level_current == 1) level1.SetActive(true);
                else if (level_current == 2) level2.SetActive(true);
                player.transform.position = new Vector3(0f, 3f, -75f);
                break;
            case State.LEVELCOMPLETED:
                panel_level_completed.SetActive(true);
                level1.SetActive(false);
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                panel_game_over.SetActive(true);
                break;
        }
    }

    void EndState()
    {
        switch (_state)
        {
            case State.MENU:
                panel_menu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                panel_level_completed.SetActive(false);
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                panel_hud.SetActive(false);
                panel_game_over.SetActive(false);
                break;
        }
    }

    public void SwitchState(State new_state)
    {
        EndState();
        BeginState(new_state);
        print(new_state);
    }

    public void PlayClicked()
    {
        SwitchState(State.INIT);
    }
}