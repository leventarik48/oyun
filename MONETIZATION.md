# Monetization Rehberi - Hill Climb Car Game

## Reklam Sisteminin Kullanımı

Oyununuzda 3 farklı reklam tipi bulunmaktadır:

### 1. Banner Reklamlar (Alt/Üst Bantlar)
**Konum**: Oyun ekranının alt veya üst kısmında sürekli görünür
**Ne zaman gösterilir**: Oyun başladığında otomatik
**Gelir**: Düşük ama sabit
**Kullanıcı deneyimi**: Minimal rahatsızlık

```csharp
// AdManager'da banner gösterme
AdManager.Instance.ShowBannerAd();

// Banner'ı gizleme
AdManager.Instance.HideBannerAd();
```

### 2. Interstitial Reklamlar (Tam Ekran)
**Konum**: Tam ekran reklam
**Ne zaman gösterilir**: Her 3 ölümde bir (ayarlanabilir)
**Gelir**: Orta seviye
**Kullanıcı deneyimi**: Orta düzey rahatsızlık

**Ayarlama:**
```csharp
// AdManager component'inde
showInterstitialEveryNDeaths = 3; // Her 3 ölümde bir göster
```

**Manuel gösterme:**
```csharp
AdManager.Instance.ShowInterstitialAd();
```

### 3. Rewarded Video (Ödüllü Video)
**Konum**: Tam ekran video reklam
**Ne zaman gösterilir**: Oyuncu öldüğünde "Continue" butonu ile
**Gelir**: YÜKSEK (en karlı!)
**Kullanıcı deneyimi**: İYİ (kullanıcı kendi isteğiyle izler)

**Kullanım:**
```csharp
AdManager.Instance.ShowRewardedAd(
    onSuccess: () => {
        // Ödülü ver
        Debug.Log("Oyuncu videoyu izledi!");
    },
    onFailed: () => {
        // Reklam gösterilemedi
        Debug.Log("Video yüklenemedi veya iptal edildi");
    }
);
```

---

## Reklam Yerleşimleri (Oyundaki Mevcut Sistem)

### 🎮 Oyun Sırasında
- **Banner Reklamlar**: Ekranın altında (varsayılan olarak kapalı)
  - Aktifleştirmek için: `AdManager > Show Banner On Start = true`

### 💀 Ölüm Anında
1. **Interstitial Reklam**: Her 3 ölümde bir otomatik gösterilir
2. **Rewarded Video**: "Watch Ad to Continue" butonu ile

### ⚙️ Ayarlar (AdManager Inspector)
- `Test Mode`: true = test reklamları, false = gerçek reklamlar
- `Show Banner On Start`: Oyun başlangıcında banner göster
- `Show Interstitial Every N Deaths`: Kaç ölümde bir interstitial göster
- `Android Game Id`: Unity Dashboard'dan aldığınız Android ID
- `iOS Game Id`: Unity Dashboard'dan aldığınız iOS ID

---

## Gelir Tahminleri

### Küçük Ölçek (1,000 günlük aktif kullanıcı)
| Reklam Tipi | Günlük Gelir | Aylık Gelir |
|-------------|--------------|-------------|
| Banner      | $5-10        | $150-300    |
| Interstitial| $10-20       | $300-600    |
| Rewarded    | $20-40       | $600-1,200  |
| **TOPLAM**  | **$35-70**   | **$1,050-2,100** |

### Orta Ölçek (10,000 günlük aktif kullanıcı)
| Reklam Tipi | Günlük Gelir | Aylık Gelir |
|-------------|--------------|-------------|
| Banner      | $50-100      | $1,500-3,000   |
| Interstitial| $100-200     | $3,000-6,000   |
| Rewarded    | $200-400     | $6,000-12,000  |
| **TOPLAM**  | **$350-700** | **$10,500-21,000** |

### Büyük Ölçek (100,000 günlük aktif kullanıcı)
| Reklam Tipi | Aylık Gelir |
|-------------|-------------|
| Banner      | $15,000-30,000   |
| Interstitial| $30,000-60,000   |
| Rewarded    | $60,000-120,000  |
| **TOPLAM**  | **$105,000-210,000** |

