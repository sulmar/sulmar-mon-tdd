using FluentAssertions;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Api.IntegrationTests
{
    public class SuperHeavyWeight
    {
        public SuperHeavyWeight()
        {
            Thread.Sleep(TimeSpan.FromSeconds(3));
        }

        public int Add(int x, int y)
        {
            return x + y;
        }

        public int Multiply(int x, int y)
        {
            return x * y;
        }
    }


    public class SuperHeavyWeightTests
    {
        private SuperHeavyWeight sut;

        public SuperHeavyWeightTests()
        {
            sut = new SuperHeavyWeight();
        }


        [Fact]
        public void Add_WhenCalled_ShouldSum()
        {
            // Act
            var result = sut.Add(1, 2);

            // Asserts
            result.Should().Be(3);
        }


        [Fact]
        public void Multiply_WhenCalled_ShouldMultiply()
        {
            // Act
            var result = sut.Multiply(1, 2);

            // Asserts
            result.Should().Be(2);
        }
    }


    public class SuperHeavyWeightFixture
    {
        public SuperHeavyWeight SUT { get; private set; }

        public SuperHeavyWeightFixture()
        {
            SUT = new SuperHeavyWeight();
        }

    
    }

    public class SuperHeavyWeightTestsClassFixture : IClassFixture<SuperHeavyWeightFixture>
    {
        private SuperHeavyWeightFixture fixture;

        public SuperHeavyWeightTestsClassFixture(SuperHeavyWeightFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Add_WhenCalled_ShouldSum()
        {
            // Act
            var result = fixture.SUT.Add(1, 2);

            // Asserts
            result.Should().Be(3);
        }


        [Fact]
        public void Multiply_WhenCalled_ShouldMultiply()
        {
            // Act
            var result = fixture.SUT.Multiply(1, 2);

            // Asserts
            result.Should().Be(2);
        }
    }
}
