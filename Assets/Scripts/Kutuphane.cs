using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GoogleMobileAds.Api;

namespace Ayberk
{
    public class Matematiksel_islemler
    {
        public void Carpma(int GelenSayi, List<GameObject> Karakterler, Transform Pozisyon, List<GameObject> OlusturmaEfektleri)
        {
            int DonguSayisi = (GameManager.AnlikKarakterSayisi * GelenSayi) - GameManager.AnlikKarakterSayisi;
            int sayi = 0;
            foreach (var item in Karakterler)
            {
                if (sayi < DonguSayisi)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in OlusturmaEfektleri)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = Pozisyon.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }

                        item.transform.position = Pozisyon.position;
                        item.SetActive(true);
                        sayi++;
                    }
                }
                else
                {
                    sayi = 0;
                    break;
                }
            }

            GameManager.AnlikKarakterSayisi *= GelenSayi;
        }

        public void Toplama(int GelenSayi, List<GameObject> Karakterler, Transform Pozisyon,
            List<GameObject> OlusturmaEfektleri)
        {
            int sayi2 = 0;
            foreach (var item in Karakterler)
            {
                if (sayi2 < GelenSayi)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in OlusturmaEfektleri)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = Pozisyon.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }

                        item.transform.position = Pozisyon.position;
                        item.SetActive(true);
                        sayi2++;
                    }
                }
                else
                {
                    sayi2 = 0;
                    break;
                }
            }

            GameManager.AnlikKarakterSayisi += GelenSayi;
        }

        public void Cikartma(int GelenSayi, List<GameObject> Karakterler, List<GameObject> YokOlmaEfektleri)
        {
            if (GameManager.AnlikKarakterSayisi > 1)
            {
                if (GameManager.AnlikKarakterSayisi < GelenSayi)
                {
                    foreach (var item in Karakterler)
                    {
                        foreach (var item2 in YokOlmaEfektleri)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                Vector3 yeniPoz = new Vector3(item.transform.position.x, .23f,
                                    item.transform.position.z);

                                item2.SetActive(true);
                                item2.transform.position = yeniPoz;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }

                        item.transform.position = Vector3.zero;
                        item.SetActive(false);
                    }

                    GameManager.AnlikKarakterSayisi = 1;
                }
                else
                {
                    int sayi3 = 0;
                    foreach (var item in Karakterler)
                    {
                        if (sayi3 != GelenSayi)
                        {
                            if (item.activeInHierarchy)
                            {
                                foreach (var item2 in YokOlmaEfektleri)
                                {
                                    if (!item2.activeInHierarchy)
                                    {
                                        Vector3 yeniPoz = new Vector3(item.transform.position.x, .23f,
                                            item.transform.position.z);

                                        item2.SetActive(true);
                                        item2.transform.position = yeniPoz;
                                        item2.GetComponent<ParticleSystem>().Play();
                                        item2.GetComponent<AudioSource>().Play();
                                        break;
                                    }
                                }

                                item.transform.position = Vector3.zero;
                                item.SetActive(false);
                                sayi3++;
                            }
                        }
                        else
                        {
                            sayi3 = 0;
                            break;
                        }
                    }

                    GameManager.AnlikKarakterSayisi -= GelenSayi;
                }
            }
        }

        public void Bolme(int GelenSayi, List<GameObject> Karakterler, List<GameObject> YokOlmaEfektleri)
        {
            if (GameManager.AnlikKarakterSayisi <= GelenSayi)
            {
                foreach (var item in Karakterler)
                {
                    foreach (var item2 in YokOlmaEfektleri)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            Vector3 yeniPoz = new Vector3(item.transform.position.x, .23f, item.transform.position.z);

                            item2.SetActive(true);
                            item2.transform.position = yeniPoz;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }

                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }

                GameManager.AnlikKarakterSayisi = 1;
            }
            else
            {
                int bolen = GameManager.AnlikKarakterSayisi / GelenSayi;

                int sayi3 = 0;
                foreach (var item in Karakterler)
                {
                    if (sayi3 != bolen)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in YokOlmaEfektleri)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    Vector3 yeniPoz = new Vector3(item.transform.position.x, .23f,
                                        item.transform.position.z);

                                    item2.SetActive(true);
                                    item2.transform.position = yeniPoz;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    item2.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }

                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            sayi3++;
                        }
                    }
                    else
                    {
                        sayi3 = 0;
                        break;
                    }
                }

                if (GameManager.AnlikKarakterSayisi % GelenSayi == 0)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                }
                else if (GameManager.AnlikKarakterSayisi % GelenSayi == 1)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                    GameManager.AnlikKarakterSayisi++;
                }
                else if (GameManager.AnlikKarakterSayisi % GelenSayi == 2)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                    GameManager.AnlikKarakterSayisi += 2;
                }
            }
        }
    }

    public class BellekYonetim
    {
        public void VeriKaydet_string(string Key, string value)
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }

        public void VeriKaydet_int(string Key, int value)
        {
            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();
        }

        public void VeriKaydet_float(string Key, float value)
        {
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }

        public string VeriOku_s(string Key)
        {
            return PlayerPrefs.GetString(Key);
        }

        public int VeriOku_i(string Key)
        {
            return PlayerPrefs.GetInt(Key);
        }

        public float VeriOku_f(string Key)
        {
            return PlayerPrefs.GetFloat(Key);
        }

        public void KontrolEtveTanimla()
        {
            if (!PlayerPrefs.HasKey("SonLevel"))
            {
                PlayerPrefs.SetInt("SonLevel", 6);
                PlayerPrefs.SetInt("Puan", 100);
                PlayerPrefs.SetInt("Elmas", 0);

                PlayerPrefs.SetInt("AktifSapka", -1);
                PlayerPrefs.SetInt("AktifSopa", -1);
                PlayerPrefs.SetInt("AktifBoya", -1);

                PlayerPrefs.SetFloat("MenuSes", 1);
                PlayerPrefs.SetFloat("MenuFx", 1);
                PlayerPrefs.SetFloat("OyunSes", 1);

                PlayerPrefs.SetString("Dil", "TR");

                PlayerPrefs.SetInt("GecisReklamisayisi", 1);

                // Ozellikler

                PlayerPrefs.SetInt("UpgradeCharacter", 1);
                PlayerPrefs.SetInt("UpgradePuan", 1);
                PlayerPrefs.SetFloat("AltinCarpani", 1f);
                PlayerPrefs.SetInt("UpgradeElmas", 1);
            }
        }
    }

    [Serializable]
    public class ItemBilgileri
    {
        public int GrupIndex;
        public int Item_Index;
        public string Item_Ad;
        public int Para;
        public bool SatinAlmaDurumu;
    }

    public class VeriYonetimi
    {
        public void Save(List<ItemBilgileri> _ItemBilgileri)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemVerileri.gd");
            bf.Serialize(file, _ItemBilgileri);
            file.Close();
        }

        private List<ItemBilgileri> _ItemicListe;

        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri.gd", FileMode.Open);
                _ItemicListe = (List<ItemBilgileri>)bf.Deserialize(file);
                file.Close();
            }
        }

        public List<ItemBilgileri> ListeyiAktar()
        {
            return _ItemicListe;
        }

        public void ilkkurulumDosyaOlusturma(List<ItemBilgileri> _ItemBilgileri, List<DilVerileriAnaObje> _DilVerileri)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
                bf.Serialize(file, _ItemBilgileri);
                file.Close();
            }

            if (!File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/DilVerileri.gd");
                bf.Serialize(file, _DilVerileri);
                file.Close();
            }
            if (!PlayerPrefs.HasKey("Dil"))
            {
                // Eğer dil ayarı yapılmamışsa, cihazın diline göre ayarlama yapalım
                SystemLanguage systemLanguage = Application.systemLanguage;
                if (systemLanguage == SystemLanguage.English)
                {
                    PlayerPrefs.SetString("Dil", "EN");
                }
                else if (systemLanguage == SystemLanguage.Turkish)
                {
                    PlayerPrefs.SetString("Dil", "TR");
                }
                // Eğer başka bir dil varsa, varsayılan olarak İngilizce yapalım
                else
                {
                    PlayerPrefs.SetString("Dil", "EN");
                }
                PlayerPrefs.Save();
            }
        }

        //----------------------------

        private List<DilVerileriAnaObje> _DilVerileriicListe;

        public void Dil_Load()
        {
            if (File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/DilVerileri.gd", FileMode.Open);
                _DilVerileriicListe = (List<DilVerileriAnaObje>)bf.Deserialize(file);
                file.Close();
            }
        }

        public List<DilVerileriAnaObje> DilVerileriListeyiAktar()
        {
            return _DilVerileriicListe;
        }
    }

    //------ DİL YÖNETİMİ

    [Serializable]
    public class DilVerileriAnaObje
    {
        public List<DilVerileri_TR> _DilVerileri_TR = new List<DilVerileri_TR>();
        public List<DilVerileri_EN> _DilVerileri_EN = new List<DilVerileri_EN>();
    }

    [Serializable]
    public class DilVerileri_TR
    {
        public string Metin;
    }

    [Serializable]
    public class DilVerileri_EN
    {
        public string Metin;
    }

    //------ REKLAM YÖNETİMİ

    public class ReklamYonetim
    {
        private InterstitialAd insterstitial;

        private RewardedAd _RewardedAd;

        private BannerView bannerView;

        //------ GEÇİŞ REKLAMI
        public void RequestInterstitial()
        {
            string AdUnitId;

#if UNITY_ANDROID
            AdUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
                AdUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
                AdUnitId = "unexpected_platform";
#endif

            if (insterstitial != null)
            {
                insterstitial.Destroy();
                insterstitial = null;
            }

            AdRequest request = new AdRequest();
            InterstitialAd.Load(AdUnitId, request, (InterstitialAd ad, LoadAdError error) =>
            {
                insterstitial = ad;
            });

            insterstitial.OnAdFullScreenContentClosed += GecisReklamiKapatildi;
            GecisReklamiGoster();
        }

        void GecisReklamiKapatildi()
        {
            insterstitial.Destroy();
            RequestInterstitial();
        }

        public void GecisReklamiGoster()
        {
            if (PlayerPrefs.GetInt("GecisReklamisayisi") == 2)
            {
                if (insterstitial != null && insterstitial.CanShowAd())
                {
                    PlayerPrefs.SetInt("GecisReklamisayisi", 0);

                    insterstitial.Show();
                }
                else
                {
                    Debug.Log("Geçiş reklamı hazır değil.");
                    RequestInterstitial();
                }
            }
            else
            {
                PlayerPrefs.SetInt("GecisReklamisayisi", PlayerPrefs.GetInt("GecisReklamisayisi") + 1);
            }
        }

        //------ ÖDÜLLÜ REKLAM
        public void RequestRewardedAd()
        {
            string AdUnitId;

#if UNITY_ANDROID
            AdUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
                AdUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
                AdUnitId = "unexpected_platform";
#endif
            AdRequest adRequest = new AdRequest();
            RewardedAd.Load(AdUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Failed to load rewarded ad: " + error);
                    return;
                }
                _RewardedAd = ad;
            });

            _RewardedAd.OnAdPaid += OdulluReklamTamamlandi;
            _RewardedAd.OnAdFullScreenContentClosed += OdulluReklamKapatildi;
        }

        private void OdulluReklamTamamlandi(AdValue adValue)
        {
            double amount = adValue.Value;
            Debug.Log("ÖDÜL ALINSIN: " + amount);
        }

        private void OdulluReklamKapatildi()
        {
            Debug.Log("REKLAM KAPATILDI");
            RequestRewardedAd();
        }

        //private void OdulluReklamYuklendi(object sender, EventArgs e)
        //{
        //    Debug.Log("REKLAM YÜKLENDİ");
        //}

        public void OdulluReklamGoster()
        {
            if (_RewardedAd != null && _RewardedAd.CanShowAd())
            {
                _RewardedAd.Show((Reward reward) =>
                {
                    Debug.Log($"Kullanıcı ödül aldı: {reward.Amount} {reward.Type}");
                    double amount = reward.Amount;
                    Debug.Log("ÖDÜL ALINSIN: " + amount);
                });
            }
            else
            {
                Debug.Log("Reklam henüz hazır değil.");
            }
        }

        public void OdulluReklamGoster(Action OnRewarded)
        {
            if (_RewardedAd != null && _RewardedAd.CanShowAd())
            {
                _RewardedAd.Show((Reward reward) =>
                {
                    OnRewarded.Invoke();
                });
            }
            else
            {
                Debug.Log("Reklam henüz hazır değil.");
            }
        }

        public void RequestBanner()
        {
            string AdUnitId;

#if UNITY_ANDROID
            AdUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
        AdUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        AdUnitId = "unexpected_platform";
#endif

            if (bannerView != null)
            {
                bannerView.Destroy();
            }

            bannerView = new BannerView(AdUnitId, AdSize.Banner, AdPosition.Bottom);
            AdRequest request = new AdRequest();
            bannerView.LoadAd(request);

            bannerView.OnBannerAdLoaded += BannerYuklendi;
            bannerView.OnAdFullScreenContentClosed += BannerKapatildi;
        }

        private void BannerYuklendi()
        {
            Debug.Log("BANNER YÜKLENDİ");
        }

        private void BannerKapatildi()
        {
            Debug.Log("BANNER KAPATILDI");
        }

        public void BannerGoster()
        {
            bannerView?.Show();
        }

        public void BannerGizle()
        {
            bannerView?.Hide();
        }

        public void BannerYokEt()
        {
            bannerView?.Destroy();
        }
    }
}