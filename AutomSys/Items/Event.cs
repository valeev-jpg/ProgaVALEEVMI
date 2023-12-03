using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutomSys.Items
{
    public class Event
    {
        public Event(object sender) : this()
        {
            switch (sender)
            {
                case Button button:
                    Source = "" + button.Content;
                    break;
                case string str:
                    Source = str;
                    break;
                default:
                    Source = "Вызов бригады";
                    break;
            }
        }

        public Event()
        {
            currId++;
            Id = currId;
            Date = DateTime.Now.AddDays(MainWindow.adddays).ToString();
            Source = "Вызов бригады";
        }
        [DisplayName("Идентификатор")]
        public int Id { get; set; }
        [DisplayName("Источник")]
        public string Source { get; set; }
        [DisplayName("Событие")]
        public string Message { get; set; }
        [DisplayName("Дата")]
        public string Date { get; set; }

        public static int currId { get; set; } = 0;
    }
}
