using Agenda.Domain.Entities;
using FluentAssertions;
using Test.CommonUtilities.Entities;

namespace Tests.Domain.Entities;

public class DayRestTest
{
    [Fact]
    public void Should_CreatingDayRestInstance()
    {
        var (_, dayOfWeek) = DayRestBuilder.Build();

        DayRest dayRest = new(dayOfWeek);

        dayRest.CreatedAt.Should().NotBeNull();
        dayRest.DayOnWeek.Should().Be(dayOfWeek);
    }

    [Fact]
    public void Should_UpdatingDayRestInstance()
    {
        var (id, dayOfWeek) = DayRestBuilder.Build();

        DayRest dayRest = new(dayOfWeek);

        const DayOfWeek newDayOnWeek = DayOfWeek.Monday;
        dayRest.Update(id, newDayOnWeek);
        
        dayRest.CreatedAt.Should().NotBeNull();
        dayRest.UpdatedAt.Should().NotBeNull();
        dayRest.Id.Should().Be(id);
        dayRest.DayOnWeek.Should().Be(newDayOnWeek);
        dayRest.DayOnWeek.Should().NotBe(dayOfWeek);
    }
}