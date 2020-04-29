using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesManager : MonoBehaviour
{
    private int amountOfFoundNotes;
    [SerializeField] public List<Note> notes = new List<Note>();
    [SerializeField] public List<Note> foundNotes = new List<Note>();
    [SerializeField] public Dictionary<string, string> noteTextContainer;

    [SerializeField] private Dropdown noteSelection;
    [SerializeField] private Text noteTextField;

    [SerializeField] private GameObject notePickUpIndicator;


    public void GenerateNoteSelection(Note noteToAdd)
    {
        noteSelection.options.Add(new Dropdown.OptionData(noteToAdd.noteName));
    }

    private void FindNoteByName()
    {
        if (noteSelection.options[noteSelection.value] == null)
            return;
        noteTextContainer.TryGetValue(noteSelection.options[noteSelection.value].text, out string noteText);
        noteTextField.text = noteText;
    }

    public void PrintNotePickUp(bool state)
    {
        notePickUpIndicator.SetActive(state);
    }
}
