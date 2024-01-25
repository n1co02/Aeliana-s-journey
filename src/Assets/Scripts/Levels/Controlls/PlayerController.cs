using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

namespace ClearSky
{
    public class SimplePlayerController : MonoBehaviour
    {
        public float movePower = 10f;
        public float jumpPower = 15f; 
        public GameObject restartPanel = null;
        public GameObject staff = null;

        public GameObject player = null;
        public Button RestartGameYesButton = null;

        public ProjectileBehavior projectilePrefab;
        public Transform LaunchOffset;

        private Rigidbody2D rb;
        private Animator anim;
        private int direction = 1;
        bool isJumping = false;
        private bool alive = true;

        private int healthCheck;
        private float deathZoneYAxis = -100;

        public AudioSource audioSource = null;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            healthCheck = PlayerPrefs.GetInt("health");
            RestartGameYesButton.onClick.AddListener(Restart);
        }

        private void FixedUpdate()
        {
            if (alive)
            {
                Run();
            }  
        }

        private void Update()
        {
            if (alive)
            {
                Jump();
                Hurt();
                Die();
                Menu();
                if (PlayerPrefs.GetInt("shootAbility") == 1)
                {
                    staff.SetActive(true);
                    Attack();
                    Shoot();
                }
                else
                {
                    staff.SetActive(false);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.gameObject.name);
            anim.SetBool("isJump", false);
        }
      
        void Shoot()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(transform.localScale.x == 1)
                {
                    Instantiate(projectilePrefab, LaunchOffset.position, Quaternion.Euler(0, 180, 0));
                }
                else
                {
                    Instantiate(projectilePrefab, LaunchOffset.position, Quaternion.Euler(0, 0, 0));
                }
            }
        }

        void Menu()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }

        void Run()
        {
            Vector3 moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);

            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                direction = -1;
                moveVelocity = Vector3.left;

                transform.localScale = new Vector3(direction, 1, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);
            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                direction = 1;
                moveVelocity = Vector3.right;

                transform.localScale = new Vector3(direction, 1, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);
            }
            transform.position += moveVelocity * movePower * Time.deltaTime;
        }

        void Jump()
        {
            if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
            && !anim.GetBool("isJump"))
            {
                isJumping = true;
                anim.SetBool("isJump", true);
            }
            if (!isJumping)
            {
                return;
            }

            rb.velocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

            isJumping = false;
        }

        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                anim.SetTrigger("attack");
                audioSource.Play();
            }
        }

        void Hurt()
        {
            int currentHealth = PlayerPrefs.GetInt("health");
            if (currentHealth < healthCheck)
            {
                anim.SetTrigger("hurt");
                if (direction == 1)
                    rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
                else
                    rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
                healthCheck -= 1;
            }
            if (PlayerPrefs.GetInt("health") >= healthCheck)
            {
                healthCheck = currentHealth;
            }
        }

        void Die()
        {
            float characterPositionY = transform.position.y;

            if (PlayerPrefs.GetInt("health") == 0 || characterPositionY < deathZoneYAxis)
            {
                anim.SetTrigger("die");
                alive = false;
                restartPanel.SetActive(true);
            }   
        }

        void Restart()
        {
                anim.SetTrigger("idle");
                alive = true;
                player.transform.position = new Vector3(PlayerPrefs.GetFloat("playerXCoordinate"), PlayerPrefs.GetFloat("playerYCoordinate"));
        }
    }
}