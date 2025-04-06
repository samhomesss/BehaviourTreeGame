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

        if (dir.x < 0)
        {
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);

        }

    }

    void Update()
    {
        
        transform.Translate(new Vector3(dir.normalized.x, 0, 0) * SWORD_FORCE_SPEED * Time.deltaTime);
        
    }
}
