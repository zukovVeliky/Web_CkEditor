# AI Integration Playbook

Tento dokument je urceny pro AI klienta nebo developera, ktery chce rychle zprovoznit `Web_CkEditor` a propojit ho s `Web_FileManager`.

## Cíl

Vlozit editor do Razor page, otevrit FileManager picker, vratit URL do konkretni instance editoru a bezpecne fungovat i s vice editory na jedne strance.

## Udělej přesně toto

1. Zkopiruj `Areas/CKEditor5`.
2. Zkopiruj `wwwroot/lib/CKEditor5`.
3. Nainstaluj a zprovozni `Web_FileManager`.
4. Renderuj partial `~/Areas/CKEditor5/Pages/CKEditor5.cshtml`.
5. V kazde instanci nastav unikatni:
   - `instanceId`
   - `callbackName`
   - `pickerPopupName`
6. Root editoru nastav pres `Path`.

## Neprováděj

- nesdilej stejny `callbackName` mezi dvema editory
- nesdilej stejny `pickerPopupName` mezi editory na jedne strance
- nemen namespace `CKEditor5`

## Minimalni priklad

```cshtml
@await Html.PartialAsync(
    "~/Areas/CKEditor5/Pages/CKEditor5.cshtml",
    new CKEditor5.CKEditor5Model(
        text: Model.Html,
        Path: Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("UzivatelskeSoubory/Articles")),
        url: Request,
        instanceId: "article-editor",
        callbackName: "ArticleEditorFileCallback",
        pickerPopupName: "FileManagerPicker_Article"
    )
)
```

## Pokud potrebujes inline mod

Pouzij:

```csharp
useInlineEditor: true
```

Komponenta sama:

- natahne `InlineEditor`
- zapisuje obsah zpet do skryteho `textarea`

## Pokud potrebujes menit root za behu

```html
<script>
window.__ckEditorRootOverrides = {
    "ArticleEditorFileCallback": "Event/123"
};
</script>
```

## Diagnostika

- FileManager button neotevira popup:
  - popup blocker
  - neexistuje `/FileManager`
- popup se zavre, ale editor nic nevlozi:
  - callbackName nesouhlasi
  - popup nema pristup k `window.opener`
- obrazek se vlozi do spatne instance:
  - duplicate `callbackName`
  - duplicate `instanceId`
  - duplicate `pickerPopupName`
