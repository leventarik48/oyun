# Unity'de Oyunu Kurma Rehberi

Bu rehber, oyunu Unity'de adım adım nasıl kuracağınızı gösterir.

## 1. Unity Projesini Açma

1. Unity Hub'ı açın
2. "Open" butonuna tıklayın (veya Add > Add project from disk)
3. `oyun` klasörünü seçin
4. Unity 2021.3 veya daha yeni bir versiyon kullanın (2D template önerilir)

## 2. Ana Sahneyi Oluşturma

### Adım 1: Yeni Sahne
1. `File > New Scene` veya `Ctrl+N`
2. `2D` template seçin (varsa)
3. `File > Save As` ile `Assets/Scenes/MainGame.unity` olarak kaydedin

### Adım 2: Kamera Ayarları
1. **Main Camera**'yı seçin (Hierarchy'de zaten var)
2. Inspector'da:
   - Position: `(0, 0, -10)`
   - Projection: `Orthographic`
   - Size: `8`
   - Background: Açık mavi (gökyüzü rengi)
3. Add Component > `CameraController` script
   - Offset: `(5, 3, -10)`
   - Smooth Speed: `0.125`
   - Look Ahead: `✓` (checked)

## 3. Terrain (Arazi) Oluşturma

1. Hierarchy'de sağ tıklayın > `Create Empty`
2. İsim verin: `Terrain`
3. Inspector'da:
   - Position: `(0, 0, 0)`
   - Tag: `Ground` (Dropdown'dan seçin, yoksa Add Tag yapın)
4. Add Component > `TerrainGenerator` script
5. TerrainGenerator ayarları:
   - Terrain Length: `200`
   - Segment Length: `2`
   - Height Variation: `5`
   - Min Height: `-2`
   - Max Height: `10`
   - Noise Scale: `0.1`
   - Hill Steepness: `2`

## 4. Araba Oluşturma

### Ana Araba Gövdesi

1. Hierarchy'de sağ tıklayın > `Create Empty`
2. İsim: `Car`
3. Position: `(5, 5, 0)` (Başlangıç pozisyonu - arazi üstünde)

4. **Add Component** > `Rigidbody2D`:
   - Mass: `100`
   - Linear Drag: `0.5`
   - Angular Drag: `0.5`
   - Gravity Scale: `1`
   - Collision Detection: `Continuous`

5. **Add Component** > `Box Collider 2D`:
   - Size: `(2, 1)`

6. **Add Component** > `CarController` script

### Görsel (Opsiyonel - Şimdilik kutu şeklinde)

1. Car'a sağ tıklayın > `2D Object > Sprite > Square`
2. İsim: `CarBody`
3. Position: `(0, 0, 0)` (Car'a göre local)
4. Scale: `(2, 1, 1)`
5. Sprite Renderer > Color: Kırmızı veya istediğiniz renk

### Ön Tekerlek

1. Car'a sağ tıklayın > `Create Empty`
2. İsim: `FrontWheel`
3. Position: `(1, -0.5, 0)`

4. **Add Component** > `Rigidbody2D`:
   - Mass: `10`
   - Collision Detection: `Continuous`

5. **Add Component** > `Circle Collider 2D`:
   - Radius: `0.5`

6. **Add Component** > `Hinge Joint 2D`:
   - Connected Rigidbody: `Car` (Car'ın Rigidbody2D'sini sürükleyin)
   - Anchor: `(0, 0)`
   - Connected Anchor: `(1, -0.5)`
   - Enable Collision: `✓`

7. **Add Component** > `WheelController` script

**Görsel (Tekerlek):**
- FrontWheel'e sağ tıklayın > `2D Object > Sprite > Circle`
- İsim: `WheelGraphic`
- Scale: `(1, 1, 1)`
- Color: Siyah
- FrontWheel'in WheelController'ında `Wheel Graphic` alanına bu sprite'ı sürükleyin

### Arka Tekerlek

1. Car'a sağ tıklayın > `Create Empty`
2. İsim: `RearWheel`
3. Position: `(-1, -0.5, 0)`
4. Ön tekerlek ile tamamen aynı componentleri ekleyin:
   - Rigidbody2D (Mass: 10)
   - Circle Collider 2D (Radius: 0.5)
   - Hinge Joint 2D (Connected Rigidbody: Car, Anchor: (0,0), Connected Anchor: (-1, -0.5))
   - WheelController script
5. Görsel için Circle sprite ekleyin

### Sürücü Kafası

1. Car'a sağ tıklayın > `Create Empty`
2. İsim: `DriverHead`
3. Position: `(0, 0.7, 0)`

**Görsel (Opsiyonel):**
- DriverHead'e Circle sprite ekleyin
- Scale: `(0.4, 0.4, 1)`
- Color: Ten rengi

### Car Controller Bağlantıları

1. `Car` GameObject'ini seçin
2. Inspector'da `CarController` script bölümüne:
   - Car Body: Car'ın `Rigidbody2D`'sini sürükleyin
   - Front Wheel: FrontWheel'in `Rigidbody2D`'sini sürükleyin
   - Rear Wheel: RearWheel'in `Rigidbody2D`'sini sürükleyin
   - Front Wheel Transform: `FrontWheel` transform'unu sürükleyin
   - Rear Wheel Transform: `RearWheel` transform'unu sürükleyin
   - Driver Head: `DriverHead` transform'unu sürükleyin
   - Motor Torque: `500`
   - Max Speed: `30`

### Kamera Bağlantısı

1. `Main Camera`'yı seçin
2. `CameraController` script'te:
   - Target: `Car` transform'unu sürükleyin

## 5. UI Oluşturma

### Canvas

1. Hierarchy'de sağ tıklayın > `UI > Canvas`
2. Canvas ayarları (otomatik olmalı):
   - Render Mode: `Screen Space - Overlay`

3. Canvas altına `EventSystem` otomatik eklenir

### Game UI Panel

1. Canvas'a sağ tıklayın > `UI > Panel`
2. İsim: `GameUI`
3. Inspector'da Image component:
   - Color: Tamamen transparan (Alpha: 0)

### Distance Text

1. GameUI'ye sağ tıklayın > `UI > Text - TextMeshPro` (yoksa Legacy Text)
2. İsim: `DistanceText`
3. Rect Transform:
   - Anchor: Sol üst köşe
   - Pos X: `150`, Pos Y: `-50`
   - Width: `300`, Height: `50`
4. Text ayarları:
   - Text: `Distance: 0m`
   - Font Size: `24`
   - Color: Beyaz
   - Alignment: Sol

### Speed Text

1. GameUI'ye sağ tıklayın > `UI > Text`
2. İsim: `SpeedText`
3. Rect Transform:
   - Anchor: Sol üst köşe
   - Pos X: `150`, Pos Y: `-100`
   - Width: `300`, Height: `50`
4. Text ayarları:
   - Text: `Speed: 0 m/s`
   - Font Size: `24`
   - Color: Beyaz

### Game Over Panel

1. Canvas'a sağ tıklayın > `UI > Panel`
2. İsim: `GameOverPanel`
3. Rect Transform: Tam ekran (varsayılan)
4. Image component:
   - Color: Siyah, Alpha: `200` (yarı saydam)
5. Inspector'ın en üstünde GameObject'i deaktif edin (checkbox'u kaldırın)

### Game Over Message

1. GameOverPanel'e sağ tıklayın > `UI > Text`
2. İsim: `GameOverMessage`
3. Rect Transform:
   - Anchor: Merkez
   - Pos: `(0, 50, 0)`
   - Width: `600`, Height: `200`
4. Text ayarları:
   - Text: `Game Over!`
   - Font Size: `36`
   - Color: Beyaz
   - Alignment: Merkez
   - Vertical: Merkez

### Game Over Distance

1. GameOverPanel'e sağ tıklayın > `UI > Text`
2. İsim: `GameOverDistance`
3. Rect Transform:
   - Anchor: Merkez
   - Pos: `(0, -50, 0)`
   - Width: `400`, Height: `50`
4. Text ayarları:
   - Text: `Distance: 0m`
   - Font Size: `28`
   - Color: Sarı
   - Alignment: Merkez

## 6. Game Manager

### GameObject Oluşturma

1. Hierarchy'de sağ tıklayın > `Create Empty`
2. İsim: `GameManager`
3. Position: `(0, 0, 0)`

### GameManager Script

1. Add Component > `GameManager`
2. Referansları bağlayın:
   - Car Controller: `Car`'ın CarController component'ini sürükleyin

### UI Manager Script

1. GameManager'a Add Component > `UIManager`
2. Referansları bağlayın:
   - Distance Text: `DistanceText` text component'ini sürükleyin
   - Speed Text: `SpeedText` text component'ini sürükleyin
   - Game UI: `GameUI` panel'ini sürükleyin
   - Game Over Panel: `GameOverPanel`'i sürükleyin
   - Game Over Distance Text: `GameOverDistance` text'ini sürükleyin
   - Game Over Message Text: `GameOverMessage` text'ini sürükleyin
   - Car Controller: `Car`'ın CarController'ını sürükleyin

### GameManager ile UIManager Bağlantısı

1. `GameManager`'ı seçin
2. GameManager script'te:
   - UI Manager: Aynı GameObject'teki UIManager component'ini sürükleyin

## 7. Test ve Oyna!

1. Sahneyi kaydedin (Ctrl+S)
2. Play butonuna basın (veya Ctrl+P)

### Kontroller:
- **A/D** veya **←/→**: Gaz/Geri
- **W/S** veya **↑/↓**: Havada rotasyon
- **Space**: Fren
- **R**: Restart (ölünce)
- **ESC**: Quit (ölünce)

## Sorun Giderme

### Araba düşüyor / zemin yok
- Terrain GameObject'inin aktif olduğunu kontrol edin
- TerrainGenerator script'inin çalıştığını kontrol edin (Play mode'da)
- Terrain'in "Ground" tag'ine sahip olduğunu doğrulayın

### Araba hareket etmiyor
- Tüm Rigidbody2D'lerin doğru ayarlandığını kontrol edin
- Hinge Joint 2D'lerin Connected Rigidbody'lerinin Car olduğunu doğrulayın
- CarController'da wheel referanslarının doğru olduğunu kontrol edin

### UI görünmüyor
- Canvas Render Mode'unun "Screen Space - Overlay" olduğunu kontrol edin
- UI elementlerinin aktif olduğunu doğrulayın
- UIManager'da referansların doğru bağlandığını kontrol edin

### Kamera takip etmiyor
- Main Camera'da CameraController script'inin olduğunu kontrol edin
- Target'ın Car olduğunu doğrulayın
- Camera'nın Z pozisyonunun -10 olduğunu kontrol edin

## Ek Özelleştirmeler

### Daha Zor Arazi
TerrainGenerator'da:
- Height Variation: `8`
- Hill Steepness: `3`
- Max Height: `15`

### Daha Hızlı Araba
CarController'da:
- Motor Torque: `800`
- Max Speed: `50`

### Daha Hassas Ölüm
CarController'da:
- Max Head Height: `1.5`
- Min Head Height: `-0.5`

İyi eğlenceler! 🚗🏔️
