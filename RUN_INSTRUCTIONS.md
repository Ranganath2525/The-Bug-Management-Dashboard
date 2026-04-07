# Bug Management Dashboard - Run Instructions

Follow these steps to set up and run the Bug Management Dashboard.

## Prerequisites
- **.NET 8 SDK** installed
- **Node.js (v18+)** and **npm** installed
- **PostgreSQL** server running locally
- **Angular CLI** (`npm install -g @angular/cli`)

---

## Step 1: Database Setup
1. Open your PostgreSQL query tool (e.g., pgAdmin, psql).
2. Create a new database named `BugDashboard`.
3. Run the scripts located in the `/database` folder:
   - First, run `schema.sql` to create the table.
   - Then, run `seed.sql` to populate sample data.

---

## Step 2: Backend (.NET API)
1. Open a terminal in the `/backend` directory.
2. Update `appsettings.json` with your PostgreSQL username and password (default is `postgres`/`postgres`).
3. Run the following commands:
   ```bash
   dotnet restore
   dotnet run
   ```
   *The API should now be running at `http://localhost:5000` (check terminal for exact port).*

---

## Step 3: Frontend (Angular)
1. Open a new terminal in the `/frontend` directory.
2. Update `src/app/services/bug.service.ts` if your API port is different from `5000`.
3. Run the following commands:
   ```bash
   npm install
   npm start
   ```
   *The Dashboard will be available at `http://localhost:4200`.*

---

## Troubleshooting
- **CORS Errors**: Ensure the frontend URL in `backend/Program.cs` matches your Angular dev server (default `http://localhost:4200`).
- **Database Connection**: Verify your `DefaultConnection` string in `appsettings.json` is correct for your local PostgreSQL instance.

---
**Good luck with your internship assessment!**
