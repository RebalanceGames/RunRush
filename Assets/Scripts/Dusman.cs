using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Dusman : MonoBehaviour
{
    public struct Character
    {
        public int hp;
        public int defance;
        public int damage;
    }

    public Character boss;
    public Character Enemy;

    public GameObject Saldir_Hedefi;
    public NavMeshAgent _NavmeshAgent;
    public Animator _Animator;
    public GameManager _GameManager;
    private bool Saldiri_Basladimi;
    public bool isBoss;

    private bool bulunanKonumaGittimi;
    private bool animAcildimi;
    private void Start()
    {
        boss.hp = 10;
    }

    private void Update()
    {
        if (Saldiri_Basladimi)
        {
            float distance = Vector3.Distance(transform.position, Saldir_Hedefi.transform.position);
            if (distance < 0.2f && isBoss)
            {
                Debug.Log("girdi");
                _Animator.SetBool("Saldir", false);
                _NavmeshAgent.isStopped = true;
            }
            else
            {
                _NavmeshAgent.SetDestination(Saldir_Hedefi.transform.position);
                _NavmeshAgent.isStopped = false;
            }
        }
    }
    
    public void AnimasyonTetikle()
    {
        if (_Animator != null)
            _Animator.SetBool("Saldir", true);

        Saldiri_Basladimi = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AltKarakterler") && !isBoss)
        {
            if (_GameManager != null && _GameManager.DusmanlarKontrol.Count > 0)
                _GameManager.DusmanlarKontrol.RemoveAt(0);

            bulunanKonumaGittimi = true;
            EnemyDeath();
        }
        else if (other.CompareTag("AltKarakterler") && isBoss)
        {
            boss.hp--;

            if (boss.hp <= 0)
            {
                BossDeath();
                Debug.Log("Boss Oldu");
            }
        }
    }

    public void EnemyDeath()
    {
        var col = gameObject.GetComponent<BoxCollider>();
        if (col != null)
            col.enabled = false;

        Vector3 yeniPoz = new Vector3(transform.position.x, 0.23f, transform.position.z);

        if (_GameManager != null)
            _GameManager.YokolmaEfektiOlustur(yeniPoz, true, true);

        gameObject.SetActive(false);
    }

    public void BossDeath()
    {
        var col = gameObject.GetComponent<BoxCollider>();
        if (col != null)
            col.enabled = false;

        Vector3 yeniPoz = new Vector3(transform.position.x, 0.23f, transform.position.z);

        if (_GameManager != null)
            _GameManager.YokolmaEfektiOlustur(yeniPoz, true, false, true);

        gameObject.SetActive(false);
    }
}