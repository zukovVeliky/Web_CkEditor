# Web CKEditor (Razor Pages)

Znovupouzitelna CKEditor 5 integrace pro ASP.NET Core Razor Pages s primo zabudovanou vazbou na `Web_FileManager`.

## Souvisejici repozitare (cross-reference)

Pokud mas otevreny pouze tento repozitar, ostatni komponenty dohledej zde:

- `Web_FileManager`: https://github.com/zukovVeliky/Web_FileManager
  - centralni file picker + sprava souboru + API.
- `Web_Avatar`: https://github.com/zukovVeliky/Web_Avatar
  - avatar crop komponenta navazana na FileManager.

## Zasadni informace

- Tato komponenta je navrzena tak, aby vybirala soubory pres stranku `/FileManager`.
- Bez funkcniho `Web_FileManager` nebude tlacitko souboroveho manageru fungovat.
- Vazba funguje pres callback funkci (`callback`) a picker parametry (`picker=1`, `allowExt`, `root`).

## Obsah repozitare

- `Areas/CKEditor5/Pages/CKEditor5.cshtml`
- `Areas/CKEditor5/Pages/CKEditor5.cshtml.cs`
- `Areas/CKEditor5/Pages/CKEditor5Inline.cshtml`
- `Areas/CKEditor5/Pages/CKEditor5Inline.cshtml.cs`
- `wwwroot/lib/CKEditor5/*` (build + translations)

## Dulezite: demo vs integrace

- Komponenta obsahuje i demo prvky (napr. pomocne otevreni FileManageru na `lib`), aby bylo jasne videt vazbu na FileManager.
- Demo cast je orientacni a neslouzi jako povinna integracni vrstva.
- Pro realnou integraci vloz komponentu do vlastni stranky a nastav callback/root dle sveho projektu.

## Pozadavky

- ASP.NET Core Razor Pages
- nainstalovana komponenta `Web_FileManager` a dostupna URL `/FileManager`
- neni potreba Entity Framework

## Instalace

1. Naklonuj:
   - `git clone https://github.com/zukovVeliky/Web_CkEditor.git`
2. Zkopiruj soubory do ciloveho projektu pri zachovani cest.
3. Ujisti se, ze v layoutu je dostupny Bootstrap (volitelne) a JS/CSS staticke soubory.

## Zakladni pouziti (stranka pro editaci clanku)

V Razor Page:

```cshtml
@await Html.PartialAsync(
    "~/Areas/CKEditor5/Pages/CKEditor5.cshtml",
    new CKEditor5.CKEditor5Model(
        text: Model.ArticleContent,
        Path: Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("UzivatelskeSoubory/Articles")),
        url: Request,
        instanceId: "article-editor",
        callbackName: "ArticleEditorFileCallback",
        pickerAllowExt: "jpg,jpeg,png,gif,webp,svg,pdf",
        pickerPopupName: "FileManagerPicker_Article")
)
```

## Konfigurace

Konfiguruj per-instanci pres parametry konstruktoru `CKEditor5Model`:

- `instanceId`: unikatni id editoru na strance
- `callbackName`: globalni JS callback, ktery prevezme URL z FileManageru
- `pickerAllowExt`: omezeni pripon ve FileManager pickeru
- `pickerPopupName`: jmeno popup okna
- `Path`: Base64 URL root cesta pro FileManager

## Vice editoru na jedne strance

Pokud mas na strance vice CKEditor instanci, musi mit kazda vlastni:

- `instanceId`
- `callbackName`
- `pickerPopupName`

Doporuceni:

- nikdy nepouzivej stejny `callbackName` pro 2 editory
- `callbackName` skladat z prefixu + `instanceId` (napr. `ArticleEditorFileCallback_home_editor`)
- zachovat oddelenou konfiguraci v `component-settings.json` per `instanceId`

Aktualni verze partialu `CKEditor5.cshtml` je izolovana per instance (IIFE), aby callback z FileManageru necilil vzdy na posledni editor na strance.

## Vazba na FileManager

CKEditor otevira:

- `/FileManager?picker=1&allowExt=...&callback=...&root=...`

Po vyberu souboru FileManager zavola callback (napr. `ArticleEditorFileCallback(url, fileName)`).

Vysledek:

- obrazek -> `insertImage`
- ostatni soubor -> odkaz (`link`)

## Nasazeni

- over, ze `/FileManager` je dostupny i v produkci
- pokud je app za reverzni proxy, udrz kompatibilni URL scheme/host
- povol popup okna pro domenu aplikace

## Troubleshooting

- tlacitko FileManager nic neotevre:
  - popup blocker
  - chybi `/FileManager` route
- soubor se nevlozi:
  - nesouhlasi `callbackName` mezi CKEditorem a FileManager URL parametrem
