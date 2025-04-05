using UnityEngine;

public class Rasengan : MonoBehaviour
{
    private float _time;
    private float _size;
    [SerializeField]
    private float _maxTime = 2f;
    [SerializeField]
    private float _maxSize = 2f;
    private bool isMove = false;

    private Vector3 rasenganEndPos;
    private float _speed;
    
    private void Start()
    {
        _time = 0;
        _size = 0;
        Destroy(gameObject, 4f);
    }
    
    private void Update()
    {
        SizeUpRasengan();
        RasenganMove();
    }

    public void RasenganMoveOn(Vector3 rasenganEndPos , float _speed)
    {
        isMove = true;
        this.rasenganEndPos = rasenganEndPos;
        this._speed = _speed;
    }
    
    //_time 초 동안 _size 만큼 커지게
    private void SizeUpRasengan()
    {
        _time += Time.deltaTime;
        _size = Mathf.Lerp(0, _maxSize, _time / _maxTime);
        transform.localScale = new Vector3(_size, _size, 1);
    }

    private void RasenganMove()
    {
        if(isMove)
            transform.position = Vector3.MoveTowards(transform.position, rasenganEndPos, _speed * Time.deltaTime);
    }
}
