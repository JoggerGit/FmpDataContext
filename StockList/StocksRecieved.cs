﻿using FmpDataContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FmpDataContext.StockList
{
    /// <summary>
    /// StocksRecieved
    /// </summary>
    public class StocksRecieved : StockListBase
    {
        protected StocksCleaned _stocksCleaned;

        /// <summary>
        /// StocksRecieved
        /// </summary>
        /// <param name="stockList"></param>
        /// <param name="years"></param>
        /// <param name="dates"></param>
        /// <param name="dataContext"></param>
        public StocksRecieved(List<Stock> stockList, List<string> years, List<string> dates, DataContext dataContext) : base(stockList, years, dates, dataContext)
        {
            var stocksCleanedList = new List<Stock>();

            stocksCleanedList = _stockList.Where(s => !string.IsNullOrWhiteSpace(s.Symbol)).ToList();
            stocksCleanedList = stocksCleanedList.Where(s => !string.IsNullOrWhiteSpace(s.Name)).ToList();
            stocksCleanedList = stocksCleanedList.Where(s => !string.IsNullOrWhiteSpace(s.Exchange)).ToList();
            stocksCleanedList = stocksCleanedList.Select(s => new Stock
            {
                Symbol = s.Symbol.Trim().ToUpper(),
                Name = s.Name.Trim(),
                Price = s.Price,
                Exchange = s.Exchange
            }).ToList();

            _stocksCleaned = new StocksCleaned(stocksCleanedList, years, dates, dataContext);
        }

        /// <summary>
        /// Recieved
        /// </summary>
        public List<Stock> Recieved
        {
            get
            {
                return _stockList;
            }
        }

        /// <summary>
        /// Cleaned
        /// </summary>
        public StocksCleaned Cleaned
        {
            get
            {
                return _stocksCleaned;
            }
        }
    }
}
