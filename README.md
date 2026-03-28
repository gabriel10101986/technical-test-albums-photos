# Technical Test - Albums & Photos

Solución técnica desarrollada con **ASP.NET Web API**, **ASP.NET Web Forms** y **SQL Server** para la gestión y visualización de **albums** y **photos**.

---

## Tecnologías utilizadas

- .NET Framework 4.8.1
- ASP.NET Web API
- ASP.NET Web Forms
- ADO.NET
- SQL Server Express
- IIS Express
- Visual Studio

---

## Estructura de la solución

- `TechnicalTest.AlbumsPhotos.Api`  
  Proyecto backend con la API REST.

- `TechnicalTest.AlbumsPhotos.Web`  
  Proyecto frontend en Web Forms.

- `TechnicalTest.AlbumsPhotos.DataBase`  
  Scripts SQL para creación de base de datos y carga de datos.

---

## Requisitos previos

- Visual Studio con soporte para **ASP.NET (.NET Framework)**
- SQL Server Express
- IIS Express
- .NET Framework 4.8.1

---

## Configuración de base de datos

1. Crear una base de datos SQL Server local.
2. Ejecutar los scripts ubicados en:

```text
TechnicalTest.AlbumsPhotos.DataBase/
```

### Scripts incluidos

- `01_CreateDatabase.sql`
- `02_SeedData.sql`

---

## Configuración de la API

En el proyecto `TechnicalTest.AlbumsPhotos.Api`, revisar la cadena de conexión en `Web.config`.

Ejemplo:

```xml
<connectionStrings>
  <add name="TechnicalTestDb"
       connectionString="Server=localhost\SQLEXPRESS;Database=TechnicalTestAlbumsPhotos;Trusted_Connection=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

---

## Configuración del proyecto Web

En el proyecto `TechnicalTest.AlbumsPhotos.Web`, revisar `Web.config` y configurar las URLs base:

```xml
<appSettings>
  <add key="InternalApiBaseUrl" value="https://localhost:44329/" />
  <add key="ExternalApiBaseUrl" value="https://jsonplaceholder.typicode.com/" />
</appSettings>
```

### Importante

La URL de la API interna puede variar según el puerto asignado por IIS Express.  
Si fuera necesario, actualizar el valor de `InternalApiBaseUrl`.

---

## Cómo ejecutar la solución

1. Abrir la solución `TechnicalTest.AlbumsPhotos.sln`.
2. Configurar **múltiples proyectos de inicio**:
   - `TechnicalTest.AlbumsPhotos.Api`
   - `TechnicalTest.AlbumsPhotos.Web`
3. Ejecutar la solución.
4. La API y el frontend se levantarán en puertos locales distintos.

---

## Funcionalidad implementada

### Backend API

#### Albums

- `GET /api/albums`
- `GET /api/albums/{id}`
- `GET /api/albums/search?title=valor`
- `POST /api/albums`
- `PUT /api/albums/{id}`
- `DELETE /api/albums/{id}`

#### Photos

- `GET /api/photos`
- `GET /api/photos/{id}`
- `GET /api/photos/search?albumId=1&title=valor`
- `POST /api/photos`
- `PUT /api/photos/{id}`
- `DELETE /api/photos/{id}`

---

## Frontend Web

El proyecto Web Forms implementa el flujo principal solicitado:

- Listado de albums
- Filtro por título
- Selección de fuente:
  - API interna
  - API externa
- Navegación a fotos del álbum seleccionado
- Visualización de fotos por álbum
- Soporte para fuente interna y externa

### Pantallas principales

#### Albums

Pantalla principal del sistema (`Default.aspx`), desde donde se puede:

- listar albums
- filtrar por título
- elegir fuente de datos
- navegar a la pantalla de fotos

#### Photos

Pantalla de detalle que muestra:

- el álbum seleccionado
- el título del álbum
- la fuente utilizada
- las fotos asociadas al álbum

---

## Fuentes de datos

### Interna

API propia respaldada por SQL Server.

### Externa

- Albums: `https://jsonplaceholder.typicode.com/albums`
- Photos: `https://jsonplaceholder.typicode.com/photos`

---

## Decisiones de implementación

- Se utilizó **ASP.NET Web API** para el backend.
- Se utilizó **ASP.NET Web Forms** para el frontend, de acuerdo al stack pedido.
- Se implementó persistencia con **SQL Server** usando **ADO.NET**.
- Se mantuvieron separadas la fuente interna y la externa desde el frontend.
- La pantalla principal del proyecto Web (`Default.aspx`) actúa como pantalla principal de albums.
- Se agregó una página inicial simple en la API para mostrar el estado del servicio y los endpoints disponibles.

---

## Notas

- Para las pruebas locales se utilizó IIS Express.
- En algunos casos, las imágenes externas pueden no estar disponibles. En ese escenario, la interfaz muestra un fallback visual.
- El puerto de la API interna puede variar según la configuración local de IIS Express.
- Si el frontend no logra consumir la API interna, revisar primero el valor de `InternalApiBaseUrl` en el `Web.config` del proyecto Web.

---

## Autor

Gabriel Martinez D'Ambrosio
