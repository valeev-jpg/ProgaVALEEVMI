using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace AutomSys.Items
{
    public class Automate
    {
        [DisplayName("Имя")]
        public string Name
        {
            get
            {
                return _node.Dispatcher.Invoke(() =>
                {
                    return _node.Name;
                });
            }
        }
        [DisplayName("Состояние")]
        public string Status { get; set; }

        public Image _node;

        private Label _automateLabel;

        private bool _isFixing = false;

        private List<string> _resources;

        private EventsController _eventsController;

        private int _maxItemsInAutomate = 20;
        [DisplayName("Количество, шт")]
        public int ResourcesCount { get => _resources.Count; }

        public static List<string> ListStore = new List<string> { "Пирожки(срок годности 3 дня)", "Торт-Муравейник(срок годности 3 дня)", "Яблочный пирог(срок годности 3 дня)", "Треугольники(срок годности 3 дня)", "Очпочмак(срок годности 3 дня)" };

        public static List<string> EventsList = new List<string> { "Отключилось электричество!", "Температура внутри автомата нарушена", "Сломалось устройство выдачи!", "Клиент сообщил о поломке автомата!" };


        public Automate(Image node, Label isEmptyLabel, string status, EventsController controller)
        {
            _node = node;
            _automateLabel = isEmptyLabel;
            Status = status;
            _resources = new List<string>();
            _eventsController = controller;
        }

        public Automate()
        {
        }

        public async void SetImageSource(ImageSource source)
        {
            await _node.Dispatcher.InvokeAsync(new Action(() => { _node.Source = source; }));
        }

        public void DestroyAutomate2()
        {
            SetImageSource(DefaultValues.RedMachineSource);
            this.Status = AutomateStatuses.Red;
        }

        public void DestroyAutomate(bool destroyedAll)
        {
            if (_isFixing) return;

            SetImageSource(DefaultValues.RedMachineSource);
            this.Status = AutomateStatuses.Red;
            if (!destroyedAll)
            {
                Random rnd = new Random((int)DateTime.Now.Ticks);
                _eventsController.AddEvent(Name, EventsList[rnd.Next(EventsList.Count)]);
            }
        }

        public async void FixAutomate()
        {
            if (_isFixing || Status == AutomateStatuses.Green) return;

            SetImageSource(DefaultValues.YellowMachineSource);
            this.Status = AutomateStatuses.Yellow;

            _isFixing = true;
            _eventsController.AddEvent(Name,"Починка началась!");
            await Task.Delay(2000);
            SetImageSource(DefaultValues.GreenMachineSource);
            this.Status = AutomateStatuses.Green;
            _isFixing = false;
            _eventsController.AddEvent(Name, "Починка завершена!");

        }

        public async void FixAutomate2()
        {
            SetImageSource(DefaultValues.GreenMachineSource);
            this.Status = AutomateStatuses.Green;
        }

        public bool CompareImageWithNode(Image img) => _node == img;

        public static Automate FindImageBySource(DataGrid grid, Image image)
        {
            foreach (var item in grid.ItemsSource)
            {
                if(item is Automate автомат && автомат.CompareImageWithNode(image))
                {
                    return автомат;
                }
            }

            return new Automate();
        }

        public void FillItems()
        {
            if(_resources.Count > _maxItemsInAutomate / 2) 
            {
                _eventsController.AddEvent(Name, "В автомате достаточно товара!");
                return; 
            }

            Random rnd = new Random((int)DateTime.Now.Ticks);
            var itemsToRestore = _maxItemsInAutomate - ResourcesCount;
            for (int i = 0; i < itemsToRestore; i++)
            {
                _resources.Add(ListStore.ElementAt(rnd.Next(ListStore.Count)));
            }

            SetLabelFilled();

            _eventsController.AddEvent(Name, "Пополнили до максимума!(Срок годности 3 дня)");
        }

        public void ResetItemCount()
        {
            if (_resources.Count > _maxItemsInAutomate / 10) // Обнуляем количество товара
            // Любая другая логика, связанная с обнулением количества товара
            {
                _resources.Clear();
            }

        }

        public bool SellRandom()
        {
            var isLastItem = false;
            if (_resources.Count == 0)
            {
                SetLabelEmpty();
            }

            if (_resources.Count == 0 || _isFixing || !(Status == AutomateStatuses.Green))
            {
                return false;
            }

            if (_resources.Count == 1)
            {
                isLastItem = true;
            }

            Random rnd = new Random((int)DateTime.Now.Ticks);

            var elToSell = _resources.ElementAt(rnd.Next(_resources.Count));

            _resources.Remove(elToSell);

            _eventsController.AddEvent(Name, $"Продано: {elToSell}");

            if (isLastItem)
            {
                _eventsController.AddEvent(Name, "Нехватка товара!");
            }

            return true;
        }

        public void SetLabelEmpty()
        {
            this._automateLabel.Dispatcher.Invoke(new Action(() =>
            {
                this._automateLabel.Content = "Пополните!";
                this._automateLabel.Foreground = Brushes.Red;
            }));
        }

        private void SetLabelFilled()
        {
            this._automateLabel.Dispatcher.Invoke((Action)(() =>
            {
                this._automateLabel.Content = "Готово";
                this._automateLabel.Foreground = Brushes.Green;
            }));
        }

    }
}
