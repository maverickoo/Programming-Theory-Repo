using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Animal : MonoBehaviour
{


    private const float Bound = 30;

    protected Rigidbody rb;

    // ENCAPSULATION
    protected string m_name;
    public string name
    {
        get { return m_name; }
        set
        {
            if (value.Length > 10)
            {
                Debug.Log("animal's name is too long");
            }
            else
            {
                m_name = value;
            }
        }
    }


    private const float MaxSpeed = 1000;
    private float m_Speed = 100;
    public float speed
    {
        get { return m_Speed; }
        set
        {
            if(value > 0 && value < MaxSpeed)
            {
                m_Speed = value;
            }
            else
            {
                Debug.Log("speed value cant set negative or higher than " + MaxSpeed);
            }
        }
    }


    protected float jumpPower = 30;
    private bool isOnGround;

    public bool isSelected;


    public Animal()
    {
        speed = 100;
    }

    public virtual void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.AddForce(Vector3.forward * m_Speed * verticalInput);
        rb.AddForce(Vector3.right * m_Speed * horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround) { Jump(); }

    }

    public virtual void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isOnGround = false;
    }


    //Prevent the player from leaving the top or bottom of the screen
    void ConstrainPosition()
    {
        if (transform.position.z > Bound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Bound);
        }
        else if (transform.position.z < -Bound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -Bound);
        }

        if (transform.position.x > Bound)
        {
            transform.position = new Vector3(Bound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -Bound)
        {
            transform.position = new Vector3(-Bound, transform.position.y, transform.position.z);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(m_Speed + "aa0 " + speed);
    }

    // Update is called once per frame
    void Update()
    {

        if(isSelected)
        {
            // ABSTRACTION
            Move();
 
        }
        ConstrainPosition();

    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }

    private void OnMouseDown()
    {
        GameManager.Instance.ClearSelect();
        isSelected = true;
        GameManager.Instance.SetName(name);
    }

    public void ClearSelect()
    {
        isSelected = false;
    }
}
