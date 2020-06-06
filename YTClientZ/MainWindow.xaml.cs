using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YTClientZ.Content.WorkClases;

namespace YTClientZ
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Класс работы с браузером
        /// </summary>
        private BrowserWorkClass browserWork;

        /// <summary>
        /// Конструктор окна
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Инициализатор класса
        /// </summary>
        private void Init()
        {
            //Инициализируем класс работы с браузером
            browserWork = new BrowserWorkClass(MainPanel);
            //Добавляем обработчик события получения перевода
            browserWork.TranslateResult += BrowserWork_TranslateResult;
            //Добавляем обработчик события запроса перевода
            App.TranslateRequest += App_TranslateRequest;
        }

        /// <summary>
        /// Обработчик события запроса перевода
        /// </summary>
        /// <param name="request">Текст запроса</param>
        private void App_TranslateRequest(string request)
        {
            //Втыкаем в окно текст для перевода
            ToTranslateText.Text = request;
            //Вызываем запрос перевода
            browserWork.TranslateRequest(request);
        }

        /// <summary>
        /// Обработчик события получения перевода
        /// </summary>
        /// <param name="result">Текст перевода</param>
        private void BrowserWork_TranslateResult(string result)
        {
            //Втыкаем в окно переведёнынй текст
            TranslatedText.Text = result;
            //Закидываем текст в буфер обмена
            Clipboard.SetText(result);
            //Бибикаем, что всё ок
            SystemSounds.Beep.Play();
        }

    }
}
