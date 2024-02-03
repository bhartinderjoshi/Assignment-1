using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.EssentialKit;

public class Manager : MonoBehaviour
{
  
    Data_Fetch fetch;// declaring for to feed data
    private void Start()
    {
       fetch = FindObjectOfType<Data_Fetch>();//assigning Data fetch for each Panel
       // ForContact();// Calling the fuction in start
    }
    private void OnRequestContactsAccessFinish(AddressBookRequestContactsAccessResult result,  Error error)
    {
        Debug.Log("Request for contacts access finished.");
        Debug.Log("Address book contacts access status: " + result.AccessStatus);
    }
    public void ForContact()//For getting Access from user to get contact
    {
        AddressBookContactsAccessStatus status = AddressBook.GetContactsAccessStatus();
        AddressBook.RequestContactsAccess(callback: OnRequestContactsAccessFinish);
        AddressBook.ReadContacts(OnReadContactsFinish);
        
    }
    public void OnReadContactsFinish(AddressBookReadContactsResult result, Error error)
    {
        if (error == null)
        {
            fetch.StringData(result);//Calling Data fetch With result reference
            
            var contacts = result.Contacts;
            Debug.Log("Request to read contacts finished successfully.");
            Debug.Log("Total contacts fetched: " + contacts.Length);
            Debug.Log("Below are the contact details (capped to first 10 results only):");
            for (int iter = 0; iter < contacts.Length && iter < 10; iter++)
            {
              
                // Debug.Log(string.Format("[{0}]: {1}", iter, contacts[iter]));
                
            }
           
        }
        else
        {
            Debug.Log("Request to read contacts failed with error. Error: " + error);
        }
    }

}
