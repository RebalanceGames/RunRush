using System.Collections.Generic;
using Ayberk;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Ayberk;
using TMPro;
using GoogleMobileAds.Api;
public class Ayarlar_Manager : MonoBehaviour
{
    public AudioSource ButonSes;
    public Slider MenuSes;
    public Slider MenuFx;
    public Slider OyunSes;
    
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    ReklamYonetim _ReklamYonetimi = new ReklamYonetim();
    
    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;

    [Header("DİL TERCİHİ OBJELERİ")]
    public TextMeshProUGUI DilText;
    public Button[] DilButonlari;
    private int AktifDilIndex;
    private void Start()
    {
        MobileAds.Initialize(initStatus => { Debug.Log("AdMob initialized."); });

        _ReklamYonetimi.RequestBanner();
        
        ButonSes.volume = _BellekYonetim.VeriOku_f("MenuSes");
        MenuSes.value = _BellekYonetim.VeriOku_f("MenuSes");
        MenuFx.value = _BellekYonetim.VeriOku_f("MenuFx");
        OyunSes.value = _BellekYonetim.VeriOku_f("OyunSes");
        
        _VeriYonetimi.Dil_Load();
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeyiAktar();
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[4]);
        DilTercihiYonetimi();
        DilDurumunuKontrolEt();
    }
    public void DilTercihiYonetimi()
    {
        if (_BellekYonetim.VeriOku_s("Dil") == "TR")
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_TR[i].Metin;
            }
        }
        else
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_EN[i].Metin;
            }
        }
    }
    public void SesAyarla(string HangiAyar)
    {
        switch (HangiAyar)
        {
            case "menuses":
                _BellekYonetim.VeriKaydet_float("MenuSes", MenuSes.value);
                break;
            case "menufx":
                _BellekYonetim.VeriKaydet_float("MenuFx", MenuFx.value);
                break;
            case "oyunses":
                _BellekYonetim.VeriKaydet_float("OyunSes", OyunSes.value);
                break;
        }
    }
    public void GeriDon()
    {
        ButonSes.Play();
        _BellekYonetim.VeriKaydet_float("MenuSes", MenuSes.value);
        _BellekYonetim.VeriKaydet_float("MenuFx", MenuFx.value);
        _BellekYonetim.VeriKaydet_float("OyunSes", OyunSes.value);
        SceneManager.LoadScene(0);
    }

    public void DilDurumunuKontrolEt()
    {
        if (_BellekYonetim.VeriOku_s("Dil") == "TR")
        {
            AktifDilIndex = 0;
            DilText.text = "TÜRKÇE";
            DilButonlari[0].interactable = false;
        }
        else
        {
            AktifDilIndex = 1;
            DilText.text = "ENGLISH";
            DilButonlari[1].interactable = false;
        }
    }
    public void DilDegistir(string Yon)
    {
        ButonSes.Play();

        if (Yon == "ileri")
        {
            AktifDilIndex = 1;
            DilText.text = "ENGLISH";
            DilButonlari[1].interactable = false;
            DilButonlari[0].interactable = true;
            _BellekYonetim.VeriKaydet_string("Dil", "EN");
            DilTercihiYonetimi();
        }
        else
        {
            AktifDilIndex = 0;
            DilText.text = "TÜRKÇE";
            DilButonlari[0].interactable = false;
            DilButonlari[1].interactable = true;
            _BellekYonetim.VeriKaydet_string("Dil", "TR");
            DilTercihiYonetimi();
        }
    }
}
