using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemy : MonoBehaviour
{
    public Transform[] target;
    public float speed;
    public Transform fish_target;
    public Transform awareness;
    private int current;
    public bool eat;
    // Start is called before the first frame update
    void Start()
    {
        eat = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (eat) 
        {
            Vector3 curtar = fish_target.position;
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
