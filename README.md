# GoogleServiceAuth

Reusable activities to support authentication to Google APIs using service account context.

Developed as a [custom Code activity for UiPath Studio](https://docs.uipath.com/developer/docs/creating-activities-with-code).
Steps:
- Install Visual Studio with Desktop development tools
	- Install .NET framework 4.6.1
- Create C# class library
	- Target framework options did not show 4.6.1. Created library then modified `.csproj` framework manually.
- Write activity logic following template from UiPath documentation
- Build solution in Visual Studio to create `.dll` file
- Create `.nupkg` using [Visual Studio and the Nuget CLI](https://learn.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-visual-studio-net-framework)
	- Update project properties to include "Activities" within the ID
	- Create `.nuspec` file using nuget CLI, then `.nupkg` file
- Load created `.nupkg` to Orchestrator or to local packages folder (`C:\Program Files\UiPath\Studio\Packages`)
- Install package from package manager

## Alternatives
Rather than using this custom C# CodeActivity, a raw access token can be generated using `gcloud` ([docs](https://cloud.google.com/sdk/gcloud/reference/auth/print-access-token)) via PowerShell or Command Line:

```shell
gcloud auth activate-service-account SERVICE_ACCOUNT@DOMAIN.COM --key-file=/path/key.json --project=PROJECT_ID
gcloud auth print-access-token --scopes=SCOPES
```

_NOTE: The `--scopes` flag for `print-access-token` is not currently listed in the public documentation (as of October 2023), but is supported_
