﻿//stock.cs
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace StockObj
{
    //-----------------------------------------------------------------------------------
    public class Stock
    {
        public event EventHandler<StockNotification> StockEvent;
        //Name of our stock.
        private string _name;
        //Starting value of the stock.
        private int _initialValue;
        //Max change of the stock that is possible.
        private int _maxChange;
        //Threshold value where we notify subscribers to the event.
        private int _threshold;
        //Amount of changes the stock goes through.
        private int _numChanges;
        //Current value of the stock.
        private int _currentValue;
        private readonly Thread _thread;
        public string StockName { get => _name; set => _name = value; }
        public int InitialValue;
        public int CurrentValue;
        public int MaxChange;
        public int Threshold;
        public int NumChanges;
        //-----------------------------------------------------------------------------
        /// <summary>
        /// Stock class that contains all the information and changes of the stock
        /// </summary>
        /// <param name="name">Stock name</param>
        /// <param name="startingValue">Starting stock value</param>
        /// <param name="maxChange">The max value change of the stock</param>
        /// <param name="threshold">The range for the stock</param>
        public Stock(string name, int startingValue, int maxChange, int threshold)
        {
            _name = name;
            _initialValue = startingValue;
            _currentValue = InitialValue;
            _maxChange = maxChange;
            _threshold = threshold;
            this._thread = new Thread(new ThreadStart(Activate));
             _thread.Start();
        }
        //-------------------------------------------------------------------------------
        /// <summary>
        /// Activates the threads synchronizations
        /// </summary>
        public void Activate()
        {
            for (int i = 0; i < 25; i++)
            {
                Thread.Sleep(500); // 1/2 second
                ChangeStockValue();
            }
        }
        //--------------------------------------------------------------------------------------
        // delegate
        //public delegate void StockNotification(String stockName, int currentValue, int numberChanges);
        // event
        //public event StockNotification ProcessComplete;
        //-------------------------------------------------------------------------------

        /// <summary>
        /// Changes the stock value and also raising the event of stock value changes
        /// </summary>
        public void ChangeStockValue()
        {
            var rand = new Random();
            CurrentValue += rand.Next(1, MaxChange);
            NumChanges++;
            if ((CurrentValue - InitialValue) > Threshold)
            { //RAISE THE EVENT
              // _____ Invoke _____________________________________________________
                StockEvent?.Invoke(this, new StockNotification("test",1,2));
            }
        }
    }
}
//---------------------------------------------------------------------------------------