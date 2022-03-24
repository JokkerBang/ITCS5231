using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ghost ghost = gameObject.GetComponentInParent<Ghost>();
        float pitch = ghost.pitch;
        float yaw = ghost.yaw;
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }
}
