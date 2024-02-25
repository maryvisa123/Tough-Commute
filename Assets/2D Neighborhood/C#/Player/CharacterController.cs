using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CharacterController {
    public class CharacterController : MonoBehaviour {
        public ParticleSystem dust;
        private Animator anim;
        private float horizontal = 1.0f;  // Set to 1.0f for constant right movement
        private bool isJumping;
        private float coyoteTimeCounter;
        private float jumpBufferCounter;
        
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private ToggleImages toggleImages;

        public float baseSpeed;
        [SerializeField] private float speed = 8f;
        [SerializeField] private float jumpingPower = 16f;
        [SerializeField] private float coyoteTime = 0.2f;
        [SerializeField] private float jumpBufferTime = 0.5f;

        private bool isWalking = true;
        private bool canMove = true;  // variable to control if the character is allowed to move
        
        private const string MainSceneName = "Main Scene"; // Use a constant for the main scene name

        private void Start() 
        {
            anim = GetComponent<Animator>();

            if (GameManager.Instance != null)
            {
                string currentScene = SceneManager.GetActiveScene().name;

                if (currentScene == GameManager.Instance.mainSceneName)
                {
                    GameManager.Instance.RestorePlayerPosition(this);
                    isWalking = true;
                }
                else
                {
                    isWalking = false;
                }
            }
            else
            {
                Debug.LogWarning("GameManager instance not found.");
            }
        }

        private void Update() {
            if (!isWalking) {
                // If not walking, don't proceed with movement code
                return;
            }

            if (IsGrounded()) {
                coyoteTimeCounter = coyoteTime;
            } else {
                coyoteTimeCounter -= Time.deltaTime;
            }

            if (Input.GetButtonDown("Jump") && !isJumping) {
                CreateDust();
                isJumping = true;
                anim.SetTrigger("takeOff");
                jumpBufferCounter = jumpBufferTime;
            } else {
                jumpBufferCounter -= Time.deltaTime;
                isJumping = false;
            }

            if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping) {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                jumpBufferCounter = 0f;
                StartCoroutine(JumpCooldown());
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                isJumping = false;
                coyoteTimeCounter = 0f;
            }

            if (rb.velocity.y == 0) {
                anim.SetBool("isJumping", false);
            } else {
                anim.SetBool("isJumping", true);
            }

            if (rb.velocity.y > 0) {
                anim.SetBool("isJumping", true);
            } else {
                anim.SetBool("isJumping", false);
            }
        }

        private void FixedUpdate() {
            if (!isWalking) {
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetBool("isWalking", false);
                return;
            }

            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            anim.SetBool("isWalking", horizontal != 0);
        }

        public void ToggleWalking() {
            isWalking = !isWalking; // Toggle the walking state
        }

        public void StopCharacter()
        {
            canMove = false;
            isWalking = false;
            if (toggleImages != null)
            {
                toggleImages.UpdateUI(); // Update the UI
            }
        }

        public bool IsCharacterStopped()
        {
            return !canMove;
        }

        public void ResumeCharacter()
        {
            if (canMove)
            {
                isWalking = true;
            }
        }

        public void AllowMovement()
        {
            canMove = true;
            isWalking = true;
            if (toggleImages != null)
            {
                toggleImages.UpdateUI(); // Update the UI
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("MagnifyingGlass")) {
                StopCharacter();
            }

            if (collision.gameObject.CompareTag("Finish")) {
                // Call the GoToScene function in the GameManager to switch to the game over scene
                GameManager.Instance.GoToScene("GameOverScene 1");
            }
        }


        private bool IsGrounded() {
            return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        }

        private IEnumerator JumpCooldown() {
            isJumping = true;
            yield return new WaitForSeconds(0.1f);
            isJumping = false;
        }
        
        void CreateDust() {
            dust.Play();
        }
    }
}
