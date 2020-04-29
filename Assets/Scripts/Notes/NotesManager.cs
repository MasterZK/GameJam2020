using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesManager : MonoBehaviour
{
    private int amountOfFoundNotes;
    [SerializeField] public List<Note> notes = new List<Note>();
    [SerializeField] public List<Note> foundNotes = new List<Note>();
    [SerializeField] public Dictionary<string, string> noteTextContainer = new Dictionary<string, string>();

    [SerializeField] private Dropdown noteSelection;
    [SerializeField] private Text noteTextField;

    [SerializeField] private GameObject notePickUpIndicator;

    private void Update()
    {
        FindNoteByName();
    }

    public void GenerateNoteSelection(Note noteToAdd)
    {
        noteSelection.options.Add(new Dropdown.OptionData(noteToAdd.noteName));
    }

    private void FindNoteByName()
    {
        if (noteSelection.options.Count == 0)
            return;
        noteTextContainer.TryGetValue(noteSelection.options[noteSelection.value].text, out string noteText);
        noteTextField.text = noteText;

    }

    public void PrintNotePickUp(bool state)
    {
        notePickUpIndicator.SetActive(state);
    }
}
