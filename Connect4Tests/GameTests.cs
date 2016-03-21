using Microsoft.VisualStudio.TestTools.UnitTesting;
using Connect4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Tests
{
    [TestClass()]
    public class GameTests
    {
        public Player GetTestPlayer(Game g)
        {
            return new Human(0, g);
        }

        public Player GetTestRival(Game g)
        {
            return new Human(1, g);
        }

        [TestMethod()]
        public void SingleCounterWithLength1()
        {
            int expected = 1;

            var game = new Game(7, 6);
            var counter = game.Drop(GetTestPlayer(game), 2);
            int actual = counter.LineLength(game.Board);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LeftToRightLineWithLength2()
        {
            int expected = 2;

            var game = new Game(7, 6);
            var player = GetTestPlayer(game);

            var leftCounter = game.Drop(player, 2);
            game.Drop(player, 3);

            int actual = leftCounter.LineLength(game.Board);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LeftToRightLineWithLength4()
        {
            int expected = 4;

            var game = new Game(7, 6);
            var player = GetTestPlayer(game);

            var leftCounter = game.Drop(player, 2);
            game.Drop(player, 3);
            game.Drop(player, 4);
            game.Drop(player, 5);

            int actual = leftCounter.LineLength(game.Board);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TopToBottomLineWithLength2()
        {
            int expected = 2;

            var game = new Game(7, 6);
            var player = GetTestPlayer(game);

            //First drop goes to the bottom
            game.Drop(player, 2);

            //Second drop goes on top
            var topCounter =  game.Drop(player, 2);

            int actual = topCounter.LineLength(game.Board);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TopToBottomLineWithLength4()
        {
            int expected = 4;

            var game = new Game(7, 6);
            var player = GetTestPlayer(game);

            //First drops go to the bottom
            game.Drop(player, 2);
            game.Drop(player, 2);
            game.Drop(player, 2);

            //Last drop goes on top
            var topCounter = game.Drop(player, 2);

            int actual = topCounter.LineLength(game.Board);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ForwardSlashLineWithLength2()
        {
            // 234
            //  O
            // OX
            //

            var game = new Game(7, 6);
            var player = GetTestPlayer(game);
            var rival = GetTestRival(game);

            var leftCounter = game.Drop(player, 2);
            game.Drop(rival, 3);

            var topRightCounter = game.Drop(player, 3);

            //Check from [2] should return 1 because it can't scan upwards
            int scanFrom2 = leftCounter.LineLength(game.Board);
            Assert.AreEqual(1, scanFrom2);

            //Check from [3] should return 2 as it scans down-and-left
            int scanFrom3 = topRightCounter.LineLength(game.Board);
            Assert.AreEqual(2, scanFrom3);
        }

        [TestMethod()]
        public void BackSlashWithLength4()
        {
            // 1234567
            // O
            // XO
            // XXO
            // XXXO

            var game = new Game(7, 6);
            var player = GetTestPlayer(game);
            var rival = GetTestRival(game);

            game.Drop(rival, 1);
            game.Drop(rival, 1);
            game.Drop(rival, 1);

            game.Drop(rival, 2);
            game.Drop(rival, 2);

            game.Drop(rival, 3);

            var leftCounter = game.Drop(player, 1);
            game.Drop(player, 2);
            game.Drop(player, 3);
            game.Drop(player, 4);

            int expected = 4;
            int actual = leftCounter.LineLength(game.Board);
            Assert.AreEqual(expected, actual) ;
        }


        [TestMethod()]
        public void RivalCountersNotCountedInLine()
        {
            // 234
            // OXO
            //
            // Checking counter in [2] should return length = 1

            int expected = 1;

            var game = new Game(7, 6);
            var player = GetTestPlayer(game);
            var rival = GetTestRival(game);

            var leftCounter = game.Drop(player, 2);
            game.Drop(rival, 3);
            game.Drop(player, 4);

            int actual = leftCounter.LineLength(game.Board);

            Assert.AreEqual(expected, actual);
        }
    }
}