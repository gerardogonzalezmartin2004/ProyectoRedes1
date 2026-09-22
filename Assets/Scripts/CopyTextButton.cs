
using UnityEngine;
using TMPro;

public class CopyTextButton : MonoBehaviour
{
   
    public TMP_Text joinCodeText;
    public void CopyTextFromTextField()
    {
     
        GUIUtility.systemCopyBuffer = joinCodeText.text;
    }
}
