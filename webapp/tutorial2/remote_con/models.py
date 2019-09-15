from django.db import models

# Create your models here.
class Category(models.Model):
   class Meta:
       #テーブル名の指定
       db_table ="category"
       verbose_name ="カテゴリ"         #追加
       verbose_name_plural ="カテゴリ"  #追加

   #カラム名の定義
   category_name = models.CharField(max_length=255,blank=False,unique=True)

class Remote_con(models.Model):
   class Meta:
       #テーブル名
       db_table ="remote_con"
       verbose_name ="リモコン"
       verbose_name_plural ="リモコン"

   def __str__(self):
       return self.machine_name

   #カラムの定義
   machine_name = models.CharField(verbose_name="機器の名前", max_length=500)
   vertical_angle = models.IntegerField(verbose_name="縦の角度")
   horizontal_angle = models.IntegerField(verbose_name="横の角度")
   infrared = models.CharField(verbose_name="赤外線の値", max_length=500)
