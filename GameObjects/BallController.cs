using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 touchStartPos;
    private Vector3 objectStartPos;
    private bool isDragging = false;
    [SerializeField] private float _power = 0.01f;

    private Transform _transform;
    [SerializeField] ParticleSystem _ballDeathParticles;
    [SerializeField] ParticleSystem _balSuccessParticles;
    private LevelManager _lvlManager;
    private SpawnBall _ballSpawner;



    private void Awake()
    {
        _lvlManager = FindAnyObjectByType<LevelManager>();
        _ballSpawner = FindAnyObjectByType<SpawnBall>();
        _rb = GetComponent<Rigidbody2D>();
        _transform = transform.parent.transform;
    }


    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    StartDragging(touch.position);
                    break;

                case TouchPhase.Moved:
                    DragObject(touch.position);
                    break;

                case TouchPhase.Ended:
                    StopDragging();
                    break;
            }
        }
    }

    void StartDragging(Vector2 touchPos)
    {
        _rb.isKinematic = true; _rb.velocity = Vector2.zero;
        isDragging = true;
        touchStartPos = touchPos;
        objectStartPos = _transform.position;

    }

    void DragObject(Vector2 touchPos)
    {
        if (isDragging)
        {
            _rb.isKinematic = true;
            Vector2 delta = touchPos - touchStartPos;
            Vector3 newPosition = objectStartPos + new Vector3(delta.x, delta.y, 0) * _power;

            _transform.position = newPosition;
        }
    }

    void StopDragging()
    {
        isDragging = false;
        _rb.isKinematic = false;  
    }

    private void OnDestroy()
    {
       // _ballSpawner.ActivateParticles();//observer
        //_lvlManager.RemoveBall(); //observer
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Finish"))
        {
            SpawnParticles(_balSuccessParticles);
            return;
        }

        SpawnParticles(_ballDeathParticles);

    }


    public void SpawnParticles(ParticleSystem particleSystem)
    {
        Instantiate(particleSystem, transform.position, Quaternion.identity);
    }
   


}
