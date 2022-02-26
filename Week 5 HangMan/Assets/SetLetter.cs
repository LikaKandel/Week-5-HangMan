using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLetter : MonoBehaviour
{
   [SerializeField] private Text letter;
   
   private void Start()
   {
       ClearText();
   }
   public void InsertLetter(string _letter)
   {
       letter.text = _letter;
   }
   private void ClearText() {
       letter.text = "";
   }
}
