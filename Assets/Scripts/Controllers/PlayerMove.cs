using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;

    public float speed;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode jumpKey2 = KeyCode.Space;

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject healthBar;
    

    private bool crouched = false;
    private float originalHeight;
    [SerializeField] private float crouchHeight = 0.5f;
    public KeyCode crouchKey = KeyCode.C;
    public KeyCode crouchKey2 = KeyCode.C;

    public GameObject maskIcon;
    public bool maskCheck = false;

    public static bool enemyStop = false;
    public Rigidbody rb;
    public int health = 5;
    public bool damageTimer = true;
    

    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    bool gamePaused = false;
    bool gameOver = false;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        maskCheck = false;
        enemyStop = false;
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(crouchKey) || Input.GetKeyDown(crouchKey2))
        {
            crouched = !crouched;

            CheckCrouch();
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if ((Input.GetKeyDown(jumpKey) || Input.GetKeyDown(jumpKey2)) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Escape"))
        {


            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    void CheckCrouch()
    {
        if (crouched == true)
        {
            controller.height = crouchHeight;
            speed = speed / 2;
        }
        else
        {
            controller.height = originalHeight;
            if (speed != 12)
            {
                speed = 12;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.CompareTag("Enemy"))
        {
            {
                // health -= 1;
                StartCoroutine(Pain()); ;
                Debug.Log("Health = " + health.ToString());

                
            }

            if (health == 0)
            {
                gameObject.SetActive(false);
                SceneManager.LoadScene("Lose Scene");
                gameOver = true;
                if (gameOver == true)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }*/

        if (other.gameObject.CompareTag("Mask"))
        {
            {
                //value used to check if enemy can be disabled
                enemyStop = true;
                other.gameObject.SetActive(false);
                maskIcon.SetActive(true);
                maskCheck = true;

            }
        }

        if (other.gameObject.CompareTag("Goal"))
        {
            SceneManager.LoadScene("Win Scene");
            gameOver = true;
            if (gameOver == true)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (damageTimer == true)
            {
                {
                    health -= 1;
                    StartCoroutine(Pain());
                    Debug.Log("Health = " + health.ToString());
                }
            }
            if (health == 0)
            {
                gameObject.SetActive(false);
                SceneManager.LoadScene("Lose Scene");
                gameOver = true;
                if (gameOver == true)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
    }

    IEnumerator Pain()
    {
        damageTimer = false;
        yield return new WaitForSecondsRealtime(1.5f);
        damageTimer = true;
    }

    public void Resume()
    {

        pauseMenuUI.SetActive(false);
        healthBar.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        if(maskCheck == true)
        {
            maskIcon.SetActive(true);
        }

    }
    void Pause()

    {

        pauseMenuUI.SetActive(true);
        healthBar.SetActive(false);
        maskIcon.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;


    }
}