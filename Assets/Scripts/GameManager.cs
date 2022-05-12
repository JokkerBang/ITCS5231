using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text TextHUD;
    public Text text_level;

    public GameObject panel_menu;
    public GameObject panel_hud;
    public GameObject panel_level_completed;
    public GameObject panel_game_over;
    public GameObject level1;
    public GameObject level2;
    public GameObject player;
    public Text text_game_over;
    public Text text_level_complete;
    public GameObject panel_tutorial;
    public GameObject panel_buttons;
    public GameObject panel_win;

    float level_countdown;
    float tutorial_countdown;
    int level_current;
    bool tutorial_active;

    public enum State { MENU, INIT, PLAY, LEVELCOMPLETED, LOADLEVEL, GAMEOVER, WIN }
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
        text_game_over.text = "";
        tutorial_active = false;
        level2.SetActive(false);
        tutorial_countdown = 0;
    }

    private void Reset()
    {
        HidePanels();
        HideTutorial();
        level_countdown = 10f;
        level_current = 1;
        text_game_over.text = "";
        tutorial_active = false;
        level2.SetActive(false);
        tutorial_countdown = 0;
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
                if (tutorial_active)
                {
                    tutorial_countdown -= Time.deltaTime;
                    if (tutorial_countdown <= 0)
                    {
                        HideTutorial();
                    }
                }
                break;
            case State.LEVELCOMPLETED:
                level_countdown -= Time.deltaTime;
                text_level_complete.text = Mathf.Round(level_countdown).ToString();
                if (level_countdown <= 0)
                {
                    SwitchState(State.PLAY);
                }
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                break;
            case State.WIN:
                break;
        }
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.N))
        {
            SwitchState(State.LEVELCOMPLETED);
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
                Reset();
                SwitchState(State.PLAY);
                break;
            case State.PLAY:
                if (level_current == 1) level1.SetActive(true);
                else if (level_current == 2) level2.SetActive(true);
                panel_hud.SetActive(true);
                string level_text = $"Level {level_current}";
                text_level.text = level_text;
                ResetPlayer();
                break;
            case State.LEVELCOMPLETED:
                panel_level_completed.SetActive(true);
                DisplayTutorial("I'm falling!!!");
                level1.SetActive(false);
                level_current++;
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                ResetPlayer();
                HideTutorial();
                panel_game_over.SetActive(true);
                break;
            case State.WIN:
                ResetPlayer();
                level2.SetActive(false);
                panel_win.SetActive(true);
                break;
        }
    }

    void EndState()
    {
        HidePanels();
        HideTutorial();
        if (_state == State.PLAY && level_current == 1)
        {
            GameObject.Find("fish").GetComponent<Fish>().Quiet();
        }
    }

    public void SwitchState(State new_state)
    {
        EndState();
        BeginState(new_state);
        _state = new_state;
        print(new_state);
    }

    public void PlayClicked()
    {
        SwitchState(State.INIT);
    }

    public void GameOver()
    {
        SwitchState(State.GAMEOVER);
    }

    public void Win()
    {
        SwitchState(State.WIN);
    }

    private void ResetPlayer()
    {
        player.transform.position = new Vector3(0f, 3f, -75f);

    }

    public void DisplayTutorial(string text_to_show, float countdown = 10, bool activate_buttons = false)
    {
        panel_tutorial.SetActive(true);
        panel_tutorial.GetComponentInChildren<Text>().text = text_to_show;
        tutorial_countdown = countdown;
        if (activate_buttons)
        {
            panel_buttons.SetActive(true);
        } else panel_buttons.SetActive(false);
        tutorial_active = true;
    }

    public void HideTutorial()
    {
        panel_tutorial.SetActive(false);
        tutorial_active = false;
    }

    void HidePanels()
    {
        panel_game_over.SetActive(false);
        panel_hud.SetActive(false);
        panel_level_completed.SetActive(false);
        panel_menu.SetActive(false);
        panel_tutorial.SetActive(false);
    }
}