using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class NotesTextConverter : MonoBehaviour
{
    private const string CFG_FOLDER = "./notes/";
    private const string KEYS_CFG = "notesText.json";

    private NotesManager notesManager;

    private void Start()
    {
        notesManager = this.gameObject.GetComponent<NotesManager>();
        ConvertTxtToList();
    }

    private void ConvertTxtToList()
    {
        if (!Directory.Exists(CFG_FOLDER))
        {
            Debug.Log("No config folder, creating...");
            Directory.CreateDirectory(CFG_FOLDER);
        }

        if (File.Exists(CFG_FOLDER + KEYS_CFG))
        {
            notesManager.noteTextContainer = JsonConvert.DeserializeObject<Dictionary<string,string>>(File.ReadAllText(CFG_FOLDER + KEYS_CFG));
        }
        else
        {
            Debug.Log("No file, creating...");
            notesManager.noteTextContainer.Add("ok","test one");
            StreamWriter sw = File.CreateText(CFG_FOLDER + KEYS_CFG);
            sw.Write(JsonConvert.SerializeObject(notesManager.noteTextContainer, Formatting.Indented));
            sw.Close();
        }

        Destroy(this);
    }
}
