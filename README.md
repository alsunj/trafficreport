alsunj
Alex Šunjajev
alsunj@taltech.ee
222442IACB

dotnet ef migrations   --project App.DAL.EF --startup-project TrafficReports add trafficreport4
dotnet ef database  --project App.DAL.EF --startup-project TrafficReports update

cd TrafficReports 
dotnet aspnet-codegenerator controller -name ViolationTypeController     -actions -m  App.Domain.Violations.ViolationType       -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name ViolationController        -actions -m  App.Domain.Violations.Violation        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name VehicleTypeController      -actions -m  App.Domain.Vehicles.VehicleType        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name VehicleController      -actions -m  App.Domain.Vehicles.Vehicle        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name AdditionalVehicleController      -actions -m  App.Domain.Vehicles.AdditionalVehicle        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name EvidenceTypeController      -actions -m  App.Domain.Evidences.EvidenceType        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name EvidenceController      -actions -m  App.Domain.Evidences.Evidence       -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name CommentController      -actions -m  App.Domain.Evidences.Comment        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name AppUserController        -actions -m  App.Domain.Identity.AppUser      -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name VehicleViolationController        -actions -m  App.Domain.Violations.VehicleViolation        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

identity

dotnet aspnet-codegenerator controller identity -f --userClass=App.Domain.Identity.AppUser -gl

area

dotnet aspnet-codegenerator controller -name AppUserController        -actions -m  App.Domain.Identity.AppUser      -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name ViolationTypeController     -actions -m  App.Domain.Violations.ViolationType -dc AppDbContext -outDir Areas\Admin\Controllers  --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name ViolationController     -actions -m  App.Domain.Violations.Violation -dc AppDbContext -outDir Areas\Admin\Controllers  --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name VehicleTypeController     -actions -m  App.Domain.Vehicles.VehicleType -dc AppDbContext -outDir Areas\Admin\Controllers  --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name VehicleController     -actions -m  App.Domain.Vehicles.Vehicle -dc AppDbContext -outDir Areas\Admin\Controllers  --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f


ApiControllers

dotnet aspnet-codegenerator controller -name ViolationTypeController   -m  App.Domain.Violations.ViolationType       -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name ViolationController   -m  App.Domain.Violations.Violation     -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name VehicleViolationController   -m  App.Domain.Violations.VehicleViolation       -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name VehicleViolationController   -m  App.Domain.Violations.VehicleViolation       -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f


dotnet aspnet-codegenerator controller -name VehicleTypeController   -m  App.Domain.Vehicles.VehicleType       -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name VehicleController   -m  App.Domain.Vehicles.Vehicle       -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name AdditionalVehicleController   -m  App.Domain.Vehicles.AdditionalVehicle       -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f


dotnet aspnet-codegenerator controller -name CommentController   -m  App.Domain.Evidences.Comment       -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name EvidenceTypeController   -m  App.Domain.Evidences.EvidenceType      -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

dotnet aspnet-codegenerator controller -name EvidenceController   -m  App.Domain.Evidences.Evidence     -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f














SUBJECTS TO CHANGE:

--Comment
----Fix ParentcommentId









