using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private Transform ground_check_transform = null;
    [SerializeField] private LayerMask player_mask;
    [SerializeField] float speed_h;
    [SerializeField] float speed_v;
    public float yaw = 0f;
    public float pitch = 0.5f;
    public int scale;

    bool pressed_jump;
    float horizontal_input, forward_input;
    Rigidbody rigid_body;
    Animator animator;
    public int state = 0;

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
            rigid_body.AddForce(Vector3.up * 2 * scale, ForceMode.VelocityChange);
            pressed_jump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string m_ClipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (other.gameObject.layer == 7 && (m_ClipName.Equals("Grab") || m_ClipName.Equals("Push")))
        {
            Destroy(other.gameObject);
            if (other.name.Equals("Fish"))
            {
                if (m_ClipName.Equals("Grab"))
                {
                    state = 1;
                }
                else
                {
                    state = 2;
                }
            }
        }
        if (other.gameObject.layer == 8 && m_ClipName.Equals("Push"))
        {
            Destroy(other.gameObject);
            state = 1;
        }
        print(m_ClipName);
    }

    private void Action(string action_name, string key)
    {
        bool pressed_action = Input.GetKey(key);
        bool doing_action = animator.GetBool(action_name);

        if (!doing_action && pressed_action)
        {
            animator.SetBool(action_name, true);
        }
        else if (doing_action && !pressed_action)
        {
            animator.SetBool(action_name, false);
        }
    }
}
