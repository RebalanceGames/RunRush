using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ayberk;
using TMPro;
using UnityEngine.UI;
using GoogleMobileAds.Api;
public class AnaMenu_Manager : MonoBehaviour
{
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    ReklamYonetim _ReklamYonetimi = new ReklamYonetim();
    public GameObject CikisPaneli;

    public List<ItemBilgileri> _Varsayilan_ItemBilgileri = new List<ItemBilgileri>();
    public List<DilVerileriAnaObje> _Varsayilan_DilVerileri = new List<DilVerileriAnaObje>();
    public AudioSource ButonSes;

    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;
    public GameObject YuklemeEkrani;
    public Slider YuklemeSlider;

    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI ElmasText;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { Debug.Log("AdMob initialized."); });

        _ReklamYonetimi.RequestBanner();
        
        _BellekYonetim.KontrolEtveTanimla();
        _VeriYonetimi.ilkkurulumDosyaOlusturma(_Varsayilan_ItemBilgileri, _Varsayilan_DilVerileri);

        if (ButonSes != null)
            ButonSes.volume = _BellekYonetim.VeriOku_f("MenuSes");
        else
            Debug.LogWarning("ButonSes referansı atanmadı!");

        _VeriYonetimi.Dil_Load();
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeyiAktar();

        if (_DilOkunanVeriler.Count > 0)
        {
            _DilVerileriAnaObje.Add(_DilOkunanVeriler[0]);
            DilTercihiYonetimi();
        }
        else
        {
            Debug.LogError("Dil verisi okunamadı.");
        }

        int yeniLevel = _BellekYonetim.VeriOku_i("SonLevel") - 4;
        int paraOku = _BellekYonetim.VeriOku_i("Puan");
        int elmasOku = _BellekYonetim.VeriOku_i("Elmas");

        if (LevelText != null) LevelText.text = yeniLevel.ToString();
        if (MoneyText != null) MoneyText.text = paraOku.ToString();
        if (ElmasText != null) ElmasText.text = elmasOku.ToString();
    }

    public void DilTercihiYonetimi()
    {
        if (_DilVerileriAnaObje.Count == 0 || _DilVerileriAnaObje[0] == null)
        {
            Debug.LogError("Dil verileri eksik.");
            return;
        }

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
        SceneManager.LoadScene(Index);
        DynamicGI.UpdateEnvironment();
    }

    public void Oyna()
    {
        if (ButonSes != null) ButonSes.Play();
        //StartCoroutine(LoadAsync(_BellekYonetim.VeriOku_i("SonLevel")));
        SceneManager.LoadScene(_BellekYonetim.VeriOku_i("SonLevel"));
        DynamicGI.UpdateEnvironment();
    }

    IEnumerator LoadAsync(int SceneIndex)
    {
        AsyncOperation Op = SceneManager.LoadSceneAsync(SceneIndex);

        if (YuklemeEkrani != null)
            YuklemeEkrani.SetActive(true);
        else
            Debug.LogWarning("YuklemeEkrani atanmadı!");

        while (!Op.isDone)
        {
            float progress = Mathf.Clamp01(Op.progress / 0.9f);

            if (YuklemeSlider != null)
                YuklemeSlider.value = progress;

            yield return null;
        }
    }

    public void CikisButonislem(string durum)
    {
        if (ButonSes != null) ButonSes.Play();

        if (durum == "Evet")
        {
            Application.Quit();
        }
        else if (durum == "Cikis")
        {
            if (CikisPaneli != null)
                CikisPaneli.SetActive(true);
        }
        else
        {
            if (CikisPaneli != null)
                CikisPaneli.SetActive(false);
        }
    }
}
