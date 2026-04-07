# 🐞 The Bug Management Dashboard

A full-stack web application to track, manage, and resolve bugs efficiently. Built with Angular (frontend) and .NET (backend), this dashboard helps teams monitor bug status, prioritize issues, and improve development workflow.

---

## 🚀 Features

* 🔐 User Authentication (Login / Signup / Google Sign-In)
* 🐛 Create, update, and delete bugs
* 📊 Dashboard with bug analytics
* 🏷️ Bug status tracking (Open, In Progress, Closed)
* 👤 User profile management
* 🔄 Real-time updates (optional)
* 📧 Forgot Password functionality

---

## 🛠️ Tech Stack

### Frontend

* Angular
* TypeScript
* HTML / CSS

### Backend

* .NET Core Web API
* Entity Framework Core
* SQLite Database

### Deployment

* Render (Backend + Frontend via `wwwroot`)
* GitHub (Version Control)

---

## 📁 Project Structure

```
The-Bug-Management-Dashboard/
│
├── backend/              # .NET backend
│   ├── Controllers/
│   ├── Models/
│   ├── Data/
│   ├── wwwroot/         # Angular build files (dist)
│   └── Program.cs
│
├── frontend-app/        # Angular project
│   ├── src/
│   └── dist/
│
├── database/            # SQLite database (if included)
└── README.md
```

---

## ⚙️ Setup Instructions

### 🔹 1. Clone Repository

```
git clone https://github.com/YOUR_USERNAME/The-Bug-Management-Dashboard.git
cd The-Bug-Management-Dashboard
```

---

### 🔹 2. Run Frontend (Development)

```
cd frontend-app
npm install
ng serve
```

Open:

```
http://localhost:4200
```

---

### 🔹 3. Run Backend

```
cd backend
dotnet run
```

Open:

```
http://localhost:5000
```

---

## 🏗️ Build for Production

### 🔹 Build Angular

```
ng build --configuration production
```

Copy contents of:

```
dist/your-project-name/
```

Paste into:

```
backend/wwwroot/
```

---

### 🔹 Build .NET

```
dotnet publish -c Release -o publish
```

---

## 🌐 Deployment (Render)

1. Push code to GitHub
2. Go to Render → Create Web Service
3. Use:

**Build Command:**

```
dotnet publish -c Release -o out
```

**Start Command:**

```
dotnet out/YOUR_PROJECT_NAME.dll
```

---

## 🔑 Environment Configuration

Make sure to configure:

* Firebase Authentication (Google login)
* API URLs (if frontend & backend separated)

---

## 📸 Screenshots

*Add your project screenshots here*

---

## 🤝 Contributing

Contributions are welcome!
Feel free to fork this repo and submit pull requests.

---

## 📄 License

This project is licensed under the MIT License.

---

## 👨‍💻 Author

* Ranganath
* GitHub: https://github.com/Ranganath2525

---

## ⭐ Support

If you like this project, give it a ⭐ on GitHub!
