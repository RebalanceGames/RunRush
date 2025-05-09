using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using Ayberk;
public class Deneme : MonoBehaviour
{
    ReklamYonetim _ReklamYonetimi = new ReklamYonetim();
    private void Awake()
    {
        MobileAds.Initialize(initStatus => { Debug.Log("AdMob initialized."); });
    }

    public void BannerGoster()
    {
        _ReklamYonetimi.RequestBanner();
        _ReklamYonetimi.BannerGoster();
    }

    public void GecisGoster()
    {
        _ReklamYonetimi.RequestInterstitial();
        _ReklamYonetimi.GecisReklamiGoster();
    }

    public void OdulluGoster()
    {
        _ReklamYonetimi.RequestRewardedAd();
        _ReklamYonetimi.OdulluReklamGoster();
    }
}
