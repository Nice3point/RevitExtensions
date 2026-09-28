using Autodesk.Revit.UI;
using Nice3point.Revit.Extensions.Tests.Abstractions;
using Nice3point.Revit.Extensions.Tests.Commands;
using Nice3point.Revit.Extensions.UI;
using UIFramework;
using UIFrameworkServices;

namespace Nice3point.Revit.Extensions.Tests;

/// <summary>
///     Captures the shortcut changes instead of applying them, which keeps the keyboard shortcuts of the user intact.
/// </summary>
public sealed class RibbonShortcutsTests : RibbonUiTest
{
    private object? _applyShortcutChanges;

    private List<AppliedShortcuts> AppliedChanges => field ??= [];

    [Before(Test)]
    public void CaptureShortcutChanges()
    {
        _applyShortcutChanges = RibbonExtensions.ApplyShortcutChanges;
        RibbonExtensions.ApplyShortcutChanges = changes => AppliedChanges.AddRange(changes.Select(change => new AppliedShortcuts(change.Key, change.Value.Shortcuts.ToList())));
    }

    [After(Test)]
    public void RestoreShortcutChanges()
    {
        RibbonExtensions.FlushShortcuts();
        RibbonExtensions.ApplyShortcutChanges = (Action<Dictionary<string, ShortcutItem>>)_applyShortcutChanges!;
    }

    [Test]
    [Arguments("")]
    [Arguments("#")]
    [Arguments("##")]
    public async Task TryAddShortcuts_NoShortcuts_ReturnsFalse(string representation)
    {
        // Arrange
        var button = CreateTestPanel().AddPushButton<EmptyCommand>("Run");

        // Act
        var added = button.TryAddShortcuts(representation);

        // Assert
        await Assert.That(added).IsFalse();
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task TryAddShortcuts_ShortcutOfAnotherCommand_ReturnsFalse(bool lowerCase)
    {
        // Arrange
        var button = CreateTestPanel().AddPushButton<EmptyCommand>("Run");
        var usedShortcut = GetUsedShortcut();

        // Act
        var added = button.TryAddShortcuts(lowerCase ? usedShortcut.ToLowerInvariant() : usedShortcut);
        RibbonExtensions.FlushShortcuts();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(added).IsFalse();
            await Assert.That(AppliedChanges).IsEmpty();
        }
    }

    [Test]
    public async Task TryAddShortcuts_CollectionOfUsedShortcuts_ReturnsFalse()
    {
        // Arrange
        var button = CreateTestPanel().AddPushButton<EmptyCommand>("Run");
        var usedShortcut = GetUsedShortcut();

        // Act
        var added = button.TryAddShortcuts(usedShortcut, usedShortcut.ToLowerInvariant());

        // Assert
        await Assert.That(added).IsFalse();
    }

    [Test]
    public async Task AddShortcuts_NewButton_AppliesTheShortcutsToTheButtonCommand()
    {
        // Arrange
        var panel = CreateTestPanel();
        var button = panel.AddPushButton<EmptyCommand>("Run");
        var shortcuts = GetFreeShortcuts(2);
        var representation = string.Join("#", shortcuts);

        // Act
        button.AddShortcuts(representation);
        RibbonExtensions.FlushShortcuts();

        // Assert
        var appliedShortcuts = GetAppliedShortcuts(panel, "Run");
        await Assert.That(appliedShortcuts).IsEquivalentTo(shortcuts);
    }

    [Test]
    public async Task AddShortcuts_ShortcutCollection_AppliesEveryShortcut()
    {
        // Arrange
        var panel = CreateTestPanel();
        var button = panel.AddPushButton<EmptyCommand>("Run");
        var shortcuts = GetFreeShortcuts(3);

        // Act
        button.AddShortcuts(shortcuts);
        RibbonExtensions.FlushShortcuts();

        // Assert
        var appliedShortcuts = GetAppliedShortcuts(panel, "Run");
        await Assert.That(appliedShortcuts).IsEquivalentTo(shortcuts);
    }

