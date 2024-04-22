using CongestionTax.Api.Domain;

namespace CongestionTax.Api.UnitTests.Domain;

public class TollCalculatorTest
{
        [Theory]
        [InlineData("2013-01-01")]
        [InlineData("2013-01-06")]
        [InlineData("2013-03-29")]
        [InlineData("2013-04-01")]
        [InlineData("2013-05-01")]
        [InlineData("2013-05-09")]
        [InlineData("2013-05-19")]
        [InlineData("2013-06-06")]
        [InlineData("2013-06-22")]
        [InlineData("2013-11-02")]
        [InlineData("2013-12-25")]
        [InlineData("2013-12-26")]
        public void IsTollFreeDate_PublicHoliday_ReturnsTrue(string dateString)
        {
            // arrange
            var date = DateTime.Parse(dateString);
            var tollCalculator = new TollCalculator();

            // act
            var isDateToolFree = tollCalculator.IsTollFreeDate(date);

            // assert
            Assert.True(isDateToolFree);
        }

        [Theory]
        [InlineData("2013-01-01")] // fails, existing implementation only regards 2013
        [InlineData("2013-01-06")]
        [InlineData("2013-03-29")]
        [InlineData("2013-04-01")]
        [InlineData("2013-05-01")]
        [InlineData("2013-05-09")]
        [InlineData("2013-05-19")]
        [InlineData("2013-06-06")]
        [InlineData("2013-06-22")]
        [InlineData("2013-11-02")]
        [InlineData("2013-12-25")]
        [InlineData("2013-12-26")]
        public void IsTollFreeDate_DayBeforePublicHoliday_ReturnsTrue(string dateString)
        {
            // arrange
            var date = DateTime.Parse(dateString).AddDays(-1);
            var tollCalculator = new TollCalculator();

            // act
            var isDateToolFree = tollCalculator.IsTollFreeDate(date);

            // assert
            Assert.True(isDateToolFree);
        }

        [Theory]
        [InlineData("2013-07-01")]
        [InlineData("2013-07-12")]
        [InlineData("2013-07-31")]
        public void IsTollFreeDate_MonthOfJuly_ReturnsTrue(string dateString)
        {
            // arrange
            var date = DateTime.Parse(dateString);
            var tollCalculator = new TollCalculator();

            // act
            var isDateToolFree = tollCalculator.IsTollFreeDate(date);

            // assert
            Assert.True(isDateToolFree);
        }

        [Theory]
        [InlineData("2013-06-28")] // 30th is a Sunday
        [InlineData("2013-08-01")]
        public void IsTollFreeDate_DaysAroundMonthOfJuly_ReturnsFalse(string dateString)
        {
            // arrange
            var date = DateTime.Parse(dateString);
            var tollCalculator = new TollCalculator();

            // act
            var isDateToolFree = tollCalculator.IsTollFreeDate(date);

            // assert
            Assert.False(isDateToolFree);
        }

        [Theory]
        [InlineData("2013-02-09")]
        [InlineData("2013-02-10")]
        public void IsTollFreeDate_Weekend_ReturnsTrue(string dateString)
        {
            // arrange
            var date = DateTime.Parse(dateString);
            var tollCalculator = new TollCalculator();

            // act
            var isDateToolFree = tollCalculator.IsTollFreeDate(date);

            // assert
            Assert.True(isDateToolFree);
        }

        [Theory]
        [InlineData("2013-02-04")]
        [InlineData("2013-02-05")]
        [InlineData("2013-02-06")]
        [InlineData("2013-02-07")]
        [InlineData("2013-02-08")]
        public void IsTollFreeDate_NotWeekend_ReturnsFalse(string dateString)
        {
            // arrange
            var date = DateTime.Parse(dateString);
            var tollCalculator = new TollCalculator();

            // act
            var isDateToolFree = tollCalculator.IsTollFreeDate(date);

            // assert
            Assert.False(isDateToolFree);
        }
}