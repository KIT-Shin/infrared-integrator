from django.urls import path

from . import views

app_name = 'remote_con'

urlpatterns = [
    path('remote_con_list/', views.Remote_conListView.as_view(), name='remote_con_list'),
    #path('remote_con_create/', views.Remote_conCreateView.as_view(), name='remote_con_create'),
    path('remote_con_form/', views.Remote_conCreateView.as_view(), name='remote_con_form'),
    path('create_done/', views.create_done, name='create_done'),
    path('update/<int:pk>/', views.Remote_conUpdateView.as_view(), name='remote_con_update'),
    path('update_done/', views.update_done, name='update_done'),
    path('delete/<int:pk>/', views.Remote_conDeleteView.as_view(), name='remote_con_delete'),
    path('delete_done/', views.delete_done, name='delete_done')
    ]
