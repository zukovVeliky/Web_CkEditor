# CKEditor Deployment Checklist

## 1. Required dependency

- `Web_FileManager` je nainstalovan a dostupny na `/FileManager`.

## 2. Assets

- `wwwroot/lib/CKEditor5/ckeditor.js` existuje
- translation soubory zkopirovany (volitelne, doporucene)

## 3. Rendering

- partial je renderovan:
  - `~/Areas/CKEditor5/Pages/CKEditor5.cshtml`
- model ma unikatni:
  - `instanceId`
  - `callbackName`

## 4. File picker binding

- CKEditor URL volani obsahuje:
  - `picker=1`
  - `callback=<callbackName>`
  - `root=<base64-url path>`
- callback funkce existuje v `window[...]`

## 5. Smoke test

1. otevri stranku s editorem
2. klikni tlacitko FileManager v toolbaru
3. vyber obrazek
4. over vlozeni obrazku do editoru
5. vyber ne-obrazek a over vlozeni odkazu

Poznamka:
- pripadne demo helpery jsou volitelne a nejsou nutne pro produkcni integraci.
