using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Ayberk;
using TMPro;
using GoogleMobileAds.Api;
public class LevelManager : MonoBehaviour
{
    public Button[] Butonlar;
    public int Level;
    public Sprite KilitliButon;

    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    ReklamYonetim _ReklamYonetimi = new ReklamYonetim();
    public AudioSource ButonSes;

    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;

    public GameObject YuklemeEkrani;
    public Slider YuklemeSlider;

    public List<GameObject> LevelSayfalari;
    public List<Button> Ileri_Geri_Buton;
    private int aktifSayfaIndex = 0;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { Debug.Log("AdMob initialized."); });

        _ReklamYonetimi.RequestBanner();
        
        _VeriYonetimi.Dil_Load();
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeyiAktar();

        if (_DilOkunanVeriler.Count > 2)
        {
            _DilVerileriAnaObje.Add(_DilOkunanVeriler[2]);
            DilTercihiYonetimi();
        }
        else
        {
            Debug.LogError("LevelManager: Dil verileri eksik.");
        }

        if (ButonSes != null)
            ButonSes.volume = _BellekYonetim.VeriOku_f("MenuSes");

        int mevcutLevel = _BellekYonetim.VeriOku_i("SonLevel") - 6;
        int Index = 1;

        for (int i = 0; i < Butonlar.Length; i++)
        {
            if (i + 1 <= mevcutLevel)
            {
                var textObj = Butonlar[i].GetComponentInChildren<TextMeshProUGUI>();
                if (textObj != null)
                    textObj.text = (i + 1).ToString();

                int sahneIndex = Index + 6;
                Butonlar[i].onClick.AddListener(delegate { SahneYukle(sahneIndex); });
            }
            else
            {
                if (Butonlar[i].GetComponent<Image>() != null)
                    Butonlar[i].GetComponent<Image>().sprite = KilitliButon;

                Butonlar[i].enabled = false;
            }

            Index++;
        }

        if (Ileri_Geri_Buton.Count > 0)
            Ileri_Geri_Buton[0].interactable = false;
    }

    public void DilTercihiYonetimi()
    {
        if (_DilVerileriAnaObje.Count == 0) return;

        if (_BellekYonetim.VeriOku_s("Dil") == "TR")
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                if (_DilVerileriAnaObje[0]._DilVerileri_TR.Count > i)
                    TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_TR[i].Metin;
            }
        }
        else
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                if (_DilVerileriAnaObje[0]._DilVerileri_EN.Count > i)
                    TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_EN[i].Metin;
            }
        }
    }

    public void SahneYukle(int Index)
    {
        if (ButonSes != null) ButonSes.Play();
        StartCoroutine(LoadAsync(Index));
        DynamicGI.UpdateEnvironment();
    }

    IEnumerator LoadAsync(int SceneIndex)
    {
        AsyncOperation Op = SceneManager.LoadSceneAsync(SceneIndex);

        if (YuklemeEkrani != null)
            YuklemeEkrani.SetActive(true);

        while (!Op.isDone)
        {
            float progress = Mathf.Clamp01(Op.progress / 0.9f);

            if (YuklemeSlider != null)
                YuklemeSlider.value = progress;

            yield return null;
        }
    }

    public void SayfaDegistir(string durum)
    {
        if (LevelSayfalari.Count < 2 || Ileri_Geri_Buton.Count < 2) return;

        if (durum == "ileri" && aktifSayfaIndex == 0)
        {
            Ileri_Geri_Buton[0].interactable = true;
            Ileri_Geri_Buton[1].interactable = false;
            LevelSayfalari[1].SetActive(true);
            LevelSayfalari[0].SetActive(false);
            aktifSayfaIndex += 1;
        }
        else if (durum == "geri" && aktifSayfaIndex == 1)
        {
            Ileri_Geri_Buton[0].interactable = false;
            Ileri_Geri_Buton[1].interactable = true;
            LevelSayfalari[0].SetActive(true);
            LevelSayfalari[1].SetActive(false);
            aktifSayfaIndex -= 1;
        }
    }

    public void GeriDon()
    {
        if (ButonSes != null) ButonSes.Play();
        SceneManager.LoadScene(0);
        DynamicGI.UpdateEnvironment();
    }
}
