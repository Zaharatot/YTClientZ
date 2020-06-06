using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace YTClientZ.Content.WorkClases
{
    /// <summary>
    /// Класс работы с браузером
    /// </summary>
    internal class BrowserWorkClass
    {
        /// <summary>
        /// Делегат события возврата результата перевода
        /// </summary>
        /// <param name="result">Результат перевода</param>
        public delegate void TranslateResultEventHandler(string result);
        /// <summary>
        /// Событие возврата результата перевода
        /// </summary>
        public event TranslateResultEventHandler TranslateResult;



        /// <summary>
        /// Адресч страницы переваода
        /// </summary>
        private const string YTURL = "https://translate.yandex.ru/?clid=2270456&win=425&lang=en-ru&text=";
        /// <summary>
        /// Скрипт запроса результата перевода
        /// </summary>
        private const string GET_RESULT_SCRIPT = "document.querySelector('span[data-complaint-type=fullTextTranslation]').innerText";

        /// <summary>
        /// Панель, для помещения браузера
        /// </summary>
        private StackPanel panel;
        /// <summary>
        /// Браузер для работы
        /// </summary>
        private WebBrowser browser;
        /// <summary>
        /// Таймер ожидания
        /// </summary>
        private Timer waitTimer;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="panel">Панель, для помещения браузера</param>
        public BrowserWorkClass(StackPanel panel)
        {
            //Проставляем переданные значения
            this.panel = panel;
            //Инициализируем класс
            Init();
        }

        /// <summary>
        /// Инициализатор класса
        /// </summary>
        private void Init()
        {
            InitBrowser();
            InitEvents();
            InitTimer();
        }

        /// <summary>
        /// Инициализируем таймер
        /// </summary>
        private void InitTimer()
        {
            //Инициализируем таймер
            waitTimer = new Timer(WaitScript, null, Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// Инициализируем браузер
        /// </summary>
        private void InitBrowser()
        {
            //Инициализируем контролл
            browser = new WebBrowser();
            //Проставляем свойства
            browser.Visibility = Visibility.Collapsed;
            //Добавляем его на панель
            panel.Children.Add(browser);
        }

        /// <summary>
        /// Инициализируем обработчики событий
        /// </summary>
        private void InitEvents()
        {
            //Добавляем обработчик событяи догрузки страницы
            browser.LoadCompleted += Browser_LoadCompleted;
        }

        /// <summary>
        /// Обработчик событяи догрузки страницы
        /// </summary>
        private void Browser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            //Запрещаем отображение ошибок
            DisableJsErrors.SetSilent(browser, true);
            //Запускаем скрипт ожидания получения перевода
            waitTimer.Change(100, Timeout.Infinite);
        }

        /// <summary>
        /// Скрипт ожидания появления перевода
        /// </summary>
        private void WaitScript(object e)
        {
            //Выполняем в UI потоке
            panel.Dispatcher.BeginInvoke(new Action(() =>
            {
                string result = "";
                try
                {
                    //Запрашиваем результат
                    result = (string)browser.InvokeScript("eval", GET_RESULT_SCRIPT);
                }
                catch { }
                //Если результат есть
                if ((result != null) && (result.Length != 0))
                    //ОВзвращаем результат
                    TranslateResult?.Invoke(result);
                //Если результата нету
                else
                    //Запускаем таймер ещё раз
                    waitTimer.Change(100, Timeout.Infinite);
            }));
        }


        /// <summary>
        /// Запрос на перевод строки
        /// </summary>
        /// <param name="text">Текст строки</param>
        public void TranslateRequest(string text)
        {
            //Сбрасываем работу таймера
            waitTimer.Change(Timeout.Infinite, Timeout.Infinite);
            //Если текст больше лимита
            if (text.Length > 1000)
                //Обрезаем его
                text = text.Substring(0, 1000);
            //Декодируем строку в параметр запроса
            string param = HttpUtility.UrlEncode(text);
            //Выполняем запрос
            browser.Source = new Uri(YTURL + param);
        }

        /// <summary>
        /// Финализатор класса
        /// </summary>
        ~BrowserWorkClass()
        {
            //Если таймер есть
            if(waitTimer != null)
                //Грохаем его
                waitTimer.Dispose();
        }
    }
}
