# Web_CkEditor Component Package

Znovupouzitelna CKEditor 5 integrace pro ASP.NET Core Razor Pages s primou vazbou na `Web_FileManager`.

Aktualni verze podporuje:

- `ClassicEditor`
- `InlineEditor`
- bezpecne vice instanci editoru na jedne strance
- jednorazove nacitani build scriptu
- vazbu na FileManager picker
- vlozeni obrazku i neobrazkovych souboru
- viewport offset pro sticky admin hlavicku
- volitelne root overrides per editor instance

## Co zkopirovat 1:1

- `Areas/CKEditor5`
- `wwwroot/lib/CKEditor5`

Neměn:

- `Areas/CKEditor5`
- `wwwroot/lib/CKEditor5`
- namespace `CKEditor5`

## Závislosti

- ASP.NET Core Razor Pages
- nainstalovany a funkcni `Web_FileManager`
- dostupna route `/FileManager`

## Jak se komponenta renderuje

Zakladni partial:

- `~/Areas/CKEditor5/Pages/CKEditor5.cshtml`

Inline varianta:

- `~/Areas/CKEditor5/Pages/CKEditor5Inline.cshtml`

## Základní použití

```cshtml
@await Html.PartialAsync(
    "~/Areas/CKEditor5/Pages/CKEditor5.cshtml",
    new CKEditor5.CKEditor5Model(
        text: Model.ArticleContent,
        Path: Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("UzivatelskeSoubory/Articles")),
        url: Request,
        instanceId: "article-editor",
        callbackName: "ArticleEditorFileCallback",
        pickerAllowExt: "jpg,jpeg,png,gif,bmp,webp,svg,pdf",
        pickerPopupName: "FileManagerPicker_Article"
    )
)
```

## Inline editor

```cshtml
@await Html.PartialAsync(
    "~/Areas/CKEditor5/Pages/CKEditor5.cshtml",
    new CKEditor5.CKEditor5Model(
        text: Model.SummaryHtml,
        Path: Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("UzivatelskeSoubory/Summary")),
        url: Request,
        instanceId: "summary-inline",
        callbackName: "SummaryInlineCallback",
        pickerPopupName: "FileManagerPicker_Summary",
        useInlineEditor: true
    )
)
```

`useInlineEditor: true` prepne komponentu na `InlineEditor` a zaroven synchronizuje HTML do skryteho `textarea`, aby sel obsah standardne odeslat ve formulari.

## Parametry modelu

- `text`: pocatecni HTML
- `Path`: root pro FileManager jako Base64 nebo Base64URL string
- `url`: aktualni `HttpRequest`
- `instanceId`: unikatni identifikator editoru na strance
- `callbackName`: jmeno globalni callback funkce pro FileManager
- `pickerAllowExt`: povolene pripony v pickeru
- `pickerPopupName`: jmeno popup okna
- `useInlineEditor`: `false` pro Classic, `true` pro Inline

## Multi-instance pravidla

Kazda instance editoru musi mit vlastni:

- `instanceId`
- `callbackName`
- `pickerPopupName`

Aktualni verze partialu zamerne:

- registruje callback per instance
- nespousti se nad posledni instanci na strance
- nenačítá zbytečně `ckeditor.js` a `ckeditorInline.js` vicekrat

## Vazba na FileManager

CKEditor otevírá:

```text
/FileManager?picker=1&allowExt=...&callback=...&root=...
```

Po vyberu FileManager vola:

```js
window[callbackName](url, fileName);
```

Komponenta pak:

- obrazek vlozi jako `insertImage`
- neobrazkovy soubor vlozi jako odkaz

## Root overrides

Pokud chces root menit dynamicky az na strance, pouzij:

```html
<script>
window.__ckEditorRootOverrides = {
    "ArticleEditorFileCallback": "Event/123",
    "summary-inline": "UzivatelskeSoubory/Summary"
};
</script>
```

Komponenta stale podporuje i legacy jmeno:

- `window.__kulturaRootOverrides`

## Viewport offset pro sticky hlavičku

Kdyz host projekt ma vlastni sticky admin toolbar, komponenta umi upravit viewport offset.

Moznosti:

- automaticky fallback pro `#v2-tools-header-shell` a `.v2-tools-header-shell`
- explicitne:

```html
<script>
window.__ckEditorViewportOffsetTop = 72;
</script>
```

## components.settings.json

CKEditor typicky pouziva sekci:

```json
{
  "components": {
    "ckEditor": {
      "defaults": {
        "setPath": "UzivatelskeSoubory",
        "callbackName": "ZachyceniURLCKeditor",
        "pickerAllowExt": "jpg,jpeg,png,gif,bmp,webp,svg,pdf",
        "popupName": "FileManagerPicker"
      }
    }
  }
}
```

## Smoke test

1. Otevri stranku s editorem.
2. Over, ze se editor vyrenderoval.
3. Klikni FileManager button v toolbaru.
4. Over popup `/FileManager`.
5. Vyber obrazek a over vlozeni.
6. Vyber PDF a over vlozeni odkazu.
7. Pokud mas vice editoru na strance, proveď stejny test pro kazdou instanci.

## Dokumentace

- deployment checklist: [docs/DEPLOYMENT.md](docs/DEPLOYMENT.md)
- AI playbook: [docs/AI-INTEGRATION.md](docs/AI-INTEGRATION.md)

## Souvisejici repozitare

- https://github.com/zukovVeliky/Web_CkEditor
- https://github.com/zukovVeliky/Web_FileManager
- https://github.com/zukovVeliky/Web_Avatar
