// Función para cargar y mostrar clientes
async function loadClientes() {
    const response = await fetch('/odata/Clientes');
    const clientes = await response.json();

    const clientesTableBody = document.getElementById('clientesTableBody');
    clientesTableBody.innerHTML = '';

    clientes.value.forEach(cliente => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${cliente.Id}</td>
            <td>${cliente.Nombre}</td>
            <td>${cliente.Email}</td>
            <td>${cliente.Telefono}</td>
            <td>${cliente.Direccion}</td>
            <td>
                <button class="btn btn-primary btn-sm">Editar</button>
                <button class="btn btn-danger btn-sm">Eliminar</button>
            </td>
        `;
        clientesTableBody.appendChild(row);
    });
}

// Función para cargar y mostrar productos
async function loadProductos() {
    const response = await fetch('/odata/Productos');
    const productos = await response.json();

    const productosTableBody = document.getElementById('productosTableBody');
    productosTableBody.innerHTML = '';

    productos.value.forEach(producto => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${producto.Id}</td>
            <td>${producto.Nombre}</td>
            <td>${producto.CodigoProducto}</td>
            <td>${producto.Precio}</td>
            <td>
                <button class="btn btn-primary btn-sm">Editar</button>
                <button class="btn btn-danger btn-sm">Eliminar</button>
            </td>
        `;
        productosTableBody.appendChild(row);
    });
}

// Función para cargar y mostrar órdenes
async function loadOrdenes() {
    const response = await fetch('/odata/Ordenes');
    const ordenes = await response.json();

    const ordenesTableBody = document.getElementById('ordenesTableBody');
    ordenesTableBody.innerHTML = '';

    ordenes.value.forEach(orden => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${orden.Id}</td>
            <td>${orden.ClienteId}</td>
            <td>${new Date(orden.OrdenFecha).toLocaleDateString()}</td>
            <td>${orden.CantidadTotal}</td>
            <td>${orden.Impuestos}</td>
            <td>
                <button class="btn btn-primary btn-sm">Editar</button>
                <button class="btn btn-danger btn-sm">Eliminar</button>
            </td>
        `;
        ordenesTableBody.appendChild(row);
    });
}

// Función para cargar y mostrar facturas
async function loadFacturas() {
    const response = await fetch('/odata/Facturas');
    const facturas = await response.json();

    const facturasTableBody = document.getElementById('facturasTableBody');
    facturasTableBody.innerHTML = '';

    facturas.value.forEach(factura => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${factura.Id}</td>
            <td>${factura.OrdenId}</td>
            <td>${new Date(factura.FacturaFecha).toLocaleDateString()}</td>
            <td>${factura.Cantidad}</td>
            <td>
                <button class="btn btn-primary btn-sm">Editar</button>
                <button class="btn btn-danger btn-sm">Eliminar</button>
            </td>
        `;
        facturasTableBody.appendChild(row);
    });
}

// Función para cargar y mostrar tasas de cambio de divisa
async function loadTasasCambio() {
    const response = await fetch('/odata/TasaCambios');
    const tasasCambio = await response.json();

    const tasasCambioTableBody = document.getElementById('tasasCambioTableBody');
    tasasCambioTableBody.innerHTML = '';

    tasasCambio.value.forEach(tasa => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${tasa.Id}</td>
            <td>${tasa.Moneda}</td>
            <td>${tasa.Tasa}</td>
            <td>${new Date(tasa.Fecha).toLocaleDateString()}</td>
            <td>
                <button class="btn btn-primary btn-sm">Editar</button>
                <button class="btn btn-danger btn-sm">Eliminar</button>
            </td>
        `;
        tasasCambioTableBody.appendChild(row);
    });
}

// Cargar datos al cargar la página
document.addEventListener('DOMContentLoaded', async () => {
    await loadClientes();
    await loadProductos();
    await loadOrdenes();
    await loadFacturas();
    await loadTasasCambio();
});
