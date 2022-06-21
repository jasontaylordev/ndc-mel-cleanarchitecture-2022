using CaWorkshop.Application.TodoLists.Commands.CreateTodoList;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

namespace CaWorkshop.Application.UnitTests.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommandTests : TestFixture
{
    [Fact]
    public async Task Handle_ShouldPersistTodoList()
    {
        // Arrange
        var command = new CreateTodoListCommand
        {
            Title = "Bucket List"
        };

        var handler = new CreateTodoListCommandHandler(Context);

        // Act
        var id = await handler.Handle(command,
            CancellationToken.None);
        
        // Assert
        var entity = await Context.TodoLists
            .FirstAsync(tl => tl.Id == id);

        entity.Should().NotBeNull();
        entity.Title.Should().Be(command.Title);
    }
}
