using System;
using Weather.Models;
using Windows.UI.Xaml.Controls;

namespace Weather.Core.Contracts
{
    public interface IInteractionService
    {
        EventHandler<WeatherDailyInfo> ItemClicked { get; set; }
    }
}
