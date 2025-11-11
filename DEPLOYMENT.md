# Deployment Rehberi - Hill Climb Car Game

Bu rehber, oyununuzu mobil (Android/iOS) ve PC platformlarında yayınlamanız için gerekli adımları içerir.

## İçindekiler

1. [Unity Ads Kurulumu](#unity-ads-kurulumu)
2. [Android Build](#android-build)
3. [iOS Build](#ios-build)
4. [PC Build (Windows/Mac/Linux)](#pc-build)
5. [Yayınlama Platformları](#yayınlama-platformları)

---

## Unity Ads Kurulumu

### 1. Unity Ads Package Kurulumu

1. Unity Editor'de `Window > Package Manager` açın
2. Sol üstte `Packages: Unity Registry` seçin
3. **Advertisement** (veya **Monetization**) paketini bulun
4. `Install` butonuna tıklayın

**Alternatif:** Manuel kurulum:
- `Window > Package Manager`
- `+ (Add)` > `Add package by name...`
- Package name: `com.unity.ads`
- Version: `4.0.0` veya daha yeni

### 2. Unity Dashboard'da Proje Oluşturma

1. [Unity Dashboard](https://dashboard.unity3d.com/) adresine gidin
2. `Create` > `Project` seçin
3. Proje adını girin (örn: "HillClimbCar")
4. Platform seçin: Android ve/veya iOS

### 3. Game ID'leri Alma

1. Dashboard'da projenizi açın
2. Sol menüden `Monetization` > `Ad Units` seçin
3. **Game ID**'leri kopyalayın:
   - Android Game ID
   - iOS Game ID

**Örnek:**
- Android: `1234567`
- iOS: `7654321`

### 4. Ad Unit ID'lerini Not Alma

Varsayılan Ad Unit ID'ler:
- **Android:**
  - Banner: `Banner_Android`
  - Interstitial: `Interstitial_Android`
  - Rewarded: `Rewarded_Android`

- **iOS:**
  - Banner: `Banner_iOS`
  - Interstitial: `Interstitial_iOS`
  - Rewarded: `Rewarded_iOS`

### 5. Unity Editor'de Game ID'leri Ayarlama

1. Hierarchy'de `AdManager` GameObject'ini seçin (yoksa oluşturun)
2. `AdManager` script component'inde:
   - `Android Game Id`: Dashboard'dan aldığınız Android ID'yi yapıştırın
   - `Ios Game Id`: Dashboard'dan aldığınız iOS ID'yi yapıştırın
   - `Test Mode`: Test ederken `true`, yayına çıkarken `false`

**ÖNEMLİ:** Test Mode'u yayınlamadan önce `false` yapın!

---

## Android Build

### Gereksinimler
- Unity 2021.3 veya üzeri
- Android SDK & NDK (Unity Hub üzerinden yüklenebilir)
- JDK (Java Development Kit)

### Adım 1: Build Settings

1. `File > Build Settings`
2. Platform: `Android` seçin
3. `Switch Platform` butonuna tıklayın (gerekiyorsa)

### Adım 2: Player Settings

1. Build Settings penceresinde `Player Settings...` butonuna tıklayın

#### Company & Product
- **Company Name**: Şirket adınız
- **Product Name**: `Hill Climb Car` (veya oyun adınız)

#### Icon
- **Default Icon**: 512x512 boyutunda oyun ikonu
- **Adaptive Icon**: Android Oreo+ için

#### Resolution and Presentation
- **Default Orientation**: `Landscape Left` veya `Auto Rotation`
- **Allowed Orientations**: Landscape seçeneklerini işaretleyin

#### Other Settings
- **Package Name**: `com.yourcompany.hillclimbcar` (benzersiz olmalı!)
- **Version**: `1.0.0`
- **Bundle Version Code**: `1` (her güncellemede artırın)
- **Minimum API Level**: `Android 5.1 (API level 22)` veya üzeri
- **Target API Level**: `Android 13 (API level 33)` veya en yeni

#### Scripting Backend
- **Scripting Backend**: `IL2CPP` (Google Play için zorunlu)
- **Target Architectures**:
  - ✓ ARMv7
  - ✓ ARM64 (Google Play için zorunlu)

#### Graphics
- **Graphics API**: `OpenGLES3`, `OpenGLES2`, `Vulkan`
- **Color Space**: `Linear` (daha iyi görsel)

### Adım 3: Build APK/AAB

#### APK (Test için):
1. `File > Build Settings`
2. `Build` butonuna tıklayın
3. Kaydetme konumu seçin
4. Bekleme: Build tamamlanacak

#### AAB (Google Play için):
1. `File > Build Settings`
2. `Build App Bundle (Google Play)` seçeneğini işaretleyin
3. `Build` butonuna tıklayın
4. Kaydetme konumu seçin

### Adım 4: Testing

#### USB üzerinden test:
1. Android cihazınızda `Developer Options` > `USB Debugging` aktifleştirin
2. USB ile bilgisayara bağlayın
3. `File > Build Settings > Build And Run`

#### APK ile test:
1. APK dosyasını cihaza atın
2. Dosya yöneticisinden APK'yı açıp yükleyin

### Google Play Store'a Yükleme

1. [Google Play Console](https://play.google.com/console) açın
2. `Create App` (ilk kez) veya mevcut uygulamanızı seçin
3. `Production` > `Create new release`
4. AAB dosyasını yükleyin
5. Açıklama, ekran görüntüleri, vs. ekleyin
6. Review için gönderin

**Maliyet:** $25 (bir kerelik kayıt ücreti)

---

## iOS Build

### Gereksinimler
- macOS işletim sistemi
- Xcode (Mac App Store'dan ücretsiz)
- Apple Developer hesabı ($99/yıl)
- iOS Build Support (Unity Hub'dan yüklenebilir)

### Adım 1: Build Settings

1. `File > Build Settings`
2. Platform: `iOS` seçin
3. `Switch Platform` butonuna tıklayın

### Adım 2: Player Settings

#### Company & Product
- **Company Name**: Şirket adınız
- **Product Name**: `Hill Climb Car`

#### Icon & Splash
- **Icon**: 1024x1024 boyutunda ikon
- **Launch Screen**: Açılış ekranı ayarları

#### Resolution and Presentation
- **Default Orientation**: `Landscape Left` veya `Auto Rotation`

#### Other Settings
- **Bundle Identifier**: `com.yourcompany.hillclimbcar` (benzersiz!)
- **Version**: `1.0.0`
- **Build**: `1` (her güncellemede artırın)
- **Requires iOS Minimum Version**: `11.0` veya üzeri
- **Target SDK**: `Device SDK`
- **Architecture**: `ARM64`

#### Scripting Backend
- **Scripting Backend**: `IL2CPP`

### Adım 3: Xcode Build

1. `File > Build Settings > Build`
2. Klasör seçin (örn: `iOS_Build`)
3. Build tamamlanınca Xcode projesi oluşur

### Adım 4: Xcode'da Sign & Build

1. Oluşan `.xcodeproj` dosyasını Xcode ile açın
2. **Signing & Capabilities** sekmesinde:
   - `Automatically manage signing` işaretleyin
   - `Team`: Apple Developer hesabınızı seçin
3. Üst kısımda cihaz seçin (veya Generic iOS Device)
4. `Product > Archive` seçin
5. Archive tamamlanınca `Distribute App` seçin

### App Store'a Yükleme

1. Xcode'da `Distribute App` > `App Store Connect`
2. Yükleme tamamlanınca [App Store Connect](https://appstoreconnect.apple.com/) açın
3. `My Apps` > Uygulamanız
4. `+` > `New Version`
5. Ekran görüntüleri, açıklama, vs. ekleyin
6. `Submit for Review`

**Maliyet:** $99/yıl (Apple Developer Program)

---

## PC Build

PC için build çok daha basit ve ücretsiz!

### Windows Build

#### Adım 1: Build Settings
1. `File > Build Settings`
2. Platform: `Windows, Mac, Linux` seçin
3. Target Platform: `Windows`
4. Architecture: `x86_64` (64-bit)

#### Adım 2: Player Settings
- **Company Name**: Şirket adınız
- **Product Name**: `Hill Climb Car`
- **Default Icon**: 256x256 veya 512x512 ikon
- **Fullscreen Mode**: `Windowed` veya `Fullscreen Window`
- **Default Screen Width**: `1920`
- **Default Screen Height**: `1080`
- **Resizable Window**: ✓ işaretleyin

#### Adım 3: Build
1. `Build` butonuna tıklayın
2. Klasör seçin (örn: `Builds/Windows`)
3. `.exe` dosyası oluşur

### Mac Build

1. Build Settings > Platform: `Mac`
2. Architecture: `Intel 64-bit` veya `Apple Silicon`
3. Player Settings ayarları Windows ile aynı
4. Build

**Not:** Mac build için Mac bilgisayar gerekmez!

### Linux Build

1. Build Settings > Platform: `Linux`
2. Architecture: `x86_64`
3. Player Settings ayarları aynı
4. Build

---

## Yayınlama Platformları

### Mobil Platformlar

#### 1. Google Play Store (Android)
- **Maliyet**: $25 (bir kerelik)
- **Erişim**: Milyarlarca kullanıcı
- **Süreç**: 1-3 gün onay süreci
- **Link**: https://play.google.com/console

#### 2. Apple App Store (iOS)
- **Maliyet**: $99/yıl
- **Erişim**: iOS kullanıcıları
- **Süreç**: 1-7 gün onay süreci
- **Link**: https://developer.apple.com/

#### 3. Amazon Appstore (Android)
- **Maliyet**: Ücretsiz
- **Erişim**: Amazon cihazları
- **Süreç**: 1-3 gün
- **Link**: https://developer.amazon.com/

### PC Platformlar (Ücretsiz/Düşük Maliyetli)

#### 1. Itch.io (ÖNERİLİR - Başlangıç için)
- **Maliyet**: Ücretsiz (istediğiniz revenue share)
- **Erişim**: Indie oyun severler
- **Süreç**: Anında yayın
- **Link**: https://itch.io/developers
- **Avantajlar**:
  - Kolay yükleme
  - Anında yayın
  - Web player desteği
  - Pay-what-you-want pricing

#### 2. Steam
- **Maliyet**: $100 Steam Direct ücreti (oyun başına)
- **Erişim**: En büyük PC oyun platformu
- **Süreç**: 1-2 hafta onay
- **Link**: https://partner.steamgames.com/
- **Gereksinimler**:
  - Steam Direct ücreti
  - Vergi bilgileri
  - Marketing materyalleri

#### 3. Epic Games Store
- **Maliyet**: Ücretsiz
- **Erişim**: Büyüyen PC platformu
- **Süreç**: Küratörlü (başvuru gerekli)
- **Link**: https://www.epicgames.com/store/publish

#### 4. Game Jolt
- **Maliyet**: Ücretsiz
- **Erişim**: Indie oyun topluluğu
- **Süreç**: Anında yayın
- **Link**: https://gamejolt.com/

#### 5. Microsoft Store (Windows)
- **Maliyet**: Ücretsiz (Microsoft Developer hesabı gerekli - ~$19)
- **Erişim**: Windows 10/11 kullanıcıları
- **Süreç**: 1-3 gün onay
- **Link**: https://developer.microsoft.com/

### Tavsiyeler

#### Yeni Başlayanlar için:
1. **İlk**: Itch.io (ücretsiz, kolay, anında)
2. **İkinci**: Google Play (düşük maliyet, geniş erişim)
3. **Üçüncü**: Steam (daha fazla çaba gerektirir ama büyük potansiyel)

#### Reklam Geliri için:
- Mobil platformlar (Android/iOS) en iyi
- Unity Ads, AdMob, veya IronSource kullanın
- Banner + Interstitial + Rewarded Video kombinasyonu

#### Satış için:
- Steam en iyi (PC)
- Mobile için premium (ücretli) model zor
- Freemium (ücretsiz + IAP) modeli tercih edin

---

## Build Optimization

### Dosya Boyutu Azaltma

1. **Player Settings > Publishing Settings**:
   - `Compression Method`: LZ4 veya LZ4HC
   - `Split Application Binary`: ✓ (Android)

2. **Texture Compression**:
   - Android: ASTC
   - iOS: ASTC
   - PC: DXT5

3. **Audio Compression**:
   - Music: Vorbis
   - SFX: ADPCM

4. **Code Stripping**:
   - `Managed Stripping Level`: High
   - `Strip Engine Code`: ✓

### Performance Optimization

1. **Graphics**:
   - Basit 2D grafiklere sadık kalın
   - Texture atlas kullanın
   - Dynamic batching aktif olsun

2. **Physics**:
   - Fixed Timestep: 0.02 (50 FPS)
   - Physics 2D Auto Sync: false

3. **Target Frame Rate**:
   - Mobile: 60 FPS (Application.targetFrameRate = 60)
   - PC: -1 (unlimited)

---

## Sorun Giderme

### Unity Ads çalışmıyor
- Game ID'lerin doğru olduğunu kontrol edin
- Unity Ads package'ın yüklü olduğunu doğrulayın
- Test Mode'u true yapın
- Dashboard'da projenin aktif olduğunu kontrol edin

### Android build hatası
- Android SDK path'i doğru mu?
- NDK yüklü mü?
- JDK versiyonu uygun mu?
- API Level minimumları karşılanıyor mu?

### iOS build hatası
- macOS kullanıyor musunuz?
- Xcode güncel mi?
- iOS Build Support yüklü mü?
- Apple Developer hesabı aktif mi?

### App Store/Play Store Red
- Politikalara uygunluğu kontrol edin
- Yaş derecelendirmesi doğru mu?
- Privacy policy var mı? (gerekli!)
- Ekran görüntüleri yeterli mi?

---

## Checklist - Yayınlamadan Önce

### Genel
- [ ] Test Mode kapatıldı (AdManager'da testMode = false)
- [ ] Tüm placeholder'lar (YOUR_GAME_ID vb.) değiştirildi
- [ ] Version number doğru
- [ ] Company name ve product name ayarlandı
- [ ] İkonlar eklendi
- [ ] Build optimize edildi

### Android
- [ ] Package name benzersiz
- [ ] Bundle version code artırıldı
- [ ] IL2CPP + ARM64 aktif
- [ ] Minimum API level 22+
- [ ] Target API level en yeni
- [ ] AAB dosyası oluşturuldu

### iOS
- [ ] Bundle identifier benzersiz
- [ ] Build number artırıldı
- [ ] iOS minimum version 11.0+
- [ ] IL2CPP + ARM64 aktif
- [ ] Apple Developer hesabı aktif
- [ ] Xcode signing yapıldı

### PC
- [ ] Executable oluşturuldu
- [ ] Tüm platformlarda test edildi
- [ ] Ekran çözünürlükleri test edildi
- [ ] Kontroller (keyboard/mouse) çalışıyor

### Marketing
- [ ] Ekran görüntüleri hazırlandı (5+)
- [ ] Oyun açıklaması yazıldı
- [ ] Trailer/video hazırlandı (opsiyonel ama önerilir)
- [ ] Privacy policy oluşturuldu
- [ ] Sosyal medya linkleri hazır

---

## Monetization Stratejisi

### Reklam Yerleşimleri (Bu oyunda)

1. **Banner Ads**: Oyun sırasında alt/üst kısımda
   - Gelir: Düşük ama sürekli
   - Kullanıcı deneyimi: Minimal etki

2. **Interstitial Ads**: Her 3 ölümde bir
   - Gelir: Orta
   - Kullanıcı deneyimi: Orta etki
   - **ÖNEMLİ**: Çok sık göstermeyin!

3. **Rewarded Video**: Continue için
   - Gelir: Yüksek
   - Kullanıcı deneyimi: İyi (kullanıcı seçer)
   - **En iyi strateji!**

### Gelir Tahminleri (Yaklaşık)

**1000 günlük aktif kullanıcı (DAU) için:**
- Banner: $5-10/gün
- Interstitial: $10-20/gün
- Rewarded: $20-40/gün
- **Toplam**: ~$35-70/gün ($1000-2000/ay)

**10,000 DAU için:**
- ~$10,000-20,000/ay

**Not:** Gerçek gelirler bölgeye, oyun türüne, ve engagement'a bağlı olarak çok değişir.

### Geliri Artırma İpuçları

1. Kullanıcı tutma (retention) en önemli
2. Rewarded video'ları teşvik edin
3. Reklam sıklığını optimize edin
4. A/B testing yapın
5. Analytics kullanın (Unity Analytics, Firebase)

---

Başarılar! 🚀🎮

Sorularınız için: [Issues](https://github.com/yourrepo/oyun/issues)
