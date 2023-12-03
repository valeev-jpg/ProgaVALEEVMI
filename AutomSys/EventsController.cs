using AutomSys.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutomSys
{
    public class EventsController
    {
        private readonly DataGrid _eventGrid;

        public EventsController(DataGrid grid)
        {
            _eventGrid = grid;
        }

        public static List<Event> Events = new List<Event>();

        public void AddEvent(object source, string message)
        {
            Events.Add(new Event(source)
            {
                Message = message
            });

            _eventGrid.Items.Dispatcher.Invoke(() => _eventGrid.Items.Refresh());
        }

        public void AddEvent(string message)
        {
            Events.Add(new Event()
            {
                Message = message
            });

            _eventGrid.Items.Dispatcher.Invoke(() => _eventGrid.Items.Refresh());
        }
    }
}