# 📦 Sistema Distribuido de Gestión de Pedidos  
## Arquitectura Políglota con RabbitMQ, Python, Node.js y C#

Este repositorio contiene un sistema distribuido y desacoplado para la gestión y procesamiento de pedidos en tiempo real. La solución implementa una arquitectura **políglota**, integrando múltiples lenguajes de programación mediante un bróker de mensajería en la nube.

---

# 🗺️ Arquitectura del Sistema

El flujo de datos funciona de la siguiente manera:

1. **Publicador (C#)**  
   Envía un objeto JSON con la información del pedido hacia un *Exchange* de RabbitMQ.

2. **Bróker de Mensajería (CloudAMQP)**  
   El *Exchange* recibe el mensaje y, mediante una **Routing Key**, lo replica y distribuye en paralelo a dos colas independientes.

3. **Suscriptor de Base de Datos (Python)**  
   Consume los mensajes desde su cola dedicada e inserta la información en SQL Server, almacenando cada campo en su respectiva columna.

4. **Suscriptor Gráfico (Node.js)**  
   Consume los mensajes desde otra cola dedicada, actualiza una interfaz web en tiempo real utilizando WebSockets y realiza un respaldo local en un archivo de texto.

---

# 🗄️ Configuración de la Base de Datos (SQL Server)

Para que el suscriptor desarrollado en Python pueda almacenar los pedidos correctamente, realiza la siguiente configuración en tu máquina.

## 1️⃣ Habilitar el protocolo TCP/IP

1. Abre **SQL Server Configuration Manager**.
2. Ve a:

```text
SQL Server Network Configuration
→ Protocols for SQLEXPRESS
```

> *(O el nombre de tu instancia de SQL Server).*

3. Haz clic derecho sobre **TCP/IP** y selecciona **Enable**.
4. Abre las propiedades de **TCP/IP** y entra a la pestaña **IP Addresses**.
5. Desplázate hasta la sección **IPAll**:
   - Limpia el campo **TCP Dynamic Ports**
   - En **TCP Port** escribe:

```text
1433
```

6. Ve a:

```text
SQL Server Services
```

7. Reinicia el servicio de tu instancia de SQL Server.

---

## 2️⃣ Crear la base de datos y la tabla

Abre **SQL Server Management Studio (SSMS)**, crea una nueva consulta (*New Query*) y ejecuta el siguiente script:

```sql
CREATE DATABASE sistema_pedidos;
GO

USE sistema_pedidos;
GO

CREATE TABLE pedidos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    sku VARCHAR(50) NOT NULL,
    cantidad INT NOT NULL,
    talla VARCHAR(10) NOT NULL,
    color VARCHAR(20) NOT NULL,
    fecha_registro DATETIME DEFAULT GETDATE()
);
GO
```

---

# ⚙️ Instalación de Dependencias

## 🐍 Python

Instala las dependencias del suscriptor de Python con:

```bash
pip install -r requirements.txt
```

---

## 🟢 Node.js

Instala las dependencias del suscriptor web con:

```bash
npm install
```