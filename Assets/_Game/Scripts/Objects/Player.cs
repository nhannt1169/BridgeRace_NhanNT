using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    private Joystick joystick;
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity != Vector3.zero)
        {
            ChangeAnim(Utils.animMove);
        }
        else
        {
            ChangeAnim(Utils.animIdle);
        }
    }

    private void FixedUpdate()
    {
        if (joystick == null)
        {
            return;
        }
        rb.velocity = new Vector3(joystick.Horizontal * speed * Time.fixedDeltaTime, rb.velocity.y, joystick.Vertical * speed * Time.fixedDeltaTime);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    public void SetJoystick(Joystick joystick)
    {
        this.joystick = joystick;
    }

    public void Win(Vector3 position)
    {
        TF.position = position;
        rb.velocity = Vector3.zero;

        rb.AddForce(20f * Vector3.up);
        ChangeAnim(Utils.animJump);
    }
}
