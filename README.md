# MVC Clásico - Gestión de Productos

## Descripción
Aplicación web MVC para la gestión de compra y venta de zapatillas.

## Configuración

### Base de Datos
La aplicación usa SQL Server LocalDB. La cadena de conexión está configurada en `appsettings.json`:

```json
{
  "ConnectionString": {
    "EcommerceDBConnection": "Server=(localdb)\\mssqllocaldb;Database=EcommerceDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### Migraciones
Para crear la base de datos, ejecuta:
```bash
dotnet ef database update
```

## Funcionalidades

### Crear Producto
1. Navega a `/Productos/Create`
2. Completa el formulario con:
   - Nombre (obligatorio)
   - Precio (obligatorio, mayor a 0)
   - Talle (obligatorio, entre 1 y 50)
   - Descripción (opcional)
   - Imagen (opcional)
3. Las imágenes se guardan en `wwwroot/public/` con nombres únicos

### Ver Productos
- Navega a `/Productos` para ver la lista de productos
- Las imágenes se muestran con un tamaño máximo de 100x100px
- Hover sobre las imágenes para efecto de zoom

## Estructura de Archivos

```
MVCClasico/
├── Controllers/
│   └── ProductosController.cs    # Controlador principal
├── Models/
│   └── Producto.cs              # Modelo de datos
├── Views/
│   └── Productos/
│       ├── Create.cshtml        # Formulario de creación
│       └── Index.cshtml         # Lista de productos
├── wwwroot/
│   ├── public/                  # Carpeta para imágenes
│   └── css/
│       └── site.css            # Estilos personalizados
└── Context/
    └── EcommerceDatabaseContext.cs  # Contexto de BD
```

## Tecnologías Utilizadas
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server LocalDB
- Bootstrap (para estilos)
- HTML5/CSS3

## Notas Importantes
- Las imágenes se guardan con nombres únicos usando GUID
- Solo se aceptan archivos de imagen (atributo `accept="image/*"`)
- La carpeta `wwwroot/public/` debe existir para el guardado de imágenes 