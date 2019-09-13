from django.shortcuts import render
from . forms import Remote_conForm  #forms.pyからRemote_conFormをインポート
from django.urls import reverse_lazy

# Create your views here.
#ここから下を追加
from django.views.generic import CreateView, ListView, UpdateView, DeleteView
from .models import Category, Remote_con

#一覧表示用のDjango標準ビュー(ListView)を承継して一覧表示用のクラスを定義
class Remote_conListView(ListView):
   #利用するモデルを指定
   model = Remote_con
   #データを渡すテンプレートファイルを指定
   template_name = 'remote_con/remote_con_list.html'

   #家計簿テーブルの全データを取得するメソッドを定義
   def queryset(self):
       return Remote_con.objects.all()

class Remote_conCreateView(CreateView):
    #利用するモデルを指定
    model = Remote_con
    #利用するフォームクラス名を指定
    form_class = Remote_conForm
    #登録処理が正常終了した場合の遷移先を指定
    success_url = reverse_lazy('remote_con:create_done')

def create_done(request):
   #登録処理が正常終了した場合に呼ばれるテンプレートを指定
   return render(request, 'remote_con/create_done.html')

class Remote_conUpdateView(UpdateView):
   #利用するモデルを指定
   model = Remote_con
   #利用するフォームクラス名を指定
   form_class = Remote_conForm
   #登録処理が正常終了した場合の遷移先を指定
   success_url = reverse_lazy('remote_con:update_done')

def update_done(request):
    #更新処理が正常終了した場合に呼ばれるテンプレートを指定
    return render(request, 'remote_con/update_done.html')

class Remote_conDeleteView(DeleteView):
    #利用するモデルを指定
    model = Remote_con
    #削除処理が正常終了した場合の遷移先を指定
    success_url = reverse_lazy('remote_con:delete_done')

def delete_done(request):
    return render(request, 'remote_con/delete_done.html')
