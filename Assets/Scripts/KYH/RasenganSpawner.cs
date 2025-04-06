using UnityEngine;
using Unity.Behavior;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class RasenganSpawner : MonoBehaviour
{
    private GameObject _rasengan;
    private GameObject _boss;
    private GameObject rasenganInstance;
    private BehaviorGraphAgent _behaviorGraphAgent;
    private PlayableDirector pd;
    private float targetPosX;
    [SerializeField]
    private float _speed = 40f;
    
    
    
    private void Start()
    {
       // pd = FindFirstObjectByType<PlayableDirector>();
        _behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
        _rasengan = Resources.Load<GameObject>("KYH/Prefabs/Rasengan");
        if (_rasengan == null)
        {
            Debug.LogError("Rasengan prefab not found!");
        }

        _boss = FindFirstObjectByType<BossController_HSC>().gameObject;
    }

    public void RasenganSpawn()
    {
        
        //pd.GetComponent<TimelineManager>().PlayTimeline();
        if (_rasengan != null && _boss != null)
        {
            _behaviorGraphAgent.GetVariable("Target", out BlackboardVariable<GameObject> target);
            _behaviorGraphAgent.GetVariable("CurrentDirection", out BlackboardVariable<float> dir);
            targetPosX = target.Value.transform.position.x;
            if(dir.Value > 0) rasenganInstance = Instantiate(_rasengan, _boss.transform.position + new Vector3(-0.39f,5f,0), Quaternion.identity);
            if(dir.Value <= 0) rasenganInstance = Instantiate(_rasengan, _boss.transform.position + new Vector3(0.39f,5f,0), Quaternion.identity);
            rasenganInstance.GetComponent<CircleCollider2D>().enabled = false;
            //rasenganInstance.transform.localScale = new Vector3(0. 1, 1);
        }
        else
        {
            Debug.LogError("Rasengan or Boss not found!");
        }
    }

    public void RasenganAttack()
    {
        rasenganInstance.GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(Dash(targetPosX));
    }

    IEnumerator Dash(float posX)
    {
        //Lerf로 max time 동안 boss와 rasenganInstance의 위치를 lerp로 이동
        Vector3 bossStartPos = _boss.transform.position;
        Vector3 bossEndPos = new Vector3(posX,bossStartPos.y,0);
       
        
        
        float rasenganOffset;
        if(bossStartPos.x > bossEndPos.x) rasenganOffset = -4.5f;
        else rasenganOffset = 4.5f;
        
        Vector3 rasenganStartPos = new Vector3(rasenganInstance.transform.position.x + rasenganOffset, bossStartPos.y+2.6f, 0);
        Vector3 rasenganEndPos = new Vector3(posX + rasenganOffset*10,bossStartPos.y+2,0);
        
        rasenganInstance.transform.position = rasenganStartPos;
        rasenganInstance.GetComponent<Rasengan>().RasenganMoveOn(rasenganEndPos, _speed);
        
        while (Vector3.Distance(_boss.transform.position, bossEndPos) > 0.1f)
        {
            //_speed 속도로 이동
            _boss.transform.position = Vector3.MoveTowards(_boss.transform.position, bossEndPos, _speed * Time.deltaTime);
            //rasenganInstance.transform.position = Vector3.MoveTowards(rasenganInstance.transform.position, rasenganEndPos, _speed * Time.deltaTime);
            //rasenganInstance.transform.position = _boss.transform.position + new Vector3(rasenganOffset,2f,0);
            yield return null;
        }
        //Destroy(rasenganInstance);
    }
    
    
    
}
