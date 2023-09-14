using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public GameObject parctefek;
    private PhotonView pw;
    private Animator animator;
    public TextMeshProUGUI nameTxt;
    public GameObject canvas;
    void Start()
    {
        pw = GetComponent<PhotonView>();
        animator = GetComponentInChildren<Animator>();
        nameTxt.text = pw.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        if (pw.IsMine) // Sadece kendi kullanıcısının bu kodları okumasını sağlar.
        {
            canvas.transform.rotation = Quaternion.Euler(90, 0, 0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       
            RaycastHit hit;
        
            if (Physics.Raycast(ray, out hit))        {
           
                Vector3 dir = hit.point - transform.position;            
                dir.y = 0;             
                transform.rotation = Quaternion.LookRotation(dir * Time.deltaTime * 2f);         
                Debug.DrawLine(transform.position, hit.point);
            
            }
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * 20f;
            float y = Input.GetAxis("Vertical") * Time.deltaTime * 20f;
            transform.Translate(x, 0, y);
       

            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray2, out hit))

                    Debug.Log(hit.transform.gameObject.tag);

                //Instantiate(parctefek, transform.position, Quaternion.Euler(-90f, transform.eulerAngles.y, 0f));
                PhotonNetwork.Instantiate("FirePart", transform.position, Quaternion.Euler(-90f, transform.eulerAngles.y, 0f));
            }

            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetBool("big",true);
            }
            else
            {
                animator.SetBool("big",false);
            }
        }
        
    }
   
}
