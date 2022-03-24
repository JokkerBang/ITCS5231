using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private Transform ground_check_transform = null;
    [SerializeField] private LayerMask player_mask;
    [SerializeField] float speed_h = 2f;
    [SerializeField] float speed_v = 2f;
    public float yaw = 0f;
    public float pitch = 0.5f;

    bool pressed_jump;
    float horizontal_input, forward_input;
    int scale;
    Rigidbody rigid_body;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        scale = 5;
        rigid_body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressed_jump = true;
        }
        horizontal_input = Input.GetAxis("Horizontal");
        forward_input = Input.GetAxis("Vertical");

        Action("grabbing", "g");
        Action("pushing", "e");

        // camera and movement direction
        yaw -= speed_h * Input.GetAxis("Mouse X");
        pitch -= speed_v * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(0.5f, yaw, 0f);
    }

    // FixedUpdate is called once per physics update
    private void FixedUpdate()
    {
        Camera camera = gameObject.GetComponentInChildren<Camera>();
        
        Vector3 movement = new Vector3(horizontal_input * scale, 0f, forward_input * scale);
        movement = camera.transform.TransformDirection(movement);
        rigid_body.velocity = new Vector3(movement.x, rigid_body.velocity.y, movement.z);

        if (Physics.OverlapSphere(ground_check_transform.position, 1, player_mask).Length == 0)
        {
            return;
        }
        if (pressed_jump)
        {
            rigid_body.AddForce(Vector3.up * scale, ForceMode.VelocityChange);
            pressed_jump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
        }
    }

    private void Action(string action_name, string key)
    {
        bool pressed_grab = Input.GetKey(key);
        bool grabbing = animator.GetBool(action_name);

        if (!grabbing && pressed_grab)
        {
            animator.SetBool(action_name, true);
        }
        else if (grabbing && !pressed_grab)
        {
            animator.SetBool(action_name, false);
        }
    }
}
