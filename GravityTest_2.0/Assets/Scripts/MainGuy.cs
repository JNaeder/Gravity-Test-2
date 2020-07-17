using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGuy : MonoBehaviour
{
    public Transform world, player;
    public Planet currentPlanet;
    public float moveSpeed = 2f;
    public float jumpForce = 10f;
    public bool isOnFoot;
    public LayerMask shipLayer;
    

    float h;
    Vector2 diffVec;

    Rigidbody2D rB;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnFoot)
        {
            Movement();
        }
    }


    void Movement() {
        h = Input.GetAxis("Horizontal");
        transform.position += transform.right * h * Time.deltaTime * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space)) {
            rB.AddForce(transform.up * jumpForce);
        }

        diffVec = world.position - player.position;
        float rot_z = Mathf.Atan2(diffVec.y, diffVec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

    }

    public bool IsNextToShip() {
        bool isNextToShip = Physics2D.OverlapCircle(transform.position, 2, shipLayer);
        return isNextToShip;
    }
}
