# Group6_FinalProject – Inventory Management System (WinForms + ASP.NET Core API)

**Course:** Systems Integration and Architecture 2 Laboratory  
**Project Type:** Windows Forms Client + RESTful Web API  
**Database:** SQL Server LocalDB (via Entity Framework Core)

---

##  Overview

This project implements a **complete inventory management system** that integrates a **Windows Forms desktop client** with a **RESTful Web Service API** built in ASP.NET Core.  
It fulfills the laboratory requirements of creating a GUI-based application using database-backed CRUD operations extended with stock-in and stock-out functionalities — going beyond basic CRUD to demonstrate a **fully functional system**.

---

##  Features

###  Windows Forms Client
- Connects to the API via HTTP using `HttpClient`.
- Displays item list with category, supplier, price, and quantity.
- Search and filter by item name or brand.
- Supports CRUD operations for items.
- Stock transactions:
  - **Receive (In)** — increases quantity.
  - **Issue (Out)** — decreases quantity with validation.
- Uses dialogs for stock movements (`ReceiveDialog`, `IssueDialog`).
- Category and Supplier dropdowns dynamically populated from API.

###  ASP.NET Core Web API
- REST endpoints for:
  - **Items**
  - **Categories**
  - **Suppliers**
  - **Stock (In / Out)**
  - **Reports** – on-hand and low-stock summaries
- Entity Framework Core + SQL Server LocalDB
- Automatic migrations and seeding on startup
- Swagger UI for testing and documentation

###  Database Design
| Table | Description |
|--------|--------------|
| **Items** | Inventory items with category, supplier, price, quantity |
| **Categories** | Item categories |
| **Suppliers** | Supplier information |
| **StockLedgers** | Tracks stock movements (In / Out) |

---

## ⚙️ Technologies Used

| Layer | Technology |
|--------|-------------|
| Front-End | Windows Forms (.NET 8) |
| Back-End | ASP.NET Core Web API (.NET 8) |
| ORM | Entity Framework Core |
| Database | SQL Server LocalDB |
| Language | C# |

---

##  How to Run

### 1️⃣ Requirements
- Visual Studio 2022 (or newer)
- .NET 8 SDK
- SQL Server LocalDB (default with Visual Studio)

### 2️⃣ Run Steps
1. Open the solution in Visual Studio.
2. Right-click the **Solution** → **Set Startup Projects…**
3. Select **Multiple Startup Projects** and set both:
   - `Api` → *Start*
   - `Client.WinForms` → *Start*
4. Clean and rebuild if needed (`bin` / `obj` can be deleted first).
5. Run the solution:
   - API will launch Swagger at `http://localhost:5238/swagger`.
   - WinForms client opens automatically and connects to the same API.

### IMPORTANT IN RUNNING
- Database Setup (One-Time Only)
- This project uses Entity Framework Core with SQL Server LocalDB for database management.
- After cloning the repository, you need to create and seed the database once before running the application.

#### Steps to Set Up the Database

- Open a terminal or command prompt in the Api folder:

cd Api


- (Optional) Make sure EF Core tools are installed:

dotnet tool install --global dotnet-ef


- Apply the migrations to create the database:

dotnet ef database update


#### This will automatically:

- Create the database in your LocalDB instance

- Apply all necessary tables and relationships

- Seed sample data for testing (e.g., items, suppliers, categories)

####Run the API:

dotnet run


####Once it’s running, open your browser and go to:

http://localhost:5238/swagger


- You should see the Swagger documentation page if everything is working correctly.

- Run the WinForms application:

- Open the solution in Visual Studio.

- Set Client.WinForms as the Startup Project.

- Press F5 or click Start to launch the GUI.

- The application will automatically connect to the API at http://localhost:5238/swagger.

#### Notes

- You only need to run the migration once. After the database is created, the app will work immediately in future runs.

- If the database gets deleted or corrupted, just re-run:

dotnet ef database update


- The default connection string in Api/appsettings.json uses:

"Server=(localdb)\\MSSQLLocalDB;Database=Group6_FinalProject;Trusted_Connection=True;MultipleActiveResultSets=true"


- This works out-of-the-box on Windows with Visual Studio.

- If you change your SQL Server setup, update the connection string accordingly.

