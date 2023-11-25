using System;
using System.IO;
using Xunit;
using OrderBot;
using Microsoft.Data.Sqlite;

namespace OrderBot.tests
{
    public class OrderBotTest
    {
        public OrderBotTest()
        {
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        DELETE FROM orders
    ";
                commandUpdate.ExecuteNonQuery();

            }
        }

        [Fact]
        public void TestWelcome()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            Assert.Contains("Welcome", sInput);
        }
        [Fact]
        public void TestWelcomPerformance()
        {
            DateTime oStart = DateTime.Now;
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            DateTime oFinished = DateTime.Now;
            long nElapsed = (oFinished - oStart).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + nElapsed);
            Assert.True(nElapsed < 10000);
        }

        [Fact]
        public void TestCheckReservation()
        {
            Session oSession = new Session("12345");


            String welMessage = oSession.OnMessage("hello")[0];
            String transmission = oSession.OnMessage("1")[0]; // Sedan
            String reservation = oSession.OnMessage("1")[0]; // Automatic

            Assert.Contains("reservation", reservation);
        }

        [Fact]
        public void TestCheckRent()
        {
            Session oSession = new Session("12345");


            String welMessage = oSession.OnMessage("hello")[0];
            String transmission = oSession.OnMessage("1")[0]; // Sedan
            String reservation = oSession.OnMessage("1")[0]; // Automatic
            String rented = oSession.OnMessage("2")[0]; // Yes

            Assert.Contains("rented", rented);
        }


        [Fact]
        public void TestCompleteBooking()
        {
            Session oSession = new Session("12345");


            String welMessage = oSession.OnMessage("hello")[0];
            String transmission = oSession.OnMessage("1")[0]; // Sedan
            String reservation = oSession.OnMessage("1")[0]; // Automatic
            String rented = oSession.OnMessage("2")[0]; // Yes
            String canCancel = oSession.OnMessage("1")[0]; // Yes
            String output = oSession.OnMessage("2")[0]; // yes

            Assert.Contains("Completed", output);
        }

        [Fact]
        public void TestCompleteBookingPerfomance()
        {
            DateTime oStart = DateTime.Now;
            Session oSession = new Session("12345");
            String welMessage = oSession.OnMessage("hello")[0];
            String transmission = oSession.OnMessage("1")[0]; // Sedan
            String reservation = oSession.OnMessage("1")[0]; // Automatic
            String rented = oSession.OnMessage("2")[0]; // Yes
            String canCancel = oSession.OnMessage("1")[0]; // Yes
            String output = oSession.OnMessage("2")[0]; // yes

            //Assert.Contains("Completed", output);

            DateTime oFinished = DateTime.Now;
            long nElapsed = (oFinished - oStart).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + nElapsed);
            Assert.True(nElapsed < 20000);
        }


    }
}