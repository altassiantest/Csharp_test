# Patient Aliases — Blazor Server (.NET 8)

Faithful recreation of the Quanum **Patient Aliases** screen (Patient Summary Information module).

## Solution Layout
```
PatientAliases.sln
├── src/
│   └── PatientAliases.Web/                # Blazor Server app (.NET 8)
│       ├── Components/
│       │   ├── App.razor
│       │   ├── Routes.razor
│       │   ├── _Imports.razor
│       │   ├── Layout/MainLayout.razor
│       │   └── Pages/PatientAliases.razor # Main screen
│       ├── DTOs/                          # AliasDto, PatientDto, SaveAliasesRequest
│       ├── Services/                      # IPatientAliasesService + InMemory impl
│       ├── Validation/                    # AliasValidator
│       ├── ViewModels/                    # PatientAliasesViewModel (INotifyPropertyChanged)
│       ├── wwwroot/css/patient-aliases.css
│       ├── appsettings.json
│       ├── Program.cs
│       └── PatientAliases.Web.csproj
└── tests/
    └── PatientAliases.Tests/              # xUnit test skeletons
```

## Run
```bash
dotnet restore
dotnet run --project src/PatientAliases.Web
```
Browse to `https://localhost:5001/patient-aliases`.

## Screen Fidelity
| Spec Element | Implementation |
|---|---|
| Dark gray title bar `#6C6C6C` | `.pa-panel__header` |
| Readonly Patient Name (`Steinberg, Fred`) | `<input readonly>` with `#EEF1F4` bg |
| Green `+` add button | `.pa-icon-btn--add` (`#5CB85C`) |
| Aliases table (Last/First/Middle/Suffix/Actions) | `.pa-table` |
| Edit ✏️ / Delete 🗑 / Swap 🔀 icons | `.pa-icon-btn--edit / --delete / --swap` |
| Centered Save (green) / Cancel (orange) footer | `.pa-footer` + `.pa-btn--save / --cancel` |
| Seed row `Steinberg / Freddy / J / JR` | `InMemoryPatientAliasesService` |

## Testing
```bash
dotnet test
```
