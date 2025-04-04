using UnityEngine;

public class KunaiMove : MonoBehaviour
{
    private float _speed;
    private float _lifeTime;
    private float _damage;
    private Rigidbody2D _rb;
    private BossController_HSC _bossController;
    private Vector2 _direction;
    
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _speed = 20f;
        _lifeTime = 5f;
        _damage = 10f;
        _direction = new Vector2(-1, 0); // 초기 방향 설정
        _bossController = FindObjectOfType<BossController_HSC>();
        if(_bossController.GetComponent<SpriteRenderer>().flipX == false)
        {
            _direction.x = 1; // 보스가 오른쪽 바라보면 방향을 반대로 설정
            transform.position += new Vector3(0.7f, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            _direction.x = -1; // 보스가 왼쪽을 바라보면 방향을 반대로 설정
            transform.position += new Vector3(-0.7f, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        Destroy(gameObject, _lifeTime);
    }
    
    private void Update()
    {
        _rb.linearVelocity = _direction * _speed;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Hit!");
            Destroy(gameObject); // 충돌 후 쿠나이 삭제
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject); // 벽에 충돌하면 쿠나이 삭제
        }
    }
}
