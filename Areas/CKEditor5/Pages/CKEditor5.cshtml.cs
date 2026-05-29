//using Blog.Pages.Editace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace CKEditor5
{
    public class CKEditor5Model : PageModel
    {
        public readonly HttpRequest URLpath;
        public string Text { get; set; } = string.Empty;
        public string SetPath { get; private set; } = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Path.Combine("UzivatelskeSoubory")));
        public string InstanceId { get; private set; } = "ckeditor-default";
        public string EditorElementId { get; private set; } = "ckeditor5_ckeditor_default";
        public string CallbackName { get; private set; } = "ZachyceniURLCKeditor";
        public string PickerAllowExt { get; private set; } = "jpg,jpeg,png,gif,bmp,webp,svg,pdf";
        public string PickerPopupName { get; private set; } = "FileManagerPicker";
        public bool UseInlineEditor { get; private set; } = false;

        public CKEditor5Model(
            string text,
            string Path,
            HttpRequest url,
            string instanceId = "ckeditor-default",
            string callbackName = "ZachyceniURLCKeditor",
            string pickerAllowExt = "jpg,jpeg,png,gif,bmp,webp,svg,pdf",
            string pickerPopupName = "FileManagerPicker",
            bool useInlineEditor = false)
        {
            /// path onsahuje cestu kam se budou ukladat obrazky ve formatu  System.Text.Encoding.UTF8.GetBytes
            this.SetPath = Path;
            this.Text = text;
            this.URLpath = url;
            ApplyIdentity(instanceId, callbackName, pickerAllowExt, pickerPopupName, useInlineEditor);
        }

        public CKEditor5Model(
            string text,
            HttpRequest url,
            string instanceId = "ckeditor-default",
            string callbackName = "ZachyceniURLCKeditor",
            string pickerAllowExt = "jpg,jpeg,png,gif,bmp,webp,svg,pdf",
            string pickerPopupName = "FileManagerPicker",
            bool useInlineEditor = false)
        {
            this.Text = text;
            this.URLpath= url;
            ApplyIdentity(instanceId, callbackName, pickerAllowExt, pickerPopupName, useInlineEditor);
        }
        public CKEditor5Model(
            HttpRequest url,
            string instanceId = "ckeditor-default",
            string callbackName = "ZachyceniURLCKeditor",
            string pickerAllowExt = "jpg,jpeg,png,gif,bmp,webp,svg,pdf",
            string pickerPopupName = "FileManagerPicker",
            bool useInlineEditor = false)
        {
            this.URLpath = url;
            ApplyIdentity(instanceId, callbackName, pickerAllowExt, pickerPopupName, useInlineEditor);
        }
        public void OnGet()
        {
        }

        private void ApplyIdentity(string instanceId, string callbackName, string pickerAllowExt, string pickerPopupName, bool useInlineEditor)
        {
            InstanceId = string.IsNullOrWhiteSpace(instanceId) ? "ckeditor-default" : instanceId.Trim();
            EditorElementId = "ckeditor5_" + SanitizeForHtmlId(InstanceId);
            CallbackName = string.IsNullOrWhiteSpace(callbackName) ? "ZachyceniURLCKeditor" : callbackName.Trim();
            PickerAllowExt = string.IsNullOrWhiteSpace(pickerAllowExt) ? "jpg,jpeg,png,gif,bmp,webp,svg,pdf" : pickerAllowExt.Trim();
            PickerPopupName = string.IsNullOrWhiteSpace(pickerPopupName) ? "FileManagerPicker" : pickerPopupName.Trim();
            UseInlineEditor = useInlineEditor;
        }

        private static string SanitizeForHtmlId(string value)
        {
            var chars = value.Select(ch => char.IsLetterOrDigit(ch) ? ch : '_').ToArray();
            var sanitized = new string(chars);
            return string.IsNullOrWhiteSpace(sanitized) ? "ckeditor_default" : sanitized;
        }
    }
}
