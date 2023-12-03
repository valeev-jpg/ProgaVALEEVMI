using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutomSys
{
    public static class DefaultValues
    {
        public static string GreenMachine { get; } = "pack://application:,,,/AutomSys;component/Images/green.png";
        public static string YellowMachine { get; } = "pack://application:,,,/AutomSys;component/Images/yellow.png";
        public static string RedMachine { get; } = "pack://application:,,,/AutomSys;component/Images/red.png";

        public static ImageSource GreenMachineSource { get; } = new ImageSourceConverter().ConvertFromString(DefaultValues.GreenMachine) as ImageSource;
        public static ImageSource YellowMachineSource { get; } = new ImageSourceConverter().ConvertFromString(DefaultValues.YellowMachine) as ImageSource;
        public static ImageSource RedMachineSource { get; } = new ImageSourceConverter().ConvertFromString(DefaultValues.RedMachine) as ImageSource;

    }
}
