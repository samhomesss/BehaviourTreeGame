using System.Collections;
using UnityEngine;
using Unity.Cinemachine; // Cinemachine ���ӽ����̽� ���

public class TriggerBattleEnter : MonoBehaviour
{
    GameObject _boss;
    GameObject _player;
    GameObject _door;
    GameObject _bossAnimation;
    CinemachineCamera _cinemachineCamera;
    CameraDirector _cameraDirector;

    Vector3 startPos = new Vector3(20.06f, 4.63f, 0f);    // ���� ��ġ
    Vector3 jumpPos = new Vector3(9.86f, 7.82f, 0f);      // ���� ��ġ
    Vector3 endPos = new Vector3(3.39f, -3.75f, 0f);      // ���� ��ġ

    float jumpDuration = 1.5f;  // ���� -> �������� �ɸ��� �ð�
    float fallDuration = 0.3f;  // ���� -> �������� �ɸ��� �ð�
    float jumpHeight = 1f;    // ���� ���� (������ ȿ��)

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
        // TODO: �÷��̾� ������ ����
        Managers.InputManager.SetPlayerMoveable(false);
        _cameraDirector.PlayTimeline(CameraType.Enter);
        
        StartCoroutine(MoveSequence());

        StartCoroutine(ActiveDoor());
    }

    IEnumerator ActiveDoor()
    {
        yield return new WaitForSeconds(1.5f);
        _door.SetActive(true);
    }


    IEnumerator MoveSequence()
    {
        yield return new WaitForSeconds(5f);
        _bossAnimation.GetComponent<Animator>().Play("JUMP-START");
        // 1�ܰ�: ���� -> ���� ��ġ�� �̵�
        yield return StartCoroutine(MoveToPosition(startPos, jumpPos, jumpDuration, true));

        // 2�ܰ�: ���� -> ���� ��ġ�� �̵�
        yield return StartCoroutine(MoveToPosition(jumpPos, endPos, fallDuration, false));

        // TODO: �÷��̾� ������ Ǯ��
        Managers.InputManager.SetPlayerMoveable(true);

        yield return new WaitForSeconds(1.5f);
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
            float t = elapsedTime / duration; // 0���� 1�� �����

            // �⺻ ���� �̵�
            Vector3 newPos = Vector3.Lerp(from, to, t);

            // ���� ���̶�� ������ ���� �߰�
            if (isJump)
            {
                float height = jumpHeight * Mathf.Sin(t * Mathf.PI); // ������ �
                newPos.y += height;
            }

            _bossAnimation.transform.position = newPos;
            yield return null; // ���� �����ӱ��� ���
        }

        // ��Ȯ�� ��ǥ ��ġ�� �����ϵ��� ����
        _bossAnimation.transform.position = to;
    }
}