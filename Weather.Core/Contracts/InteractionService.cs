using System;
using System.Collections.Generic;
using Weather.Models;
using Windows.UI.Xaml.Controls;

namespace Weather.Core.Contracts
{
    public class InteractionService : IInteractionService
    {
        public EventHandler<WeatherDailyInfo> ItemClicked { get; set; }
    }
}
