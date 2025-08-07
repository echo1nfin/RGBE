# 🖼️ RGBE - RGB Encryptor

> 💡 **Securely encrypt messages as PNG images using RGB values**

---

## 🚀 Platforms & Downloads

| Platform | Download |
|---------|----------|
| 🪟 Windows (x64) | [Download win.exe](https://github.com/echo1nfin/RGBE/releases) |
| 🐧 Linux (x64)   | [Download linux.AppImage](https://github.com/echo1nfin/RGBE/releases) |

> 📁 It's best to extract each build into a **separate directory** to avoid clutter.

---

## 📦 Installation & Usage

To install, you need to download **RGBE** and **RGBkeygen**. `RGBkeygen` is used to generate the encryption key.

⚠️ **IMPORTANT**
- File names (especially `k.png`, `m.png`) are **case-sensitive** and must remain unchanged.
- If you're sending images via messengers: **send them as FILES**, not embedded images.

### ✅ What you need:
1. Generate a key using **RGBkeygen** → this creates `k.png`.
2. Put the key (`k.png`) in the **same directory** as the program.
3. To encrypt:
   - Run RGBE → Select `2. Encrypter`
   -  input file name (name.type)
   - It will generate `m.png` → send this file to the recipient
4. To decrypt:
   - Ensure both `k.png` and `m.png` are in the directory
   - Run RGBE → Select `1. Decrypter`
   - The file `em.txt` will be created containing the decrypted message.

### 💬 Message Flow
| Role        | What You Do |
|-------------|-------------|
| **Sender**  | Send `k.png` once. Then, send only `m.png` for each message. |
| **Receiver**| Must have both `k.png` and `m.png` to decrypt the message. |

---

## 📄 File Summary

| File     | Purpose                        |
|----------|---------------------------------|
| `k.png` | Encryption key (must be same for all messages) |
| `m.png` | Encrypted message image         |
| `em.txt`| Output of decryption (UTF-8 text) |

---

## 🌍 [Перейти к русской версии](#русская-версия)

---

# 🇷🇺 Русская версия

## 🧩 Установка и запуск
Для работы вам нужны **RGBE** и **RGBkeygen**. `RGBkeygen` используется для генерации ключа.

⚠️ **ВАЖНО**
- Все имена файлов (особенно `k.png`, `m.png`) **обязательны** и чувствительны к регистру.
- При передаче через мессенджеры — **отправляйте файлы как ФАЙЛЫ**, не как изображения.

### 📌 Пошагово:
1. Сгенерируйте ключ через `RGBkeygen` → появится `k.png`
2. Убедитесь, что `k.png` лежит в папке с программой
3. Чтобы **зашифровать**:
   - Запустите RGBE → выберите `2. Encrypter`
   - Укажите путь к текстовому файлу (имя_файла.тип)
   - Получите `m.png` → отправьте этот файл получателю
4. Чтобы **расшифровать**:
   - Убедитесь, что в папке лежат `k.png` и `m.png`
   - Запустите RGBE → выберите `1. Decrypter`
   - Получите `em.txt` с расшифрованным текстом

### 🔁 Передача сообщений
| Роль         | Что нужно делать |
|--------------|------------------|
| **Отправитель** | Один раз отправить `k.png`, потом только `m.png` |
| **Получатель**  | Хранить `k.png`, получать и расшифровывать `m.png` |

---
