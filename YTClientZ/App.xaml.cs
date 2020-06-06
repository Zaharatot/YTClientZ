using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using YTClientZ.Content.WorkClases;

namespace YTClientZ
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Делегат событяи запроса перевода
        /// </summary>
        /// <param name="request">Текст для перевода</param>
        public delegate void TranslateRequestEventHandler(string request);
        /// <summary>
        /// Событие запроса перевода
        /// </summary>
        public static event TranslateRequestEventHandler TranslateRequest;

        /// <summary>
        /// Класс хука клавиатуры
        /// </summary>
        private KeyboardHook kh;


        /// <summary>
        /// Обработчик события запуска приложения
        /// </summary>
        void App_Startup(object sender, StartupEventArgs e)
        {
            //Инициализируем хук
            kh = new KeyboardHook(true);
            //Добавляем обработчик глобального осбытия нажатия на кнопку мыши
            kh.KeyDown += Kh_KeyDown;
        }

        /// <summary>
        /// Обработчик глобального осбытия нажатия на кнопку мыши
        /// </summary>
        /// <param name="key">Нажатая кнопка</param>
        /// <param name="Shift">Статус нажатия клавиши Shift</param>
        /// <param name="Ctrl">Статус нажатия клавиши Ctrl</param>
        /// <param name="Alt">Статус нажатия клавиши Alt</param>
        private void Kh_KeyDown(System.Windows.Forms.Keys key, bool Shift, bool Ctrl, bool Alt)
        {
            //Если нажато сочетание Ctrl+Num0
            if (Ctrl && Shift && Alt && (key == System.Windows.Forms.Keys.H))
            {
                try
                {
                    //Получаем текст и отправляем в ивент
                    TranslateRequest?.Invoke(Clipboard.GetText());
                }
                catch { }
            }
        }




        /// <summary>
        /// Обработчик события завершения работы приложения
        /// </summary>
        void App_Exit(object sender, ExitEventArgs e)
        {
            //Очищаем хук
            if(kh != null)
                kh.Dispose();
        }
    }
}
