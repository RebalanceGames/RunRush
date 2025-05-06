using System;
using System.Collections;
using System.Collections.Generic;
using Ayberk;
using UnityEngine;
using Ayberk;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class Ozellikler_Manager : MonoBehaviour
{
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    ReklamYonetim _ReklamYonetimi = new ReklamYonetim();
    
    public AudioSource ButonSes;
    private void Awake()
    {
        MobileAds.Initialize(initStatus => { Debug.Log("AdMob initialized."); });

        _ReklamYonetimi.RequestBanner();
        
        _BellekYonetim.KontrolEtveTanimla();
    }
    
    public void KarakterYukselt(int OlusacakKarakter)
    {
        int karaktersayisi = _BellekYonetim.VeriOku_i("UpgradeCharacter") + 1;
        _BellekYonetim.VeriKaydet_int("UpgradeCharacter", karaktersayisi);
    }

    public void AltinYukselt()
    {
        int altinSeviyesi = _BellekYonetim.VeriOku_i("UpgradePuan");
        altinSeviyesi += 1;

        float carpim = Mathf.Pow(1.05f, altinSeviyesi);

        _BellekYonetim.VeriKaydet_float("AltinCarpani", carpim);
        _BellekYonetim.VeriKaydet_int("UpgradePuan", altinSeviyesi);
        
        Debug.Log(_BellekYonetim.VeriOku_f("AltinCarpani"));
    }

    public void ElmasYukselt()
    {
        int elmassayisi = _BellekYonetim.VeriOku_i("UpgradeElmas") + 1;
    }
    
    public void GeriDon()
    {
        if (ButonSes != null) ButonSes.Play();
        SceneManager.LoadScene(0);
        DynamicGI.UpdateEnvironment();
    }
}
