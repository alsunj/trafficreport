alsunj
Alex Šunjajev
alsunj@taltech.ee
222442IACB



dotnet aspnet-codegenerator controller -name ViolationTypeController     -actions -m  App.Domain.Violations.ViolationType       -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ViolationController        -actions -m  App.Domain.Violations.Violation        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name VehicleTypeController      -actions -m  App.Domain.Vehicles.VehicleType        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name VehicleController      -actions -m  App.Domain.Vehicles.Vehicle        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AdditionalVehicleController      -actions -m  App.Domain.Vehicles.AdditionalVehicle        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name EvidenceTypeController      -actions -m  App.Domain.Evidences.EvidenceType        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name EvidenceController      -actions -m  App.Domain.Evidences.Evidence       -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CommentController      -actions -m  App.Domain.Evidences.Comment        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name VehicleViolationController        -actions -m  App.Domain.Violations.VehicleViolation        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f




SUBJECT TO CHANGE:
--ViolationType
----Can't insert decimal from UI

--VehicleType 
----Fix EVehicleSize, cannot choose from UI,

--Comment
----Fix ParentcommentId









