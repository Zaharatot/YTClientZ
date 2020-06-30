# YTClientZ
Небольшая программка, сделанная "На коленке".
По сути - является чем-то вроде бота для сервиса Yandex.Translate.
По нажатию комбинации клавиш (настраивается в файле App.xaml.cs), программа:
1. Получает текст из буфера обмена
2. Открывает страницу "Яндекс.Переводчика" (на встроенном скрытом браузере)
3. Вставляет в неё текст
4. Дожидается появления перевода
5. Вытягивает текст перевода со страницы
6. Закидывает полученный перевод обратно в буфер обмена
7. Бибикает для индикации
Операция занимает в районе 1-2 секнуд.

Я периодически, в качестве хобби балуюсь переводами разных комиксов - вот и решил немного автоматизировать работу.

Именна такая реализация выбрана потому, что Yandex перестал выдавать Free ключи для Api сервиса. 
А покупать пакет на милион символов за 15$ в месяц, ради использования 5-10% от него - бредово (на мой взгляд).

Программа выкладывается (по большей части) для того, чтобы банально не потерять её, ну и в целях демонстрации.
Тут реализована работа со стандартным браузером (со скрытым окном) - может кому и будет интересно.  
Хотя, для подобных задач лучше использовать что-то типа CefSharp, тут достаточно и встроенного.
И, да - реализовано всё крайне криво, т.к. делалось на "порыве вдохновения" под утро.