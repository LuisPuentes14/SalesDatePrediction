# 📊 Sales Date Prediction

![.NET](https://img.shields.io/badge/.NET-8.0-blue) ![Node.js](https://img.shields.io/badge/Node.js-22.16.0-green) ![Angular](https://img.shields.io/badge/Angular-19.2.15-red) ![SQL Server](https://img.shields.io/badge/SQL%20Server-2019-blue)

Sistema para la **predicción de ventas** desarrollado con **.NET Core 8 (Back End)**, **Angular (Front End)** y **D3.js (visualización)**.

---

## 📌 Requisitos previos

Antes de comenzar, asegúrate de tener instalado:

* [SQL Server]
* [SDK .NET Core 8]
* [Node.js 22.16.0]
* [Angular CLI 19.2.15]
* [npm 10.9.2]

---

## 📂 Estructura del proyecto

```
SalesDatePrediction/
│── Date Base/                  # Scripts SQL de la base de datos
│── Back End/
│   └── SalesDatePrediction.API # Proyecto .NET Core 8 (API)
│── Front-End/                  # Proyecto Angular
│── D3/                         # Visualización en D3.js
```

---

## 🚀 Instrucciones de instalación y ejecución

### 1️⃣ Base de datos (SQL Server)

1. Ir a la carpeta **`Date Base/`**.
2. Ejecutar los scripts SQL en el orden numérico indicado.
3. Crear un usuario con permisos de **lectura y escritura** sobre los esquemas:

   * `HR`
   * `Production`
   * `Sales`

---

### 2️⃣ Back End (.NET Core 8)

1. Abrir la carpeta **`Back End/SalesDatePrediction.API/`**.
2. Configurar la conexión a la base de datos en el archivo **`appsettings.json`**.
3. Ejecutar el proyecto:

```bash
dotnet run
```

---

### 3️⃣ Front End (Angular)

1. Abrir la carpeta **`Front-End/`**.
2. Instalar dependencias:

```bash
npm install
```

3. Iniciar la aplicación Angular:

```bash
ng serve
```

La aplicación quedará disponible en:
👉 [http://localhost:4200](http://localhost:4200)

---

### 4️⃣ Visualización (D3.js)

1. Abrir la carpeta **`D3/`**.
2. Ejecutar el archivo **`index.html`** en tu navegador.

---

## ✅ Ejecución completa

1. Base de datos lista en SQL Server.
2. Back End ejecutándose en .NET Core 8.
3. Front End corriendo en Angular (`http://localhost:4200`).
4. Visualización con D3.js en navegador.


---

## 👨‍💻 Autor

Desarrollado por **Luis Alejandro Puentes Angel**
