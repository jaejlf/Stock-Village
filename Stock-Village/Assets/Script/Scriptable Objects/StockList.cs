using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "StockList")]
public class StockList : ScriptableObject
{
    //�����ڵ�� ���� api �ڷḦ ������ ��ųʸ�
    public Dictionary<string, APIData> apiInfo = new Dictionary<string, APIData>();
    public class APIData //API���� �ҷ��� ������ return �ϱ� ���� Ŭ����
    {
        public double api_marketprice; //���� �ð�
        public string api_divDate; //�����
        public double api_divRate; //����
        public string api_sector; //���� ������ �з�
        public double api_marketcap; //�ð��Ѿ�
        public double api_per; //PER
        public double api_52week; //�ð� ���� ��ȭ(52 week change)
        public double api_preclose; //���� ������
        public double api_volume; //�ŷ���
        public double api_avgVolume; //��� �ŷ���(10days)

        public APIData(double api_marketprice, string api_divDate, double api_divRate, string api_sector,
            double api_marketcap, double api_per, double api_52week, double api_preclose, double api_volume, double api_avgVolume)
        {
            this.api_marketprice = api_marketprice;
            this.api_divDate = api_divDate;
            this.api_divRate = api_divRate;
            this.api_sector = api_sector;
            this.api_marketcap = api_marketcap;
            this.api_per = api_per;
            this.api_52week = api_52week;
            this.api_preclose = api_preclose;
            this.api_volume = api_volume;
            this.api_avgVolume = api_avgVolume;
        }
    }
    public void add(string code, double send_price, string send_divdate, double send_divrate, string send_sector,
        double send_marketcap, double send_per, double send_52, double send_preclose, double send_volume, double send_avgVolume)
    {
        apiInfo.Add(code, new APIData(send_price, send_divdate, send_divrate, send_sector, send_marketcap, send_per, send_52, send_preclose, send_volume, send_avgVolume));
    }
}
