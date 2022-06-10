using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("Transforms")]

    public VariableJoystick variableJoystick;

    public float velocity;
    public float turnSpeed = 10;
    [Header("Other Variables")]
    public bool isControlActive;
    [SerializeField] bool move, canRotate = true;

    Rigidbody rb;
    public Animator animator;

    public float angle;
    float deadzone = 0.01f;

    Vector2 input;
    Quaternion targetRotation;
    Quaternion lastRotation;

    Transform cam;

    bool animating, boostMode;

    public SkinnedMeshRenderer skin;

    #endregion

    #region Unity-Methods
    private void Start()
    {
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        lastRotation = transform.rotation;
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (PlayerManager.instance.isDead)
            return;
        
        if(!IsGrounded() && !animating)
        {
            GetGrounded();
        }

        if (isControlActive)
        {
            if (move)
            {
                // Move();
                Move2();
                AnimationSpeed();
            }
            else
            {
                IdleState(false);
            }

            GetInput();

            if (input.magnitude > deadzone)
            {
                CalculateDirection();
                if(canRotate)
                    Rotate();
            }
        }
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.transform.CompareTag("Enemy"))
    //     {
    //         if(velocity < 3)
    //            WinFailScenario.Instance.Fail();
    //     }
    // }
    #endregion

    #region Other-Methods
    public void IncreaseSpeed(bool state)
    {
        boostMode = state;
        velocity = state ? 4.5f : 2.7f;
        animator.SetFloat("speed", state ? 1.5f : 1);
    }

    public void PlaceMeAt(Transform t)
    {
        transform.SetPositionAndRotation(t.position, t.rotation);
    }

    public void ChangeMyMaterial(Material mat)
    {
        skin.material = mat;
    }

    public void CanRotate(bool state)
    {
        canRotate = state;
    }

    public void Animating(bool state)
    {
        rb.velocity = Vector3.zero;

        rb.angularVelocity = Vector3.zero;

        Move(!state);

        CanRotate(!state);

        GetComponent<CapsuleCollider>().enabled = !state;

        animating = state;
        //angle = 0;
    }

    public void Move(bool state)
    {
        move = state;
    }

    void IdleState(bool stopSmoothly)
    {
        input = Vector2.zero;
        transform.rotation = lastRotation;
        if (!stopSmoothly)
        {
            rb.velocity = Vector3.zero;

            PlayIdle();
        }
        else
        {
            StartCoroutine(StopSmoothly(rb.velocity, 0.5f, 0.05f));
        }
    }

    IEnumerator StopSmoothly(Vector3 startVel, float speedVal ,float duration)
    {
        float counter = 0f;

        while(counter < duration)
        {
            rb.velocity = Vector3.Lerp(startVel, Vector3.zero, counter / duration);
            counter += Time.deltaTime;
            yield return null;
        }
        rb.velocity = Vector3.zero;
        PlayIdle();
    }

    void GetInput()
    {
        input.x = variableJoystick.Direction.x;
        input.y = variableJoystick.Direction.y;
    }

    void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }

    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        lastRotation = transform.rotation;
    }

    void Move()
    {
        rb.velocity = transform.forward * velocity;
    }

    public float changeableVelocity = 0;
    // move forward amount with joystick 
    void Move2()
    {
        changeableVelocity = Mathf.Lerp(0, velocity,  Mathf.Abs(variableJoystick.Horizontal) + Mathf.Abs(variableJoystick.Vertical));
        rb.velocity = transform.forward * changeableVelocity;
    }

    public void EndControls()
    {
        animator.SetFloat("speed", 0);
        isControlActive = false;
        move = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void GetGrounded()
    {
        rb.AddForce(-transform.up * 10, ForceMode.VelocityChange);
    }

    bool IsGrounded()
    {
        //Debug.DrawRay(transform.position, -Vector3.up * (GetComponent<Collider>().bounds.extents.y + 0.12f));
        return Physics.Raycast(transform.position, -Vector3.up, 0.105f);
    }
    #endregion

    #region Animator

    void AnimationSpeed()
    {
        if(!boostMode)
            animator.SetFloat("speed", Mathf.Abs(variableJoystick.Horizontal) + Mathf.Abs(variableJoystick.Vertical));
    }
    
    public void PlayIdle()
    {
       animator.SetBool("canRun", false);
    }

    public void PlayWinAnim()
    {
        animator.SetTrigger("Win");
    }

    public void PlayFailAnim()
    {
        animator.SetTrigger("Fail");
    }
    
    public void DieAndFail()
    {
        animator.SetTrigger("Fail");
        //GameManager.instance.LevelFailed();
    }
    #endregion
}
