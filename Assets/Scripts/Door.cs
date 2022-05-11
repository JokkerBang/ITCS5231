using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public GameObject pivot;
    public float final_angle;
    public float direction;
    public GameObject opposite_door;

    public enum State { CLOSED, OPENING, OPEN }
    public State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.CLOSED;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.OPEN:
                break;
            case State.OPENING:
                float y_angle = gameObject.transform.eulerAngles.y;
                if (Mathf.Abs(final_angle - y_angle) > 5)
                {
                    print($"diff: {final_angle - y_angle} dir: {direction} movement: {Vector3.up * direction}");
                    transform.RotateAround(pivot.transform.position, Vector3.up * direction, 20 * Time.deltaTime);
                }
                else state = State.OPEN;

                break;
            case State.CLOSED:
                break;
        }
    }

    public void Open()
    {
        Door other_door = opposite_door.GetComponent<Door>();
        if (state == State.CLOSED) state = State.OPENING;
        if (other_door.state == State.CLOSED) other_door.Open();
    }
}
