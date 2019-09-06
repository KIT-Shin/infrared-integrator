#include <Servo.h>
#define SENSOR 6
#define LED 8

Servo s1, s2; 
int i, f, s, now1, now2, goal1,goal2;

void setup() {
  pinMode(SENSOR, INPUT);
  pinMode(LED, OUTPUT);
  s1.attach(2);
  s2.attach(3);
  now1 = 90;//横回転初期角度
  now2 = 90;//縦回転初期角度
  Serial.begin(9600);
  Serial.print(now1);
  Serial.print(" ");
  Serial.println(now2);
}

void input() {
   int data[128];
   while(digitalRead(SENSOR));//センサがHIGHの間なにもしない
   unsigned long t1=micros(),t2;//LOWになる時点までの時間を計る
   int i;
   for(i=0;i<128;i++){
    while(!digitalRead(SENSOR));//センサがLOWの間なにもしない
    t2=micros();//HIGHになる時点までの時間を計る
    data[i*2]=t2-t1;//LOW時間を配列dataの偶数に保存
    while(digitalRead(SENSOR)&&micros()-t2<=65535);//センサがHIGHかつ時間内の間なにもしない
    t1=micros();//LOWになる時点までの時間を計る
    if(t1-t2>65535)break;//時間内を超えたら終了
    data[i*2+1]=t1-t2;//HIGH時間を配列dataの奇数に保存
   }
   int len=i*2+1;//全体の長さlen
   Serial.write((char*)&len,2);//ポインタとアドレスでlenをchar型に変換し2Bで送信
   Serial.write((char*)data,(i*2+1)*2);//長さlen*2のdataの配列をchar型に変換し送信
}

void output() {
  int i, len, data[128],light;
  len = Serial.read()<<8|Serial.read();//シフト演算でlenの読み込み
  for(i=0;i<=len;i++){
    data[i] = Serial.read()<<8|Serial.read();//シフト演算でdataの読み込み
  }
  light = 0;
  for(i=0;i<=len;i++){
    light = !light;//0(LOW)1(HIGH)を反転
    digitalWrite(LED, light);
    delayMicroseconds(data[i]);//データの時間一時停止
  }
  digitalWrite(LED, 0);//最後はLOWにしておく
}

void loop() {
  if (Serial.available()>=2){
    f = Serial.read();
    s = Serial.read();
    if (f==-1 && s==0){
      input();//入力値が-1,0ならinput
      return;
    }else if (f==0 && s==-1){
      output();//入力値が0,-1ならoutput
      return;
    }
  }
  goal1 = f;
  goal2 = s;
  //横回転
  if (goal1 - now1 > 0){
    now1 = now1 + 1;
    s1.write(now1);//1度＋回転
  }else if(goal1 != now1){      
    now1 = now1 - 1;
    s1.write(now1);//1度ー回転
  }
  //縦回転
  if (goal2 - now2 > 0){
    now2 = now2 + 1;
    s2.write(now2);//1度＋回転
  }else if(goal2 != now2){
    now2 = now2 - 1;
    s2.write(now2);//1度ー回転
  }
  delay(20);//1度回転したら20m秒待つ
  Serial.print(now1);
  Serial.print(" ");
  Serial.println(now2); 
}
