using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Book Data", menuName = "Book Data", order = 51)]
public class BookData : ScriptableObject
{
    public string bookID;
    public string bookTitle;
    [TextArea] public string bookDescription;
    public GameObject bookPrefab;
    public Sprite bookIcon;
}
