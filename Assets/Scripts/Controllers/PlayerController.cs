using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    [SerializeField] float JumpPower;
    [SerializeField] Vector2 HorizontalMoveRange;
    [SerializeField] Light2D mineLight;

    private Rigidbody2D playerRigidbody;
    private LevelUIController uiContoller;

    private float currentDirection;
    private bool _onGround;
    private SaveManager saveManager;

    [SerializeField] private AudioSource source;

    [SerializeField] private AudioClip GameMusic;
    [SerializeField] private AudioClip CoinCollectSound;
    [SerializeField] private AudioClip JumpSound;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        uiContoller = FindAnyObjectByType<LevelUIController>();
        source = GetComponent<AudioSource>();
        saveManager = FindAnyObjectByType<SaveManager>();
        source.clip = GameMusic;
        source.loop = true;
        source.Play();
        source.volume = saveManager.GetMusicLevel();
    }

    private void OnEnable()
    {
        uiContoller.MoveInputEvent.AddListener(Move);
        uiContoller.JumpInputEvent.AddListener(Jump);
    }

    private void OnDisable()
    {
        uiContoller.MoveInputEvent.RemoveListener(Move);
        uiContoller.JumpInputEvent.RemoveListener(Jump);
    }

    void Update()
    {
/*#if UNITY_EDITOR*/
        Move(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
/*#endif*/

        playerRigidbody.AddForce((currentDirection * MoveSpeed * Time.deltaTime) * Vector2.right);
        //playerRigidbody.AddTorque((currentDirection * MoveSpeed * Time.deltaTime) );

        if (transform.position.x < HorizontalMoveRange.x)
        {
            playerRigidbody.velocity = Vector2.zero;
            transform.position = new Vector2(HorizontalMoveRange.x + .1f, transform.position.y);
        }
        else if (transform.position.x > HorizontalMoveRange.y)
        {
            transform.position = new Vector2(HorizontalMoveRange.y - .1f, transform.position.y);
            playerRigidbody.velocity = Vector2.zero;
        }


        var rayOrgin = new Vector2(transform.position.x, transform.position.y - (transform.localScale.y / 2) - .02f);
        var hit = Physics2D.Raycast(rayOrgin, Vector2.down, .1f);
        _onGround = hit.collider;
        //Debug.DrawRay(rayOrgin, Vector2.down * .1f);
        //Debug.Log("Moveing " + currentDirection);
    }

    public void Move(float direction) => currentDirection = direction;

    public void Jump()
    {
        if (_onGround)
        {
            playerRigidbody.AddForce(JumpPower * Vector2.up);
            source.PlayOneShot(JumpSound, saveManager.GetSoundFxLevel());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            if (collision.TryGetComponent(out LightUpPower lightUpPower))
            {
                mineLight.pointLightOuterRadius += lightUpPower.lightAreaInceaseAmount;
                mineLight.falloffIntensity += lightUpPower.lightIntensityIncreaseAmount;
            }
            else if (collision.TryGetComponent(out ScaleUpPower scaleUpPower))
            {
                transform.localScale = Vector3.one * (transform.localScale.x + scaleUpPower.scaleIncreaseAmount);
            }
            Destroy(collision.gameObject);
        }

        if (collision.TryGetComponent(out CoinBehaviour coinBehaviour))
        {
            coinBehaviour.Collect();
            source.PlayOneShot(CoinCollectSound, saveManager.GetSoundFxLevel());
        }
    }
}
