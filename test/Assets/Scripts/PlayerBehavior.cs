using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    private float _vInput;
    private float _hInput;
    private Rigidbody _rb;
    public GameObject Bullet;
    public float BulletSpeed = 100f;
    private bool _isShooting;
    public float JumpVelocity = 5f;
    private bool _isJumping; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;
        _isShooting |= Input.GetKeyDown(KeyCode.Space);
        _isJumping |= Input.GetKeyDown(KeyCode.J);
    }

    void FixedUpdate()
    {
        // 2
        Vector3 rotation = Vector3.up * _hInput;
        // 3
        Quaternion angleRot = Quaternion.Euler(rotation *
            Time.fixedDeltaTime);
        // 4
        _rb.MovePosition(this.transform.position +
            this.transform.forward * _vInput * Time.fixedDeltaTime);
        // 5
        _rb.MoveRotation(_rb.rotation * angleRot);

        if (_isShooting)
        {
            // 5
            Vector3 spawnPos = transform.position +
                                   transform.forward * 1f;
            // 6
            GameObject newBullet = Instantiate(Bullet, spawnPos,
                                       this.transform.rotation);
            // 7
            Rigidbody bulletRB =
                newBullet.GetComponent<Rigidbody>();

            // 8
            bulletRB.linearVelocity = this.transform.forward *
                                          BulletSpeed;
        }

        if(_isJumping)
        {
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
        }

        _isJumping = false;
    }
    private bool IsGrounded()
    {
        return true;
    }

    
}
