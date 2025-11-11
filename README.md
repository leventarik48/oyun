# Hill Climb Car Game - 2D Araba Oyunu

Unity ile yapılmış 2D fizik tabanlı araba oyunu. Dağlık arazilerde saçma sapan hareketler yaparak ilerleyin!

## Özellikler

- 🚗 Gerçekçi 2D araba fiziği
- 🏔️ Prosedürel olarak oluşturulan engebeli arazi
- 💥 Ölüm sistemi (devrilme, kafa çarpması)
- 📊 Mesafe ve hız göstergesi
- 🎮 Basit kontroller

## Kurulum

1. Unity Hub'ı açın
2. "Open" butonuna tıklayın
3. Bu klasörü seçin (`oyun` klasörü)
4. Unity versiyonu: 2021.3 veya üzeri (2D template)

## Oyunu Kurma (Unity İçinde)

### 1. Yeni Sahne Oluşturma

1. `Assets/Scenes` klasöründe sağ tıklayın
2. `Create > Scene` seçin
3. İsim verin: `MainGame`

### 2. Terrain (Arazi) Oluşturma

1. Hierarchy'de sağ tıklayın: `Create Empty`
2. İsim verin: `Terrain`
3. Inspector'da `Add Component` > `Terrain Generator` script'ini ekleyin
4. Tag ekleyin: `Ground` (Inspector'ın en üstünde Tag dropdown)

### 3. Araba Oluşturma

#### Ana Gövde:
1. `Create Empty` > İsim: `Car`
2. `Add Component`:
   - `Rigidbody2D` (Mass: 100, Linear Drag: 0.5, Angular Drag: 0.5)
   - `Box Collider 2D` (Size: 2x1)
   - `Car Controller` script

#### Ön Tekerlek:
1. Car'ın altına `Create Empty` > İsim: `FrontWheel`
2. Position: (1, -0.5, 0)
3. `Add Component`:
   - `Rigidbody2D` (Mass: 10)
   - `Circle Collider 2D` (Radius: 0.5)
   - `Wheel Controller` script
4. `Hinge Joint 2D` ekleyin:
   - Connected Rigidbody: Car'ın Rigidbody2D'si
   - Anchor: (0, 0)

#### Arka Tekerlek:
1. Car'ın altına `Create Empty` > İsim: `RearWheel`
2. Position: (-1, -0.5, 0)
3. Ön tekerlek ile aynı componentleri ekleyin

#### Sürücü Kafası (Ölüm kontrolü için):
1. Car'ın altına `Create Empty` > İsim: `DriverHead`
2. Position: (0, 0.5, 0)

#### Car Controller'ı Ayarlama:
- Car Body: Car'ın Rigidbody2D'si
- Front Wheel: FrontWheel'in Rigidbody2D'si
- Rear Wheel: RearWheel'in Rigidbody2D'si
- Front Wheel Transform: FrontWheel transform
- Rear Wheel Transform: RearWheel transform
- Driver Head: DriverHead transform

### 4. Kamera Ayarları

1. Main Camera'yı seçin
2. `Add Component` > `Camera Controller` script
3. Target: Car transform
4. Offset: (5, 3, -10)
5. Camera'yı Orthographic yapın (Inspector > Projection: Orthographic)
6. Orthographic Size: 8

### 5. UI Oluşturma

1. Hierarchy'de sağ tıklayın: `UI > Canvas`
2. Canvas altına:

#### Game UI:
- `Create > Text` - İsim: `DistanceText`
  - Pos: (-400, 450)
  - Text: "Distance: 0m"

- `Create > Text` - İsim: `SpeedText`
  - Pos: (-400, 420)
  - Text: "Speed: 0 m/s"

#### Game Over Panel:
- `Create > Panel` - İsim: `GameOverPanel`
- Renk: Yarı saydam siyah
- Panel altına:
  - `Text` - İsim: `GameOverMessage` (ortada, büyük font)
  - `Text` - İsim: `GameOverDistance` (altında)

### 6. Game Manager

1. `Create Empty` > İsim: `GameManager`
2. `Add Component` > `Game Manager` script
3. `Add Component` > `UI Manager` script
4. Referansları bağlayın:
   - Car Controller: Car'ın controller'ı
   - Distance Text, Speed Text, vb.

## Kontroller

- **A/D** veya **Sol/Sağ Ok**: Gaz ver / Geri git
- **W/S** veya **Yukarı/Aşağı Ok**: Havada dönüş kontrolü
- **Space**: Fren
- **R**: Yeniden başlat (ölünce)
- **ESC**: Çıkış (ölünce)

## Ölüm Koşulları

- Araba ters dönerse (2 saniye boyunca)
- Sürücünün kafası yere çarparsa
- Araba ezilirse

## Geliştirme

### Script'ler

- `CarController.cs`: Araba fiziği ve kontrolleri
- `TerrainGenerator.cs`: Prosedürel arazi oluşturma
- `CameraController.cs`: Kamera takibi
- `GameManager.cs`: Oyun durumu yönetimi
- `UIManager.cs`: UI güncellemeleri
- `WheelController.cs`: Tekerlek görsellerinin dönmesi

### İyileştirmeler İçin Fikirler

- Farklı araba modelleri
- Güçlendirmeler (fuel, boost)
- Coin toplama sistemi
- Leaderboard
- Ses efektleri
- Daha fazla arazi çeşidi
- Multiplayer

## Sorun Giderme

### Araba hareket etmiyor:
- Rigidbody2D'lerin doğru ayarlandığından emin olun
- Hinge Joint 2D'lerin connected body'sinin doğru olduğunu kontrol edin
- Terrain'e "Ground" tag'i eklendiğinden emin olun

### Kamera takip etmiyor:
- Camera Controller'da target'ın Car olduğunu kontrol edin
- Camera'nın Orthographic olduğundan emin olun

### UI görünmüyor:
- Canvas Render Mode'unun "Screen Space - Overlay" olduğunu kontrol edin
- UI Manager'da referansların doğru bağlandığını kontrol edin

## Lisans

Bu proje eğitim amaçlıdır. Özgürce kullanabilirsiniz!

## Keyifli Oyunlar! 🎮🚗
