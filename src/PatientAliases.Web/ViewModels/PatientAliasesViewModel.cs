using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PatientAliases.Web.DTOs;

namespace PatientAliases.Web.ViewModels;

/// <summary>
/// ViewModel backing the Patient Aliases screen.
/// Holds the primary patient name and the editable collection of aliases.
/// </summary>
public sealed class PatientAliasesViewModel : INotifyPropertyChanged
{
    private string _patientName = string.Empty;
    private AliasDto? _selectedAlias;
    private bool _isBusy;

    /// <summary>Readonly patient display name shown at the top of the panel.</summary>
    public string PatientName
    {
        get => _patientName;
        set => SetField(ref _patientName, value);
    }

    /// <summary>Row currently being edited (null when no inline edit is active).</summary>
    public AliasDto? SelectedAlias
    {
        get => _selectedAlias;
        set => SetField(ref _selectedAlias, value);
    }

    /// <summary>True while a backend request is in flight.</summary>
    public bool IsBusy
    {
        get => _isBusy;
        set => SetField(ref _isBusy, value);
    }

    /// <summary>Observable collection of aliases bound to the grid.</summary>
    public ObservableCollection<AliasDto> Aliases { get; } = new();

    /// <summary>Patient identifier being edited.</summary>
    public Guid PatientId { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
