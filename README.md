**Group6_FinalProject**
The Client.WinForms project serves as the graphical user interface (GUI) for the Inventory Management System.
It connects directly to the ASP.NET Core Web API using HttpClient to perform full CRUD operations and manage stock movements.

Features
CRUD Operations — Add, update, and delete Items, Categories, and Suppliers
API Integration — Real-time sync with backend using RESTful endpoints
Stock Movements Tab — Adjust quantities through Purchase, Sale, or Adjustment transactions
Live Search Filter — Instantly filter items by name, code, or brand
Async Refresh — Data grids refresh dynamically without freezing the UI

How to Run
Open the solution in Visual Studio 2022 or later
Set multiple startup projects → select both Api and Client.WinForms to start
Press F5 to run
The API will launch in Swagger, and the WinForms client will open automatically

API Connection
The client communicates with the API endpoints via:
http://localhost:5238/api/
http://localhost:5238/swagger/

Technologies Used
C# (.NET 8)
Windows Forms (WinForms)
ASP.NET Core Web API
Entity Framework Core (via API)
SQL Server LocalDB
