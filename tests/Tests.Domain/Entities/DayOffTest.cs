using Agenda.Domain.Entities;
using FluentAssertions;
using Test.CommonUtilities.Entities;

namespace Tests.Domain.Entities;

public class DayOffTest
{
    [Fact]
    public void Should_CreatingDayRestInstance()
    {
        var (_, dayOfWeek) = DayRestBuilder.Build();

        DayOff dayOff = new(dayOfWeek);

        dayOff.CreatedAt.Should().NotBeNull();
        dayOff.DayOnWeek.Should().Be(dayOfWeek);
    }

    [Fact]
    public void Should_UpdatingDayRestInstance()
    {
        var (id, dayOfWeek) = DayRestBuilder.Build();

        DayOff dayOff = new(dayOfWeek);

        const DayOfWeek newDayOnWeek = DayOfWeek.Monday;
        dayOff.Update(id, newDayOnWeek);
        
        dayOff.CreatedAt.Should().NotBeNull();
        dayOff.UpdatedAt.Should().NotBeNull();
        dayOff.Id.Should().Be(id);
        dayOff.DayOnWeek.Should().Be(newDayOnWeek);
        dayOff.DayOnWeek.Should().NotBe(dayOfWeek);
    }
}