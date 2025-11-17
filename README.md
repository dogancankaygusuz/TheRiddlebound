# 🔦 The Riddlebound – Tünel Kaçış Sahnesi

**The Riddlebound**, çevresel hikâye anlatımına ve bulmaca çözmeye odaklanan bir macera oyunudur.  
Bu proje, oyunun gerilim ve tempo açısından en dinamik bölümü olan **“Tünel Kaçış Sahnesi”**ni içerir.
Oyuncu hem çevresel engellerden kaçınmalı hem de düşmanların görüş alanına girmemelidir.

---

## 🎮 Oynanış Özeti

Tünel sahnesi, **karanlık, dar ve tehditkâr** bir atmosferde geçer.  
Oyuncunun amacı, düşman karakterlere yakalanmadan, engellere takılmadan belirlenen puanı toplamalıdır.  

Bu sahnede oyuncu:

- 👣 **Düşmanlardan kaçar:**
- ⚙️ **Engelleri aşar:**
- 🧠 **Stratejik ilerleme gerekir:**
- 💀 **Yakaladığında sahne yeniden başlar**

---

## ⚙️ Öne Çıkan Sistemler

### 👁️ Düşman Takip Sistemi 
- Görüş alanına giren oyuncu, `OnPlayerDetected` event’i ile **Observer sistemi** üzerinden diğer düşmanlara sinyal gönderir.  
- Düşman, `Patrol`, `Chase` ve `Search` durumları arasında **State Pattern** ile geçiş yapar.

### 📸 Kamera Sarsıntısı
- `CameraShake.cs`, kısa sarsıntı efekti üretir.  
- Parametreler (`amplitude`, `duration`) olay şiddetine göre dinamik olarak belirlenir.

---

## 🧠 Geliştirme Süreci

Tünel Kaçış sahnesi, oyunun “ritmini yükselten” bölümünü temsil eder.  
Geliştirme sırasında amaç:

- Gerilim duygusunu teknik sistemlerle desteklemek  
- Performans optimizasyonu yaparak sahnenin akıcı çalışmasını sağlamak  
- Oynanışı zaman baskısı ve çevresel tehditlerle dengelemek  
- Sahne yönetiminde **Observer & State Pattern** prensiplerini uygulamak

---
