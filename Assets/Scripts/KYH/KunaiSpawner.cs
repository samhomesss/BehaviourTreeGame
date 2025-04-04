using System;
using UnityEngine;

public class KunaiSpawner : MonoBehaviour
{
    
    private GameObject _kunai;
    private GameObject _boss;
    
    private void Start()
    {
        _kunai = Resources.Load<GameObject>("KYH/Prefabs/Kunai");
        if (_kunai == null)
        {
            Debug.LogError("Kunai prefab not found!");
        }

        _boss = FindObjectOfType<BossController_HSC>().gameObject;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            SpawnKunai();
        }
    }

    public void SpawnKunai()
    {
        if (_kunai != null && _boss != null)
        {
            GameObject kunaiInstance = Instantiate(_kunai, _boss.transform.position + new Vector3(0,2,0), Quaternion.identity);
            kunaiInstance.transform.localScale = new Vector3(_boss.transform.localScale.x, 1, 1);
            
            //kunaiInstance.transform.SetParent(_boss.transform);
        }
        else
        {
            Debug.LogError("Kunai or Boss not found!");
        }
    }
}
