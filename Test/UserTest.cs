using System;
using JarvisNG.Models.Domain;
using Xunit;

namespace Test {
    public class UserTest {
        [Fact]
        public void TestAddBalance() {
            User user = new User(1, "name", 100F, false);
            user.AddBalance(100F);
            Assert.Equal(200F, user.Balance);
        }
        [Fact]
        public void TestAddBalanceException() {
            User user = new User(1, "name", 100F, false);

            Assert.Throws<ArgumentException>(() => user.AddBalance(-100F));
        }
        [Fact]
        public void TestSubBalance() {
            User user = new User(1, "name", 100F, false);
            user.SubtractBalance(100F);
            Assert.Equal(0F, user.Balance);
        }
        [Fact]
        public void TestSubBalanceException() {
            User user = new User(1, "name", 100F, false);

            Assert.Throws<ArgumentException>(() => user.SubtractBalance(-100F));
        }

        [Fact]
        public void TestSubBalanceNegativeException() {
            User user = new User(1, "name", 100F, false);

            Assert.Throws<ArgumentException>(() => user.SubtractBalance(-200F));
        }
    }
}
