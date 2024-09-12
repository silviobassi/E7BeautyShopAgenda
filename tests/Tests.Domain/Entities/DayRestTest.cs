using Agenda.Domain.Entities;
using FluentAssertions;
using Test.CommonUtilities.Entities;

namespace Tests.Domain.Entities;

public class DayRestTest
{
    [Fact]
    public void Should_CreatingDayRestInstance()
    {
        var (_, dayOfWeek, agendaId) = DayRestBuilder.Build();

        DayRest dayRest = new(dayOfWeek, agendaId);

        dayRest.CreatedAt.Should().NotBeNull();
        dayRest.UpdatedAt.Should().NotBeNull();
        dayRest.DayOnWeek.Should().Be(dayOfWeek);
        dayRest.AgendaId.Should().Be(agendaId);
    }

    [Fact]
    public void Should_UpdatingDayRestInstance()
    {
        var (id, dayOfWeek, agendaId) = DayRestBuilder.Build();

        DayRest dayRest = new(dayOfWeek, agendaId);
        
        dayRest.CreatedAt.Should().NotBeNull();
        dayRest.UpdatedAt.Should().NotBeNull();
        dayRest.DayOnWeek.Should().Be(dayOfWeek);
        dayRest.AgendaId.Should().Be(agendaId);

        dayRest.Update(id, DayOfWeek.Monday, 4);
        
        dayRest.CreatedAt.Should().NotBeNull();
        dayRest.UpdatedAt.Should().NotBeNull();
        dayRest.Id.Should().Be(id);
        dayRest.DayOnWeek.Should().Be(DayOfWeek.Monday);
        dayRest.DayOnWeek.Should().NotBe(dayOfWeek);
        dayRest.AgendaId.Should().NotBe(agendaId);
    }
}