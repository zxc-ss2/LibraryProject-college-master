﻿using LibraryProject.Models;
using LibraryProject.Properties;
using StringCheckLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryProject.Controllers
{
   public class TradingController
    {
        readonly DbHelper dbHelper = new DbHelper();

        public List<trading> GetTradingInfo()
        {
            return dbHelper.context.trading.ToList();
        }

        //public List<trading> TradingInfoOutput()
        //{
        //    return dbHelper.context.trading.Where(x=>x.login == "UliyaBay").ToList();
        //}

        public bool AddNewTrading(int bookId, string ticket, DateTime deliveryDate, DateTime receptionDate, string userLogin)
        {
            try
            {
                StringCheck check = new StringCheck();
                if (bookId != 0 && check.CheckTradingTicket(ticket) && check.CheckDate(Convert.ToString(deliveryDate.ToString("yyyy.MM.dd"))) && check.CheckDate(Convert.ToString(receptionDate.ToString("yyyy.MM.dd"))) && check.CheckLogin(userLogin))
                {
                    dbHelper.context.trading.Add(new trading
                    {
                        book_id = bookId,
                        ticket = ticket,
                        delivery = deliveryDate,
                        reception = receptionDate,
                        login = userLogin
                    });

                    dbHelper.context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public string GetNeededTicket(int tradingId)
        {
            try
            {
                return dbHelper.context.trading.Where(t => t.trading_id == tradingId).First().ticket;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        public int GetNeededTradingId(int selectBook)
        {
            return dbHelper.context.trading.Where(t => t.book_id == selectBook).First().trading_id;
        }

        public List<int> GetBooksId()
        {
            List<int> tradingBooksId = new List<int>();

            foreach (var item in dbHelper.context.trading.ToList())
            {
                tradingBooksId.Add(item.book_id);
            }

            if(tradingBooksId != null)
            {
                return tradingBooksId;
            }
            else{
                return null;
            }
        }

        public List<trading> GetTradingString(int selectedBook)
        {
            return dbHelper.context.trading.Where(t => t.trading_id == selectedBook).ToList();
        } 

        public bool UpdateTradingInfo(int newBook_id, string newTicket, DateTime newDelivery, DateTime newReception, List<trading> oldBook)
        {
            try
            {
                StringCheck check = new StringCheck();
                if (newBook_id == 0 || !check.CheckTradingTicket(newTicket) || !check.CheckDate(Convert.ToString(newDelivery.ToString("yyyy.MM.dd"))) || !check.CheckDate(Convert.ToString(newReception.ToString("yyyy.MM.dd"))))
                {
                    return false;
                }

                else
                {
                    foreach (var item in oldBook)
                    {
                        item.book_id = newBook_id;
                        item.ticket = newTicket;
                        item.delivery = newDelivery;
                        item.reception = newReception;
                    }

                    dbHelper.context.SaveChanges();
                    if (dbHelper.context.SaveChanges() == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public bool RemoveTrading(int selectString)
        {
            try
            {
                if(selectString == 0)
                {
                    return false;
                }
                else
                {
                    var selectTrading = from search in dbHelper.context.trading
                                        where search.trading_id == selectString
                                        select search;

                    if (selectTrading != null)
                    {
                        foreach (var item in selectTrading)
                        {
                            dbHelper.context.trading.Remove(item);
                        }
                        dbHelper.context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false; 
            }

        }
    }
}
