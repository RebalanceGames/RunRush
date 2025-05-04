using UnityEngine;
using UnityEngine.AI;

public class BosKarakter : MonoBehaviour
{
    public SkinnedMeshRenderer _Renderer;
    public Material AtanacakOlanMateryal;
    public NavMeshAgent _Navmesh;
    public Animator _Animator;
    public GameObject Target;
    public GameManager _GameManager;

    private bool Temasvar;

    void LateUpdate()
    {
        if (Temasvar && Target != null && _Navmesh != null)
            _Navmesh.SetDestination(Target.transform.position);
    }

    Vector3 PozisyonVer()
    {
        return new Vector3(transform.position.x, .23f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AltKarakterler") || other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("BosKarakter"))
            {
                MaterialDegistirveAnimasyonTetikle();

                var audio = GetComponent<AudioSource>();
                if (audio != null)
                    audio.Play();

                Temasvar = true;
            }
        }
        else if (other.CompareTag("igneliKutu") || other.CompareTag("Testere") || other.CompareTag("Pervaneigneler"))
        {
            if (_GameManager != null)
                _GameManager.YokolmaEfektiOlustur(PozisyonVer());

            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Balyoz"))
        {
            if (_GameManager != null)
                _GameManager.YokolmaEfektiOlustur(PozisyonVer(), true);

            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Dusman"))
        {
            var col = gameObject.GetComponent<BoxCollider>();
            if (col != null) col.enabled = false;

            if (_GameManager != null)
                _GameManager.YokolmaEfektiOlustur(PozisyonVer(), true, false);

            gameObject.SetActive(false);
        }
    }

    void MaterialDegistirveAnimasyonTetikle()
    {
        if (_Renderer != null && AtanacakOlanMateryal != null)
        {
            Material[] mats = _Renderer.materials;
            if (mats.Length > 0)
            {
                mats[0] = AtanacakOlanMateryal;
                _Renderer.materials = mats;
            }
        }

        if (_Animator != null)
            _Animator.SetBool("Saldir", true);

        gameObject.tag = "AltKarakterler";
        GameManager.AnlikKarakterSayisi++;
    }
}
