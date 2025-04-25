
# Karlov1 AI Görsel Üretim Sistemi (Google Colab + Ngrok + ASP.NET MVC)

Bu proje, ASP.NET MVC mimarisi ile OpenAI benzeri bir görsel üretim yapay zekasını Google Colab üzerinde çalıştırarak, API üzerinden prompt gönderip gelen görsel çıktıyı web arayüzünde kullanıcıya sunmayı amaçlar.

---

## 📌 Genel Akış

1. Google Colab üzerinde görsel üretici model (örnek: ControlNet, SD, DALL·E benzeri) kurulur.
2. Flask ile küçük bir API servisi oluşturulur (Colab içinde).
3. Ngrok ile Colab API'si dış dünyaya açılır.
4. ASP.NET MVC uygulaması bu API'ye prompt gönderir.
5. Gelen görsel ASP.NET MVC'de kullanıcıya gösterilir.

---

## 🔧 Kullanılan Teknolojiler

- 🧠 Google Colab (GPU destekli model çalıştırma)
- 🌐 Flask (Python API sunucusu)
- 🚇 Ngrok (Localhost'u internetten erişilebilir hale getirme)
- 💻 ASP.NET MVC (.NET 6+ web arayüzü)
- 🔗 HTTPClient (.NET ile API bağlantısı)

---

## 📁 Proje Yapısı

```
/colab/
    app.py               # Flask tabanlı API sunucusu
    model_setup.ipynb    # Google Colab üzerinde çalıştırılan model kurulumu
/ngrok/
    ngrok_setup.sh       # Ngrok bağlantı başlatma
/aspnet-mvc/
    Controllers/
        ImageController.cs
    Views/
        Image/Index.cshtml
    Services/
        ImageGenerationService.cs
```

---

## ⚙️ Kurulum Adımları

### 🔹 1. Google Colab Model Kurulumu
- `model_setup.ipynb` çalıştırılır.
- Gerekli model dosyaları ve Flask kurulumları yapılır.

### 🔹 2. Flask API (Colab içi)
```python
from flask import Flask, request, jsonify
app = Flask(__name__)

@app.route("/generate", methods=["POST"])
def generate_image():
    prompt = request.json.get("prompt")
    # prompt -> görsel üretimi -> görsel URL veya base64 döner
    return jsonify({"image_url": "https://..."})
```

### 🔹 3. Ngrok ile dış dünyaya açma
```bash
!ngrok http 5000
```
- Size verilen `https://...ngrok.io` adresini ASP.NET içinde kullanın

---

## 🌐 ASP.NET Tarafı

### 🔹 Prompt Gönderme

```csharp
public async Task<IActionResult> GenerateImage(string prompt)
{
    var client = new HttpClient();
    var response = await client.PostAsJsonAsync("https://<ngrok-link>/generate", new { prompt });

    var data = await response.Content.ReadFromJsonAsync<YourResponseModel>();
    ViewBag.ImageUrl = data.image_url;

    return View();
}
```

### 🔹 Görsel Gösterimi

```html
@if (ViewBag.ImageUrl != null)
{
    <img src="@ViewBag.ImageUrl" alt="Üretilen görsel" />
}
```

---

## ⚠️ Dikkat Edilmesi Gerekenler

- Ngrok bağlantısı her Colab restart'ında değişir. ASP.NET tarafına güncel linki yazmalısınız.
- Colab bağlantısı koparsa görsel üretim API'si çalışmaz.
- Üretilen görsel base64 ise ASP.NET tarafında dönüştürme yapılmalıdır.

---

## 💡 İyileştirme Önerileri

- Otomatik Ngrok URL güncellemesi için ASP.NET arayüzüne alan eklenebilir.
- Colab yerine stabil sunucuda çalıştırmak maliyet ve hız açısından iyileştirilebilir.
- Üretilen görseller veritabanına kaydedilebilir.

---

## 📞 İletişim

Bu proje prototip amaçlıdır. Geliştiriciler için AI destekli görsel üretim sistemlerinin mimarisini öğrenmek adına tasarlanmıştır.
