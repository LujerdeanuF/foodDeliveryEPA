﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodDeliveryEPA.Models;
using System.Data.SqlClient;

namespace foodDeliveryEPA.Controllers
{
    public class AccountController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = "data source= DESKTOP-82JHAH6; database= EPAdb; integrated security = SSPI;";


        }
        [HttpPost]
        public ActionResult Verify(Account acc)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from foodaccounts where name='"+acc.Name+"' and password= '"+acc.Password+"'";
            dr = com.ExecuteReader();
            if(dr.Read())
            {
                con.Close();

                return View("Create");
            }
            else
            {
                con.Close();

                return View("Error");
            }

            
        }
    }
}