    [Test]
    public async Task TryAddShortcuts_FreeShortcut_AppliesTheShortcut()
    {
        // Arrange
        var panel = CreateTestPanel();
        var button = panel.AddPushButton<EmptyCommand>("Run");
        var shortcut = GetFreeShortcuts(1)[0];

        // Act
        var added = button.TryAddShortcuts(shortcut);
        RibbonExtensions.FlushShortcuts();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(added).IsTrue();
            await Assert.That(GetAppliedShortcuts(panel, "Run")).Contains(shortcut);
        }
    }

    [Test]
    public async Task TryAddShortcuts_UsedAndFreeShortcuts_AppliesOnlyTheFreeShortcut()
    {
        // Arrange
        var panel = CreateTestPanel();
        var button = panel.AddPushButton<EmptyCommand>("Run");
        var usedShortcut = GetUsedShortcut();
        var freeShortcut = GetFreeShortcuts(1)[0];

        // Act
        var added = button.TryAddShortcuts(usedShortcut, freeShortcut);
        RibbonExtensions.FlushShortcuts();

        // Assert
        var appliedShortcuts = GetAppliedShortcuts(panel, "Run");
        using (Assert.Multiple())
        {
            await Assert.That(added).IsTrue();
            await Assert.That(appliedShortcuts).Contains(freeShortcut);
            await Assert.That(appliedShortcuts).DoesNotContain(usedShortcut);
        }
    }

    [Test]
    public async Task TryAddShortcuts_FreeShortcutForTwoButtons_AppliesTheShortcutToTheFirstButton()
    {
        // Arrange
        var panel = CreateTestPanel();
        var firstButton = panel.AddPushButton<EmptyCommand>("Run");
        var secondButton = panel.AddPullDownButton("Tools").AddPushButton<AnotherEmptyCommand>("Run again");
        var shortcut = GetFreeShortcuts(1)[0];

        // Act
        var firstAdded = firstButton.TryAddShortcuts(shortcut);
        var secondAdded = secondButton.TryAddShortcuts(shortcut);
        RibbonExtensions.FlushShortcuts();

        // Assert

        using (Assert.Multiple())
        {
            await Assert.That(firstAdded).IsTrue();
            await Assert.That(secondAdded).IsFalse();
            await Assert.That(AppliedChanges.Count(item => item.Shortcuts.Contains(shortcut))).IsEqualTo(1);
        }
    }

    [Test]
    public async Task TryAddShortcuts_ShortcutQueuedByAddShortcuts_ReturnsFalse()
    {
        // Arrange
        var panel = CreateTestPanel();
        var firstButton = panel.AddPushButton<EmptyCommand>("Run");
        var secondButton = panel.AddPullDownButton("Tools").AddPushButton<AnotherEmptyCommand>("Run again");
        var shortcut = GetFreeShortcuts(1)[0];
        firstButton.AddShortcuts(shortcut);

        // Act
        var added = secondButton.TryAddShortcuts(shortcut);

        // Assert
        await Assert.That(added).IsFalse();
    }

    [Test]
    public async Task TryAddShortcuts_ShortcutTakenAfterRejectedCall_ReturnsFalse()
    {
        // Arrange
        var panel = CreateTestPanel();
        var button = panel.AddPushButton<EmptyCommand>("Run");
        var shortcut = GetFreeShortcuts(1)[0];
        button.TryAddShortcuts(GetUsedShortcut());

        var anotherCommand = ShortcutsHelper.Commands.Values.First(command => command.Shortcuts is not null);
        anotherCommand.Shortcuts.Add(shortcut);

        try
        {
            // Act
            var added = button.TryAddShortcuts(shortcut);
            RibbonExtensions.FlushShortcuts();

            // Assert
            await Assert.That(added).IsFalse();
        }
        finally
        {
            anotherCommand.Shortcuts.Remove(shortcut);
        }
    }

    [Test]
    public async Task TryAddShortcuts_ShortcutReleasedAfterRejectedCall_ReturnsTrue()
    {
        // Arrange
        var panel = CreateTestPanel();
        var button = panel.AddPushButton<EmptyCommand>("Run");
        var shortcut = GetFreeShortcuts(1)[0];

        var anotherCommand = ShortcutsHelper.Commands.Values.First(command => command.Shortcuts is not null);
        anotherCommand.Shortcuts.Add(shortcut);
        try
        {
            button.TryAddShortcuts(shortcut);
        }
        finally
        {
            anotherCommand.Shortcuts.Remove(shortcut);
        }

        // Act
        var added = button.TryAddShortcuts(shortcut);
        RibbonExtensions.FlushShortcuts();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(added).IsTrue();
            await Assert.That(GetAppliedShortcuts(panel, "Run")).Contains(shortcut);
        }
    }

    private List<string> GetAppliedShortcuts(RibbonPanel panel, string buttonText)
    {
        var itemId = FindRibbonPanel(panel.Name).Source.Items.Single(item => item.Text == buttonText).Id;
        return AppliedChanges.Single(change => change.ItemId == itemId).Shortcuts;
    }

    private sealed record AppliedShortcuts(string ItemId, List<string> Shortcuts);

    private static HashSet<string> GetUsedShortcuts()
    {
        if (ShortcutsHelper.Commands.Count == 0)
        {
            ShortcutsHelper.LoadCommands();
        }

        return ShortcutsHelper.Commands.Values
            .Where(command => command.Shortcuts is not null)
            .SelectMany(command => command.Shortcuts)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
    }

    private static string GetUsedShortcut()
    {
        return GetUsedShortcuts().First(shortcut => shortcut.Any(char.IsLetter));
    }

    private static List<string> GetFreeShortcuts(int count)
    {
        var usedShortcuts = GetUsedShortcuts();
        var letters = Enumerable.Range('A', 26).Select(letter => (char)letter).ToList();

        return letters
            .SelectMany(_ => letters, (second, third) => $"Z{second}{third}")
            .Where(shortcut => !usedShortcuts.Contains(shortcut))
            .OrderBy(_ => Guid.NewGuid())
            .Take(count)
            .ToList();
    }
}