**Not:** Bu tahminler yaklaşık değerlerdir. Gerçek gelirler şunlara bağlıdır:
- Coğrafi konum (ABD/Avrupa > Asya)
- Oyun kategorisi
- Kullanıcı engagement'ı
- Reklam kalitesi
- Sezon (Aralık en yüksek!)

---

## Geliri Maksimize Etme Stratejileri

### 1. Rewarded Video'yu Teşvik Edin ⭐ (EN ÖNEMLİ)
Rewarded video en yüksek geliri sağlar:
- Continue özelliği oyunculara değer sağlar
- Oyuncular kendi istekleriyle izler
- Completion rate çok yüksek
- CPM (bin gösterim başı kazanç) en yüksek

**İpucu:** Continue'yu değerli yapın! Oyuncu gerçekten devam etmek istesin.

### 2. Reklam Sıklığını Optimize Edin
**ÇOK FAZLA REKLAM = OYUNCULAR KAÇAR!**

Önerilen sıklıklar:
- Banner: Sürekli (ama alt köşede, dikkat dağıtmadan)
- Interstitial: Her 3-5 oyunda bir (daha sık göstermeyin!)
- Rewarded: Sınırsız (kullanıcı seçimi)

**Ayarlama:**
```csharp
// AdManager Inspector'da
showInterstitialEveryNDeaths = 5; // Daha az agresif
```

### 3. Kullanıcı Tutmaya (Retention) Odaklanın
1,000 kullanıcının %10'u = 100 aktif kullanıcı
1,000 kullanıcının %50'si = 500 aktif kullanıcı

**Retention artırma yolları:**
- Düzenli güncellemeler
- Yeni haritalar/arabalar
- Leaderboard (liderlik tablosu)
- Daily rewards (günlük ödüller)
- Zorluk dengesi

### 4. Analytics Kullanın
Mutlaka analytics ekleyin:
- **Unity Analytics** (ücretsiz, kolay)
- **Firebase Analytics** (ücretsiz, gelişmiş)
- **GameAnalytics** (ücretsiz, oyun odaklı)

**Takip edilmesi gerekenler:**
- DAU (Daily Active Users)
- Retention (1, 7, 30 gün)
- Session length
- Ad impressions
- Ad revenue per user

### 5. A/B Testing
Farklı stratejileri test edin:
- Interstitial sıklığı (3 vs 5 ölüm)
- Banner pozisyonu (alt vs üst)
- Continue reward değeri
- Game difficulty

---

## Platform Önerileri

### Mobil (Android/iOS) - ÖNERİLİR
**Artıları:**
- Reklam geliri yüksek
- Geniş kullanıcı tabanı
- Free-to-play modeli iyi çalışır
- Touch kontroller bu oyun için ideal

**Ekstralar:**
- Unity Ads, AdMob veya IronSource
- In-App Purchases (gelecekte eklenebilir)

### PC (Steam, Itch.io)
**Artıları:**
- Reklam yok, satış geliri
- Premium fiyatlandırma ($0.99 - $4.99)
- Steam Workshop desteği

**Ekstralar:**
- Achievements
- Leaderboards
- Cloud saves

---

## Kurulum Checklist

### Unity Dashboard Kurulumu
- [ ] Unity Dashboard'da proje oluştur
- [ ] Android Game ID al
- [ ] iOS Game ID al
- [ ] Ad Units oluştur (Banner, Interstitial, Rewarded)
- [ ] Ödeme bilgilerini ekle (gelir almak için)

### Unity Editor Kurulumu
- [ ] Unity Ads package yükle
- [ ] AdManager GameObject oluştur
- [ ] Game ID'leri AdManager'a gir
- [ ] Test Mode = true ile test et
- [ ] Banner gösterimini test et
- [ ] Interstitial gösterimini test et
- [ ] Rewarded video test et

### Build Öncesi
- [ ] Test Mode = false yap
- [ ] Placeholder ID'leri değiştir
- [ ] Banner açık/kapalı seç
- [ ] Interstitial sıklığını ayarla
- [ ] Privacy policy ekle (gerekli!)

