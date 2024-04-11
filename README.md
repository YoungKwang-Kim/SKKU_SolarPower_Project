# SKKU_Energy_Project

**시연영상** - https://youtu.be/0-Z_JaoFdKU
[![Video Label](http://img.youtube.com/vi/0-Z_JaoFdKU/0.jpg)](https://youtu.be/0-Z_JaoFdKU)

## 의도
+ 세계 각국이 탄소 중립에 힘을 기울임과 비례하여 신재생에너지도 많은 주목을 받으며 규모가 증가하기 시작하였습니다. 신재생 에너지에는 많은 종류가 있지만, 그 중 날이 갈수록 규모가 가파르게 상승하는 태양광발전소 분야를 선택하였습니다.
+ 전국의 발전량 현황과 전남 해남의 솔라시도 발전소를 모델로 프로그램을 제작하고자 합니다.
+ 추가로 태양광의 패널이 직사광선을 받는다면 더욱 발전량의 효율이 증가하지 않을까 하는 생각이 더하여져 해당 기능을 솔라시도의 패널에 적용하여 진행할 예정입니다.

## 컨셉
+ 전국현황판 - 전국 태양광 발전소 지역별 날짜 및 시간별 충전량
+ 솔라시도 태양광발전소 디지털트윈 - 각 구역별 충전량, 이벤트 발생시 알림기능, 드론을 활용한 점검 등
+ 패널의 태양추적기능 - 태양 위치에 따른 패널의 기울기 변경, 패널을 가장한 H/W의 모터 회전각도 동기화

* * *
## 대시보드씬

![image](https://github.com/YoungKwang-Kim/SKKU_SolarPower_Project/assets/54823568/3d00d73a-aa71-4dcc-a2ed-33e58d523c78)

### 기술스택
+ Unity
+ C#
+ Python
+ Firebase

### 🔵 프로그램 작동 순서

프로그램을 처음 시작하면 오늘의 날짜와 시간을 불러옵니다. 

**Firebase Realtime Database**에서 실시간 데이터를 불러오고 대시보드에 띄웁니다.

오른쪽 하단에 캘린더로 날짜를 선택하면 그날의 12시에 데이터를 불러옵니다.

그리고 슬라이더로 시간을 선택하며, 선택한 시간의 데이터를 불러옵니다.

### 🔵 웹크롤링

__신재생에너지__ __통합모니터링시스템__(REMS)에서 Python코드로 **금일발전량**을 크롤링 해서 데이터들을 수집하고 **Firebase RealtimeDatabase**에 "금일날짜/수집한 시간/지역"에 저장을 했습니다.

### 🔵 XCharts

주간 발전량과 태양광 발전량의 그래프들

**XCharts 패키지**를 사용해서 구현했습니다.

### 🔵 광역시 top3와 도별 top4

광역시의 이름과 발전량을 **Dictionary<string, double>**형태로 저장합니다.

**Enumerable.OrderByDescending 메서드**를 이용해서 Dictionary를 내림차순으로 정렬합니다.
**Dictionary**의 Key값을 0부터 2번 index까지 text로 보여줍니다.

* * *
## 솔라시도 씬
![image](https://github.com/YoungKwang-Kim/SKKU_SolarPower_Project/assets/54823568/5b0f0f20-7bf4-4a50-83bc-a99e53f5e420)


전라남도 해남에 위치한 솔라시도 태양광 발전소를 유니티로 구현을 했습니다.

### 🔵 태양고도 구현
![image](https://github.com/YoungKwang-Kim/SKKU_SolarPower_Project/assets/54823568/704ffac7-4127-4c4b-914f-aa6273944818)

공공데이터 포털에 공개된 **한국천문연구원_태양고도 정보**에서 태양의 고도값을 API로 xml형태로 호출을 했습니다.

캘린더를 선택하면 그 날의 남중고도 데이터를 가져와서 슬라이더의 중간 값인 12시에 넣어줍니다.

슬라이더를 조절하면 그 날의 태양의 위치값을 움직일 수 있도록 유니티로 구현을 했습니다.

### 🔵 적외선 카메라 구현
![image](https://github.com/YoungKwang-Kim/SKKU_SolarPower_Project/assets/54823568/66dc333a-9ce7-4cd6-a212-bcd76784b3c5)
![image](https://github.com/YoungKwang-Kim/SKKU_SolarPower_Project/assets/54823568/1322c686-4f86-4c97-a65b-832b36b51525)

### 🔵 드론 점검 가상 시뮬레이션

각 구역 별 발전량을 텍스트로 표시합니다.
![image](https://github.com/YoungKwang-Kim/SKKU_SolarPower_Project/assets/54823568/d5ed5c4b-a576-4ef8-9b2e-080c6888ab17)
