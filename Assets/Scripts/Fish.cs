using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fish : MonoBehaviour
{
    public Transform[] target;
    public float speed;
    private int current;
    // Use this for initialization    
    void Start() { }
    // Update is called once per frame    
    void Update()
    {
        if (transform.position != target[current].position)
        {
            Vector3 curpos = transform.position;
            Vector3 curtar = target[current].position;
            float angle = Mathf.Atan2(curtar.x - curpos.x, curtar.z - curpos.z) * Mathf.Rad2Deg;
            Vector3 new_vec = new Vector3(0f, angle, 0f);
            transform.eulerAngles = new_vec;
            Vector3 new_pos = Vector3.MoveTowards(curpos, curtar, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(new_pos);
        }
        else current = (current + 1) % target.Length;
    }
}
