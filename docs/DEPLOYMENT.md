# CKEditor Deployment Checklist

## 1. Soubory

- `Areas/CKEditor5` je zkopirovane 1:1
- `wwwroot/lib/CKEditor5` je zkopirovane 1:1

## 2. Zavislost na FileManageru

- `Web_FileManager` je nainstalovan
- route `/FileManager` funguje
- picker callback z popupu se vraci do openeru

## 3. Rendering

- renderuj partial `~/Areas/CKEditor5/Pages/CKEditor5.cshtml`
- kazda instance ma unikatni `instanceId`
- kazda instance ma unikatni `callbackName`
- kazda instance ma unikatni `pickerPopupName`

## 4. Classic vs Inline

- `useInlineEditor: false` nebo vynechany parametr = `ClassicEditor`
- `useInlineEditor: true` = `InlineEditor`

## 5. Multi-instance test

1. vloz 2+ editoru na jednu stranku
2. otevri FileManager z prvni instance
3. vloz obrazek
4. over, ze obrazek skoncil ve spravnem editoru
5. zopakuj pro druhou instanci

## 6. Root routing

- `Path` nebo override odpovida realnemu rootu ve FileManageru
- pokud root meni az host stranka, pouzij `window.__ckEditorRootOverrides`

## 7. Sticky toolbar

Pokud toolbar koliduje s fixni hlavickou:

- nastav `window.__ckEditorViewportOffsetTop`
- nebo nech fallback na `#v2-tools-header-shell`

## 8. Smoke test

1. otevri editor
2. napis text
3. vloz obrazek pres FileManager
4. vloz PDF nebo jiny soubor jako odkaz
5. uloz formular
6. over vysledne HTML
