#include <Servo.h>
#define SENSOR 6
#define LED 8

Servo s1, s2;
int i, f, s, now1, now2, goal1, goal2;

void setup() {
  pinMode(SENSOR, INPUT);
  pinMode(LED, OUTPUT);
  pinMode(13, OUTPUT);
  s1.attach(2);
  s2.attach(3);
  now1 = 90;//横回転初期角度
  now2 = 90;//縦回転初期角度
  goal1 = 90;
  goal2 = 90;
  Serial.begin(38400);
  //  Serial.print(now1);
  //  Serial.print(" ");
  //  Serial.println(now2);
}

void input() {
  digitalWrite(13, 1);
  unsigned long t1, t2 = micros(), d;
  while (digitalRead(SENSOR) && micros() - t2 <= 10000000); //センサがHIGHの間なにもしない
  t1 = micros(); //LOWになる時点までの時間を計る
  if (t1 - t2 > 10000000) {
    Serial.write(0xff);
    Serial.write(0xff);
    return;
  }
  for (int i = 0; i < 128; i++) {
    while (!digitalRead(SENSOR)); //センサがLOWの間なにもしない
    t2 = micros(); //HIGHになる時点までの時間を計る
    d = t2 - t1;
    Serial.write((char*)&d, 2);
    while (digitalRead(SENSOR) && micros() - t2 <= 65535); //センサがHIGHかつ時間内の間なにもしない
    t1 = micros(); //LOWになる時点までの時間を計る
    if ((d = t1 - t2) > 65535)break; //時間内を超えたら終了
    Serial.write((char*)&d, 2);
  }
  digitalWrite(13, 0);
}

void output() {
  delay(100);
  int light = 1, dt;
  unsigned long t1, t2;
  t1 = micros();
  for (int i = 0; Serial.available() >= 2 ; i++, i %= 16) {
    digitalWrite(LED, light);
    light = !light;//0(LOW)1(HIGH)を反転
    dt = Serial.read() << 8 | Serial.read();
    if (i == 0)Serial.write(0);
    while ((t2 = micros()) < t1 + dt);
    t1 = t2;
    //    delayMicroseconds(data[i]);//データの時間一時停止
  }
  digitalWrite(LED, 0);//最後はLOWにしておく
  while (Serial.available() > 0){
    Serial.read();
    Serial.write(0);
    delay(1);
  }
}

void loop() {
  if (Serial.available() >= 2) {
    f = Serial.read();
    s = Serial.read();
    //    Serial.print(f);
    //    Serial.print(" ");
    //    Serial.println(s);
    if (f == 0xff && s == 0) {
      input();//入力値が-1,0ならinput
      return;
    } else if (f == 0 && s == 0xff) {
      output();//入力値が0,-1ならoutput
      return;
    }
    goal1 = constrain(f, 0, 180);
    goal2 = constrain(s, 0, 180);
  }
  //横回転
  if (goal1 - now1 > 0) {
    now1 = now1 + 1;
    s1.write(now1);//1度＋回転
  } else if (goal1 != now1) {
    now1 = now1 - 1;
    s1.write(now1);//1度ー回転
  }
  //縦回転
  if (goal2 - now2 > 0) {
    now2 = now2 + 1;
    s2.write(now2);//1度＋回転
  } else if (goal2 != now2) {
    now2 = now2 - 1;
    s2.write(now2);//1度ー回転
  }
  delay(20);//1度回転したら20m秒待つ
  //  Serial.print(now1);
  //  Serial.print(" ");
  //  Serial.println(now2);
}
