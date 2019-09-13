from django import forms
from .models import Remote_con
class Remote_conForm(forms.ModelForm):
   """
   新規データ登録画面用のフォーム定義
   """
   class Meta:
       model = Remote_con
       fields =['machine_name', 'vertical_angle', 'horizontal_angle']#'infrared']
