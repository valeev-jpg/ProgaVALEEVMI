using AutomSys.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace AutomSys
{
    public class SimulateController
    {
        public static bool onSimulate = false;
        private static int _minTimeBeforeNextEvent = 7;
        private static int _maxTimeBeforeNextEvent = 15;

        public async static void Simulate(List<Automate> automates, EventsController eventsController)
        {
            if (onSimulate) return;

            Random rnd = new Random((int)DateTime.Today.Ticks);
            onSimulate = true;

            var count = automates.Count();
            
            Task.Run(async () =>
            {
                while (onSimulate)
                {
                    await Task.Delay(rnd.Next(_minTimeBeforeNextEvent, _maxTimeBeforeNextEvent) * 1000);

                    var rndIndex = rnd.Next(0, 4);
                    if (onSimulate && rndIndex == 0)
                    {
                        automates.ForEach(automate => automate.DestroyAutomate(true));
                        eventsController.AddEvent("Система", "Авария в энергосистеме! Устройста были выключены.");
                        continue;
                    }

                    var automateIndx = rnd.Next(0, count);
                    if (onSimulate) {
                        automates[automateIndx].DestroyAutomate(false);
                    }
                }
            });

            Task.Run(async () =>
            {
                while (onSimulate)
                {
                    await Task.Delay(1000);

                    var automateIndx = rnd.Next(0, count);
                    if (onSimulate)
                        automates[automateIndx].SellRandom();
                }
            });
        }
        public static void StopSimulation()
        {
            if (!onSimulate) return;
            onSimulate = false;
        }
    }
}
