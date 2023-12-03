using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomSys
{
    public static class AutomateStatuses
    {
        public static string Green { get; } = "Готов";
        public static string Yellow { get; } = "Ремонт";
        public static string Red { get; } = "Сломан";
    }
}
