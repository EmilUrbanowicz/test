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
    }


    void Start()
    {

    }

    void Update() 
    {
        _isShooting |= Input.GetKeyDown(KeyCode.Space);
    }

    void FixedUpdate()
    {
        if (_isShooting)
        {
            Vector3 spawnPos = transform.position +
                                   transform.forward * 1f;

            GameObject newBullet = Instantiate(Bullet, spawnPos,
                                        this.transform.rotation);

            Rigidbody bulletRB =
               newBullet.GetComponent<Rigidbody>();

            bulletRB.linearVelocity = this.transform.forward *
                                         BulletSpeed;

        }
        _isShooting = false;

    }
    private bool IsGrounded()
    {
    
    }
}
