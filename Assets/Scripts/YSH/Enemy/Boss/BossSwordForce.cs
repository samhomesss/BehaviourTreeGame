using UnityEngine;

public class BossSwordForce : MonoBehaviour
{
    PlayerStateManager _player;
    const int SWORD_FORCE_SPEED = 25;
    float dir;
    
    void Start()
    {
        
        _player = FindAnyObjectByType<PlayerStateManager>();
        dir = (_player.transform.position.x - this.transform.position.x) > 0 ? 1 : -1 ;

        if (dir < 0)
        {
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        }
    }

    void Update()
    {
        transform.Translate(new Vector3(dir, 0, 0) * SWORD_FORCE_SPEED * Time.deltaTime);
    }
}
