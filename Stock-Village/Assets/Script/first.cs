using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

public class first : MonoBehaviour
{
    //start ��ư Ŭ�� �� Load ������
    public async void StartBtnClick()
    {

        SceneManager.LoadScene("Load");
  
    }

}