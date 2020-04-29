using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] public int noteId;
    [SerializeField] public string noteName;
    [SerializeField] public bool found;
    [SerializeField] private GameObject noteObject;

    [SerializeField] private NotesManager notesManager;

    private void Start()
    {
        CheckIfInList();
    }

    private void CheckIfInList()
    {
        foreach (var note in notesManager.notes)
        {
            if (note == this)
                return;
        }
        notesManager.notes.Add(this);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            noteObject.SetActive(false);
            Found(); 
            notesManager.PrintNotePickUp(false);
            notesManager.GenerateNoteSelection(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        notesManager.PrintNotePickUp(true);
    }

    private void OnTriggerExit(Collider other)
    {
        notesManager.PrintNotePickUp(false);
    }

    private void Found()
    {
        notesManager.notes.Remove(this);
        notesManager.foundNotes.Add(this);
        found = true;
    }

}