### Yayın Sonrası
- [ ] Analytics kur
- [ ] Günlük gelir takip et
- [ ] Retention rates kontrol et
- [ ] Kullanıcı feedback'ini dinle
- [ ] Reklam sıklığını optimize et

---

## Sık Sorulan Sorular

### Q: Ne kadar kazanabilirim?
**A:** Kullanıcı sayısına bağlı:
- 100 DAU: ~$3-7/gün ($90-210/ay)
- 1,000 DAU: ~$35-70/gün ($1,000-2,000/ay)
- 10,000 DAU: ~$350-700/gün ($10,000-20,000/ay)

### Q: Hangi platform daha karlı?
**A:** Mobil (özellikle iOS). iOS kullanıcıları genelde daha yüksek CPM sağlar.

### Q: Test Mode'u ne zaman kapatmalıyım?
**A:** Build alıp yayına çıkarmadan hemen önce. Test sırasında MUTLAKA açık bırakın!

### Q: Reklam geliri ne zaman alırım?
**A:** Unity Ads için minimum $100 kazanmanız gerekir. Aylık ödeme yapılır.

### Q: Çok fazla reklam koyarsam ne olur?
**A:** Kullanıcılar oyunu siler. Denge çok önemli!

### Q: Rewarded video mecburi olmalı mı?
**A:** HAYIR! Rewarded video mutlaka opsiyonel olmalı. Zorlamak kullanıcıları kaçırır.

### Q: Banner her zaman gösterilmeli mi?
**A:** Tercihinize bağlı. Ben oyun sırasında göstermeyi öneriyorum ama dikkat dağıtmamalı.

### Q: Continue sadece bir kez mi kullanılabilir?
**A:** Şu an evet (GameManager'da `hasUsedContinue`). Oyuncu oyun başına bir kez continue yapabilir.

### Q: Continue sayısını artırabilir miyim?
**A:** Evet! GameManager.cs'de `hasUsedContinue` logic'ini değiştirebilirsiniz.

---

## Alternatif Monetization Modelleri

### 1. Premium (Ücretli Oyun)
- Reklamsız oyun
- $0.99 - $4.99 arası fiyat
- Daha az kullanıcı ama direk gelir

### 2. Freemium + IAP (In-App Purchases)
- Oyun ücretsiz
- Kozmetik satın alımlar (arabalar, renkler)
- "Remove Ads" satın alımı ($2.99)
- Coin/currency sistemi

### 3. Hybrid (Karma)
- Ücretsiz + Reklamlar + IAP
- "Remove Ads" premium satın alımı
- Rewarded video opsiyonel kalır

---

## Önerilen Strateji (Bu Oyun İçin)

### Aşama 1: Launch (İlk Ay)
- ✓ Sadece reklamlar (mevcut sistem)
- ✓ Banner kapalı (oyun deneyimini bozmamak için)
- ✓ Interstitial = her 5 ölümde bir (az agresif)
- ✓ Rewarded video için continue
- **Hedef**: Kullanıcı kazanmak ve retention artırmak

### Aşama 2: Optimizasyon (2-3. Ay)
- Analytics verisine bak
- Reklam sıklığını optimize et
- A/B testing yap
- Feedback'e göre düzenle
- **Hedef**: Geliri maksimize etmek

### Aşama 3: Expansion (3+. Ay)
- IAP ekle (yeni arabalar, renkler)
- "Remove Ads" seçeneği ekle ($2.99)
- Daily rewards ekle
- Leaderboard ekle
- **Hedef**: Çeşitlendirme ve büyüme

---

## İletişim ve Destek

Sorularınız için:
- Unity Ads Documentation: https://docs.unity.com/ads/
- Unity Ads Dashboard: https://dashboard.unity3d.com/
- Unity Forum: https://forum.unity.com/

**Başarılar! Para kazanmanın anahtarı: İyi oyun + İyi retention + Akıllı reklam yerleşimi** 💰🎮
