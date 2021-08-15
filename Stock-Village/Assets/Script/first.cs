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
    public Text apikey;
    public API api;

    public async void StartBtnClick()
    {
        bool result = await checkApi();

        //��ȿ�� API key�� ���
        if (result)
        {
            api.key = apikey.text; //API key�� Scriptable Object API�� ����
            SceneManager.LoadScene("Load"); //Load ������
        }
        //��ȿ���� ���� API key�� ���. 
        else
        {
            Debug.Log("API Key error !");
        }
    }
    async Task<bool> checkApi()
    {
        try
        {
            await BeginNetwork();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
    async Task BeginNetwork()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v2/get-summary?symbol=AMRN&region=US"),
            Headers =
    {
        { "x-rapidapi-key", apikey.text },
        { "x-rapidapi-host", "apidojo-yahoo-finance-v1.p.rapidapi.com" },
    },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            JObject obj = JObject.Parse(body);
            //Debug.Log(obj);
        };
    }
}