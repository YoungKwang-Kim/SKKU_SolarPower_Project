// 파싱할 데이터 구조
using System.Collections.Generic;

[System.Serializable]
public class PowerDataInfoArray
{
    public List<PowerData> powerDataInfo;
}

[System.Serializable]
public class PowerData
{
    public double dayGelec; // 금일발전량
    public double accumGelec; // 누적발전량
    public int co2;
    public double dayPrdct; // 금일사용량
    public double hco2;
    public double cntuAccumPowerPrdct; // 누적사용량
}

[System.Serializable]
public class InstcapaData
{
    public double gelecInstcapa;
    public double heatInstcapa;
    public double heathInstcapa;
}