using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using VoxelBusters.EssentialKit;
using VoxelBusters.EssentialKit.AddressBookCore;

public class Data_Fetch : MonoBehaviour
{
    public GameObject Panel_Parent;
    public GameObject Prefab_Panel;
    public Text First_name_Text;
    public Text Last_name_Text;
    public string First_name;
    public string Last_name;
    public Manager manager;
    public GameObject Button;
    private void Start()
    {
        manager = FindFirstObjectByType<Manager>();
    }
    //To Create Panel and Assing value 
    public void StringData(AddressBookReadContactsResult result)
    {
      foreach(var contact in result.Contacts)
        {
            GameObject Panel = Instantiate(Prefab_Panel); // Create a panel
            Panel.transform.SetParent(Panel_Parent.transform);// Make it Parent of main panel to scroll
            var contacts = result.Contacts; // Assign Contacts data from Manager
            First_name_Text.text = contact.FirstName;// Assgning First name Text Layer to Contat First Name
            Last_name_Text.text = contact.LastName;//Assgning Lasr name Text Layer to Contat First Name
            CanSendemail();//Checking CanSendEmail to toggle invite button
         }
    }
    //Mail Composer For Invite Button
    public void MailsComposer()
    {
        MailComposer composer = MailComposer.CreateInstance();
        composer.SetToRecipients(new string[1] { "to@gmail.com" });
        composer.SetCcRecipients(new string[1] { "cc@gmail.com" });
        composer.SetBccRecipients(new string[1] { "bcc@gmail.com" });

        composer.SetSubject("Hi my Friend");
        composer.SetBody("Body", false);
        composer.SetCompletionCallback((result, error) => {
            Debug.Log("Mail composer was closed. Result code: " + result.ResultCode);
        });
        composer.Show();
    }
   //Checking Can Send Email 
    public void CanSendemail()
    {
        bool canSendMail = MailComposer.CanSendMail();// Bool For CansendEmail
        if (canSendMail)// Cheking true
        {
            Button.SetActive(true); // enable the button
        }
        else //ohterwise false
        {
            Button.SetActive(false);// disable the button
        }
    }
}
