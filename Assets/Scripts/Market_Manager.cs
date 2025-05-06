using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ayberk;
using TMPro;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using UnityEngine.Purchasing;

public class Market_Manager : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_ExtensionProvider;

    // GERÇEK ürün ID'leri girilmeli
    private static string Para_300 = "para_300";
    private static string Para_600 = "para_600";
    private static string Elmas_50 = "elmas_50";
    private static string Elmas_100 = "elmas_100";

    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    ReklamYonetim _ReklamYonetimi = new ReklamYonetim();

    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;

    public AudioSource ButonSes;

    private void Awake()
    {
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob initialized.");
        });
    }

    private void Start()
    {
        _ReklamYonetimi.RequestRewardedAd();
        
        _ReklamYonetimi.RequestBanner();

        _VeriYonetimi.Dil_Load();
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeyiAktar();

        if (_DilOkunanVeriler.Count > 3)
        {
            _DilVerileriAnaObje.Add(_DilOkunanVeriler[3]);
            DilTercihiYonetimi();
        }
        else
        {
            Debug.LogError("Market: Dil verileri eksik.");
        }

        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
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

    public void OdulKazan()
    {
        _ReklamYonetimi.OdulluReklamGoster(()=> {
            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_i("Puan") + 300);
        });
        Debug.Log("Reklam Gösterildi");

    }

    public void GeriDon()
    {
        if (ButonSes != null) ButonSes.Play();
        SceneManager.LoadScene(0);
        DynamicGI.UpdateEnvironment();
    }

    public void UrunAl300() => BuyProductID(Para_300);
    public void UrunAl600() => BuyProductID(Para_600);
    public void UrunAl50() => BuyProductID(Elmas_50);
    public void UrunAl100() => BuyProductID(Elmas_100);

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.LogWarning("Satın alınabilir ürün bulunamadı: " + productId);
            }
        }
        else
        {
            Debug.LogWarning("Satın alma sistemi hazır değil.");
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized()) return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(Para_300, ProductType.Consumable);
        builder.AddProduct(Para_600, ProductType.Consumable);
        builder.AddProduct(Elmas_50, ProductType.Consumable);
        builder.AddProduct(Elmas_100, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_ExtensionProvider != null;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        string id = e.purchasedProduct.definition.id;

        if (id == Para_300)
            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_i("Puan") + 300);

        else if (id == Para_600)
            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_i("Puan") + 600);

        else if (id == Elmas_50)
            _BellekYonetim.VeriKaydet_int("Elmas", _BellekYonetim.VeriOku_i("Elmas") + 50);

        else if (id == Elmas_100)
            _BellekYonetim.VeriKaydet_int("Elmas", _BellekYonetim.VeriOku_i("Elmas") + 100);

        return PurchaseProcessingResult.Complete;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_ExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError("IAP Başlatma Başarısız: " + error.ToString());
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError($"IAP Başlatma Başarısız: {error} | {message}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogError($"Satın alma başarısız: {product.definition.id} | Sebep: {failureReason}");
    }
}
