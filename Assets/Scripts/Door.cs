using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public GameObject pivot;
    public float final_angle;
    public float direction;
    public GameObject opposite_door;

    public enum DoorState { CLOSED, OPENING, OPEN }
    public DoorState state;

    // Start is called before the first frame update
    void Start()
    {
        state = DoorState.CLOSED;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case DoorState.OPEN:
                break;
            case DoorState.OPENING:
                float y_angle = gameObject.transform.eulerAngles.y;
                if (Mathf.Abs(final_angle - y_angle) > 5)
                {
                    transform.RotateAround(pivot.transform.position, Vector3.up * direction, 20 * Time.deltaTime);
                }
                else
                {
                    state = DoorState.OPEN;
                    gameObject.GetComponent<AudioSource>().Stop();
                }
                break;
            case DoorState.CLOSED:
                break;
        }
    }

    public void Open()
    {
        Door other_door = opposite_door.GetComponent<Door>();
        if (state == DoorState.CLOSED)
        {
            state = DoorState.OPENING;
            gameObject.GetComponent<AudioSource>().Play();
        }
        if (other_door.state == DoorState.CLOSED) other_door.Open();
    }
}
