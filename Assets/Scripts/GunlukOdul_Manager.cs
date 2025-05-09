using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using Ayberk;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GunlukOdul_Manager : MonoBehaviour
{
    ReklamYonetim _ReklamYonetimi = new ReklamYonetim();
    public BellekYonetim _BellekYonetimi = new BellekYonetim(); 
    
    public AudioSource ButonSes;
    
    public Button[] odulButonlari; // 16 adet buton
    public TextMeshProUGUI[] zamanTextleri;   // 16 adet text, kalan süreyi gösterir

    private DateTime lastClaimTime;
    private int claimedCount;
    private void Awake()
    {
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob initialized.");
        });
    }

    void Start()
    {
        LoadData();
        StartCoroutine(SureyiHerSaniyeGuncelle());
        _ReklamYonetimi.RequestBanner();
    }
    
    void Update()
    {
        UpdateOduller(); // her frame değilse bunu belli aralıklarla çağır (performans için)
    }
    
    void LoadData()
    {
        string zamanStr = _BellekYonetimi.VeriOku_s("OdulLastClaim");
        if (!string.IsNullOrEmpty(zamanStr))
            lastClaimTime = DateTime.Parse(zamanStr);
        else
            lastClaimTime = DateTime.MinValue;

        claimedCount = _BellekYonetimi.VeriOku_i("OdulClaimedCount");
    }

    void SaveData()
    {
        _BellekYonetimi.VeriKaydet_string("OdulLastClaim", lastClaimTime.ToString());
        _BellekYonetimi.VeriKaydet_int("OdulClaimedCount", claimedCount);
    }
    void UpdateOduller()
    {
        TimeSpan sinceLastClaim = DateTime.Now - lastClaimTime;

        for (int i = 0; i < odulButonlari.Length; i++)
        {
            if (i < claimedCount)
            {
                odulButonlari[i].interactable = false;
                zamanTextleri[i].text = "Alındı";
            }
            else if (i == claimedCount)
            {
                if (sinceLastClaim.TotalHours >= 24 || claimedCount == 0)
                {
                    odulButonlari[i].interactable = true;
                    zamanTextleri[i].text = "Hazır!";
                }
                else
                {
                    odulButonlari[i].interactable = false;
                    double kalan = 24 - sinceLastClaim.TotalHours;

                    if (kalan >= 24)
                    {
                        int gun = Mathf.FloorToInt((float)(kalan / 24));
                        zamanTextleri[i].text = gun + " gün";
                    }
                    else
                    {
                        int saat = Mathf.FloorToInt((float)kalan);
                        int dakika = Mathf.FloorToInt((float)((kalan - saat) * 60));
                        zamanTextleri[i].text = saat.ToString("00") + ":" + dakika.ToString("00");
                    }
                }
            }
            else
            {
                odulButonlari[i].interactable = false;
                zamanTextleri[i].text = "Kilitli";
            }
        }
    }
    public void OdulAl(int index)
    {
        if (index != claimedCount) return;

        lastClaimTime = DateTime.Now;
        claimedCount++;
        SaveData();
        UpdateOduller();

        switch (index)
        {
            case 0:  OdulParaEkle(50); break;
            case 1:  OdulParaEkle(70); break;
            case 2:  OdulParaEkle(80); break;
            case 3:  OdulParaEkle(100); break;
            case 4:  OdulElmasEkle(5); break;
            case 5:  OdulElmasEkle(10); break;
            case 6:  OdulElmasEkle(20); break;
            case 7:  OdulElmasEkle(30); break;
            case 8:  OdulParaEkle(300); break;
            case 9:  OdulParaEkle(600); break;
            case 10: OdulParaEkle(1000); break;
            case 11: OdulParaEkle(2000); break;
            case 12: OdulElmasEkle(50); break;
            case 13: OdulElmasEkle(70); break;
            case 14: OdulElmasEkle(80); break;
            case 15: OdulElmasEkle(150); break;
            default:
                Debug.LogWarning("Bilinmeyen ödül index: " + index);
                break;
        }

        Debug.Log("Günlük ödül verildi! Gün: " + (index + 1));
    }
    
    void OdulParaEkle(int miktar)
    {
        int mevcutPuan = _BellekYonetimi.VeriOku_i("Puan");
        mevcutPuan += miktar;
        _BellekYonetimi.VeriKaydet_int("Puan", mevcutPuan);
        Debug.Log(miktar + " puan eklendi. Yeni toplam: " + mevcutPuan);
    }

    void OdulElmasEkle(int miktar)
    {
        int mevcutElmas = _BellekYonetimi.VeriOku_i("Elmas");
        mevcutElmas += miktar;
        _BellekYonetimi.VeriKaydet_int("Elmas", mevcutElmas);
        Debug.Log(miktar + " elmas eklendi. Yeni toplam: " + mevcutElmas);
    }
    
    private IEnumerator SureyiHerSaniyeGuncelle()
    {
        while (true)
        {
            UpdateOduller();
            yield return new WaitForSeconds(1f); // her 1 saniyede bir günceller
        }
    }
    public void GeriDon()
    {
        if (ButonSes != null) ButonSes.Play();
        SceneManager.LoadScene(0);
        DynamicGI.UpdateEnvironment();
    }
}
