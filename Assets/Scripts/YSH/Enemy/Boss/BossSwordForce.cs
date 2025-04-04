using UnityEngine;

public class BossSwordForce : MonoBehaviour
{
    PlayerStateManager _player;
    const int SWORD_FORCE_SPEED = 25;
    Vector2 dir;
    void Start()
    {

        _player = FindAnyObjectByType<PlayerStateManager>();
        dir = _player.transform.position - this.transform.position;

        Debug.Log(dir.x);

        if (dir.x < 0)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, -this.transform.localScale.y, this.transform.localScale.z);
        }

        Destroy(this, 10f);
    }

    void Update()
    {
        // Todo: 좆 버그가 있네요 
        transform.Translate(new Vector3(dir.normalized.x, 0, 0) * SWORD_FORCE_SPEED * Time.deltaTime);
        
    }
}
