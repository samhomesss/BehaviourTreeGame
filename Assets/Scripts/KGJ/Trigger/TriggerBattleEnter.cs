using System.Collections;
using UnityEngine;
using Unity.Cinemachine; // Cinemachine 네임스페이스 사용

public class TriggerBattleEnter : MonoBehaviour
{
    GameObject _boss;
    GameObject _player;
    GameObject _door;
    GameObject _bossAnimation;
    CinemachineCamera _cinemachineCamera;
    CameraDirector _cameraDirector;

    Vector3 startPos = new Vector3(20.06f, 4.63f, 0f);    // 시작 위치
    Vector3 jumpPos = new Vector3(9.86f, 7.82f, 0f);      // 점프 위치
    Vector3 endPos = new Vector3(3.39f, -3.75f, 0f);      // 도착 위치

    float jumpDuration = 1.5f;  // 시작 -> 점프까지 걸리는 시간
    float fallDuration = 0.3f;  // 점프 -> 도착까지 걸리는 시간
    float jumpHeight = 1f;    // 점프 높이 (포물선 효과)

    void Start()
    {
        _boss = FindAnyObjectByType<BossAnimationEventController>().gameObject;
        _player = FindAnyObjectByType<PlayerAnimationEventController>().gameObject;
        _bossAnimation = FindAnyObjectByType<AppearAnimation>().gameObject;
        _door = FindAnyObjectByType<Door>().gameObject;
        _cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();
        _door.SetActive(false);
        _boss.SetActive(false);
        _bossAnimation.transform.position = startPos;
        _cameraDirector = FindAnyObjectByType<CameraDirector>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        _cameraDirector.PlayTimeline(CameraType.Enter);
        _bossAnimation.GetComponent<Animator>().Play("JUMP-START");
        StartCoroutine(MoveSequence());

        StartCoroutine(AddPlayer());
    }

    IEnumerator AddPlayer()
    {
        yield return new WaitForSeconds(1.5f);
        _door.SetActive(true);

    }


    IEnumerator MoveSequence()
    {
        // 1단계: 시작 -> 점프 위치로 이동
        yield return StartCoroutine(MoveToPosition(startPos, jumpPos, jumpDuration, true));

        // 2단계: 점프 -> 도착 위치로 이동
        yield return StartCoroutine(MoveToPosition(jumpPos, endPos, fallDuration, false));
        _bossAnimation.SetActive(false);
        _boss.SetActive(true);
        GetComponent<PolygonCollider2D>().enabled = false;
    }

    IEnumerator MoveToPosition(Vector3 from, Vector3 to, float duration, bool isJump)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration; // 0에서 1로 진행률

            // 기본 선형 이동
            Vector3 newPos = Vector3.Lerp(from, to, t);

            // 점프 중이라면 포물선 높이 추가
            if (isJump)
            {
                float height = jumpHeight * Mathf.Sin(t * Mathf.PI); // 포물선 곡선
                newPos.y += height;
            }

            _bossAnimation.transform.position = newPos;
            yield return null; // 다음 프레임까지 대기
        }

        // 정확히 목표 위치에 도달하도록 보정
        _bossAnimation.transform.position = to;
    }
}