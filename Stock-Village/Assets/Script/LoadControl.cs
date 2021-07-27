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
using System;

public class LoadControl : MonoBehaviour
{
    IEnumerator coroutine1;

    float count = 0; //�ҷ��� ���� ���� ����
    public Slider LoadSlider; //�ε���
    public StockList stockList; //�ֽ� ���� ���� ��ü

    //�ֽ� ���� �ڵ� �迭
    List<string> symbolList = new List<string>() { "MSFT" }; //"GOOGL", "SBUX", "PYPL" };
    //List<string> symbolList = new List<string>() { "MSFT", "ORCL", "AAPL", "IBM", "GOOGL", "FB", "NFLX", "DIS", "AMZN", "TSLA", "SBUX", "NKE", "V", "PYPL", "BAC" };

    void Start()
    {
        LoadSlider.value = 0; //�����̴� �� ���� �ʱ�ȭ

        apiCall();
        coroutine1 = gameLoading();
        StartCoroutine(coroutine1);
    }

    IEnumerator gameLoading()
    {
        AsyncOperation ingame = SceneManager.LoadSceneAsync("InGame");
        ingame.allowSceneActivation = false;

        //�ΰ��� �� �ε尡 �Ϸ���� �ʾ�����
        while (!ingame.isDone)
        {
            yield return null;

            //�ε��� 50% ä���
            float progress1 = Mathf.Clamp01(ingame.progress / .9f) * 5f; 
            LoadSlider.value = progress1;

            //�ε��� 50~100% 
            if (progress1 >= 5)
            {
                //�ֽ� ���� �ε� ���� ������ŭ ä���
                while (count < symbolList.Count)
                {
                    float progress2 = Mathf.Clamp01(count / symbolList.Count) * 5f;
                    LoadSlider.value = 5f + progress2;
                    yield return null;
                }
                ingame.allowSceneActivation = true; //InGame �� �ε�

                yield break;
            }
        }
    }

    async Task apiCall()
    {
        try
        {
            foreach (string symbol in symbolList)
            {
                await BeginNetwork(symbol);
            }
        }
        catch (Exception)
        {
            Debug.Log("API call error !");
        }
    }

    async Task BeginNetwork(string symbol)
    {

        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v2/get-summary?symbol=" + symbol + "&region=US"),
            Headers =
    {
        { "x-rapidapi-key", ""}, //input your yahoo finance api key
        { "x-rapidapi-host", "apidojo-yahoo-finance-v1.p.rapidapi.com" },
    },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            JObject obj = JObject.Parse(body);

            //current price
            string tmp_cp = (string)obj["financialData"]["currentPrice"]["raw"];
            float.TryParse(tmp_cp, out float cp);

            //dividened date
            string dd = (string)obj["calendarEvents"]["dividendDate"]["fmt"];

            //dividened rate
            string tmp_dr = (string)obj["summaryDetail"]["dividendRate"]["raw"];
            float.TryParse(tmp_dr, out float dr);

            //sector
            string sec = (string)obj["summaryProfile"]["sector"];

            //market cap
            string tmp_mc = (string)obj["price"]["marketCap"]["raw"];
            float.TryParse(tmp_mc, out float mc);

            //PER
            string tmp_per = (string)obj["summaryDetail"]["forwardPE"]["raw"];
            float.TryParse(tmp_per, out float per);

            //52 week change
            string tmp_wc = (string)obj["defaultKeyStatistics"]["52WeekChange"]["raw"];
            float.TryParse(tmp_wc, out float wc);

            //previous close
            string tmp_pc = (string)obj["price"]["regularMarketPreviousClose"]["raw"];
            float.TryParse(tmp_pc, out float pc);

            count++;

            //apiInfo ���� ������Ʈ
            if (stockList.apiInfo.ContainsKey(symbol))
            {
                stockList.apiInfo[symbol].api_marketprice = cp;
                stockList.apiInfo[symbol].api_divDate = dd;
                stockList.apiInfo[symbol].api_divRate = dr;
                stockList.apiInfo[symbol].api_sector = sec;
                stockList.apiInfo[symbol].api_marketcap = mc;
                stockList.apiInfo[symbol].api_per = per;
                stockList.apiInfo[symbol].api_52week = wc;
                stockList.apiInfo[symbol].api_preclose = pc;
            }
            else
            {
                stockList.add(symbol, cp, dd, dr, sec, mc, per, wc, pc);
            }
        };
    }
}