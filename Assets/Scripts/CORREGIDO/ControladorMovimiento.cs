using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorMovimiento : MonoBehaviour
{
    //movimiento basico
    public Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float turnSmoothTime;
    [SerializeField] Transform cam;
    [SerializeField] float turnSmoothVelocity;
    public bool canMove;

    //sprint
    public bool estaCorriendo;
    [SerializeField] float sprintMultiplier;
    private float sprintSpeed = 1;


    //coordenadas para animaciones de movimiento
    [SerializeField] float x, y;
    public Animator anim;

    ControladorCombate player;

    void Start()
    {
        Cursor.visible = false;
        canMove = true;

        rb = GetComponent<Rigidbody>();
        player= GetComponent<ControladorCombate>();
        anim = GetComponent<Animator>();

 
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }

        else
        {

            Vector3 direction = inputMovimiento();


            if (x == 0f && y == 0f)
            {
                ControladorSonido.instance.stopFootstep();
            }
            else
            {
                ControladorSonido.instance.playFootstep(estaCorriendo);

            }
            if (direction.magnitude >= 0.1f && !player.atacando && !anim.GetBool("blocking"))
            {
                runCheck();
                mover(direction);

                anim.SetFloat("Velx", x);
                anim.SetFloat("Vely", y);

            }

        }
    }
    public Vector3 inputMovimiento()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0f, y).normalized;

        return direction;
    }
    public void mover(Vector3 direction)
    {

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        Vector3 movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;




        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 movement = movDir.normalized * speed * Time.deltaTime * sprintSpeed;
        rb.MovePosition(transform.position + movement);
    }

    //verifica si el usuario oprimió la tecla LeftShift, si lo hizó multiplica la velocidad base
    public void runCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
                estaCorriendo = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            estaCorriendo = false;

        }

        if (estaCorriendo == true && !player.atacando && canMove)
        {
            sprintSpeed = sprintMultiplier;
            anim.SetBool("running", true);


        }
        else if (estaCorriendo == false || player.atacando || !canMove)
        {
            sprintSpeed = 1;
            anim.SetBool("running", false);

        }
    }

    //setters y getters
    public float returnSpeed()
    {
        return speed;
    }
    public void setSpeed(float speed1)
    {
        speed = speed1;
    }

    public void setMultiplier(float m1)
    {
        sprintMultiplier = m1;
    }
}
