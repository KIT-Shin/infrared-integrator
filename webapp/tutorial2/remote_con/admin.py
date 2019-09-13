from django.contrib import admin

# Register your models here.

from .models import Category, Remote_con

#追加
class Remote_conAdmin(admin.ModelAdmin):
    list_display=('machine_name','vertical_angle','horizontal_angle','infrared')

admin.site.register(Category)
admin.site.register(Remote_con, Remote_conAdmin)
