using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public Transform[] target;
    public float speed;
    public GameObject door;
    public Transform passage;

    private int current;

    void Start()
    {
        
    }
    void Update()
    {
        Door door_script = door.GetComponent<Door>();
        if (door_script.state == Door.DoorState.OPEN)
        {
            Vector3 curtar = passage.position;
            Vector3 new_pos = get_new_position(curtar);
            GetComponent<Rigidbody>().MovePosition(new_pos);
        }
        else if (transform.position != target[current].position)
        {
            Vector3 curtar = target[current].position;
            Vector3 new_pos = get_new_position(curtar);
            GetComponent<Rigidbody>().MovePosition(new_pos);
        }
        else current = (current + 1) % target.Length;
    }

    Vector3 get_new_position(Vector3 target)
    {
        Vector3 curpos = transform.position;
        float angle = Mathf.Atan2(target.x - curpos.x, target.z - curpos.z) * Mathf.Rad2Deg;
        Vector3 new_vec = new Vector3(0f, angle, 0f);
        transform.eulerAngles = new_vec;
        Vector3 new_pos = Vector3.MoveTowards(curpos, target, speed * Time.deltaTime);
        return new_pos;
    }
}
